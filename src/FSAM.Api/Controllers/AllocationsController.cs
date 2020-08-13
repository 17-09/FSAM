using FSAM.Api.Core.Services;
using FSAM.Api.Requests;
using FSAM.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FSAM.Api.Controllers
{
    /// <summary>
    ///     AllocationsController is used to allocate seats
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AllocationsController : ControllerBase
    {
        private readonly IAllocationService _allocationService;

        public AllocationsController(IAllocationService allocationService)
        {
            _allocationService = allocationService;
        }

        /// <summary>
        ///     Allocates seats
        /// </summary>
        /// <param name="spec">Specification of allocations</param>
        /// <returns>Returns seats and revenue of each category will be occupied</returns>
        [HttpPost]
        public IActionResult Allocate([FromBody] AllocateRequest spec)
        {
            if (spec == null) return BadRequest($"{nameof(spec)} can not be null");

            if (spec.Offers == null || spec.Offers.Length == 0)
                return BadRequest($"{nameof(spec.Offers)} can not be empty");

            if (spec.Threshold <= 0) return BadRequest($"{nameof(spec.Threshold)} must be greater than 0");

            if (spec.BusinessSeats < 0)
                return BadRequest($"{nameof(spec.BusinessSeats)} must be greater than or equal to 0");

            if (spec.EconomySeats < 0)
                return BadRequest($"{nameof(spec.EconomySeats)} must be greater than or equal to 0");

            var allocateResult = _allocationService.Allocate(spec.ToSpec());
            return Ok(AllocateResponse.From(allocateResult));
        }
    }
}