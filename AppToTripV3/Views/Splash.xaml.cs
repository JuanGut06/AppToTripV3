using AppToTripV3.ViewModels;

namespace AppToTripV3.Views;

public partial class Splash : ContentPage
{
	public Splash()
	{
		InitializeComponent();
        BindingContext = new SplashViewModel();
    }
}