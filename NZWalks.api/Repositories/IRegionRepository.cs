using Microsoft.AspNetCore.Http.HttpResults;
using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public interface IRegionRepository
    {
      Task<List<Region>>  GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region  region);
        Task<Region?> UpdateAsync(Guid id,Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
