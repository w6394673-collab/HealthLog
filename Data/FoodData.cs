using HealthLog.Models;

namespace HealthLog.Data;

public static class FoodData
{
    public static Dictionary<string, FoodItem> Items { get; } = new()
    {
        { "apple", new FoodItem { Name = "apple", Calories = 52, Protein = 0, Carbs = 14, Fat = 0, Water = 86 } },
        { "egg", new FoodItem { Name = "egg", Calories = 78, Protein = 6, Carbs = 1, Fat = 5, Water = 0 } },
        { "chicken", new FoodItem { Name = "chicken", Calories = 165, Protein = 31, Carbs = 0, Fat = 4, Water = 0 } },
        { "banana", new FoodItem { Name = "banana", Calories = 89, Protein = 1, Carbs = 23, Fat = 0, Water = 75 } },
        { "orange", new FoodItem { Name = "orange", Calories = 47, Protein = 1, Carbs = 12, Fat = 0, Water = 87 } },
        { "rice", new FoodItem { Name = "rice", Calories = 130, Protein = 2, Carbs = 28, Fat = 0, Water = 0 } },
        { "bread", new FoodItem { Name = "bread", Calories = 265, Protein = 9, Carbs = 49, Fat = 3, Water = 0 } },
        { "milk", new FoodItem { Name = "milk", Calories = 42, Protein = 3, Carbs = 5, Fat = 1, Water = 88 } },
        { "beef", new FoodItem { Name = "beef", Calories = 250, Protein = 26, Carbs = 0, Fat = 15, Water = 0 } },
        { "pork", new FoodItem { Name = "pork", Calories = 242, Protein = 27, Carbs = 0, Fat = 14, Water = 0 } },
    { "fish", new FoodItem { Name = "fish", Calories = 206, Protein = 22, Carbs = 0, Fat = 12, Water = 0 } },
    { "shrimp", new FoodItem { Name = "shrimp", Calories = 99, Protein = 24, Carbs = 0, Fat = 1, Water = 0 } },
    { "cheese", new FoodItem { Name = "cheese", Calories = 402, Protein = 25, Carbs = 1, Fat = 33, Water = 0 } },
    { "yogurt", new FoodItem { Name = "yogurt", Calories = 59, Protein = 10, Carbs = 3, Fat = 0, Water = 85 } },
    { "potato", new FoodItem { Name = "potato", Calories = 77, Protein = 2, Carbs = 17, Fat = 0, Water = 79 } },
    { "tomato", new FoodItem { Name = "tomato", Calories = 18, Protein = 1, Carbs = 4, Fat = 0, Water = 95 } },
    { "carrot", new FoodItem { Name = "carrot", Calories = 41, Protein = 1, Carbs = 10, Fat = 0, Water = 88 } },
    { "broccoli", new FoodItem { Name = "broccoli", Calories = 55, Protein = 4, Carbs = 11, Fat = 1, Water = 89 } },
    { "noodles", new FoodItem { Name = "noodles", Calories = 138, Protein = 5, Carbs = 25, Fat = 2, Water = 0 } },
    { "pasta", new FoodItem { Name = "pasta", Calories = 131, Protein = 5, Carbs = 25, Fat = 1, Water = 0 } },
    { "pizza", new FoodItem { Name = "pizza", Calories = 266, Protein = 11, Carbs = 33, Fat = 10, Water = 0 } },
    { "burger", new FoodItem { Name = "burger", Calories = 295, Protein = 17, Carbs = 30, Fat = 12, Water = 0 } },
    { "fries", new FoodItem { Name = "fries", Calories = 312, Protein = 3, Carbs = 41, Fat = 15, Water = 0 } },
    { "chocolate", new FoodItem { Name = "chocolate", Calories = 546, Protein = 5, Carbs = 61, Fat = 31, Water = 0 } },
    { "cake", new FoodItem { Name = "cake", Calories = 257, Protein = 4, Carbs = 49, Fat = 6, Water = 0 } },
    { "icecream", new FoodItem { Name = "icecream", Calories = 207, Protein = 3, Carbs = 24, Fat = 11, Water = 0 } },
    { "coffee", new FoodItem { Name = "coffee", Calories = 2, Protein = 0, Carbs = 0, Fat = 0, Water = 99 } },
    { "tea", new FoodItem { Name = "tea", Calories = 1, Protein = 0, Carbs = 0, Fat = 0, Water = 99 } },
    { "juice", new FoodItem { Name = "juice", Calories = 45, Protein = 0, Carbs = 11, Fat = 0, Water = 90 } },
    { "watermelon", new FoodItem { Name = "watermelon", Calories = 30, Protein = 1, Carbs = 8, Fat = 0, Water = 91 } }
    };
}