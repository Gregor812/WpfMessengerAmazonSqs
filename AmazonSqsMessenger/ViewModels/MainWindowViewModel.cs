using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
        public ObservableCollection<MessageViewModel> MessagesSource { get; }
        public IEnumerable<MessageViewModel> Messages => MessagesSource.ToList();
        public ICommand SendCommand { get; set; }

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

        public IMessageQueueProvider MessageQueueProvider { get; set; }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public MainWindowViewModel()
        {
            MessagesSource = new ObservableCollection<MessageViewModel>();
            MessagesSource.CollectionChanged += (sender, args) => OnPropertyChanged(nameof(Messages));

            SendCommand = new SendButtonCommand(this);

            MessageQueueProvider = new TestMessageQueueProvider(_cts.Token);
            MessageQueueProvider.NewMessageReceived += OnMessageReceived;
        }

        private void OnMessageReceived(MessageModel message)
        {
            var msgVm = MessageConverter.ModelToViewModel(message, MessageDirection.Incoming);
            MessagesSource.Add(msgVm);
        }
    }
}
