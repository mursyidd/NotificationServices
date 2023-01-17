namespace NotificationServices.View;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        //Skip to dashboard directly if credentials exists
        CheckUser();
    }

    private async void CheckUser()
    {
        bool hasKey = Preferences.Default.ContainsKey("Username");
        bool staySignIn = Preferences.Default.ContainsKey("StaySignIn");
        if (staySignIn)
        {
            if (hasKey)
            {
                string Username = Preferences.Default.Get("Username", "Unknown");
                string Password = Preferences.Default.Get("Password", "Unknown");
                await Shell.Current.GoToAsync($"DashboardPage?Username={Username}&Password={Password}");
            }
        }
    }

    private void onExitClicked(object sender, EventArgs e)
    {
        Preferences.Default.Clear();

        Application.Current.Quit();
    }

    private async void onLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}

