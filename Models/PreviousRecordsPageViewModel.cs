using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthLog.Data;
using HealthLog.Models;
using HealthLog.Services;
using HealthLog.Pages;

namespace HealthLog.ViewModels;
// ViewModel for previous records page
public class PreviousRecordsPageViewModel : BaseViewModel
{
    private readonly RecordRepository recordRepository;
    private readonly IPageService pageService;
    // List of records
    public ObservableCollection<FoodRecord> Records { get; set; } = new();

    public ICommand LoadCommand { get; }
    public ICommand ViewCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand HomeCommand { get; }
    public ICommand GoalsSettingsCommand { get; }
    public bool IsPreviousPage => true;
    // Constructor
    public PreviousRecordsPageViewModel()
        : this(new RecordRepository(), new PageService())
    {
    }

    public PreviousRecordsPageViewModel(RecordRepository recordRepository, IPageService pageService)
    {
        this.recordRepository = recordRepository;
        this.pageService = pageService;
        // Commands for buttons
        LoadCommand = new Command(async () => await LoadRecordsAsync());
        ViewCommand = new Command<FoodRecord>(async (record) => await OnViewAsync(record));
        DeleteCommand = new Command<FoodRecord>(async (record) => await OnDeleteAsync(record));
        HomeCommand = new Command(async () => await OnHomeAsync());
        GoalsSettingsCommand = new Command(async () => await OnGoalsSettingsAsync());
    }
    // Load records from database
    public async Task LoadRecordsAsync()
    {
        Records.Clear();

        var items = await recordRepository.GetRecordsAsync();

        foreach (var item in items)
        {
            Records.Add(item);
        }
    }
    // Show record details
    private async Task OnViewAsync(FoodRecord? record)
    {
        if (record == null)
            return;

        await pageService.ShowAlertAsync(record.RecordName, record.DetailText, "OK");
    }
    // Delete record
    private async Task OnDeleteAsync(FoodRecord? record)
    {
        if (record == null)
            return;

        bool confirm = false;

        if (Application.Current?.Windows[0]?.Page != null)
        {
            // Confirm delete
            confirm = await Application.Current.Windows[0].Page.DisplayAlertAsync(
     "Delete",
     $"Delete record: {record.RecordName}?",
     "Yes",
     "No");
        }

        if (confirm)
        {
            // Delete from database
            await recordRepository.DeleteRecordAsync(record.Id);
            // Reload list
            await LoadRecordsAsync();
        }
    }
    // Go back to home page
    private async Task OnHomeAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PopAsync();
        }
    }
    private async Task OnGoalsSettingsAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new GoalsSettingsPage());
        }
    }

}