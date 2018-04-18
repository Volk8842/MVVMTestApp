namespace Models
{
    public struct AverageSalaryData
    {
        public AverageSalaryData(string jobName, decimal averageSalary)
        {
            this.JobName = jobName;
            this.AverageSalary = averageSalary;
        }

        public string JobName { get; set; }

        public decimal AverageSalary { get; set; }
    }
}
