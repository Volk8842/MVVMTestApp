using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

using Common.Intefaces;

using Models;

using Prism.Commands;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly ObservableCollection<PersonViewModel> persons = new ObservableCollection<PersonViewModel>();
        private readonly IPersonRepository personRepository;

        private string title = "TestApp";

        private DelegateCommand loadPersonsCommand;

        private PersonViewModel selectedPerson;

        public MainWindowViewModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;

            this.Persons = new ListCollectionView(this.persons);

            PropertyChangedEventManager.AddHandler(this, this.SelectedPersonChangedHandler, nameof(this.SelectedPerson));
        }

        public DelegateCommand LoadPersonsCommand => this.loadPersonsCommand ?? (this.loadPersonsCommand = new DelegateCommand(this.LoadPersonExecute));

        public ICollectionView Persons { get; }

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        public PersonViewModel SelectedPerson
        {
            get => this.selectedPerson;
            set => this.SetProperty(ref this.selectedPerson, value);
        }

        private async void LoadPersonExecute()
        {
            List<Person> rawPersons = await this.personRepository.GetAll();
            this.persons.AddRange(rawPersons.Select(p => new PersonViewModel(p)).ToList());
        }

        private void SelectedPersonChangedHandler(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }
    }
}
