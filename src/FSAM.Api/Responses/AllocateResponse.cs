using System.Collections.Generic;
using FSAM.Api.Core.Models;

namespace FSAM.Api.Responses
{
    public class AllocateResponse
    {
        public UsageResponse BusinessUsage { get; set; }
        public UsageResponse EconomyUsage { get; set; }

        public static AllocateResponse From(AllocateResult result)
        {
            return new AllocateResponse
            {
                BusinessUsage = new UsageResponse
                {
                    Revenue = result.BusinessUsage.Revenue,
                    Seats = result.BusinessUsage.Seats,
                    SelectedBusinessOffers = result.BusinessUsage.SelectedBusinessOffers,
                    SelectedEconomyOffers = result.BusinessUsage.SelectedEconomyOffers
                },
                EconomyUsage = new UsageResponse
                {
                    Revenue = result.EconomyUsage.Revenue,
                    Seats = result.EconomyUsage.Seats,
                    SelectedBusinessOffers = result.EconomyUsage.SelectedBusinessOffers,
                    SelectedEconomyOffers = result.EconomyUsage.SelectedEconomyOffers
                }
            };
        }
    }

    public class UsageResponse
    {
        public int Seats { get; set; }
        public decimal Revenue { get; set; }

        public List<int> SelectedEconomyOffers { get; set; }
        public List<int> SelectedBusinessOffers { get; set; }
    }
}