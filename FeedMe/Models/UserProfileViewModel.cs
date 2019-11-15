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
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        public UserProfileViewModel()
        {            
        }
    }
}