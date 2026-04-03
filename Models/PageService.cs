using HealthLog.Pages;
using Microsoft.Maui.Devices;

namespace HealthLog.Services;

public class PageService : IPageService
{
    public async Task ShowAlertAsync(string title, string message, string cancel)
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.DisplayAlert(title, message, cancel);
        }
    }

    public async Task GoToPreviousRecordsAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new PreviousRecordsPage());
        }
    }

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