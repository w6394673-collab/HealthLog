using HealthLog.Data;
using HealthLog.Models;

namespace HealthLog.Pages;

public partial class PreviousRecordsPage : ContentPage
{
    // Constructor: initialize page and load saved records
    public PreviousRecordsPage()
    {
        InitializeComponent();
        // Bind the record list to the CollectionView
        recordsCollectionView.ItemsSource = RecordStore.Records;
    }
    // Triggered when the "View" button is clicked
    private async void OnViewClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        // Get the selected food record
        FoodRecord record = (FoodRecord)button.CommandParameter;
        // Show detailed nutrition information
        await DisplayAlert(record.RecordName, record.DetailText, "OK");
    }
    // Triggered when the "Delete" button is clicked
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        FoodRecord record = (FoodRecord)button.CommandParameter;
        // Ask user for confirmation before deleting
        bool confirm = await DisplayAlert("Delete",
            $"Delete record: {record.RecordName}?",
            "Yes",
            "No");

        if (confirm)
        {
            RecordStore.DeleteRecord(record);
        }
    }
    // Navigate back to the Home page
    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}