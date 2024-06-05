using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppToTripV3.Services
{
    public class PinesEnumerados
    {
        public string PinesEnumeradosRespuesta(string Orden, string colorBandera)
        {
            string aunPion;
            if (colorBandera == "1")
            {
                aunPion = "ic_ubicacion_azul.png";

                switch (Orden)
                {
                    case "1":
                        aunPion = "pin_azul_1.png";
                        break;
                    case "2":
                        aunPion = "pin_azul_2.png";
                        break;
                    case "3":
                        aunPion = "pin_azul_3.png";
                        break;
                    case "4":
                        aunPion = "pin_azul_4.png";
                        break;
                    case "5":
                        aunPion = "pin_azul_5.png";
                        break;
                    case "6":
                        aunPion = "pin_azul_6.png";
                        break;
                    case "7":
                        aunPion = "pin_azul_7.png";
                        break;
                    case "8":
                        aunPion = "pin_azul_8.png";
                        break;
                    case "9":
                        aunPion = "pin_azul_9.png";
                        break;
                    case "10":
                        aunPion = "pin_azul_10.png";
                        break;
                    case "11":
                        aunPion = "pin_azul_11.png";
                        break;
                    case "12":
                        aunPion = "pin_azul_12.png";
                        break;
                    case "13":
                        aunPion = "pin_azul_13.png";
                        break;
                    case "14":
                        aunPion = "pin_azul_14.png";
                        break;
                    case "15":
                        aunPion = "pin_azul_15.png";
                        break;
                    case "16":
                        aunPion = "pin_azul_16.png";
                        break;
                    case "17":
                        aunPion = "pin_azul_17.png";
                        break;
                    case "18":
                        aunPion = "pin_azul_18.png";
                        break;
                    case "19":
                        aunPion = "pin_azul_19.png";
                        break;
                    case "20":
                        aunPion = "pin_azul_20.png";
                        break;
                }

                return aunPion;
            }
            else
            {
                aunPion = "ic_ubicacion_rojo.png";

                switch (Orden)
                {
                    case "1":
                        aunPion = "pin_rojo_1.png";
                        break;
                    case "2":
                        aunPion = "pin_rojo_2.png";
                        break;
                    case "3":
                        aunPion = "pin_rojo_3.png";
                        break;
                    case "4":
                        aunPion = "pin_rojo_4.png";
                        break;
                    case "5":
                        aunPion = "pin_rojo_5.png";
                        break;
                    case "6":
                        aunPion = "pin_rojo_6.png";
                        break;
                    case "7":
                        aunPion = "pin_rojo_7.png";
                        break;
                    case "8":
                        aunPion = "pin_rojo_8.png";
                        break;
                    case "9":
                        aunPion = "pin_rojo_9.png";
                        break;
                    case "10":
                        aunPion = "pin_rojo_10.png";
                        break;
                    case "11":
                        aunPion = "pin_rojo_11.png";
                        break;
                    case "12":
                        aunPion = "pin_rojo_12.png";
                        break;
                    case "13":
                        aunPion = "pin_rojo_13.png";
                        break;
                    case "14":
                        aunPion = "pin_rojo_14.png";
                        break;
                    case "15":
                        aunPion = "pin_rojo_15.png";
                        break;
                    case "16":
                        aunPion = "pin_rojo_16.png";
                        break;
                    case "17":
                        aunPion = "pin_rojo_17.png";
                        break;
                    case "18":
                        aunPion = "pin_rojo_18.png";
                        break;
                    case "19":
                        aunPion = "pin_rojo_19.png";
                        break;
                    case "20":
                        aunPion = "pin_rojo_20.png";
                        break;
                }

                return aunPion;

            }

        }

    }
}

