using Module03Exercise01.View;
using System.Diagnostics;
using Microsoft.Maui.ApplicationModel;
using System.Threading.Tasks;


namespace Module03Exercise01
{
    public partial class App : Application
    {
        private const string TestUrl = "https://reqbin.com";

        private readonly IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = new AppShell();
            _serviceProvider = serviceProvider;
        }

        protected override async void OnStart()
        {
            var current = Connectivity.NetworkAccess;
            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

            if (current == NetworkAccess.Internet && isWebsiteReachable)
            {
                //await Shell.Current.GoToAsync("//LoginPage");
                MainPage = _serviceProvider.GetRequiredService<LoginPage>();

                Debug.WriteLine("Application Started (Online)");
            }

            else
            {
                await Shell.Current.GoToAsync("//OfflinePage");
                Debug.WriteLine("Application Started (Offline)");
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("Application on Sleep Mode");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Application Resumed");
        }

        private async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }

            catch
            {
                return false;
            }
        }
    }
}
