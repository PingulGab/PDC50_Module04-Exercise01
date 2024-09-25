namespace Module03Exercise01.View;

public partial class OfflinePage : ContentPage
{
    private const string TestUrl = "https://google.com";
    public OfflinePage()
    {
        InitializeComponent();
    }

    private async void Retry_Btn(object sender, EventArgs e)
    {
        var current = Connectivity.NetworkAccess;
        bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

        if (current == NetworkAccess.Internet && isWebsiteReachable)
        {
            await DisplayAlert("Retry Successful", "You are now online.", "Ok");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        else
        {
            await DisplayAlert("Retry Failed", "Please check your internet connection.", "Ok");
        }
    }

    private async void Exit_Btn(object sender, EventArgs e)
    {
        Application.Current.Quit();
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