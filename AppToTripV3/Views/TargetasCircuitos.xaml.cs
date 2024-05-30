using AppToTripV3.Services;
using Microsoft.Maui.Controls.Maps;


namespace AppToTripV3.Views
{
    public partial class TargetasCircuitos : ContentPage
    {
        private readonly IPageServicio pageServicio;
        public TargetasCircuitos(string categoria, string pais, string ciudad, string Bandera, string idCircuito)
        {
            pageServicio = new PageServicio();
            InitializeComponent();
        }
    }
        
}