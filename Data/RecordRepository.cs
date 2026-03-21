using Microsoft.Data.Sqlite;
using HealthLog.Models;

namespace HealthLog.Data;
// Handles database operations for food records
public class RecordRepository
{
    private readonly string dbPath;// Path of database file
    private bool isInitialized; // Check if database is initialized

    public RecordRepository()
    {
        // Set database location
        dbPath = Path.Combine(FileSystem.AppDataDirectory, "healthlog.db");
    }
    // Initialize database and create table if not exists
    public async Task InitAsync()
    {
        if (isInitialized)
            return;

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS FoodRecords (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT NOT NULL,
                RecordName TEXT NOT NULL,
                Calories INTEGER NOT NULL,
                Protein INTEGER NOT NULL,
                Carbs INTEGER NOT NULL,
                Fat INTEGER NOT NULL,
                Water INTEGER NOT NULL
            );
        ";

        await command.ExecuteNonQueryAsync();
        isInitialized = true;
    }
    // Add a new food record
    public async Task AddRecordAsync(FoodRecord record)
    {
        await InitAsync();

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO FoodRecords
            (Date, RecordName, Calories, Protein, Carbs, Fat, Water)
            VALUES
            ($date, $recordName, $calories, $protein, $carbs, $fat, $water);
        ";
        // Bind values to parameters
        command.Parameters.AddWithValue("$date", record.Date);
        command.Parameters.AddWithValue("$recordName", record.RecordName);
        command.Parameters.AddWithValue("$calories", record.Calories);
        command.Parameters.AddWithValue("$protein", record.Protein);
        command.Parameters.AddWithValue("$carbs", record.Carbs);
        command.Parameters.AddWithValue("$fat", record.Fat);
        command.Parameters.AddWithValue("$water", record.Water);

        await command.ExecuteNonQueryAsync();
    }
    // Get all records from database
    public async Task<List<FoodRecord>> GetRecordsAsync()
    {
        await InitAsync();

        var records = new List<FoodRecord>();

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT Id, Date, RecordName, Calories, Protein, Carbs, Fat, Water
            FROM FoodRecords
            ORDER BY Id DESC;
        ";

        using var reader = await command.ExecuteReaderAsync();
        // Read data and convert to objects
        while (await reader.ReadAsync())
        {
            records.Add(new FoodRecord
            {
                Id = reader.GetInt32(0),
                Date = reader.GetString(1),
                RecordName = reader.GetString(2),
                Calories = reader.GetInt32(3),
                Protein = reader.GetInt32(4),
                Carbs = reader.GetInt32(5),
                Fat = reader.GetInt32(6),
                Water = reader.GetInt32(7)
            });
        }

        return records;
    }
    // Delete a record by ID
    public async Task DeleteRecordAsync(int id)
    {
        await InitAsync();

        using var connection = new SqliteConnection($"Data Source={dbPath}");
        await connection.OpenAsync();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM FoodRecords WHERE Id = $id;";
        command.Parameters.AddWithValue("$id", id);

        await command.ExecuteNonQueryAsync();
    }
}