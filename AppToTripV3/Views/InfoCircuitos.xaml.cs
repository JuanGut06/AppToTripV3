using AppToTripV3.ViewModels;
using Plugin.SimpleAudioPlayer;


namespace AppToTripV3.Views
{
	public partial class InfoCircuitos : ContentPage
	{
		public InfoCircuitos(string id_circuito)
		{
			InitializeComponent();
			BindingContext = new InfoCircuitosViewModel(id_circuito);
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LimpiarAudio();
        }

        private async void LimpiarAudio()
        {
            try
            {
                //if (PopupNavigation.Instance.PopupStack.Any())
                //    await PopupNavigation.Instance.PopAllAsync();
            }
            catch { }
            //await CrossMediaManager.Current.Stop();
            CrossSimpleAudioPlayer.Current.Stop();
        }
    }

    public class Images
    {
        public string Url { get; set; }
    }
}
