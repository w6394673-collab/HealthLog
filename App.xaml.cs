using Microsoft.Extensions.DependencyInjection;

namespace HealthLog;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new Pages.LoginPage());
    }
}