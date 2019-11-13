using System;
using FeedMe.Repositories;
using FeedMe.Domains;
using System.Collections.Generic;

namespace FeedMe.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public IEnumerable<User> getAll()
        {
            return _userRepo.GetUsers();
        }

        public User getByID(int ID)
        {
            return _userRepo.GetByID(ID);
        }
    }
}
