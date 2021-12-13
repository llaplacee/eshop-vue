using System;
using Core.DTO.User;
using DataLayer.Entities;

namespace Core.Contracts
{
    public interface IUserService : IDisposable
    {
        GetUsersByFilter GetAllUsers(GetUsersByFilter filter);
        User GetUserById(int userId);
        void InsertUser(User user);
        bool IsExistUserByUserName(string userName);
        bool IsExistUserByEmail(string email);
        User GetUserByEmail(string email);
        void AddUser(User user);
    }
}