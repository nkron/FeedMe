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
        [Display(Name = "Calories:")]
        public int TargetCals { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        [Display(Name = "Carbs:")]
        public int TargetMacC { get; set; }
        [Display(Name = "Fat:")]
        public int TargetMacF { get; set; }
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