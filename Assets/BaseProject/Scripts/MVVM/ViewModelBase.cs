using UnityEngine;

using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ViewModelBase : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void Set<T>(ref T target, T newValue, [CallerMemberName]string propertyName = "")
    {
        target = newValue;
        PropertyChanged?.Invoke(target, new PropertyChangedEventArgs(propertyName));
    }
}