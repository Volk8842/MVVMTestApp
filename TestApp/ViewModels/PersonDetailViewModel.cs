using Models;

using Prism.Commands;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class PersonDetailViewModel : BindableBase
    {
        private readonly Person person;

        private DelegateCommand saveCommand;
        private DelegateCommand closeCommand;

        public PersonDetailViewModel(Person person)
        {
            this.person = person;
        }

        public string FirstName
        {
            get => this.person.FirstName;
        }

        public string LastName
        {
            get => this.person.LastName;
        }

        public string BirthDay
        {
            get => this.person.BirthDay.ToLongDateString();
        }

        public string Job
        {
            get => this.person.Job.Name;
        }

        public string Salary
        {
            get => $"{this.person.Job.Salary}";
        }

        public DelegateCommand SaveCommand => this.saveCommand ?? (this.saveCommand = new DelegateCommand(this.Save));

        public DelegateCommand CloseCommand => this.closeCommand ?? (this.closeCommand = new DelegateCommand(this.Close));

        private void Save()
        {

        }

        private void Close()
        {

        }
    }
}
