using Module03Exercise01.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module03Exercise01.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee _employee;
        private string _fullName;
        private string _position;
        public EmployeeViewModel()
        {
            _employee = new Employee { FirstName = "Michael", LastName = "Scott", Position = "Manager", Department = "Administrative", IsActive = true };

            LoadEmployeeDataCommand = new Command(async () => await LoadEmployeeDataAsync());

            Employees = new ObservableCollection<Employee>
            {
                new Employee {FirstName = "Dwight", LastName = "Schrute", Position ="Assistant to the Regional Manager", Department = "Administrative", IsActive= true },
                new Employee {FirstName = "Andy", LastName = "Bernard", Position ="Assistant to the Assistant of the Regional Manager", Department = "Administrative", IsActive = true },
                new Employee {FirstName = "Jim", LastName = "Halpert", Position ="Salesman", Department = "Sales", IsActive = true }
            };
        }

        public ObservableCollection<Employee> Employees { get; set; }

        public ICommand LoadEmployeeDataCommand { get; set; }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        private async Task LoadEmployeeDataAsync()
        {
            await Task.Delay(1000);
            FullName = $"{_employee.FirstName} {_employee.LastName}";
            Position = $"{_employee.Position}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
