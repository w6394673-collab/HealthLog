using HealthLog.ViewModels;

namespace HealthLog.Pages;

public partial class PreviousRecordsPage : ContentPage
{
    private readonly PreviousRecordsPageViewModel viewModel;

    public PreviousRecordsPage()
    {
        InitializeComponent();
        viewModel = new PreviousRecordsPageViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadRecordsAsync();
    }
}