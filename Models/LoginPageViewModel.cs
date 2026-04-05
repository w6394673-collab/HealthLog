using System.Windows.Input;
using HealthLog.Services;
using HealthLog.Pages;

namespace HealthLog.ViewModels;
// ViewModel for LoginPage
public class LoginPageViewModel : BaseViewModel
{
    private readonly IPageService pageService;
    // Constructor
    public LoginPageViewModel()
        : this(new PageService())
    {
    }

    public LoginPageViewModel(IPageService pageService)
    {
        this.pageService = pageService;
        // Command for login button
        LoginCommand = new Command(async () => await OnLoginAsync());
    }
    // Username input
    private string username = "";
    public string Username
    {
        get => username;
        set
        {
            username = value;
            OnPropertyChanged();
        }
    }
    // Password input
    private string password = "";
    public string Password
    {
        get => password;
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }
    // Login command
    public ICommand LoginCommand { get; }
    // Handle login logic
    private async Task OnLoginAsync()
    {
        // Check if input is empty
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            await pageService.ShowAlertAsync("Error", "Please enter username and password.", "OK");
            return;
        }
        // Navigate to HomePage
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new HomePage());
        }
    }
}
