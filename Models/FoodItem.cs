namespace HealthLog.Models;

// Model class representing a food item
// It stores nutrition information for each food
public class FoodItem
{
    // Name of the food
    public string Name { get; set; } = "";
    // Calories value
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public double Water { get; set; }
}
