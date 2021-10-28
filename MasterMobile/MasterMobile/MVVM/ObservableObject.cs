using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MasterMobile.MVVM
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnSetNewValue<T>(
            ref T Property, T Value, [CallerMemberName] string propertyName = "")
        {
            Property = Value;
            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            //var changed = PropertyChanged;
            //if (changed == null)
            //    return;
            //changed.Invoke(this, new PropertyChangedEventArgs(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
