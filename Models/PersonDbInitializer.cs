using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Models.Context;

using Ploeh.AutoFixture;

namespace Models
{
    public class PersonDbInitializer
    {
        public static async Task Initialize()
        {
            using (var personContext = new PersonContext())
            {
                var firstNames = new List<string>
                                     {
                                         "Adina",
                                         "Adora",
                                         "Adore",
                                         "Adoree",
                                         "Adorne",
                                         "Adrea",
                                         "Adria",
                                         "Adriaens",
                                         "Adrian",
                                         "Adriana",
                                         "Adriane",
                                         "Adrianna",
                                         "Adrianne",
                                         "Adriena"
                                     };
                var lastNames = new List<string>
                                    {
                                        "Lukas",
                                        "Luke",
                                        "Lutero",
                                        "Luther",
                                        "Ly",
                                        "Lydon",
                                        "Lyell",
                                        "Lyle",
                                        "Lyman",
                                        "Lyn",
                                        "Lynn",
                                        "Lyon",
                                        "Mac",
                                        "Mace",
                                        "Mack",
                                        "Mackenzie"
                                    };

                var jobNames = new List<string>
                                   {
                                       "Java Software Engineer",
                                       "Agile Coach",
                                       "Senior C++ Developer",
                                       "Team Lead (C#)",
                                       "Full stack JavaScript Developer"
                                   };

                var random = new Random();

                var fixture = new Fixture();
                fixture.Customize<Person>(
                    p => p.Without(p1 => p1.FirstName).Without(p1 => p1.LastName)
                        .Do(job => job.FirstName = firstNames[random.Next(0, firstNames.Count)])
                        .Do(job => job.LastName = lastNames[random.Next(0, lastNames.Count)]));
                fixture.Customize<Job>(j => j.Without(j1 => j1.Name).Do(job => job.Name = jobNames[random.Next(0, jobNames.Count)]));

                personContext.AddRange(fixture.CreateMany<Person>(10000));
                await personContext.SaveChangesAsync();
            }
        }
    }
}
