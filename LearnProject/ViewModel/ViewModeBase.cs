using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace LearnProject.ViewModel
{
    class ViewModeBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Set<T>(ref T oldValue, T newValue,string propertyName) 
        {
            if (Equals(oldValue, newValue))
            {
                return;
            }
            oldValue = newValue;
            RaisePropertyChanged(propertyName);
        }
    }
}
