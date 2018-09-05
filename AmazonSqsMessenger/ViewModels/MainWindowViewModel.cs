using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
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
        public ObservableCollection<MessageViewModel> Messages { get; }
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
            Messages = new ObservableCollection<MessageViewModel>();

            SendCommand = new SendButtonCommand(this);

            MessageQueueProvider = new TestMessageQueueProvider(_cts.Token);
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
