using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonSqsMessenger.Models;

namespace AmazonSqsMessenger.Network.API
{
    delegate void MessageReceivedHandler(MessageModel message);
    interface IMessageQueueProvider
    {
        event MessageReceivedHandler NewMessageReceived;
    }
}
