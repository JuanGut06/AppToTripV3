using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToTripV3.Services
{
    public interface IPageServicio
    {
        Task PushAsync(Page page);
        Task PushModalAsync(Page page);
        Task<Page> PopAsync();
        Task<Page> PopModalAsync();


        /*Pop up*/
        Task<bool> DisplayAlert(string title, string message, string ok);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task<bool> CalificarSitio(string title, string message, string idSitio);

    }
}