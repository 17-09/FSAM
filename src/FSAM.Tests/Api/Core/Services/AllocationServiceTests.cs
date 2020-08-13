using System;
using System.Collections.Generic;
using FSAM.Api.Core.Models;
using FSAM.Api.Core.Services;
using Xunit;

namespace FSAM.Tests.Api.Core.Services
{
    public class AllocationServiceTests
    {
        public static IEnumerable<object[]> AllocateTestData =>
            new List<object[]>
            {
                new object[]
                {
                    3, 3, new AllocateResult
                    {
                        BusinessUsage = new Usage
                        {
                            Revenue = 738,
                            Seats = 3,
                            SelectedBusinessOffers = {374, 209, 155}
                        },
                        EconomyUsage = new Usage
                        {
                            Revenue = 167,
                            Seats = 3,
                            SelectedEconomyOffers = {99, 45, 23}
                        }
                    }
                },
                new object[]
                {
                    7, 5, new AllocateResult
                    {
                        BusinessUsage = new Usage
                        {
                            Revenue = 1054,
                            Seats = 6,
                            SelectedBusinessOffers = {374, 209, 155, 115, 101, 100}
                        },
                        EconomyUsage = new Usage
                        {
                            Revenue = 189,
                            Seats = 4,
                            SelectedEconomyOffers = {45, 23, 22},
                            SelectedBusinessOffers = {99}
                        }
                    }
                },
                new object[]
                {
                    2, 7, new AllocateResult
                    {
                        BusinessUsage = new Usage
                        {
                            Revenue = 583,
                            Seats = 2,
                            SelectedBusinessOffers = {374, 209}
                        },
                        EconomyUsage = new Usage
                        {
                            Revenue = 189,
                            Seats = 4,
                            SelectedEconomyOffers = {99, 45, 23, 22}
                        }
                    }
                }
            };

        [Theory]
        [MemberData(nameof(AllocateTestData))]
        public void AllocateTests(int businessSeats, int economySeats, AllocateResult expected)
        {
            // Arrange
            var offers = new[] {374, 209, 155, 115, 101, 100, 99, 45, 23, 22};
            var spec = new AllocateSpec(offers, businessSeats, economySeats, 100);
            var allocationService = new AllocationService();
            var comparer = new UsageEqualityComparer();

            // Act
            var actual = allocationService.Allocate(spec);

            // Assert
            Assert.Equal(actual.BusinessUsage, expected.BusinessUsage, comparer);
            Assert.Equal(actual.EconomyUsage, expected.EconomyUsage, comparer);
        }
    }

    public class UsageEqualityComparer : IEqualityComparer<Usage>
    {
        public bool Equals(Usage u1, Usage u2)
        {
            if (u1 == null && u2 == null)
                return true;

            if (u1 == null || u2 == null)
                return false;

            if (u1.SelectedBusinessOffers.Count != u2.SelectedBusinessOffers.Count)
                return false;

            if (u1.SelectedEconomyOffers.Count != u2.SelectedEconomyOffers.Count)
                return false;

            for (var i = 0; i < u1.SelectedBusinessOffers.Count; i++)
            {
                if (u1.SelectedBusinessOffers[i] != u2.SelectedBusinessOffers[i])
                    return false;
            }

            for (var i = 0; i < u1.SelectedEconomyOffers.Count; i++)
            {
                if (u1.SelectedEconomyOffers[i] != u2.SelectedEconomyOffers[i])
                    return false;
            }

            return u1.Revenue == u2.Revenue
                   && u1.Seats == u2.Seats;
        }

        public int GetHashCode(Usage u)
        {
            int hCode = Convert.ToInt32(u.Revenue) ^ u.Seats ^ u.SelectedBusinessOffers.Count ^
                        u.SelectedEconomyOffers.Count;
            return hCode.GetHashCode();
        }
    }
}