using FSAM.Api.Core.Models;

namespace FSAM.Api.Requests
{
    /// <summary>
    ///     AllocateRequest
    /// </summary>
    public class AllocateRequest
    {
        /// <summary>
        ///     Threshold value to distinguish between economy and business class
        /// </summary>
        public int Threshold { get; set; }

        /// <summary>
        ///     Will to pay values
        /// </summary>
        public int[] Offers { get; set; }

        /// <summary>
        ///     Free business seats
        /// </summary>
        public int BusinessSeats { get; set; }

        /// <summary>
        ///     Free economy seats
        /// </summary>
        public int EconomySeats { get; set; }

        public AllocateSpec ToSpec()
        {
            return new AllocateSpec(Offers, BusinessSeats, EconomySeats, Threshold);
        }
    }
}