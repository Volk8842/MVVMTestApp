using System;

namespace Models
{
    public class Person
    {
        public Person() { }

        public Person(Person other)
        {
            this.Id = other.Id;
            this.FirstName = other.FirstName;
            this.LastName = other.LastName;
            this.BirthDay = other.BirthDay;
            this.Job = new Job(other.Job);
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public Job Job { get; set; }
    }
}
