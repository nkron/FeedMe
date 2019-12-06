using System;
using System.ComponentModel.DataAnnotations;
using FeedMe.Services;
using FeedMe.Domains;

namespace FeedMe.Models
{
    public class UserProfileViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }

        public int TargetCals { get; set; }

        public int TargetMacC { get; set; }

        public int TargetMacF { get; set; }

        public int TargetMacP { get; set; }

        public UserProfileViewModel(User u)
        {
            Username = u.Username;
            FirstName = u.FirstName;
            LastName = u.LastName;
            TargetCals = u.TargetCals;
            TargetMacC = u.TargetMacC;
            TargetMacF = u.TargetMacF;
            TargetMacP = u.TargetMacP;
        }
        public UserProfileViewModel()
        {

        }
    }
}