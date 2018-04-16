namespace Models
{
    public class Job
    {
        public Job() { }

        public Job(Job other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.Salary = other.Salary;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }
    }
}
