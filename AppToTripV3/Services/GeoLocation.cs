
namespace AppToTripV3.Services
{
    public class GeoLocation
    {
        public double Lat { get; set; }
        public double Log { get; set; }

        public async Task<bool> GetLocationGPS()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Lat = location.Latitude;
                    Log = location.Longitude;
                }
                else
                {
                    //Nos devolvera una posicision conosida que ya tenga en cache del telefono
                    var knowLocation = await Geolocation.GetLastKnownLocationAsync();
                    Lat = knowLocation.Latitude;
                    Log = knowLocation.Longitude;
                }
                return true;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                //La manija no es compatible con la excepción del dispositivo
                string error1 = fnsEx.Message;
                return false;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                // Manija no habilitada en la excepción del dispositivo
                string error2 = fneEx.Message;
                return false;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                //Manejar excepción de permiso
                string error3 = pEx.Message;
                return false;
            }
            catch (Exception ex)
            {
                // Unable to get location 
                // No se puede obtener la ubicación
                string error4 = ex.Message;
                return false;
            }
        }
    }
}