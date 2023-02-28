using CommunityToolkit.Maui.Alerts;
using NotificationServices.ViewModel;

namespace NotificationServices;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

        this.BindingContext = new LoginViewModel(this.Navigation);
	}

    private void StaySignInRadio_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(e.Value == true)
        {
            Preferences.Default.Set("StaySignIn", true);
        }
        else
            Preferences.Default.Remove("StaySignIn");
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (onlinechk.IsChecked)
            onlinechk.IsChecked = false;
        else
            onlinechk.IsChecked = true;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        nametxt.Text = string.Empty;
        passwordtxt.Text = string.Empty;
    }
}