using SQLite;

namespace MobDev_AW;

public partial class Settings : ContentPage
{
	//Data that you don't want exposed.
    private SQLiteAsyncConnection _databaseX;
    public SettingsViewModel ViewModel { get; set; }
		
	public Settings()
    {
        InitializeComponent();
        ViewModel = new SettingsViewModel();
        BindingContext = ViewModel;


        // Initialize the SQLite database connection
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "settings.db");
        _databaseX = new SQLiteAsyncConnection(databasePath);
        _databaseX.CreateTableAsync<UserSet>().Wait();
        //_database.DeleteAllAsync<UserSet>().Wait();

        // Load the user settings
        LoadUserSettings();
    }

    private async void SaveSettings_Clicked(object sender, EventArgs e)
    {
        var name = NameEntry.Text;
        int age = 0;
        int.TryParse(AgeEntry.Text, out age);
        bool theme = togTheme.IsToggled;
        var someEnt = someEntry.Text;

        int fontSize = (int)fontSizeSlider.Value;
        float brightness = (float)brightnessSlider.Value;
        string selectedFont = fontFamilyPicker.SelectedItem.ToString();

        var userSettings = new UserSet
        {
            Name = name,
            Age = age,
            lightOrDark = theme,
            //SomeEntry = someEnt,
            SavedFontSize = fontSize,
            SavedBrightness = brightness,
            SavedFontFamily = selectedFont,
        };

        await _databaseX.InsertOrReplaceAsync(userSettings);

        // Show a confirmation message
        await DisplayAlert("Success", "User settings saved", "OK");
    }

    private async void LoadUserSettings()
    {
        // Check if the user settings already exist in the database
        var existingSettings = await _databaseX.Table<UserSet>().FirstOrDefaultAsync();
        if (existingSettings != null)
        {
            NameEntry.Text = existingSettings.Name;
            AgeEntry.Text = existingSettings.Age.ToString();

           // someEntry.Text = existingSettings.SomeEntry.ToString();

            fontSizeSlider.Value = (double)existingSettings.SavedFontSize;
            brightnessSlider.Value = (double)existingSettings.SavedBrightness;
            fontFamilyPicker.SelectedItem = existingSettings.SavedFontFamily;


            if (existingSettings.lightOrDark)
            {
                togTheme.IsToggled = true;
            }
            else
            {
                togTheme.IsToggled = false;
            }

            var currentTheme = existingSettings.lightOrDark;
            if (currentTheme)
                Application.Current.UserAppTheme = AppTheme.Dark;
            else
                Application.Current.UserAppTheme = AppTheme.Light;
        }
    }

    //PREFERENCES
    //SWITCH: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/switch
    private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
    {
        bool isDarkTheme = e.Value;
        Preferences.Set("DarkThemeOn", isDarkTheme ? "Dark" : "Light");

        // Apply the theme
        if (isDarkTheme)
            Application.Current.UserAppTheme = AppTheme.Dark;
        else
            Application.Current.UserAppTheme = AppTheme.Light;
    }
}