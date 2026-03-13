using System.Collections.ObjectModel;
using HealthLog.Models;

namespace HealthLog.Data;
// Simple data store used to keep all food records in memory
public static class RecordStore
{
    // Collection that stores all food records
    public static ObservableCollection<FoodRecord> Records { get; } = new();
    // Add a new food record to the list
    public static void AddRecord(FoodRecord record)
    {
        // Insert at the top so the newest record appears first
        Records.Insert(0, record);
    }
    // Remove a record from the list
    public static void DeleteRecord(FoodRecord record)
    {
        if (Records.Contains(record))
        {
            Records.Remove(record);
        }
    }
}