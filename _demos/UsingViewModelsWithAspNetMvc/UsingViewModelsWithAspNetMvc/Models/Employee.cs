using System;

namespace UsingViewModelsWithAspNetMvc.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public decimal? Salary { get; set; }
    }
}