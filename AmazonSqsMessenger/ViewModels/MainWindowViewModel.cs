using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using AmazonSqsMessenger.Commands;
using AmazonSqsMessenger.ModelConverters;
using AmazonSqsMessenger.Models;
using AmazonSqsMessenger.Network;
using AmazonSqsMessenger.Network.API;
using AmazonSqsMessenger.Utils;

namespace AmazonSqsMessenger.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<MessageViewModel> Messages { get; } = new ObservableCollection<MessageViewModel>();
        public ICommand SendCommand { get; }

        private string _author;

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        private string _chatId;
        public string ChatId
        {
            get =>_chatId;
            set
            {
                _chatId = value;
                OnPropertyChanged();
            }
        }

        private string _typedMessage;
        public string TypedMessage
        {
            get => _typedMessage;
            set
            {
                _typedMessage = value;
                OnPropertyChanged();
            }
        }

        private MessageViewModel _selectedMessage;
        public MessageViewModel SelectedMessage
        {
            get => _selectedMessage;
            set
            {
                _selectedMessage = value;
                OnPropertyChanged();
            }
        }

        public IMessageQueueProvider MessageQueueProvider { get; }

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public MainWindowViewModel()
        {
            SendCommand = new SendButtonCommand(this);

            MessageQueueProvider = new AmazonMessageQueueProvider(this, _cts.Token);
            MessageQueueProvider.NewMessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(MessageModel message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var msgVm = MessageConverter.ModelToViewModel(message, MessageDirection.Incoming);
                Messages.Add(msgVm);
                SelectedMessage = msgVm;
            });
        }
    }
}
