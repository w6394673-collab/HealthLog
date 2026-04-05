using HealthLog.ViewModels;

namespace HealthLog.Pages;
// Code-behind for PreviousRecordsPage (View)
public partial class PreviousRecordsPage : ContentPage
{
    // Create a ViewModel instance
    private readonly PreviousRecordsPageViewModel viewModel;
    // Constructor - runs when page is created
    public PreviousRecordsPage()
    {
        // Load UI from XAML
        InitializeComponent();
        // Initialize ViewModel
        viewModel = new PreviousRecordsPageViewModel();
        // Connect View with ViewModel (MVVM)
        BindingContext = viewModel;
    }
    // This method runs when the page appears on screen
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Load records from database when page is shown
        await viewModel.LoadRecordsAsync();
    }
}