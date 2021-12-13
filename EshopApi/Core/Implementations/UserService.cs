using System;
using System.Linq;
using Core.Contracts;
using Core.DTO.Paging;
using Core.DTO.User;
using Core.Extensions;
using DataLayer.ApplicationContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Implementations
{
    public class UserService : IUserService
    {
        private readonly EshopContext context;

        public UserService(EshopContext context)
        {
            this.context = context;
        }

        public GetUsersByFilter GetAllUsers(GetUsersByFilter filter)
        {
            var query = context.Users.Include(s => s.Orders).AsQueryable().SetUserFilters(filter);

            var count = (int)Math.Ceiling(query.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var users = query.OrderByDescending(u => u.UserId)
                .Pagging(pager)
                .AsNoTracking().ToList();

            return filter.SetUsers(users).SetPaging(pager);
        }

        public User GetUserById(int userId)
        {
            return context.Users.SingleOrDefault(s => s.UserId == userId);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool IsExistUserByUserName(string userName)
        {
            return context.Users.Any(s => s.Name == userName);
        }

        public bool IsExistUserByEmail(string email)
        {
            return context.Users.Any(s => s.Email == email);
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.SingleOrDefault(s => s.Email == email);
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}