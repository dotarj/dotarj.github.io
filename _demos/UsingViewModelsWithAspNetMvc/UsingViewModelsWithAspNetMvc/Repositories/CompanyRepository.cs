using System;
using System.Collections.Generic;
using UsingViewModelsWithAspNetMvc.Models;

namespace UsingViewModelsWithAspNetMvc.Repositories
{
    public class CompanyRepository
    {
        public Company Get()
        {
            return new Company
            {
                Id = 1,
                Name = "Microsoft",
                Employees = new List<Employee>()
                {
                    new Employee()
                    {
                        Id = 1,
                        FirstName = "Satya",
                        LastName = "Nadella",
                        Salary = 1200000
                    },
                    new Employee()
                    {
                        Id = 2,
                        FirstName = "Scott",
                        LastName = "Guthrie",
                        BirthDate = new DateTime(1975, 2, 6)
                    }
                }
            };
        }
    }
}