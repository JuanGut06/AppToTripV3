namespace AppToTripV3.Views;

public partial class IngresarCorreo : ContentPage
{
	public IngresarCorreo()
	{
		InitializeComponent();
	}
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}