using Analytics.Dtos;

namespace Analytics.Models
{
    public class Energy : IEntity
    {
        public Energy() { }

        public Energy(EnergyDto energy)
        {
            Date = energy.Timestamp;
            Consumption = energy.Consumption;
            IsAnomaly = false;
        }

        public int Id { get; set; }

        public double Consumption { get; set; }

        public bool IsAnomaly { get; set; }

        public DateTime Date { get; set; }
    }
}
