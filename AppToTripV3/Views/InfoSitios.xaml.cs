using AppToTripV3.ViewModels;
using AppToTripV3.Services;

using Plugin.SimpleAudioPlayer;

namespace AppToTripV3.Views;

public partial class InfoSitios : ContentPage
{
    private readonly IPageServicio pageServicio;
    public InfoSitios(string id_sitio, string Bandera, RecorridoMapaViewModel recorridoMapaView_)
    {
        pageServicio = new PageServicio();
        //EstadoSitio = estadoSitio;
        InitializeComponent();
        BindingContext = new InfoSitiosViewModel(id_sitio, Bandera, recorridoMapaView_);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LimpiarAudio();
    }

    private void LimpiarAudio()
    {
        //try
        //{
        //    if (PopupNavigation.Instance.PopupStack.Any())
        //        await PopupNavigation.Instance.PopAllAsync();
        //}
        //catch { }
        //await CrossMediaManager.Current.Stop();
        CrossSimpleAudioPlayer.Current.Stop();
    }
    
}