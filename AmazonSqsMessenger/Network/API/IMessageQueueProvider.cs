using AmazonSqsMessenger.Models;

namespace AmazonSqsMessenger.Network.API
{
    delegate void MessageReceivedHandler(MessageModel message);

    interface IMessageQueueProvider
    {
        event MessageReceivedHandler NewMessageReceived;

        bool SendMessage(MessageModel messageToSend, string chatId);
    }
}
