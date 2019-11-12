using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{
    [Serializable]
    [Table("Users")]
    public class Users
    {
        [Column("UserID")]
        public int Id { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("FirstName")]
        public string Fname { get; set; }

        [Column("LastName")]
        public string LName { get; set; }

        [Column("Email")]
        public string Email { get; set; }
    }
}
