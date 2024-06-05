using AppToTripV3.ViewModels;

namespace AppToTripV3.Views;
public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
        BindingContext = new RegistroViewModel();
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}