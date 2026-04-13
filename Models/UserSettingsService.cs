using HealthLog.Models;
using Microsoft.Maui.Storage;

namespace HealthLog.Services;

// Service for saving and loading user settings
public class UserSettingsService
{
    public void Save(UserSettings settings)
    {
        Preferences.Set("HeightCm", settings.HeightCm);
        Preferences.Set("WeightKg", settings.WeightKg);
        Preferences.Set("Sex", settings.Sex);
        Preferences.Set("BMI", settings.BMI);

        Preferences.Set("Goal", settings.Goal);
        Preferences.Set("BackgroundMode", settings.BackgroundMode);
        Preferences.Set("FontSizeMode", settings.FontSizeMode);

        Preferences.Set("TargetCalories", settings.TargetCalories);
        Preferences.Set("TargetProtein", settings.TargetProtein);
        Preferences.Set("TargetCarbs", settings.TargetCarbs);
        Preferences.Set("TargetFat", settings.TargetFat);
        Preferences.Set("TargetWater", settings.TargetWater);
    }

    public UserSettings Load()
    {
        return new UserSettings
        {
            HeightCm = Preferences.Get("HeightCm", 0.0),
            WeightKg = Preferences.Get("WeightKg", 0.0),
            Sex = Preferences.Get("Sex", ""),
            BMI = Preferences.Get("BMI", 0.0),

            Goal = Preferences.Get("Goal", "Maintain"),
            BackgroundMode = Preferences.Get("BackgroundMode", "Bright"),
            FontSizeMode = Preferences.Get("FontSizeMode", "Medium"),

            TargetCalories = Preferences.Get("TargetCalories", 0),
            TargetProtein = Preferences.Get("TargetProtein", 0),
            TargetCarbs = Preferences.Get("TargetCarbs", 0),
            TargetFat = Preferences.Get("TargetFat", 0),
            TargetWater = Preferences.Get("TargetWater", 0)
        };
    }
}