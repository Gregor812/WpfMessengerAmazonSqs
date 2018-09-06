using System.Windows.Controls;
using AmazonSqsMessenger.ViewModels;

namespace AmazonSqsMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void MessagesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = sender as ListView;

            if (lv?.SelectedItem != null)
            {
                lv.ScrollIntoView(lv.SelectedItem);
                lv.SelectedItem = null;
            }
        }
    }
}
