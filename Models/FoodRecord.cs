using System;
using System.Collections.Generic;
using System.Text;

namespace HealthLog.Models;
// Model class used to store one food record
public class FoodRecord
{
    // Basic information about the record
    public string Date { get; set; }
    public string RecordName { get; set; }
    // Nutrition values calculated from the food input
    public int Calories { get; set; }
    public int Protein { get; set; }
    public int Carbs { get; set; }
    public int Fat { get; set; }
    public int Water { get; set; }
    // Short text shown in the Previous Records list
    public string SummaryText
    {
        get { return $"Date: {Date}   {RecordName}"; }
    }
    // Detailed nutrition information shown when viewing a record
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