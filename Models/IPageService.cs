namespace HealthLog.Services;

public interface IPageService
{
    Task ShowAlertAsync(string title, string message, string cancel);
    Task GoToPreviousRecordsAsync();
    Task VibrateAsync(int milliseconds);
}
