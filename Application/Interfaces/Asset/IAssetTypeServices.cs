using Domain.Entities.Asset;

namespace Application.Interfaces;

public interface IAssetTypeServices
{
    //Asset Types
    Task<ICollection<AssetType>> List();
    Task<AssetType> GetById(Guid assetTypeId); // Get By AssetTypeId
    Task<ICollection<AssetType>> GetByGameId(Guid gameId); // Get By GameId
    Task<int> Count();
    Task Create(AssetType assetType);
    Task Update(Guid assetTypeId, AssetType assetType);
    Task Delete(Guid assetTypeId);
}
