using Module03Exercise01.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using Module03Exercise01.View;

namespace Module03Exercise01.ViewModel
{
    public class EmployeeLoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public EmployeeLoginViewModel()
        {
            EmployeeLoginCredentials = new ObservableCollection<EmployeeLogin>
            {
                new EmployeeLogin{emp_username="admin", emp_password="admin"}
            };
            LoginCommand = new Command(OnLogin);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EmployeeLogin> EmployeeLoginCredentials { get; set; }

        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Username)));
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public ICommand LoginCommand { get; }

        private async void OnLogin()
        {
            var matchingUser = EmployeeLoginCredentials.FirstOrDefault(e => e.emp_username == Username);

            if (matchingUser == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Incorrect username.", "OK");
            }
            else if (matchingUser.emp_password != Password)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Incorrect password.", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync("//EmployeePage");
            }
        }

    }
}
