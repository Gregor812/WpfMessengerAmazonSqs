using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AmazonSqsMessenger.ViewModels;

namespace AmazonSqsMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            }
        }
    }
}
