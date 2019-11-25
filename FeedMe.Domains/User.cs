using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{
    [Serializable]
    [Table("Users")]
    public class User
    {
        [Column("NormalizedEmail")]
        public string NormalizedEmail;

        [Column("UserID")]
        public int UserID { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("NormalizedUsername")]
        public string NormalizedUserName { get; set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Email")]
        public string Email { get; set; }
    }
}
