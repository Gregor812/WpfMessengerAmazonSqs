using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AmazonSqsMessenger.Models;
using AmazonSqsMessenger.Network.API;

namespace AmazonSqsMessenger.Network
{
    class TestMessageQueueProvider : IMessageQueueProvider
    {
        private Random _random = new Random();

        public event MessageReceivedHandler NewMessageReceived;

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
                    Author = "Sid Vicious",
                    DateTimeUtc = DateTime.UtcNow,
                    Text = "There is the test message"
                });
            }
        }
    }
}
