using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;

namespace Luke.Net.Infrastructure
{
    public static class CommandExtensions
    {
        public static DelegateCommandBase InvalidateOnPropertyChange<T>(this DelegateCommandBase command, T viewModel) where T : INotifyPropertyChanged
        {
            viewModel.PropertyChanged += (s, e) => command.RaiseCanExecuteChanged();
            return command;
        }
    }
}
