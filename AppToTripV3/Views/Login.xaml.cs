using AppToTripV3.ViewModels;

namespace AppToTripV3.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}