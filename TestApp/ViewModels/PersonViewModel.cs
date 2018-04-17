using System;
using Models;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        private readonly Person person;

        private string firstName;
        private string lastName;
        private DateTime birthDay;
        private string job;
        private decimal salary;
        
        public PersonViewModel(Person person)
        {
            this.person = person;

            this.firstName = person.FirstName;
            this.lastName = person.LastName;
            this.birthDay = person.BirthDay;
            this.job = person.Job.Name;
            this.salary = person.Job.Salary;
        }

        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        public DateTime BirthDay
        {
            get => this.birthDay;
            set => this.SetProperty(ref this.birthDay, value);
        }

        public string Job
        {
            get => this.job;
            set => this.SetProperty(ref this.job, value);
        }

        public decimal Salary
        {
            get => this.salary;
            set => this.SetProperty(ref this.salary, value);
        }

        public string FullName => $"{this.person.FirstName} {this.person.LastName}";

        public string SalaryText => $"$ {this.salary:0.00}";
    }
}
