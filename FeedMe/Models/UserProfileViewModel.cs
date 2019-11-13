using System;
using System.ComponentModel.DataAnnotations;
using FeedMe.Services;
using FeedMe.Domains;

namespace FeedMe.Models
{
    public class UserProfileViewModel
    {
        UserService _userService;
        User user;

        [Display(Name = "First Name")]
        public string FirstName{get; set;}

        public UserProfileViewModel()
        {            
        }
        
        public UserProfileViewModel(UserService userService)
        {
            _userService = userService;
            user = _userService.getByID(1);
            FirstName = user.FirstName;
        }

    }
}