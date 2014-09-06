using System.Collections.Generic;

namespace UsingViewModelsWithAspNetMvc.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}