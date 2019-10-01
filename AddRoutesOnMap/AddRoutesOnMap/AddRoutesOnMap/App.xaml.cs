
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AddRoutesOnMap
{
    public partial class App : Application
    {
        public static readonly string GOOGLE_MAP_API_KEY = "AIzaSyD5HZVzzHIy5NxhjHoUjMSMNXec4QR9PRw";
        public App()
        {
            InitializeComponent();
            
            MainPage = new NavigationPage(new Pages.AddRoutes());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
