using System.Collections.ObjectModel;
using System.Windows.Input;
using AmazonSqsMessenger.Commands;

namespace AmazonSqsMessenger.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<MessageViewModel> Messages { get; }
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

        public MainWindowViewModel()
        {
            Messages = new ObservableCollection<MessageViewModel>();
            SendCommand = new SendButtonCommand(this);
        }
    }
}
