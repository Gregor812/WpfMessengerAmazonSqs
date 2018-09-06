using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using AmazonSqsMessenger.Models;
using AmazonSqsMessenger.Network.API;
using AmazonSqsMessenger.ViewModels;
using Newtonsoft.Json;

namespace AmazonSqsMessenger.Network
{
    class AmazonMessageQueueProvider : IMessageQueueProvider
    {
        public event MessageReceivedHandler NewMessageReceived;

        private readonly AmazonSQSClient _sqsClient;
        private readonly MainWindowViewModel _parentViewModel;
        private readonly string _queueUrl;

        public AmazonMessageQueueProvider(MainWindowViewModel parentViewModel, CancellationToken ct)
        {
            _parentViewModel = parentViewModel;
            _sqsClient = new AmazonSQSClient();
            var queueUrlRequest = new GetQueueUrlRequest("Messenger.fifo");
            _queueUrl = _sqsClient.GetQueueUrl(queueUrlRequest).QueueUrl;

            Task.Factory.StartNew(GetMessages, ct);
        }

        public bool SendMessage(MessageModel messageToSend, string chatId)
        {
            try
            {
                var serializedMessage = JsonConvert.SerializeObject(messageToSend);

                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = _queueUrl,
                    MessageBody = serializedMessage,
                    MessageGroupId = chatId
                };

                _sqsClient.SendMessage(sendMessageRequest);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GetMessages()
        {
            var queueUrlRequest = new GetQueueUrlRequest("Messenger.fifo");
            var queueUrl = _sqsClient.GetQueueUrl(queueUrlRequest).QueueUrl;
            var receiveMessageRequest = new ReceiveMessageRequest { QueueUrl = queueUrl };
            while ()
            {
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                var receiveMessageResponse = _sqsClient.ReceiveMessage(receiveMessageRequest);
                foreach (var message in receiveMessageResponse.Messages)
                {
                    var deserializedMessage = JsonConvert.DeserializeObject<MessageModel>(message.Body);
                    if (deserializedMessage.ChatId.Equals(_parentViewModel.ChatId, StringComparison.InvariantCulture) && !deserializedMessage.Author.Equals(_parentViewModel.Author))
                    {
                        NewMessageReceived?.Invoke(deserializedMessage);
                        var messageRecieptHandle = receiveMessageResponse.Messages[0].ReceiptHandle;
                        var deleteRequest = new DeleteMessageRequest
                        {
                            QueueUrl = queueUrl,
                            ReceiptHandle = messageRecieptHandle
                        };
                        _sqsClient.DeleteMessage(deleteRequest);
                    }
                }
            }
        }
    }
}
