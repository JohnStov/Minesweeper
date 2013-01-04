using MinesweeperLib;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
                    button.Click += button_Click;
                    cellGrid.Children.Add(button);
                }

        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var x = (int)button.GetValue(Grid.ColumnProperty);
            var y = (int)button.GetValue(Grid.RowProperty);

            if (board[x, y].IsVisible)
                return;

            if (board[x, y].IsBomb)
            {
                button.Background = new SolidColorBrush(Colors.Red);
                button.Content = "*";
            }
            else
            {
                button.Background = new SolidColorBrush(Colors.Green);
                button.Content = board[x, y].MineCount.ToString();
            }
            board[x,y].SetVisible();
        }
    }
}
