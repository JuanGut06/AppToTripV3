using AppToTripV3.Models;
using AppToTripV3.Services;
using AppToTripV3.ViewModels;
using Microsoft.Maui.Controls.Maps;
namespace AppToTripV3.Views;

public partial class TargetasSitios : ContentPage
{
    public readonly IPageServicio pageServicio;
    public TargetasSitios(Circuito circuit, string categoria, string pais, string ciudad,
        string Bandera, string idCircuito)
    {
        pageServicio = new PageServicio();
        InitializeComponent();
        BindingContext = new TargetasSitiosVM(circuit, categoria, pais, ciudad,
        Bandera, idCircuito);
    }
}