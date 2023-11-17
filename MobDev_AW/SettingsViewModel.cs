using System.ComponentModel;

public class SettingsViewModel : INotifyPropertyChanged
{
    private float _brightness;
    public float Brightness
    {
        get => _brightness;
        set
        {
            _brightness = value;
            OnPropertyChanged(nameof(Brightness));
        }
    }

    private int _fontSize;
    public int FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            OnPropertyChanged(nameof(FontSize));
        }
    }

    private List<string> _fontFamilies;
    public List<string> FontFamilies
    {
        get => _fontFamilies;
        set
        {
            _fontFamilies = value;
            OnPropertyChanged(nameof(FontFamilies));
        }
    }

    private string _selectedFontFamily;
    public string SelectedFontFamily
    {
        get => _selectedFontFamily;
        set
        {
            _selectedFontFamily = value;
            OnPropertyChanged(nameof(SelectedFontFamily));
        }
    }

    public SettingsViewModel()
    {
        // Initialize default values
        Brightness = 0.5f;
        FontSize = 16;
        FontFamilies = new List<string> { "Arial", "Times New Roman", "Verdana" };
        SelectedFontFamily = "Arial";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
