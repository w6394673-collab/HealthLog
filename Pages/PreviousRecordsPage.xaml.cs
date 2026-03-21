using HealthLog.Data;
using HealthLog.Models;

namespace HealthLog.Pages;

public partial class PreviousRecordsPage : ContentPage
{
    private readonly RecordRepository recordRepository = new();
    // Constructor: initialize the page
    public PreviousRecordsPage()
    {
        InitializeComponent();
    }
        // Bind the record list to the CollectionView
     protected override async void OnAppearing()
    {
        base.OnAppearing();
        recordsCollectionView.ItemsSource = await recordRepository.GetRecordsAsync();
    
    }
    // Triggered when the "View" button is clicked
    private async void OnViewClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        // Get the selected food record
        FoodRecord record = (FoodRecord)button.CommandParameter;
        // Show detailed nutrition information
        await DisplayAlertAsync(record.RecordName, record.DetailText, "OK");
    }
    // Triggered when the "Delete" button is clicked
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        FoodRecord record = (FoodRecord)button.CommandParameter;
        // Ask user for confirmation before deleting
        bool confirm = await DisplayAlertAsync("Delete",
            $"Delete record: {record.RecordName}?",
            "Yes",
            "No");

        if (confirm)
        {
            await recordRepository.DeleteRecordAsync(record.Id);
            recordsCollectionView.ItemsSource = await recordRepository.GetRecordsAsync();
        }
    }
    // Navigate back to the Home page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}