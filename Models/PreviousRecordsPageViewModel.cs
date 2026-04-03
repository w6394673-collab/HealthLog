using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthLog.Data;
using HealthLog.Models;
using HealthLog.Services;

namespace HealthLog.ViewModels;

public class PreviousRecordsPageViewModel : BaseViewModel
{
    private readonly RecordRepository recordRepository;
    private readonly IPageService pageService;

    public ObservableCollection<FoodRecord> Records { get; set; } = new();

    public ICommand LoadCommand { get; }
    public ICommand ViewCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand HomeCommand { get; }
    public bool IsPreviousPage => true;
    public PreviousRecordsPageViewModel()
        : this(new RecordRepository(), new PageService())
    {
    }

    public PreviousRecordsPageViewModel(RecordRepository recordRepository, IPageService pageService)
    {
        this.recordRepository = recordRepository;
        this.pageService = pageService;

        LoadCommand = new Command(async () => await LoadRecordsAsync());
        ViewCommand = new Command<FoodRecord>(async (record) => await OnViewAsync(record));
        DeleteCommand = new Command<FoodRecord>(async (record) => await OnDeleteAsync(record));
        HomeCommand = new Command(async () => await OnHomeAsync());
    }

    public async Task LoadRecordsAsync()
    {
        Records.Clear();

        var items = await recordRepository.GetRecordsAsync();

        foreach (var item in items)
        {
            Records.Add(item);
        }
    }

    private async Task OnViewAsync(FoodRecord? record)
    {
        if (record == null)
            return;

        await pageService.ShowAlertAsync(record.RecordName, record.DetailText, "OK");
    }

    private async Task OnDeleteAsync(FoodRecord? record)
    {
        if (record == null)
            return;

        bool confirm = false;

        if (Application.Current?.Windows[0]?.Page != null)
        {
            confirm = await Application.Current.Windows[0].Page.DisplayAlert(
                "Delete",
                $"Delete record: {record.RecordName}?",
                "Yes",
                "No");
        }

        if (confirm)
        {
            await recordRepository.DeleteRecordAsync(record.Id);
            await LoadRecordsAsync();
        }
    }

    private async Task OnHomeAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PopAsync();
        }
    }
}