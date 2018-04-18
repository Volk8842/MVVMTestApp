using System;
using Models;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        private string firstName;
        private string lastName;
        private DateTime birthDay;
        private string job;
        private decimal salary;

        public PersonViewModel(Person person)
        {
            this.Person = person;

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

        public Person Person { get; }

        public string FullName => $"{this.Person.FirstName} {this.Person.LastName}";

        public string JobText => this.Person.Job.Name;

        public string SalaryText => $"$ {this.Person.Job.Salary:0.00}";

        public void ApplyChanges()
        {
            this.Person.FirstName = this.FirstName;
            this.Person.LastName = this.LastName;
            this.Person.BirthDay = this.BirthDay;
            this.Person.Job.Name = this.Job;
            this.Person.Job.Salary = this.Salary;
            this.RaisePropertyChanged(nameof(this.FullName));
            this.RaisePropertyChanged(nameof(this.JobText));
            this.RaisePropertyChanged(nameof(this.SalaryText));
        }

        public void DropChanges()
        {
            this.FirstName = this.Person.FirstName;
            this.LastName = this.Person.LastName;
            this.BirthDay = this.Person.BirthDay;
            this.Job = this.Person.Job.Name;
            this.Salary = this.Person.Job.Salary;
        }
    }
}
