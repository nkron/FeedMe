using System;
using System.ComponentModel.DataAnnotations;

namespace FeedMe.Models
{
    public class RegisterViewModel
    {
                
        [Required]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        //todo:Add regex to check for alphanumeric + special. Otherwise fails without error message.
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 3)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}