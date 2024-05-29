using AppToTripV3.Views;

namespace AppToTripV3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Filtros();
        }
    }
}
