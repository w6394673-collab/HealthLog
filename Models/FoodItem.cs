namespace HealthLog.Models;

// Model class representing a food item
// It stores nutrition information for each food
public class FoodItem
{
    // Name of the food
    public string Name { get; set; } = "";
    // Calories value
    public int Calories { get; set; }
    // Protein amount
    public int Protein { get; set; }
    // Carbohydrates amount
    public int Carbs { get; set; }
    // Fat amount
    public int Fat { get; set; }
    // Water content
    public int Water { get; set; }
}
