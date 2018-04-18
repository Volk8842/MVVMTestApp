using System;
using Models;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        private Person person;

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

        public Person Person => this.person;

        public string FullName => $"{this.person.FirstName} {this.person.LastName}";

        public string JobText => this.person.Job.Name;

        public string SalaryText => $"$ {this.person.Job.Salary:0.00}";

        public void ApplyChanges()
        {
            this.Person.FirstName = this.FirstName;
            this.Person.LastName = this.LastName;
            this.Person.BirthDay = this.BirthDay;
            this.Person.Job.Name = this.Job;
            this.Person.Job.Salary = this.Salary;
            this.RaisePropertyChanged("FullName");
            this.RaisePropertyChanged("JobText");
            this.RaisePropertyChanged("SalaryText");
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
