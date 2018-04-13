using Models;

using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class PersonViewModel : BindableBase
    {
        private readonly Person person;

        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public Person Person => this.person;

        public string Name => $"{this.person.FirstName} {this.person.LastName}";

        public string Job => this.person.Job.Name;

        public string Salary => $"$ {this.person.Job.Salary:0.00}";
    }
}
