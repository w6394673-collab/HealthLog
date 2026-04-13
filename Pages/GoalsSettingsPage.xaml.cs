using HealthLog.ViewModels;

namespace HealthLog.Pages;

// Code-behind for goals and settings page
public partial class GoalsSettingsPage : ContentPage
{
    public GoalsSettingsPage()
    {
        InitializeComponent();
        BindingContext = new GoalsSettingsViewModel();
    }
}