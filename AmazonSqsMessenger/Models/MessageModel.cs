using System;

namespace AmazonSqsMessenger.Models
{
    class MessageModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime DateTimeUtc { get; set; }
    }
}
