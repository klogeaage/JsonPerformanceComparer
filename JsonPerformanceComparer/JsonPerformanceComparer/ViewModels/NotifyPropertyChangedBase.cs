using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JsonPerformanceComparer.ViewModels
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {

        protected bool SetProperty<PT>(ref PT backingStore, PT value,
           [CallerMemberName] string propertyName = "",
           Action onChanged = null)
        {
            if (EqualityComparer<PT>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            // Issue caused by memory leaks on Android especially when updating the Busy indicator.
            // Hopefully, this will be an acceptable workaround for the time being.
            try
            {
                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception {ex.Message} in OnPropertyChanged({propertyName})!");
            }
        }

        public static void AddPropertyChangedHandler(object observable, PropertyChangedEventHandler eventHandler)
        {
            if (observable is INotifyPropertyChanged observableProperty)
            {
                observableProperty.PropertyChanged -= eventHandler;
                observableProperty.PropertyChanged += eventHandler;
            }
        }
        #endregion
    }
}
