using System.Collections.Generic;
using System.Linq;
using UsingViewModelsWithAspNetMvc.Models;
using UsingViewModelsWithAspNetMvc.ViewModels.Shared;

namespace UsingViewModelsWithAspNetMvc.ViewModels.Home
{
    public class IndexViewModel : LayoutViewModel
    {
        private readonly Company company;
        
        public IndexViewModel(Company company)
        {
            this.company = company;

            base.Title = string.Format("{0} employees", company.Name);
            base.Header1 = company.Name;
            base.MetaDescription = string.Format("A list of {0} employees with their respective salary.", company.Name);
        }

        public string Name
        {
            get { return this.company.Name; }
        }

        public IEnumerable<EmployeeViewModel> Employees
        {
            get { return this.company.Employees == null ? Enumerable.Empty<EmployeeViewModel>() : this.company.Employees.Select(EmployeeViewModel.Create); }
        }

        public class EmployeeViewModel
        {
            private readonly Employee employee;

            private EmployeeViewModel(Employee employee)
            {
                this.employee = employee;
            }

            public string Name
            {
                get { return string.Format("{0} {1}", this.employee.FirstName, this.employee.LastName); }
            }

            public string Salary
            {
                get { return this.employee.Salary.HasValue ? this.employee.Salary.Value.ToString("C") : "Unknown"; }
            }

            public string BirthDate
            {
                get { return this.employee.BirthDate.HasValue ? this.employee.BirthDate.Value.ToShortDateString() : "Unknown"; }
            }

            public static EmployeeViewModel Create(Employee employee)
            {
                return new EmployeeViewModel(employee);
            }
        }
    }
}