using HealthLog.ViewModels;

namespace HealthLog.Pages;
// This is the code-behind for HomePage
public partial class HomePage : ContentPage
{
    // Constructor - runs when page is created
    public HomePage()
    {
        // Initialize UI components defined in XAML
        InitializeComponent();
        // Set the BindingContext to ViewModel (MVVM pattern)
        // This allows UI to bind data and commands from ViewModel
        BindingContext = new HomePageViewModel();
    }
}

