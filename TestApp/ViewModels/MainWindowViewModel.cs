using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml;

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
        private DelegateCommand exportJobDataCommand;

        private PersonViewModel selectedPerson;
        private PersonDetailViewModel selectedPersonDetails;

        public MainWindowViewModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;

            this.Persons = new ListCollectionView(this.persons);

            PropertyChangedEventManager.AddHandler(this, this.SelectedPersonChangedHandler, nameof(this.SelectedPerson));
        }

        public DelegateCommand LoadPersonsCommand => this.loadPersonsCommand ?? (this.loadPersonsCommand = new DelegateCommand(this.LoadPersonExecute));

        public DelegateCommand ExportJobDataCommand => this.exportJobDataCommand ?? (this.exportJobDataCommand = new DelegateCommand(this.ExportJobDataExecute));

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

        public PersonDetailViewModel SelectedPersonDetails
        {
            get => this.selectedPersonDetails;
            set => this.SetProperty(ref this.selectedPersonDetails, value);
        }

        private async void LoadPersonExecute()
        {
            List<Person> rawPersons = await this.personRepository.GetAll();
            this.persons.AddRange(rawPersons.Select(p => new PersonViewModel(p)).ToList());
        }

        private async void ExportJobDataExecute()
        {
            Console.WriteLine("Extracting job data...");
            List<Person> rawPersons = await this.personRepository.GetAll();
            await this.ExportJobDataAsync(rawPersons);
        }

        private async Task ExportJobDataAsync(List<Person> rawPersons) => await Task.Run(() =>
        {
            var allPersonsJobsQuery = from item in rawPersons
                                      select new { item.Job.Name, item.Job.Salary };

            var jobsQuery = from item in allPersonsJobsQuery
                            group item by item.Name into jobGroup
                            select new { jobGroup.Key, AverageSalary = jobGroup.Average(item => item.Salary) };

            using (XmlWriter writer = XmlWriter.Create("JobData.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Jobs");
                foreach (var item in jobsQuery)
                {
                    writer.WriteStartElement("Job");
                    writer.WriteAttributeString("Name", item.Key);
                    writer.WriteAttributeString("AverageSalary", $"{item.AverageSalary:0.00}");
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
            Console.WriteLine("Extracting job data finished");
        });

        private void SelectedPersonChangedHandler(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (this.SelectedPerson != null)
            {
                this.SelectedPersonDetails = new PersonDetailViewModel(this.SelectedPerson);
            }
        }
    }
}
