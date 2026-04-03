using System.Windows.Input;
using HealthLog.Data;
using HealthLog.Models;
using HealthLog.Services;

namespace HealthLog.ViewModels;

public class HomePageViewModel : BaseViewModel
{
    private readonly RecordRepository recordRepository;
    private readonly IPageService pageService;

    public HomePageViewModel()
        : this(new RecordRepository(), new PageService())
    {
    }

    public HomePageViewModel(RecordRepository recordRepository, IPageService pageService)
    {
        this.recordRepository = recordRepository;
        this.pageService = pageService;

        DateText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy");

        EstimateCommand = new Command(async () => await OnEstimateAsync());
        PreviousRecordsCommand = new Command(async () => await OnPreviousRecordsAsync());
    }

    private string dateText = "";
    public string DateText
    {
        get => dateText;
        set
        {
            dateText = value;
            OnPropertyChanged();
        }
    }

    private string foodInput = "";
    public string FoodInput
    {
        get => foodInput;
        set
        {
            foodInput = value;
            OnPropertyChanged();
        }
    }

    private string caloriesText = "- Calories : 0 Kcal";
    public string CaloriesText
    {
        get => caloriesText;
        set
        {
            caloriesText = value;
            OnPropertyChanged();
        }
    }

    private string proteinText = "- Protein : 0 g";
    public string ProteinText
    {
        get => proteinText;
        set
        {
            proteinText = value;
            OnPropertyChanged();
        }
    }

    private string carbsText = "- Carbs : 0 g";
    public string CarbsText
    {
        get => carbsText;
        set
        {
            carbsText = value;
            OnPropertyChanged();
        }
    }

    private string fatText = "- Fat : 0 g";
    public string FatText
    {
        get => fatText;
        set
        {
            fatText = value;
            OnPropertyChanged();
        }
    }

    private string waterText = "- Water : 0 ml";
    public string WaterText
    {
        get => waterText;
        set
        {
            waterText = value;
            OnPropertyChanged();
        }
    }

    private string suggestionText = "After the estimation is completed, suggestions will be provided.";
    public string SuggestionText
    {
        get => suggestionText;
        set
        {
            suggestionText = value;
            OnPropertyChanged();
        }
    }

    public ICommand EstimateCommand { get; }
    public ICommand PreviousRecordsCommand { get; }
    public bool IsHomePage => true;
    private async Task OnEstimateAsync()
    {
        string input = (FoodInput ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(input))
        {
            await pageService.ShowAlertAsync("Reminder", "Please enter food.", "OK");
            return;
        }

        string[] foods = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

        int calories = 0;
        int protein = 0;
        int carbs = 0;
        int fat = 0;
        int water = 0;

        foreach (string food in foods)
        {
            if (food == "apple")
            {
                calories += 52;
                carbs += 14;
                water += 86;
            }
            else if (food == "egg")
            {
                calories += 78;
                protein += 6;
                fat += 5;
            }
            else if (food == "chicken")
            {
                calories += 165;
                protein += 31;
                fat += 4;
            }
        }

        CaloriesText = $"- Calories : {calories} Kcal";
        ProteinText = $"- Protein : {protein} g";
        CarbsText = $"- Carbs : {carbs} g";
        FatText = $"- Fat : {fat} g";
        WaterText = $"- Water : {water} ml";

        FoodRecord record = new FoodRecord
        {
            Date = DateTime.Now.ToString("dd/MM/yyyy"),
            RecordName = input,
            Calories = calories,
            Protein = protein,
            Carbs = carbs,
            Fat = fat,
            Water = water
        };

        await recordRepository.AddRecordAsync(record);

        if (protein < 20 || water < 100)
        {
            SuggestionText = "Nutrition is not enough. Please add more protein or water.";
            await pageService.ShowAlertAsync("Reminder", "Your nutrition may not be enough.", "OK");
            await pageService.VibrateAsync(500);
        }
        else
        {
            SuggestionText = "Good job. Your nutrition looks balanced.";
            await pageService.ShowAlertAsync("Reminder", "Your nutrition looks good.", "OK");
        }

        FoodInput = "";
    }

    private async Task OnPreviousRecordsAsync()
    {
        await pageService.GoToPreviousRecordsAsync();
    }
}
