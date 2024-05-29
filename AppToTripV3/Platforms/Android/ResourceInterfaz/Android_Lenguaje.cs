using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppToTripV3.Interface;

namespace AppToTripV3.Platforms.Android.ResourceInterfaz
{
    public class Android_Lenguaje : ILenguaje
    {
        public string Id_Cultura()
        {
            string lenguaje = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            string[] arrayLenguaje = lenguaje.Split("_");
            lenguaje = arrayLenguaje[0];

            if ((lenguaje == "en") || (lenguaje == "de") || (lenguaje == "fr") || (lenguaje == "es") || (lenguaje == "it") || (lenguaje == "ja") || (lenguaje == "pt") || (lenguaje == "ru") || (lenguaje == "tr"))
            {
                switch (lenguaje)
                {
                    case "en":
                        lenguaje = "en-US";
                        break;
                    case "de":
                        lenguaje = "de-DE";
                        break;
                    case "fr":
                        lenguaje = "fr-FR";
                        break;
                    case "it":
                        lenguaje = "it-IT";
                        break;
                    case "ja":
                        lenguaje = "ja-JP";
                        break;
                    case "pt":
                        lenguaje = "pt-BR";
                        break;
                    case "ru":
                        lenguaje = "ru-RU";
                        break;
                    case "tr":
                        lenguaje = "tr-TR";
                        break;
                    case "es":
                        lenguaje = "es-ES";
                        break;
                    default:
                        break;
                }

                return lenguaje;
            }
            else
            {
                lenguaje = "es";
                return lenguaje;
            }
        }

        public string Id_Lenguaje()
        {
            string lenguaje = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            string[] arrayLenguaje = lenguaje.Split("_");
            lenguaje = arrayLenguaje[0];

            if ((lenguaje == "en") || (lenguaje == "de") || (lenguaje == "fr") || (lenguaje == "es") || (lenguaje == "it") || (lenguaje == "ja") || (lenguaje == "pt") || (lenguaje == "ru") || (lenguaje == "tr"))
            {
                return lenguaje;
            }
            else
            {
                lenguaje = "es";
                return lenguaje;
            }
        }
    }
}