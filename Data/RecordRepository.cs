using SQLite;
using HealthLog.Models;

namespace HealthLog.Data;

// Handles database operations for food records
public class RecordRepository
{
    private readonly SQLiteAsyncConnection database;

    public RecordRepository()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "healthlog.db");
        database = new SQLiteAsyncConnection(dbPath);

        database.CreateTableAsync<FoodRecord>().Wait();
    }

    // Add a new food record
    public async Task AddRecordAsync(FoodRecord record)
    {
        await database.InsertAsync(record);
    }

    // Get all records from database
    public async Task<List<FoodRecord>> GetRecordsAsync()
    {
        return await database.Table<FoodRecord>()
                             .OrderByDescending(r => r.Id)
                             .ToListAsync();
    }

    // Delete a record by ID
    public async Task DeleteRecordAsync(int id)
    {
        var record = await database.Table<FoodRecord>()
                                   .FirstOrDefaultAsync(r => r.Id == id);

        if (record != null)
        {
            await database.DeleteAsync(record);
        }
    }
}