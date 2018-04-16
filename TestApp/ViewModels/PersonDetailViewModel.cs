using System;
using System.ComponentModel;
using Models;
using Prism.Commands;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class PersonDetailViewModel : BindableBase
    {
        private PersonViewModel originalPersonViewModel;
        private Person person;
        
        private DelegateCommand saveCommand;

        public PersonDetailViewModel(PersonViewModel personViewModel)
        {
            this.originalPersonViewModel = personViewModel;
            this.person = new Person(personViewModel.Person);
        }

        public string FirstName
        {
            get => this.person.FirstName;
            set
            {
                this.person.FirstName = value;
                this.RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get => this.person.LastName;
            set
            {
                this.person.LastName = value;
                this.RaisePropertyChanged();
            }
        }

        public string BirthDay
        {
            get => this.person.BirthDay.ToLongDateString();
            set
            {
                try
                {
                    this.person.BirthDay = DateTime.Parse(value);
                    this.RaisePropertyChanged();
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public string Job
        {
            get => this.person.Job.Name;
            set
            {
                this.person.Job.Name = value;
                this.RaisePropertyChanged();
            }
        }

        public string Salary
        {
            get => $"{this.person.Job.Salary}";
            set
            {
                this.person.Job.Salary = decimal.Parse(value);
                this.RaisePropertyChanged();
            }
        }

        public Person Person
        {
            get => this.person;
        }

        public DelegateCommand SaveCommand => this.saveCommand ?? (this.saveCommand = new DelegateCommand(this.Save));

        private void Save()
        {
            this.originalPersonViewModel.Person = this.person;
        }
    }
}
