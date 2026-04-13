namespace HealthLog.Models;

// Model for user goals and settings
public class UserSettings
{
    public double HeightCm { get; set; }
    public double WeightKg { get; set; }
    public string Sex { get; set; } = "";
    public double BMI { get; set; }

    public string Goal { get; set; } = "Maintain";

    public string BackgroundMode { get; set; } = "Bright";
    public string FontSizeMode { get; set; } = "Medium";

    public int TargetCalories { get; set; }
    public int TargetProtein { get; set; }
    public int TargetCarbs { get; set; }
    public int TargetFat { get; set; }
    public int TargetWater { get; set; }
}