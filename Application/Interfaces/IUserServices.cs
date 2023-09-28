using Domain.Entities.User;

namespace Application.Interfaces; 
public interface IUserServices {
    //Clients
    Task<ICollection<Client>> GetClients();
    Task<Client> GetClient(Guid ClientId);
    Task<int> CountClient();
    Task CreateClient(Client client);
    Task UpdateClient(Guid ClientId, Client client);
    Task DeleteClient(Guid ClientId);
    //Client x UserEntity
    
    //Users
    Task<ICollection<UserEntity>> GetUsers();
    Task<UserEntity> GetUser(Guid UserId);
    Task<int> CountUsers();
    Task CreateUser(UserEntity user);
    Task UpdateUser(Guid UserId, UserEntity user);
    Task DeleteUser(Guid UserId);
}
