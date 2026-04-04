using HealthLog.Data;
using HealthLog.Models;
using HealthLog.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HealthLog.ViewModels;

public class HomePageViewModel : BaseViewModel
{
    private readonly RecordRepository recordRepository;
    private readonly IPageService pageService;

    public HomePageViewModel()
    : this(new RecordRepository(), new PageService())
    {
    }

    public HomePageViewModel(
        RecordRepository recordRepository,
        IPageService pageService
        )
    {
        this.recordRepository = recordRepository;
        this.pageService = pageService;


        DateText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy");

        EstimateCommand = new Command(async () => await OnEstimateAsync());
        PreviousRecordsCommand = new Command(async () => await OnPreviousRecordsAsync());
        SelectFoodCommand = new Command<string>(OnSelectFood);
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
            UpdateSuggestions();
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

        List<string> unsupportedFoods = new();

        foreach (string food in foods)
        {
            string item = food.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(item))
                continue;

            if (FoodData.Items.ContainsKey(item))
            {
                var data = FoodData.Items[item];
                calories += data.Calories;
                protein += data.Protein;
                carbs += data.Carbs;
                fat += data.Fat;
                water += data.Water;
            }
            else
            {
                unsupportedFoods.Add(item);
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
        string unsupportedMessage = "";

        if (unsupportedFoods.Count > 0)
        {
            unsupportedMessage = " Unsupported food: " + string.Join(", ", unsupportedFoods) + ".";
        }
        if (protein < 20 || water < 100)
        {
            SuggestionText = "Nutrition is not enough. Please add more protein or water." + unsupportedMessage;
            await pageService.ShowAlertAsync("Reminder", "Your nutrition may not be enough.", "OK");
            await pageService.VibrateAsync(500);
        }
        else
        {
            SuggestionText = "Good job. Your nutrition looks balanced." + unsupportedMessage;
            await pageService.ShowAlertAsync("Reminder", "Your nutrition looks good.", "OK");
        }

        FoodInput = "";
        SuggestedFoods.Clear();
    }
    private ObservableCollection<string> suggestedFoods = new();
    public ObservableCollection<string> SuggestedFoods
    {
        get => suggestedFoods;
        set
        {
            suggestedFoods = value;
            OnPropertyChanged();
        }
    }

    public ICommand SelectFoodCommand { get; }

    private async Task OnPreviousRecordsAsync()
    {
        await pageService.GoToPreviousRecordsAsync();
    }

    private void UpdateSuggestions()
    {
        SuggestedFoods.Clear();

        string input = (FoodInput ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(input))
            return;

        foreach (var food in FoodData.Items.Keys)
        {
            if (food.StartsWith(input) && food != input)
            {
                SuggestedFoods.Add(food);
            }
        }
    }

    private void OnSelectFood(string? food)
    {
        if (string.IsNullOrWhiteSpace(food))
            return;

        FoodInput = food;
        SuggestedFoods.Clear();
    }
}