using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazonSqsMessenger.Models;
using AmazonSqsMessenger.Network.API;

namespace AmazonSqsMessenger.Network
{
    class TestMessageQueueProvider : IMessageQueueProvider
    {
        private readonly Random _random = new Random();

        public event MessageReceivedHandler NewMessageReceived;
        public bool SendMessage(MessageModel messageToSend, string chatId)
        {
            return true;
        }

        public TestMessageQueueProvider(CancellationToken ct)
        {
            Task.Factory.StartNew(GenerateMessage, ct);
        }

        void GenerateMessage()
        {
            while (true)
            {
                Thread.Sleep(_random.Next(500, 10000));
                NewMessageReceived?.Invoke(new MessageModel
                {
                    Author = "Numbers Generator",
                    DateTimeUtc = DateTime.UtcNow,
                    Text = string.Join(", ", Enumerable.Range(_random.Next(1, 1000), 5))
                });
            }
        }
    }
}
