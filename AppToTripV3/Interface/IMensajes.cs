namespace AppToTripV3.Interface
{
    public interface IMensajes
    {
        void ToastLongAlert(string message);
        void ToastShortAlert(string message);
        void SnackbarShow(string message);

    }
}
