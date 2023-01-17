namespace NotificationServices;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    static LoginDatabase database;

    public static LoginDatabase Database
    {
        get
        {
            if (database == null)
            {
                // UserName = admin, Password = 1234
                database = new LoginDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SQLiteSample.db"));
            }
            return database;
        }
    }
}
