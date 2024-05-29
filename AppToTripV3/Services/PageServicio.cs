using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppToTripV3.Views;
using AppToTripV3.ViewModels;

namespace AppToTripV3.Services
{
    public class PageServicio : IPageServicio
    {

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page, false);
        }

        public async Task PushModalAsync(Page page)
        {
            await MainPage.Navigation.PushModalAsync(page, false);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync(false);
        }

        public async Task<Page> PopModalAsync()
        {
            return await MainPage.Navigation.PopModalAsync(false);
        }


        /*Pop up*/
        public async Task<bool> DisplayAlert(string title, string message, string ok)
        {
            //string cancel = "vacio";
            bool result = false;
            //try
            //{
            //    if (PopupNavigation.Instance.PopupStack.Count > 0)
            //    {
            //        await PopupNavigation.Instance.PopAllAsync();
            //    }
            //    if (ok.ToLower() == "ok")
            //    {
            //        ok = "Aceptar";
            //    }

            //    var a = new PopupAlert
            //    {
            //        TitlePopup = title,
            //        Message = message,
            //        PrimaryButtonText = ok,
            //        SecondaryButtonText = cancel,
            //        YesorNot = false,
            //        PrimaryCommand = new Command(async () =>
            //        {
            //            result = true;
            //            await PopupNavigation.Instance.PopAllAsync();
            //        }),
            //        SecondaryCommand = new Command(async () =>
            //        {
            //            result = false;
            //            await PopupNavigation.Instance.PopAsync();
            //        })
            //    };
            //    await PopupNavigation.Instance.PushAsync(a);
            //    await a.PopupClosedTask;
            //}
            //catch { }
            return result;
        }

        public async Task<bool> CalificarSitio(string title, string message, string IdSitio)
        {
            bool result = false;
            //try
            //{
            //    if (PopupNavigation.Instance.PopupStack.Count > 0)
            //    {
            //        await PopupNavigation.Instance.PopAllAsync();
            //    }

            //    var a = new PopUpCalificacionSitio
            //    {
            //        TitlePopup = title,
            //        Message = message,
            //        PrimaryButtonText = "",
            //        SecondaryButtonText = "",
            //        YesorNot = true,
            //        idSitio = IdSitio,
            //        PrimaryCommand = new Command(async () =>
            //        {
            //            result = true;
            //            await PopupNavigation.Instance.PopAllAsync();
            //        }),
            //        SecondaryCommand = new Command(async () =>
            //        {
            //            result = false;
            //            await PopupNavigation.Instance.PopAsync();
            //        })
            //    };
            //    await PopupNavigation.Instance.PushAsync(a);
            //    await a.PopupClosedTask;
            //}
            //catch { }
            return result;
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
           
            bool result = false;
            //try
            //{
            //    if (PopupNavigation.Instance.PopupStack.Count > 0)
            //    {
            //        await PopupNavigation.Instance.PopAllAsync();
            //    }
            //    if (ok.ToLower() == "ok")
            //    {
            //        ok = "Aceptar";
            //    }

            //    var a = new PopupAlert
            //    {
            //        TitlePopup = title,
            //        Message = message,
            //        PrimaryButtonText = ok,
            //        SecondaryButtonText = cancel,
            //        YesorNot = true,
            //        PrimaryCommand = new Command(async () =>
            //        {
            //            result = true;
            //            await PopupNavigation.Instance.PopAllAsync();
            //        }),
            //        SecondaryCommand = new Command(async () =>
            //        {
            //            result = false;
            //            await PopupNavigation.Instance.PopAsync();
            //        })
            //    };
            //    await PopupNavigation.Instance.PushAsync(a);
            //    await a.PopupClosedTask;
            //}
            //catch { }
            return result;
        }

    }
}