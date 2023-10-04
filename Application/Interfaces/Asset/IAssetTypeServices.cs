using Domain.Entities.Asset;

namespace Application.Interfaces;

public interface IAssetTypeServices
{
    Task<ICollection<AssetType>> List();
    Task<AssetType> GetById(Guid assetTypeId);
    Task<ICollection<AssetType>> GetByGameId(Guid gameId);
    Task<int> Count();
    Task Create(AssetType assetType);
    Task Update(Guid assetTypeId, AssetType assetType);
    Task Delete(Guid assetTypeId);
}
