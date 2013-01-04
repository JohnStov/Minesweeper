using System;
using MinesweeperLib;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Minesweeper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Board board;
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            board = new Board(10, 10, 5);

            for (int x = 0; x < board.Width; ++x)
                cellGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40) });
            for (int y = 0; y < board.Height; ++y)
                cellGrid.RowDefinitions.Add(new RowDefinition {Height = new GridLength(40)});

            for (int y = 0; y < board.Height; ++y)
                for (int x = 0; x < board.Width; ++x)
                {
                    var button = new Button {Background = new SolidColorBrush(Colors.Purple), Content = " "};
                    button.SetValue(Grid.ColumnProperty, x);
                    button.SetValue(Grid.RowProperty, y);
                    button.Click += ButtonClick;
                    cellGrid.Children.Add(button);
                }

        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var x = (int)button.GetValue(Grid.ColumnProperty);
            var y = (int)button.GetValue(Grid.RowProperty);

            ShowCell(x, y);
        }

        private void ShowCell(int x, int y)
        {
            if (x < 0 || x >= board.Width || y < 0 || y >= board.Height || board[x, y].IsVisible)
                return;

            board[x, y].SetVisible();

            var button =
                cellGrid.Children.FirstOrDefault(b => (int)(b.GetValue(Grid.ColumnProperty)) == x && (int)(b.GetValue(Grid.RowProperty)) == y) as Button;

            if (board[x, y].IsBomb)
            {
                button.Background = new SolidColorBrush(Colors.Red);
                button.Content = "*";
            }
            else
            {
                int count = board[x, y].MineCount;
                button.Background = new SolidColorBrush(Colors.Green);
                if (count > 0)
                    button.Content = board[x, y].MineCount.ToString();
                else
                {
                    for (int i = x-1; i <= x+1; ++i)
                        for (int j = y-1; j <= y+1; ++j)
                            ShowCell(i, j);
                }
            }
        }
    }
}
