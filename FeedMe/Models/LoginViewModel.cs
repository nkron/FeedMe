using System;
using System.ComponentModel.DataAnnotations;
using FeedMe.Domains;

namespace FeedMe.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        public string Email { get; set; }
    }
}