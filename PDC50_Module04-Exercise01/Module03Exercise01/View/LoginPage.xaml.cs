using Module03Exercise01.ViewModel;
using Module03Exercise01.Services;


namespace Module03Exercise01.View;

public partial class LoginPage : ContentPage
{
    private readonly IMyService _myService; //Field for the Service
    public LoginPage(IMyService myService)
    {
        InitializeComponent();
        BindingContext = new EmployeeLoginViewModel();
        _myService = myService;

        var message = _myService.GetMessage();
        MyLabel.Text = message;
    }

    private async void AddEmployeePage_btn (object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AddEmployeePage");
    }
}