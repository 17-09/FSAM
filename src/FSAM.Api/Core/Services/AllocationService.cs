using System;
using System.Collections.Generic;
using System.Linq;
using FSAM.Api.Core.Models;

namespace FSAM.Api.Core.Services
{
    public class AllocationService : IAllocationService
    {
        public AllocateResult Allocate(AllocateSpec spec)
        {
            var businessUsage = AllocateBusinessOffers(spec.BusinessOffersDesc.ToList(), spec.BusinessSeats);

            var remainingFreeBusinessSeats = Math.Max(spec.BusinessSeats - businessUsage.Seats, 0);
            var economyUsage = AllocateEconomyOffers(spec.EconomyOffersDesc.ToList(), remainingFreeBusinessSeats,
                spec.EconomySeats);

            return new AllocateResult
            {
                BusinessUsage = businessUsage,
                EconomyUsage = economyUsage
            };
        }

        private static Usage AllocateBusinessOffers(IReadOnlyCollection<int> businessOffersDesc, int freeBusinessSeats)
        {
            var selectedSeats = Math.Min(businessOffersDesc.Count, freeBusinessSeats);

            var usage = new Usage
            {
                Seats = selectedSeats,
                Revenue = businessOffersDesc.Take(selectedSeats).Sum()
            };
            usage.SelectedBusinessOffers.AddRange(businessOffersDesc.Take(selectedSeats));

            return usage;
        }

        private static Usage AllocateEconomyOffers(IReadOnlyCollection<int> economyOffersDesc,
            int remainingFreeBusinessSeats,
            int freeEconomySeats)
        {
            // Handle remaining free business seats firstly
            var usage = new Usage {Seats = remainingFreeBusinessSeats};
            usage.SelectedBusinessOffers.AddRange(economyOffersDesc.Take(remainingFreeBusinessSeats));

            // Handle free economy seats
            var selectedSeats = Math.Min(economyOffersDesc.Count - remainingFreeBusinessSeats, freeEconomySeats);
            usage.Seats += selectedSeats;
            usage.SelectedEconomyOffers.AddRange(economyOffersDesc.Skip(remainingFreeBusinessSeats)
                .Take(selectedSeats));

            // Compute revenue
            usage.Revenue = economyOffersDesc.Take(remainingFreeBusinessSeats + selectedSeats).Sum();

            return usage;
        }
    }
}