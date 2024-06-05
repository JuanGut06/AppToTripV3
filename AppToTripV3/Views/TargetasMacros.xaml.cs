using AppToTripV3.ViewModels;
using Plugin.SimpleAudioPlayer;

namespace AppToTripV3.Views
{
    public partial class TargetasMacros : ContentPage
    {
        public string pais;
        public string ciudad;
        public string nombreMacro_;
        public TargetasMacros(string nombreMacro)
        {
            InitializeComponent();
            nombreMacro_ = nombreMacro;
            BindingContext = new TargetasMacroVM(nombreMacro);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LimpiarAudio();
        }

        private async void LimpiarAudio()
        {
            //await CrossMediaManager.Current.Stop();
            CrossSimpleAudioPlayer.Current.Stop();
        }
    }
}