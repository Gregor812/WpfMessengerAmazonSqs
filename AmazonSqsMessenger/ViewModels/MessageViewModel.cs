using System.Windows;
using System.Windows.Media;

namespace AmazonSqsMessenger.ViewModels
{
    class MessageViewModel : BaseViewModel
    {
        public string Text { get; set; }
        public SolidColorBrush Color { get; set; }
        public HorizontalAlignment Alignment { get; set; }
        public string AuthorAndDate { get; set; }
    }
}
