using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AmazonSqsMessenger.ModelConverters;
using AmazonSqsMessenger.Models;
using AmazonSqsMessenger.Utils;
using AmazonSqsMessenger.ViewModels;

namespace AmazonSqsMessenger.Commands
{
    class SendButtonCommand : ICommand
    {
        private readonly MainWindowViewModel _parentViewModel;
        public SendButtonCommand(MainWindowViewModel parentViewModel) => _parentViewModel = parentViewModel;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_parentViewModel.Author) && 
                   !string.IsNullOrWhiteSpace(_parentViewModel.TypedMessage);
        }

        public void Execute(object parameter)
        {
            var message = new MessageModel
            {
                Text = _parentViewModel.TypedMessage,
                Author = _parentViewModel.Author,
                DateTimeUtc = DateTime.UtcNow
            };

            _parentViewModel.Messages.Add
            (
                MessageConverter.ModelToViewModel(message, MessageDirection.Outcoming)
            );
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
}
}
