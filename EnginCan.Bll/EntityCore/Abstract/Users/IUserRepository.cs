using System;
using System.Collections.Generic;
using System.Linq;
using EnginCan.Core.Utilities.Results.Abstract;
using EnginCan.Dal.EfCore.Abstract;
using EnginCan.Dto.Shared;
using EnginCan.Dto.Systems;
using EnginCan.Dto.Users;
using EnginCan.Entity.Models.Users;

namespace EnginCan.Bll.EntityCore.Abstract.Users
{
    public interface IUserRepository : IEntityBaseRepository<Entity.Models.Users.User>
    {
        string BuildToken(Token userToken);

        string IndefiniteBuildToken(Token userToken);

        string GenerateSystemUserToken();

        string PasswordHash(string password);

        string GetHostName(string IpAdress);

        IDataResult<IQueryable<User>> GetAllUser();

        IDataResult<User> GetById(int id);

        IDataResult<LoginUser> LoginUser();

        IResult AddUser(User user);

        IResult UpdateUser(User user);

        IResult DeleteUser(int id);

        IDataResult<ResponseLogin> Authenticate(Login login);
        IDataResult<IQueryable<UserSelectionDto>> GetAllUsersForSelection();

    }
}