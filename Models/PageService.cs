using HealthLog.Pages;
using Microsoft.Maui.Devices;

namespace HealthLog.Services;
// Service for handling page actions
public class PageService : IPageService
{
    // Show alert message
    public async Task ShowAlertAsync(string title, string message, string cancel)
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.DisplayAlertAsync(title, message, cancel);
        }
    }
    // Navigate to previous records page
    public async Task GoToPreviousRecordsAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new PreviousRecordsPage());
        }
    }
    // Make device vibrate
    public Task VibrateAsync(int milliseconds)
    {
        try
        {
            if (Vibration.Default.IsSupported)
            {
                Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(milliseconds));
            }
        }
        catch
        {
        }

        return Task.CompletedTask;
    }
}