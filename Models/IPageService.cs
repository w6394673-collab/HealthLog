namespace HealthLog.Services;
// Interface for page actions
public interface IPageService
{
    // Show alert message
    Task ShowAlertAsync(string title, string message, string cancel);
    // Go to previous records page
    Task GoToPreviousRecordsAsync();
    // Make the device vibrate
    Task VibrateAsync(int milliseconds);
}
