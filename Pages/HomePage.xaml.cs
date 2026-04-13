using HealthLog.ViewModels;

namespace HealthLog.Pages;

// Code-behind for HomePage
public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel viewModel;

    public HomePage()
    {
        InitializeComponent();
        viewModel = new HomePageViewModel();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.RefreshTargets();
    }
}