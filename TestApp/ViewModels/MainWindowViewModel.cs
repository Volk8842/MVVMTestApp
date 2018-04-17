﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Serialization;

using Common.Intefaces;

using Models;

using Prism.Commands;
using Prism.Mvvm;

namespace TestApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public class AverageSalaryData
        {
            public string JobName { get; set; }

            public decimal AverageSalary { get; set; }
        }

        private readonly ObservableCollection<PersonViewModel> persons = new ObservableCollection<PersonViewModel>();
        private readonly IPersonRepository personRepository;

        private string title = "TestApp";

        private DelegateCommand loadPersonsCommand;
        private DelegateCommand exportJobDataCommand;

        private PersonViewModel selectedPerson;

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

        private async void LoadPersonExecute()
        {
            List<Person> rawPersons = await this.personRepository.GetAll();
            this.persons.AddRange(rawPersons.Select(p => new PersonViewModel(p)).ToList());
        }

        private async void ExportJobDataExecute()
        {
            List<Person> rawPersons = await this.personRepository.GetAll();
            await this.ExportJobDataAsync(rawPersons);
        }

        private async Task ExportJobDataAsync(List<Person> rawPersons) => await Task.Run(() =>
        {
            var jobAverageSalary = rawPersons.GroupBy(p => p.Job.Name).ToDictionary(g => g.Key, g => g.Average(p => p.Job.Salary));
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(AverageSalaryData));
            using (FileStream file = File.Create("JobData.xml"))
            {
                foreach (var item in jobAverageSalary)
                {
                    xmlSerializer.Serialize(file, new AverageSalaryData() { JobName = item.Key, AverageSalary = item.Value });
                }
            }
        });

        private void SelectedPersonChangedHandler(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }
    }
}
