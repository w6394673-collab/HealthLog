using HealthLog.ViewModels;

namespace HealthLog.Pages;
// Code-behind for LoginPage (View)
public partial class LoginPage : ContentPage
{
    // Constructor - runs when the page is created
    public LoginPage()
    {
        // Load UI components from XAML
        InitializeComponent();
        // Set ViewModel as BindingContext
        // This connects UI with logic using MVVM
        BindingContext = new LoginPageViewModel();
    }
}