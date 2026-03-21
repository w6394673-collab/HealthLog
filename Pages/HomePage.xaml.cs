using HealthLog.Data;
using HealthLog.Models;

namespace HealthLog.Pages;

public partial class HomePage : ContentPage
{
    private readonly RecordRepository recordRepository = new();
    // Constructor: initialize page and show current date
    public HomePage()
    {
        InitializeComponent();
        dateLabel.Text = "Date: " + DateTime.Now.ToString("dd/MM/yyyy");
    }

    // Triggered when the Estimate button is clicked
    private async void OnEstimateClicked(object sender, EventArgs e)
    {
        // Get food input from the text box
        string input = (foodEntry.Text ?? "").ToLower();
        // Check if input is empty
        if (string.IsNullOrWhiteSpace(input))
        {
            await DisplayAlertAsync("Reminder", "Please enter food.", "OK");
            return;
        }
        // Split multiple foods entered by the user
        string[] foods = input.Split(' ', ',');
        // Variables to store calculated nutrition values
        int calories = 0;
        int protein = 0;
        int carbs = 0;
        int fat = 0;
        int water = 0;
        // Loop through each food item and estimate nutrition
        foreach (string food in foods)
        {
            if (food == "apple")
            {
                calories += 52;
                carbs += 14;
                water += 86;
            }
            else if (food == "egg")
            {
                calories += 78;
                protein += 6;
                fat += 5;
            }
            else if (food == "chicken")
            {
                calories += 165;
                protein += 31;
                fat += 4;
            }
        }
        // Update UI labels with calculated nutrition values
        caloriesLabel.Text = $"- Calories : {calories} Kcal";
        proteinLabel.Text = $"- Protein : {protein} g";
        carbsLabel.Text = $"- Carbs : {carbs} g";
        fatLabel.Text = $"- Fat : {fat} g";
        waterLabel.Text = $"- Water : {water} ml";
        // Show nutrition suggestion based on the result
        if (protein < 20 || water < 100)
        {
            suggestionLabel.Text = "Nutrition is not enough. Please add more protein or water.";
            await DisplayAlertAsync("Reminder", "Your nutrition may not be enough.", "OK");
        }
        else
        {
            suggestionLabel.Text = "Good job. Your nutrition looks balanced.";
            await DisplayAlertAsync("Reminder", "Your nutrition looks good.", "OK");
        }
        // Create a record object and save it to the database
        FoodRecord record = new FoodRecord
        {
            Date = DateTime.Now.ToString("dd/MM/yyyy"),
            RecordName = input,
            Calories = calories,
            Protein = protein,
            Carbs = carbs,
            Fat = fat,
            Water = water
        };

        await recordRepository.AddRecordAsync(record);
        // Clear input box after calculation
        foodEntry.Text = "";
    }
    // Navigate to Previous Records page
    private async void OnPreviousRecordsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PreviousRecordsPage());
    }

}

