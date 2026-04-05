using SQLite;

namespace HealthLog.Models;

// Model class representing a saved food record
public class FoodRecord
{
    // Primary key for database (auto increment)
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Basic information about the record
    public string Date { get; set; } = "";
    public string RecordName { get; set; } = "";

    // Nutrition values calculated from the food input
    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Carbs { get; set; }
    public int Fat { get; set; }
    public int Water { get; set; }

    // Short summary shown in the previous records list
    [Ignore]
    public string SummaryText
    {
        get { return $"Date: {Date}    {RecordName}"; }
    }

    // Detailed nutrition information for each record
    [Ignore]
    public string DetailText
    {
        get
        {
            return $"Calories: {Calories} Kcal\n" +
                   $"Protein: {Protein} g\n" +
                   $"Carbs: {Carbs} g\n" +
                   $"Fat: {Fat} g\n" +
                   $"Water: {Water} ml";
        }
    }
}