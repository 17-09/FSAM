using FSAM.Api.Core.Models;

namespace FSAM.Api.Core.Services
{
    public interface IAllocationService
    {
        AllocateResult Allocate(AllocateSpec spec);
    }
}