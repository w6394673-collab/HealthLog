namespace HealthLog.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    // Handle login button click event
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Get username and password entered by the user
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;
        // Simple validation for empty input
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlertAsync("Error", "Please enter username and password.", "OK");
        }
        else
        {
            // Navigate to HomePage if login is successful
            await Navigation.PushAsync(new HomePage());
        }
    }
}