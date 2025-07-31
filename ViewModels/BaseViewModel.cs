// BaseViewModel.cs (in your ViewModels folder or a common folder)
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gameromicon.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}