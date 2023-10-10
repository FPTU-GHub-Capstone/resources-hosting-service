using DomainLayer.Entities;
using DomainLayer.Exceptions;
using RepositoryLayer.Repositories;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ServiceLayer.Business;

public class UserServices : IUserServices
{
    public readonly IGenericRepository<UserEntity> _userRepo;

    public UserServices(IGenericRepository<UserEntity> userRepo)
    {
        _userRepo = userRepo;
    }
    public async Task<ICollection<UserEntity>> List()
    {
        return await _userRepo.ListAsync();
    }
    public async Task<UserEntity> GetById(Guid UserId)
    {
        return await _userRepo.FindByIdAsync(UserId);
    }
    public async Task<int> Count()
    {
        return await _userRepo.CountAsync();
    }
    public async Task Create(UserEntity user) {
        var userList = List();
        foreach(var User in userList.Result)
        {
            if(User.Email == user.Email)
            {
                throw new BadRequestException("Email exists");
            }
        }
        if(Validation(user) == false)
        {
            throw new BadRequestException("Email/phone not in correct format");
        }
        await _userRepo.CreateAsync(user);
    }
    public async Task Update(Guid UserId, UserEntity user) {
        var target = await GetById(UserId);
        if(target is null)
        {
            throw new NotFoundException("User not exist");
        }
        if (Validation(user) == false)
        {
            throw new BadRequestException("Email/phone not in correct format");
        }
        await _userRepo.UpdateAsync(target);
    }
    public async Task Delete(Guid UserId) {
        var target = await _userRepo.DeleteSoftAsync(UserId);
        if(target is null)
        {
            throw new NotFoundException("User not exist");
        }
    }
    public bool Validation(UserEntity user)
    {
        var valid = true;
        try
        {
            var emailAddress = new MailAddress(user.Email);
            var phonePattern = @"^(03|05|07|08|09)\d{8}$";
            if(!(!string.IsNullOrWhiteSpace(user.Phone) && Regex.IsMatch(user.Phone, phonePattern)))
            {
                valid = false;
            }
        } catch
        {
            valid = false;
        }
        return valid;
    }
}
