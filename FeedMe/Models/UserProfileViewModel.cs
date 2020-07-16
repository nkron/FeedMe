using System;
using System.ComponentModel.DataAnnotations;
using FeedMe.Services;
using FeedMe.Domains;

namespace FeedMe.Models
{
    public class UserProfileViewModel
    {
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }
        [Display(Name = "Username:")]
        public string Username { get; set; }
        [Range(0, 10000, ErrorMessage = "Value for Calories must be between 0 and 10000")]
        [Display(Name = "Calories:")]
        public int TargetCals { get; set; }

        [Range(0, 100, ErrorMessage = "Percentage value for Carbs must be between 0 and 100")]
        [Display(Name = "Carbs:")]
        public int TargetMacC { get; set; }
        [Range(0, 100, ErrorMessage = "Percentage value for Fat must be between 0 and 100")]
        [Display(Name = "Fat:")]
        public int TargetMacF { get; set; }
        [Range(0, 100, ErrorMessage = "Percentage value for Protein must be between 0 and 100")]
        [Display(Name = "Protein:")]
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