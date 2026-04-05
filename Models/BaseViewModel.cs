using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HealthLog.ViewModels;
// Base class for all ViewModels
// It provides property change notification for data binding
public class BaseViewModel : INotifyPropertyChanged
{
    // Event to notify UI when a property value changes
    public event PropertyChangedEventHandler? PropertyChanged;
    // Method to trigger the PropertyChanged event
    // CallerMemberName automatically gets the property name
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        // Notify UI that the property has changed
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
