using System.Windows.Input;
using HealthLog.Models;
using HealthLog.Services;

namespace HealthLog.ViewModels;

// ViewModel for goals and settings page
public class GoalsSettingsViewModel : BaseViewModel
{
    private readonly UserSettingsService settingsService;
    private readonly IPageService pageService;
    // Default constructor
    public GoalsSettingsViewModel()
        : this(new UserSettingsService(), new PageService())
    {
    }
    // Constructor with services
    public GoalsSettingsViewModel(UserSettingsService settingsService, IPageService pageService)
    {
        this.settingsService = settingsService;
        this.pageService = pageService;
        // Commands for buttons
        SaveCommand = new Command(async () => await OnSaveAsync());
        HomeCommand = new Command(async () => await OnHomeAsync());
        PreviousRecordsCommand = new Command(async () => await OnPreviousRecordsAsync());
        // Load saved data and apply settings
        LoadSettings();
        UpdateBMI();
        UpdateTargets();
        ApplyTheme();
        ApplyFontSize();
    }

    // Height input
    private string heightText = "";
    public string HeightText
    {
        get => heightText;
        set
        {
            heightText = value;
            OnPropertyChanged();
            // Update BMI and targets when height changes
            UpdateBMI();
            UpdateTargets();
        }
    }
    // Weight input
    private string weightText = "";
    public string WeightText
    {
        get => weightText;
        set
        {
            weightText = value;
            OnPropertyChanged();
            // Update BMI and targets when weight changes
            UpdateBMI();
            UpdateTargets();
        }
    }
    // Sex selection
    private string sex = "Male";
    public string Sex
    {
        get => sex;
        set
        {
            sex = value;
            OnPropertyChanged();
        }
    }
    // BMI display text
    private string bmiText = "BMI: 0.00";
    public string BMIText
    {
        get => bmiText;
        set
        {
            bmiText = value;
            OnPropertyChanged();
        }
    }
    // Goal selection
    private string goal = "Maintain";
    public string Goal
    {
        get => goal;
        set
        {
            goal = value;
            OnPropertyChanged();
            // Update nutrition targets when goal changes
            UpdateTargets();
        }
    }

    private string backgroundMode = "Bright";
    public string BackgroundMode
    {
        get => backgroundMode;
        set
        {
            backgroundMode = value;
            OnPropertyChanged();
            // Apply theme when background mode changes
            ApplyTheme();
        }
    }

    private string fontSizeMode = "Medium";
    public string FontSizeMode
    {
        get => fontSizeMode;
        set
        {
            fontSizeMode = value;
            OnPropertyChanged();
            // Apply font size when setting changes
            ApplyFontSize();
        }
    }

    private Color pageBackgroundColor = Colors.White;
    public Color PageBackgroundColor
    {
        get => pageBackgroundColor;
        set
        {
            pageBackgroundColor = value;
            OnPropertyChanged();
        }
    }

    private Color textColor = Colors.Black;
    public Color TextColor
    {
        get => textColor;
        set
        {
            textColor = value;
            OnPropertyChanged();
        }
    }

    private double appFontSize = 16;
    public double AppFontSize
    {
        get => appFontSize;
        set
        {
            appFontSize = value;
            OnPropertyChanged();
        }
    }

    private string targetCaloriesText = "Calories target: 0 Kcal";
    public string TargetCaloriesText
    {
        get => targetCaloriesText;
        set
        {
            targetCaloriesText = value;
            OnPropertyChanged();
        }
    }

    private string targetProteinText = "Protein target: 0 g";
    public string TargetProteinText
    {
        get => targetProteinText;
        set
        {
            targetProteinText = value;
            OnPropertyChanged();
        }
    }

    private string targetCarbsText = "Carbs target: 0 g";
    public string TargetCarbsText
    {
        get => targetCarbsText;
        set
        {
            targetCarbsText = value;
            OnPropertyChanged();
        }
    }

    private string targetFatText = "Fat target: 0 g";
    public string TargetFatText
    {
        get => targetFatText;
        set
        {
            targetFatText = value;
            OnPropertyChanged();
        }
    }

