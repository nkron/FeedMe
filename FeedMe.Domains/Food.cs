using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{
    [Serializable]
    [Table("Foods")]
    public class Food
    {
        public Food()
        {
        }

        public Food(FoodForApi f)
        {
            FoodID = 0;
            APIFoodID = f.foodId;
            string s = f.label;
            FoodName = s;
            MacC = Convert.ToInt32(f.nutrients.CHOCDF);
            MacP = Convert.ToInt32(f.nutrients.PROCNT);
            MacF = Convert.ToInt32(f.nutrients.FAT);
            Cals = Convert.ToInt32(f.nutrients.ENERC_KCAL);
            ImageURL = f.image;
            CreatorID = 10;
        }

        [Column("FoodID")]
        public int FoodID { get; set; }
        [Column("ImageURL")]
        public string ImageURL { get; set; }
        public string APIFoodID { get; set; }

        [Column("FoodName")]
        public string FoodName { get; set; }
        [Column("FoodDesc")]
        public string FoodDesc { get; set; }

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

        [Column("CreatorID")]
        public int CreatorID { get; set; }
    }
}
