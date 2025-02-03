namespace color_picker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        UpdateColor();
    }

    private void UpdateColor()
    {
        int r = (int)Math.Round(RedSlider.Value);
        int g = (int)Math.Round(GreenSlider.Value);
        int b = (int)Math.Round(BlueSlider.Value);

       
        RedValueLabel.Text = r.ToString();
        GreenValueLabel.Text = g.ToString();
        BlueValueLabel.Text = b.ToString();

       
        Color selectedColor = Color.FromRgb(r, g, b);
        ColorDisplay.Color = selectedColor;
        BackgroundColor = selectedColor;

      
        string hex = $"#{r:X2}{g:X2}{b:X2}";
        HexLabel.Text = hex;

       
        if (HexEntry.Text?.ToUpper() != hex)
        {
            HexEntry.Text = hex;
        }
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateColor();
    }

    private void OnRandomColorClicked(object sender, EventArgs e)
    {
        Random rand = new Random();
        RedSlider.Value = rand.Next(0, 256);
        GreenSlider.Value = rand.Next(0, 256);
        BlueSlider.Value = rand.Next(0, 256);
        UpdateColor();
    }

    private void OnResetColorClicked(object sender, EventArgs e)
    {
        RedSlider.Value = 0;
        GreenSlider.Value = 0;
        BlueSlider.Value = 0;
        UpdateColor();
    }

    private async void OnHexLabelTapped(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(HexLabel.Text);
        await DisplayAlert("Copied", "HEX color code has been copied to clipboard!", "OK");
    }

    private void OnHexEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string hex = HexEntry.Text.Trim();

        if (hex.Length == 7 && hex[0] == '#')
        {
            try
            {
                int r = Convert.ToInt32(hex.Substring(1, 2), 16);
                int g = Convert.ToInt32(hex.Substring(3, 2), 16);
                int b = Convert.ToInt32(hex.Substring(5, 2), 16);

                
                RedSlider.Value = r;
                GreenSlider.Value = g;
                BlueSlider.Value = b;

                UpdateColor();
            }
            catch
            {
                
                HexEntry.BackgroundColor = Color.FromRgb(255, 100, 100); 
            }
        }
        else
        {
            HexEntry.BackgroundColor = Colors.Transparent; 
        }
    }
}
