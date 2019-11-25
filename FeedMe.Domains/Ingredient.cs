using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{
    [Serializable]
    [Table("Ingredients")]
    public class Ingredient
    {
        [Column("IngID")]
        public int IngID { get; set; }

        [Column("IngName")]
        public string IngName { get; set; }

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
    }
}
