using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FeedMe.Domains.Enumerations;

namespace FeedMe.Domains
{
    [Serializable]
    [Table("Meals")]
    public class Meal
    {
        [Column("MealID")]
        public int MealID { get; set; }

        [Column("MealName")]
        public string MealName { get; set; }

        [Column("MacC")]
        public int MacC { get; set; }

        [Column("MacP")]
        public int MacP { get; set; }

        [Column("MacF")]
        public int MacF { get; set; }

        [Column("Cals")]
        public int Cals { get; set; }

        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("MealTypeID")]
        public MealTypes MealTypeID { get; set; }
    }
}