    private string targetWaterText = "Water target: 0 ml";
    public string TargetWaterText
    {
        get => targetWaterText;
        set
        {
            targetWaterText = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand HomeCommand { get; }
    public ICommand PreviousRecordsCommand { get; }
    public bool IsGoalsPage => true;
    // Calculate BMI
    private void UpdateBMI()
    {
        if (double.TryParse(HeightText, out double heightCm) &&
            double.TryParse(WeightText, out double weightKg) &&
            heightCm > 0)
        {
            double heightM = heightCm / 100.0;
            double bmi = weightKg / (heightM * heightM);
            BMIText = $"BMI: {bmi:F2}";
        }
        else
        {
            BMIText = "BMI: 0.00";
        }
    }
    // Calculate nutrition targets based on goal
    private void UpdateTargets()
    {
        int calories = 0;
        int protein = 0;
        int carbs = 0;
        int fat = 0;
        int water = 2000;

        if (!double.TryParse(WeightText, out double weightKg))
            weightKg = 0;

        switch (Goal)
        {
            case "Lose fat":
                calories = (int)(weightKg * 25);
                protein = (int)(weightKg * 2.0);
                carbs = (int)(weightKg * 2.0);
                fat = (int)(weightKg * 0.8);
                break;

            case "Build muscle":
                calories = (int)(weightKg * 35);
                protein = (int)(weightKg * 2.2);
                carbs = (int)(weightKg * 4.0);
                fat = (int)(weightKg * 1.0);
                break;

            default:
                calories = (int)(weightKg * 30);
                protein = (int)(weightKg * 1.6);
                carbs = (int)(weightKg * 3.0);
                fat = (int)(weightKg * 0.9);
                break;
        }

        TargetCaloriesText = $"Calories target: {calories} Kcal";
        TargetProteinText = $"Protein target: {protein} g";
        TargetCarbsText = $"Carbs target: {carbs} g";
        TargetFatText = $"Fat target: {fat} g";
        TargetWaterText = $"Water target: {water} ml";
    }
    // Apply background theme
    private void ApplyTheme()
    {
        if (BackgroundMode == "Dark")
        {
            PageBackgroundColor = Colors.Black;
            TextColor = Colors.White;
        }
        else
        {
            PageBackgroundColor = Colors.White;
            TextColor = Colors.Black;
        }
    }

    private void ApplyFontSize()
    {
        AppFontSize = FontSizeMode switch
        {
            "Small" => 14,
            "Large" => 20,
            _ => 16
        };
    }
    // Load saved user settings
    private void LoadSettings()
    {
        var settings = settingsService.Load();

        if (settings.HeightCm > 0)
            HeightText = settings.HeightCm.ToString();

        if (settings.WeightKg > 0)
            WeightText = settings.WeightKg.ToString();

        Sex = settings.Sex == "" ? "Male" : settings.Sex;
        Goal = settings.Goal;
        BackgroundMode = settings.BackgroundMode;
        FontSizeMode = settings.FontSizeMode;
    }
    // Save user settings
    private async Task OnSaveAsync()
    {
        double.TryParse(HeightText, out double heightCm);
        double.TryParse(WeightText, out double weightKg);

        double bmi = 0;
        if (heightCm > 0)
        {
            double heightM = heightCm / 100.0;
            bmi = weightKg / (heightM * heightM);
        }

        int targetCalories = GetNumber(TargetCaloriesText);
        int targetProtein = GetNumber(TargetProteinText);
        int targetCarbs = GetNumber(TargetCarbsText);
        int targetFat = GetNumber(TargetFatText);
        int targetWater = GetNumber(TargetWaterText);

        var settings = new UserSettings
        {
            HeightCm = heightCm,
            WeightKg = weightKg,
            Sex = Sex,
            BMI = bmi,
            Goal = Goal,
            BackgroundMode = BackgroundMode,
            FontSizeMode = FontSizeMode,
            TargetCalories = targetCalories,
            TargetProtein = targetProtein,
            TargetCarbs = targetCarbs,
            TargetFat = targetFat,
            TargetWater = targetWater
        };

        settingsService.Save(settings);

        await pageService.ShowAlertAsync("Saved", "Your settings have been saved.", "OK");
    }
    // Go back to home page
    private async Task OnHomeAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PopToRootAsync();
        }
    }

    private async Task OnPreviousRecordsAsync()
    {
        if (Application.Current?.Windows[0]?.Page != null)
        {
            await Application.Current.Windows[0].Page.Navigation.PopToRootAsync();
            await Application.Current.Windows[0].Page.Navigation.PushAsync(new PreviousRecordsPage());
        }
    }
    // Get number from target text
    private int GetNumber(string text)
    {
        string number = new string(text.Where(char.IsDigit).ToArray());
        return int.TryParse(number, out int result) ? result : 0;
    }
}