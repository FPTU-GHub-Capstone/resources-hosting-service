using Application.Interfaces;
using Domain.Entities.Attribute;
using Domain.Entities.User;

namespace Application.Services.UserServices;

public class UserServices : IUserServices
{
    public readonly IGenericRepository<Client> _clientRepo;
    public readonly IGenericRepository<UserEntity> _userRepo;

    public UserServices(IGenericRepository<Client> clientRepo, IGenericRepository<UserEntity> userRepo)
    {
        _clientRepo = clientRepo;
        _userRepo = userRepo;
    }
    //Clients
    public async Task<ICollection<Client>> GetClients()
    {
        var cli = await _clientRepo.ListAsync();
        return cli;
    }
    public async Task<Client> GetClient(Guid ClientId)
    {
        var client = await _clientRepo.FindByIdAsync(ClientId);
        if (client == null)
        {
            throw new Exception($"Client not exist");
        }
        else
        {
            return client;
        }
    }
    public async Task<int> CountClient()
    {
        var cli = await _clientRepo.ListAsync();
        return cli.Count;
    }
    public async Task CreateClient(Client client) { }
    public async Task UpdateClient(Guid ClientId, Client client) { }
    public async Task DeleteClient(Guid ClientId) { }
    //Client x UserEntity

    //Users
    public async Task<ICollection<UserEntity>> GetUsers()
    {
        var user = await _userRepo.ListAsync();
        return user;
    }
    public async Task<UserEntity> GetUser(Guid UserId)
    {
        var user = await _userRepo.FindByIdAsync(UserId);
        if (user == null)
        {
            throw new Exception($"User not exist");
        }
        else
        {
            return user;
        }
    }
    public async Task<int> CountUsers()
    {
        var user = await _userRepo.ListAsync();
        return user.Count;
    }
    public async Task CreateUser(UserEntity user) { }
    public async Task UpdateUser(Guid UserId, UserEntity user) { }
    public async Task DeleteUser(Guid UserId) { }
}
