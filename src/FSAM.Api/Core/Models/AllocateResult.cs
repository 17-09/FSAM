using System.Collections.Generic;

namespace FSAM.Api.Core.Models
{
    public class AllocateResult
    {
        public Usage BusinessUsage { get; set; }
        public Usage EconomyUsage { get; set; }
    }

    public class Usage
    {
        public Usage()
        {
            SelectedBusinessOffers = new List<int>();
            SelectedEconomyOffers = new List<int>();
        }

        public int Seats { get; set; }
        public decimal Revenue { get; set; }

        public List<int> SelectedEconomyOffers { get; }
        public List<int> SelectedBusinessOffers { get; }
    }
}