using HealthLog.Data;
using HealthLog.Models;
using HealthLog.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthLog.Pages;

namespace HealthLog.ViewModels;

public class HomePageViewModel : BaseViewModel
{
    private readonly RecordRepository recordRepository;
    private readonly IPageService pageService;
    private readonly UserSettingsService settingsService;
    private int targetCalories;
    private int targetProtein;
    private int targetCarbs;
    private int targetFat;
    private int targetWater;
    public HomePageViewModel()
    : this(new RecordRepository(), new PageService(), new UserSettingsService())
    {
    }

    public HomePageViewModel(
        RecordRepository recordRepository,
        IPageService pageService,
        UserSettingsService settingsService
        )
    {
        // Inject dependencies (database and UI service)
        this.recordRepository = recordRepository;
        this.pageService = pageService;
        this.settingsService = settingsService;
        // Set current date
        DateText = "Date: " + DateTime.Now.ToString("dd/MM/yyyy");
        LoadTargetValues();
        // Commands for user actions
        EstimateCommand = new Command(async () => await OnEstimateAsync());
        PreviousRecordsCommand = new Command(async () => await OnPreviousRecordsAsync());
        SelectFoodCommand = new Command<string>(OnSelectFood);
        GoalsSettingsCommand = new Command(async () => await OnGoalsSettingsAsync());
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
    // User input for food
    private string foodInput = "";
    public string FoodInput
    {
        get => foodInput;
        set
        {
            foodInput = value;
            OnPropertyChanged();
            // Update suggestions when user types
            UpdateSuggestions();
        }
    }

    private string caloriesText = "- Calories : 0/0 Kcal";
    public string CaloriesText
    {
        get => caloriesText;
        set
        {
            caloriesText = value;
            OnPropertyChanged();
        }
    }

    private string proteinText = "- Protein : 0/0 g";
    public string ProteinText
    {
        get => proteinText;
        set
        {
            proteinText = value;
            OnPropertyChanged();
        }
    }

    private string carbsText = "- Carbs : 0/0 g";
    public string CarbsText
    {
        get => carbsText;
        set
        {
            carbsText = value;
            OnPropertyChanged();
        }
    }

    private string fatText = "- Fat : 0/0 g";
    public string FatText
    {
        get => fatText;
        set
        {
            fatText = value;
            OnPropertyChanged();
        }
    }

    private string waterText = "- Water : 0/0 ml";
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
    public ICommand GoalsSettingsCommand { get; }
    public bool IsHomePage => true;
    // Calculate nutrition based on user input
    private async Task OnEstimateAsync()
    {
        // Get and clean input
        string input = (FoodInput ?? "").Trim().ToLower();
        // Check if input is empty
        if (string.IsNullOrWhiteSpace(input))
        {
            await pageService.ShowAlertAsync("Reminder", "Please enter food.", "OK");
            return;
        }
        // Split input into food items
        string[] entries = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

        double calories = 0;
        double protein = 0;
        double carbs = 0;
        double fat = 0;
        double water = 0;

        List<string> unsupportedFoods = new();

        foreach (string entry in entries)
        {
            string part = entry.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(part))
                continue;

            string[] pieces = part.Split(':', StringSplitOptions.RemoveEmptyEntries);

            if (pieces.Length != 2)
            {
                unsupportedFoods.Add(part);
                continue;
            }

            string foodName = pieces[0].Trim();

            if (!double.TryParse(pieces[1].Trim(), out double grams) || grams <= 0)
            {
                unsupportedFoods.Add(part);
                continue;
            }

            if (FoodData.Items.ContainsKey(foodName))
            {
                var data = FoodData.Items[foodName];
                double ratio = grams / 100.0;

                calories += data.Calories * ratio;
                protein += data.Protein * ratio;
                carbs += data.Carbs * ratio;
                fat += data.Fat * ratio;
                water += data.Water * ratio;
            }
            else
            {
                unsupportedFoods.Add(foodName);
            }
        }

        CaloriesText = $"- Calories : {calories:F1}/{targetCalories} Kcal";
        ProteinText = $"- Protein : {protein:F1}/{targetProtein} g";
        CarbsText = $"- Carbs : {carbs:F1}/{targetCarbs} g";
        FatText = $"- Fat : {fat:F1}/{targetFat} g";
        WaterText = $"- Water : {water:F1}/{targetWater} ml";
        // Save record to database
        FoodRecord record = new FoodRecord
        {
            Date = DateTime.Now.ToString("dd/MM/yyyy"),
            RecordName = input,
            Calories = (int)Math.Round(calories),
            Protein = (int)Math.Round(protein),
            Carbs = (int)Math.Round(carbs),
            Fat = (int)Math.Round(fat),
            Water = (int)Math.Round(water)
        };

        await recordRepository.AddRecordAsync(record);
        string unsupportedMessage = "";

        if (unsupportedFoods.Count > 0)
        {
            unsupportedMessage = " Unsupported food: " + string.Join(", ", unsupportedFoods) + ".";
        }
        // Provide suggestion based on nutrition
        if (protein < 20 || water < 100)
        {
            SuggestionText = "Nutrition is not enough. Please add more protein or water." + unsupportedMessage;
            // Alert + vibration
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
    private void LoadTargetValues()
    {
        var settings = settingsService.Load();

        targetCalories = settings.TargetCalories;
        targetProtein = settings.TargetProtein;
        targetCarbs = settings.TargetCarbs;
        targetFat = settings.TargetFat;
        targetWater = settings.TargetWater;
    }
    public void RefreshTargets()
    {
        LoadTargetValues();

        CaloriesText = $"- Calories : 0/{targetCalories} Kcal";
        ProteinText = $"- Protein : 0/{targetProtein} g";
        CarbsText = $"- Carbs : 0/{targetCarbs} g";
        FatText = $"- Fat : 0/{targetFat} g";
        WaterText = $"- Water : 0/{targetWater} ml";
    }

    // Update food suggestions based on input
    private void UpdateSuggestions()
    {
        SuggestedFoods.Clear();

        string input = (FoodInput ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(input))
            return;

        // Get the last part after comma
        string lastPart = input.Split(',').Last().Trim();

        // If user already typed grams, only keep food name
        if (lastPart.Contains(":"))
        {
            lastPart = lastPart.Split(':')[0].Trim();
        }

        if (string.IsNullOrWhiteSpace(lastPart))
            return;

        foreach (var food in FoodData.Items.Keys)
        {
            if (food.StartsWith(lastPart) && food != lastPart)
            {
                SuggestedFoods.Add(food);
            }
        }
    }

    private void OnSelectFood(string? food)
    {
        if (string.IsNullOrWhiteSpace(food))
            return;

        string input = (FoodInput ?? "").Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            FoodInput = food;
            SuggestedFoods.Clear();
            return;
        }
        string[] parts = input.Split(',');

        // Replace only the last part
        parts[parts.Length - 1] = " " + food;

        FoodInput = string.Join(",", parts).Trim() + ":";
        SuggestedFoods.Clear();
    }
    private async Task OnGoalsSettingsAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new GoalsSettingsPage());
        }
    }
}