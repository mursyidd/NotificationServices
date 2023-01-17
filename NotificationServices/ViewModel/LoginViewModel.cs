using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotificationServices.Model;
using SQLite;

namespace NotificationServices.ViewModel
{
    public class LoginViewModel
    {
        private string _userName, _password;

        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }

        public ICommand RegisterCommand { private set; get; }
        public ICommand LoginCommand { private set; get; }
        public ICommand DeleteAllCommand { private set; get; }

        private INavigation Navigation;

        public LoginViewModel(INavigation navigation) 
        {
            RegisterCommand = new Command(OnRegisterCommand);
            LoginCommand = new Command(OnLoginCommand);
            DeleteAllCommand = new Command(OnDeleteAllCommand);
            Navigation = navigation;
        }

        private async void OnLoginCommand(object obj)
        {
            var logindata = await App.Database.GetLoginDataAsync(UserName);
            if(logindata != null)
            {
                if(string.Equals(logindata.Password, Password))
                {
                    //Navigation Next Page
                    //await Navigation.PushModelAsync(new ProductPage());
                    await Shell.Current.GoToAsync($"DashboardPage?Username={logindata.UserName}&Password={logindata.Password}");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Failure", "Invalid Password", "Ok");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    await App.Current.MainPage.DisplayAlert("Failure", "Username cannot be null", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failure", "Invalid Username", "Ok");
            }
        }

        private async void OnRegisterCommand(object obj)
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                var logindata = await App.Database.GetLoginDataAsync(UserName);
                if (logindata == null)
                {
                    LoginModel lm = new LoginModel();
                    lm.UserName = UserName;
                    lm.Password = Password;
                    await App.Database.SaveLoginDataAsync(lm);
                    await App.Current.MainPage.DisplayAlert("Success", "Registration Successfull", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Failure", "The username has been taken!", "Ok");
            }
            else
                await App.Current.MainPage.DisplayAlert("Error", "Please check your username/password!", "Ok");
        }

        private async void OnDeleteAllCommand()
        {
            await App.Database.DeleteLoginDataAsync();
        }
    }
}
