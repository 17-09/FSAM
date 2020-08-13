using System;
using System.Collections.Generic;
using System.Linq;

namespace FSAM.Api.Core.Models
{
    public class AllocateSpec
    {
        public AllocateSpec(int[] offers, int businessSeats, int economySeats, int threshold)
        {
            if (threshold <= 0) throw new ArgumentException($"{nameof(threshold)} must be greater than 0");

            if (!offers.Any()) throw new ArgumentException($"{nameof(offers)} can not be empty");

            if (businessSeats < 0)
                throw new ArgumentException($"{nameof(businessSeats)} must be greater than or equal to 0");

            if (economySeats < 0)
                throw new ArgumentException($"{nameof(economySeats)} must be greater than or equal to 0");

            Threshold = threshold;
            Offers = offers;
            BusinessSeats = businessSeats;
            EconomySeats = economySeats;
        }

        public int Threshold { get; }
        public int[] Offers { get; }
        public int BusinessSeats { get; }
        public int EconomySeats { get; }

        public IEnumerable<int> BusinessOffersDesc => Offers.Where(o => o >= Threshold).OrderByDescending(o => o);
        public IEnumerable<int> EconomyOffersDesc => Offers.Where(o => o < Threshold).OrderByDescending(o => o);
    }
}