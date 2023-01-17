
namespace NotificationServices;

[QueryProperty(nameof(Name), "Username")]
[QueryProperty(nameof(Password), "Password")]

public partial class DashboardPage : ContentPage
{
    string name;
    string password;
    public string Name
    {
        get => name;
        set
        {
            name = value;
            WelomeTxt.Text = $"Welcome {name.ToUpper()}!";
        }
    }

    public string Password
    {
        get => password;
        set
        {
            password = value;
            //WelomeTxt.Text = $"Welcome {name.ToUpper()} with password of {password.ToUpper()}!";
        }
    }

    public DashboardPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);

#if WINDOWS
        LogoutBtn.IsVisible = false;
        ExitBtn.IsVisible = false;
#endif
    }

    protected override bool OnBackButtonPressed()
    {
        //return base.OnBackButtonPressed();
        return true;
    }

    private async void onExitClicked(object sender, EventArgs e)
    {
        // Set a string value:
        //Preferences.Default.Set("Username", name);
        //Preferences.Default.Set("Password", password);

        Application.Current.Quit();

        //await Launcher.Default.OpenAsync("https://www.google.com/");
    }

    private async void onLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Default.Clear();
        await Shell.Current.GoToAsync($"///MainPage");
    }
}