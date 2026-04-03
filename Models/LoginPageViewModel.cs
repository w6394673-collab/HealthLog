using System.Windows.Input;
using HealthLog.Services;
using HealthLog.Pages;

namespace HealthLog.ViewModels;

public class LoginPageViewModel : BaseViewModel
{
    private readonly IPageService pageService;

    public LoginPageViewModel()
        : this(new PageService())
    {
    }

    public LoginPageViewModel(IPageService pageService)
    {
        this.pageService = pageService;
        LoginCommand = new Command(async () => await OnLoginAsync());
    }

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

    public ICommand LoginCommand { get; }

    private async Task OnLoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            await pageService.ShowAlertAsync("Error", "Please enter username and password.", "OK");
            return;
        }

        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new HomePage());
        }
    }
}
