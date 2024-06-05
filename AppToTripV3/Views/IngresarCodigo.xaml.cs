using AppToTripV3.ViewModels;


namespace AppToTripV3.Views
{

    public partial class IngresarCodigo : ContentPage
    {
        private int secondsRemaining = 60;
        public IngresarCodigo(string email, string verificationCode)
        {
            InitializeComponent();
            BindingContext = new IngresarCodigoViewModel(email, verificationCode);
            StartTimer();
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
        private async void StartTimer()
        {
            while (secondsRemaining > 0)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    timerLabel.Text = "Tiempo restante" + $": 00:{secondsRemaining}";
                });

                await Task.Delay(1000); // Espera 1 segundo
                secondsRemaining--;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                TxtreenviaCode.IsVisible = true;
                timerLabel.IsVisible = false;
            });
        }
    }
}