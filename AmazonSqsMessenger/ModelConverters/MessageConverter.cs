using System.Windows;
using System.Windows.Media;
using AmazonSqsMessenger.Models;
using AmazonSqsMessenger.Utils;
using AmazonSqsMessenger.ViewModels;

namespace AmazonSqsMessenger.ModelConverters
{
    static class MessageConverter
    {
        public static MessageViewModel ModelToViewModel(MessageModel model, MessageDirection direction)
        {
            return new MessageViewModel
            {
                Text = model.Text,
                AuthorAndDate = $"{model.Author}, {model.DateTimeUtc.ToLocalTime():dd-MM-yy HH:mm}",
                Alignment = direction == MessageDirection.Incoming ? HorizontalAlignment.Left : HorizontalAlignment.Right,
                Color = direction == MessageDirection.Incoming ? new SolidColorBrush(Color.FromRgb(100, 200, 160)) : new SolidColorBrush(Color.FromRgb(100, 160, 200))
            };
        }
    }
}
