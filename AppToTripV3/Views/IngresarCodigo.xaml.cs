namespace AppToTripV3.Views
{

    public partial class IngresarCodigo : ContentPage
    {
        private int secondsRemaining = 60;
        public IngresarCodigo()
        {
            InitializeComponent();

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