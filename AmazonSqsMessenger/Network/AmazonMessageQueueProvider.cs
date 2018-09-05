using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using AmazonSqsMessenger.Network.API;

namespace AmazonSqsMessenger.Network
{
    class AmazonMessageQueueProvider : IMessageQueueProvider
    {
        public event MessageReceivedHandler NewMessageReceived;

        private AmazonSQSClient _sqsClient;
        private AmazonSQSConfig _sqsConfig;

        public AmazonMessageQueueProvider()
        {
            
        }
    }
}
