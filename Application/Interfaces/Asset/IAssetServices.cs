﻿using Domain.Entities;

namespace Application.Interfaces;

public interface IAssetServices
{
    Task<ICollection<AssetEntity>> List();
    Task<AssetEntity> GetById(Guid assetId); // Get By AssetId
    Task<ICollection<AssetEntity>> GetByAssetTypeId(Guid assetTypeid); // Get By AssetTypeId
    Task<int> Count();
    Task Create(AssetEntity asset);
    Task Update(Guid assetId, AssetEntity asset);
    Task Delete(Guid assetId);
}