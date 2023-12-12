using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using WebApiLayer.Services;

namespace WebApiLayer.Configurations;

public static class DatabaseInitializer
{
    private static readonly Random _rand = new();

    public static async Task InitializeAsync(ApplicationDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();
        
        await dbContext.MockUser();
        await dbContext.MockGame();
        await dbContext.MockAttributeGroup();
        await dbContext.MockGameUser();
        await dbContext.MockGameServer();
        await dbContext.MockAssetType();
        await dbContext.MockAsset();
        await dbContext.MockAssetAttribute();
        await dbContext.MockCharacterType();
        await dbContext.MockCharacter();
        await dbContext.MockCharacterAsset();
        await dbContext.MockCharacterAttribute();
        await dbContext.MockLevel();
        await dbContext.MockLevelProgress();
        await dbContext.MockWalletCategory();
        await dbContext.MockWallet();
        await dbContext.MockTransaction();
        await dbContext.MockPayment();
        await dbContext.MockActivityType();
        await dbContext.MockActivity();
    }

    public static async Task MockAttributeGroup(this ApplicationDbContext dbContext)
    {
        if (dbContext.AttributeGroups.Any()) return;

        dynamic attGrp = SeedingServices.LoadJson("ATTRIBUTE_GROUP_MOCK_DATA.json");

        var games = dbContext.Games.ToList();
        int attGrpsLength = attGrp.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockAttGrp = attGrp[_rand.Next(attGrpsLength)];
            await dbContext.AttributeGroups.AddAsync(
                new AttributeGroupEntity()
                {
                    Name = mockAttGrp.Name,
                    Effect = mockAttGrp.Effect,
                    GameId = games[_rand.Next(games.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockUser(this ApplicationDbContext dbContext)
    {
        if (dbContext.Users.Any()) return;

        dynamic user = SeedingServices.LoadJson("USER_MOCK_DATA.json");
        for (int i = 0; i < user.Count; i++)
        {
            var mockUser = user[i];
            await dbContext.Users.AddAsync(
                new UserEntity()
                {
                    Uid = mockUser.Username,
                    Username = mockUser.Username,
                    FirstName = mockUser.FirstName,
                    LastName = mockUser.LastName,
                    Avatar = mockUser.Avatar,
                    Email = mockUser.Email,
                    Phone = mockUser.Phone,
                    Code = mockUser.Code,
                    Status = mockUser.Status,
                    Balance = mockUser.Balance,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockGame(this ApplicationDbContext dbContext)
    {
        if (dbContext.Games.Any()) return;

        dynamic game = SeedingServices.LoadJson("GAME_MOCK_DATA.json");
        var users = dbContext.Users.ToList();
        int gameLength = game.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockGame = game[_rand.Next(gameLength)];
            GameEntity newGame = new GameEntity()
                {
                    Name = mockGame.Name,
                    Logo = mockGame.Logo,
                    Link = $"dummy {mockGame.Link}",
                    Banner = mockGame.Link,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    GamePlan = GamePlan.Basic,
                };
            await dbContext.Games.AddAsync(newGame);
            await dbContext.SaveChangesAsync();
        }
    }
    public static async Task MockGameUser(this ApplicationDbContext dbContext)
    {
        if (dbContext.GamesUsers.Any()) return;

        var users = dbContext.Users.ToList();
        var games = dbContext.Games.ToList();
        for (int i =0; i < 20; i++)
        {
            await dbContext.GamesUsers.AddAsync(
                new GameUserEntity
                {
                    GameId = games[_rand.Next(games.Count)].Id,
                    UserId = users[_rand.Next(users.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
            });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockGameServer(this ApplicationDbContext dbContext)
    {
        if (dbContext.GameServers.Any()) return;

        dynamic gameServer = SeedingServices.LoadJson("GAME_SERVER_MOCK_DATA.json");
        var games = dbContext.Games.ToList();
        int gameServerLength = gameServer.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockGameServer = gameServer[_rand.Next(gameServerLength)];
            await dbContext.GameServers.AddAsync(
                new GameServerEntity()
                {
                    Name = mockGameServer.Name,
                    Location = mockGameServer.Location,
                    ArtifactUrl = mockGameServer.ArtifactUrl,
                    GameId = games[_rand.Next(games.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockAssetType(this ApplicationDbContext dbContext)
    {
        if (dbContext.AssetTypes.Any()) return;

        dynamic assetType = SeedingServices.LoadJson("ASSET_TYPE_MOCK_DATA.json");
        var games = dbContext.Games.ToList();
        int assetTypeLength = assetType.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockAssetType = assetType[_rand.Next(assetTypeLength)];
            await dbContext.AssetTypes.AddAsync(
                new AssetTypeEntity()
                {
                    Name = mockAssetType.Name,
                    GameId = games[_rand.Next(games.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockAsset(this ApplicationDbContext dbContext)
    {
        if (dbContext.Assets.Any()) return;

        dynamic asset = SeedingServices.LoadJson("ASSET_MOCK_DATA.json");
        var assetType = dbContext.AssetTypes.ToList();
        int assetLength = asset.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockAsset = asset[_rand.Next(assetLength)];
            await dbContext.Assets.AddAsync(
                new AssetEntity()
                {
                    Name = mockAsset.Name,
                    Image = mockAsset.Image,
                    Description = mockAsset.Description,
                    AssetTypeId = assetType[_rand.Next(assetType.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockAssetAttribute(this ApplicationDbContext dbContext)
    {
        if (dbContext.AssetAttributes.Any()) return;

        dynamic assetAttribute = SeedingServices.LoadJson("ASSET_ATTRIBUTE_MOCK_DATA.json");
        var assets = dbContext.Assets.ToList();
        var attributeGroups = dbContext.AttributeGroups.ToList();
        int assetAttributeLength = assetAttribute.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockAssetAttribute = assetAttribute[_rand.Next(assetAttributeLength)];
            await dbContext.AssetAttributes.AddAsync(
                new AssetAttributeEntity()
                {
                    Value = mockAssetAttribute.Value,
                    AssetId = assets[_rand.Next(assets.Count)].Id,
                    AttributeGroupId = attributeGroups[_rand.Next(attributeGroups.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockCharacterType(this ApplicationDbContext dbContext)
    {
        if (dbContext.CharacterTypes.Any()) return;

        dynamic characterType = SeedingServices.LoadJson("CHARACTER_TYPE_MOCK_DATA.json");
        var games = dbContext.Games.ToList();
        int characterTypeLength = characterType.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockCharacterType = characterType[_rand.Next(characterTypeLength)];
            await dbContext.CharacterTypes.AddAsync(
                new CharacterTypeEntity()
                {
                    Name = mockCharacterType.Name,
                    Description = mockCharacterType.Description,
                    BaseProperties = mockCharacterType.BaseProperties,
                    GameId = games[_rand.Next(games.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockCharacter(this ApplicationDbContext dbContext)
    {
        if (dbContext.Characters.Any()) return;

        dynamic character = SeedingServices.LoadJson("CHARACTER_MOCK_DATA.json");
        var users = dbContext.Users.ToList();
        var characterTypes = dbContext.CharacterTypes.ToList();
        var gameServers = dbContext.GameServers.ToList();
        int characterCount = character.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockCharacter = character[_rand.Next(characterCount)];
            await dbContext.Characters.AddAsync(
                new CharacterEntity()
                {
                    CurrentProperty = mockCharacter.CurrentProperty,
                    PointReward = mockCharacter.PointReward,
                    UserId = users[_rand.Next(users.Count)].Id,
                    CharacterTypeId = characterTypes[_rand.Next(characterTypes.Count)].Id,
                    GameServerId = gameServers[_rand.Next(gameServers.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockCharacterAsset(this ApplicationDbContext dbContext)
    {
        if (dbContext.CharacterAssets.Any()) return;

        dynamic characterAsset = SeedingServices.LoadJson("CHARACTER_ASSET_MOCK_DATA.json");
        var assets = dbContext.Assets.ToList();
        var characters = dbContext.Characters.ToList();
        int characterAssetCount = characterAsset.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockCharacterAsset = characterAsset[_rand.Next(characterAssetCount)];
            await dbContext.CharacterAssets.AddAsync(
                new CharacterAssetEntity()
                {
                    Value = mockCharacterAsset.Value,
                    ExpiredDate = mockCharacterAsset.ExpiredDate,
                    AssetsId = assets[_rand.Next(assets.Count)].Id,
                    CharacterId = characters[_rand.Next(characters.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockCharacterAttribute(this ApplicationDbContext dbContext)
    {
        if (dbContext.CharacterAttributes.Any()) return;

        dynamic characterAttribute = SeedingServices.LoadJson("CHARACTER_ATTRIBUTE_MOCK_DATA.json");
        var characters = dbContext.Characters.ToList();
        var attributeGroups = dbContext.AttributeGroups.ToList();
        int characterAttributeCount = characterAttribute.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockCharacterAttribute = characterAttribute[_rand.Next(characterAttributeCount)];
            await dbContext.CharacterAttributes.AddAsync(
                new CharacterAttributeEntity()
                {
                    Value = mockCharacterAttribute.Value,
                    CharacterId = characters[_rand.Next(characters.Count)].Id,
                    AttributeGroupId = attributeGroups[_rand.Next(attributeGroups.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockLevel(this ApplicationDbContext dbContext)
    {
        if (dbContext.Levels.Any()) return;

        dynamic level = SeedingServices.LoadJson("LEVEL_MOCK_DATA.json");
        var games = dbContext.Games.ToList();
        int levelCount = level.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockLevel = level[_rand.Next(levelCount)];
            LevelEntity newLevel;
            do
            {
                Guid gameId = games[_rand.Next(games.Count)].Id;
                newLevel = new LevelEntity()
                {
                    Description = mockLevel.Description,
                    LevelNo = dbContext.Levels.Count(g => g.GameId == gameId) + 1,
                    LevelUpPoint = mockLevel.LevelUpPoint,
                    GameId = gameId,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };
            } while (dbContext.Levels.FirstOrDefault(l => l.Description == newLevel.Description && l.GameId == newLevel.GameId) != null);
            dbContext.Levels.Add(newLevel);
            dbContext.SaveChanges(); // Await this operation
        }
    }
    public static async Task MockLevelProgress(this ApplicationDbContext dbContext)
    {
        if (dbContext.LevelProgresses.Any()) return;

        dynamic levelProgress = SeedingServices.LoadJson("LEVEL_PROGRESS_MOCK_DATA.json");
        var characters = dbContext.Characters.ToList();
        var levels = dbContext.Levels.ToList();
        int levelProgressCount = levelProgress.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockLevelProgress = levelProgress[_rand.Next(levelProgressCount)];
            LevelProgressEntity newLevelProgress;
            do
            {
                newLevelProgress = new LevelProgressEntity()
                {  
                    LevelUpDate = mockLevelProgress.LevelUpDate,
                    ExpPoint = mockLevelProgress.ExpPoint,
                    Name  = mockLevelProgress.Name,
                    CharacterId = characters[_rand.Next(characters.Count)].Id,
                    LevelId = levels[_rand.Next(levels.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };
            } while (await dbContext.LevelProgresses.FirstOrDefaultAsync(lP => (lP.CharacterId == newLevelProgress.CharacterId && lP.LevelId == newLevelProgress.LevelId) || lP.LevelId == newLevelProgress.LevelId) != null);

            await dbContext.LevelProgresses.AddAsync(newLevelProgress);
            await dbContext.SaveChangesAsync();
        }
    }
    public static async Task MockWalletCategory(this ApplicationDbContext dbContext)
    {
        if (dbContext.WalletCategories.Any()) return;

        dynamic walletCategory = SeedingServices.LoadJson("WALLET_CATEGORY_MOCK_DATA.json");
        var games = dbContext.Games.ToList();
        int walletCategoryCount = walletCategory.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockWalletCategory = walletCategory[_rand.Next(walletCategoryCount)];
            await dbContext.WalletCategories.AddAsync(
                new WalletCategoryEntity()
                {
                    Name = mockWalletCategory.Name,
                    GameId = games[_rand.Next(games.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
            await dbContext.SaveChangesAsync(); // Await this operation
        }
    }
    public static async Task MockWallet(this ApplicationDbContext dbContext)
    {
        if (dbContext.Wallets.Any()) return;

        dynamic wallet = SeedingServices.LoadJson("WALLET_MOCK_DATA.json");
        var walletCategories = dbContext.WalletCategories.ToList();
        var characters = dbContext.Characters.ToList();
        int walletCount = wallet.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockWallet = wallet[_rand.Next(walletCount)];
            await dbContext.Wallets.AddAsync(
                new WalletEntity()
                {
                    Name = mockWallet.Name,
                    TotalMoney = mockWallet.TotalMoney,
                    WalletCategoryId = walletCategories[_rand.Next(walletCategories.Count)].Id,
                    CharacterId = characters[_rand.Next(characters.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockTransaction(this ApplicationDbContext dbContext)
    {
        if (dbContext.Transactions.Any()) return;

        dynamic transaction = SeedingServices.LoadJson("TRANSACTION_MOCK_DATA.json");
        var wallets = dbContext.Wallets.ToList();
        int transactionCount = transaction.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockTransaction = transaction[_rand.Next(transactionCount)];
            await dbContext.Transactions.AddAsync(
                new TransactionEntity()
                {
                    Name = mockTransaction.Name,
                    Value = mockTransaction.Value,
                    WalletId = wallets[_rand.Next(wallets.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockPayment(this ApplicationDbContext dbContext)
    {
        if (dbContext.Payments.Any()) return;

        dynamic payment = SeedingServices.LoadJson("PAYMENT_MOCK_DATA.json");
        var characters = dbContext.Characters.ToList();
        var users = dbContext.Users.ToList();
        var wallets = dbContext.Wallets.ToList();
        int paymentCount = payment.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockPayment = payment[_rand.Next(paymentCount)];
            PaymentEntity newpayment;
            do
            {
                newpayment = new PaymentEntity()
                {
                    Amount = mockPayment.Amount,
                    Date = mockPayment.Date,
                    Content = mockPayment.Content,
                    BankCode = mockPayment.BankCode,
                    Status = mockPayment.Status,
                    CharacterId = characters[_rand.Next(characters.Count)].Id,
                    UserId = users[_rand.Next(users.Count)].Id,
                    WalletId = wallets[_rand.Next(wallets.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };
            } while (await dbContext.Payments.FirstOrDefaultAsync(l => l.WalletId == newpayment.WalletId) != null);
            await dbContext.Payments.AddAsync(newpayment);
            await dbContext.SaveChangesAsync();
        }
    }
    public static async Task MockActivityType(this ApplicationDbContext dbContext)
    {
        if (dbContext.ActivityTypes.Any()) return;

        dynamic activityType = SeedingServices.LoadJson("ACTIVITY_TYPE_MOCK_DATA.json");
        var games = dbContext.Games.ToList();
        var characters = dbContext.Characters.ToList();
        int activityTypeCount = activityType.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockActivityType = activityType[_rand.Next(activityTypeCount)];
            await dbContext.ActivityTypes.AddAsync(
                new ActivityTypeEntity()
                {
                    Name = mockActivityType.Name,
                    GameId = games[_rand.Next(games.Count)].Id,
                    CharacterId = characters[_rand.Next(characters.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
    public static async Task MockActivity(this ApplicationDbContext dbContext)
    {
        if (dbContext.Activities.Any()) return;

        dynamic activity = SeedingServices.LoadJson("ACTIVITY_MOCK_DATA.json");
        var activityType = dbContext.ActivityTypes.ToList();
        var transaction = dbContext.Transactions.ToList();
        var character = dbContext.Characters.ToList();
        int activityCount = activity.Count;
        for (int i = 0; i < 20; i++)
        {
            var mockActivity = activity[_rand.Next(activityCount)];
            await dbContext.Activities.AddAsync(
                new ActivityEntity()
                {
                    Name = mockActivity.Name,
                    Status = mockActivity.Status,
                    ActivityTypeId = activityType[_rand.Next(activityType.Count)].Id,
                    TransactionId = transaction[_rand.Next(transaction.Count)].Id,
                    CharacterId = character[_rand.Next(transaction.Count)].Id,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                });
        }
        await dbContext.SaveChangesAsync();
    }
}
