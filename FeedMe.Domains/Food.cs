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
        public Food(FoodForApi f)
        {
            FoodID = 0;
            APIFoodID = f.foodId;
            FoodName = f.label;
            Brand = f.brand;
            MacC = Convert.ToInt32(f.nutrients.CHOCDF);
            MacP = Convert.ToInt32(f.nutrients.PROCNT);
            MacF = Convert.ToInt32(f.nutrients.FAT);
            Cals = Convert.ToInt32(f.nutrients.ENERC_KCAL);
            ImageURL = f.image;
            CreatorID = 10;
        }
        public Food(APIFoodNutrients foodsIn)
        {
            APIFoodID = foodsIn.ingredients[0].parsed[0].foodId;
            FoodName = foodsIn.ingredients[0].parsed[0].food;
            MacC = Convert.ToInt32(foodsIn.totalNutrients.CHOCDF);
            MacP = Convert.ToInt32(foodsIn.totalNutrients.PROCNT);
            MacF = Convert.ToInt32(foodsIn.totalNutrients.FAT);
            Cals = Convert.ToInt32(foodsIn.totalNutrients.ENERC_KCAL);
            CreatorID = 10;
        }

        public Food()
        { }

        //For future food details implementation
        //public Food(APIFoodNutrients f)
        //{
        //    FoodID = 0;
        //    APIFoodID = f.ingredients[0].parsed[0].foodId;
        //    FoodName = f.ingredients[0].parsed[0].food;
        //    MacC = Convert.ToInt32(f.totalNutrients.CHOCDF.quantity);
        //    MacP = Convert.ToInt32(f.totalNutrients.PROCNT.quantity);
        //    MacF = Convert.ToInt32(f.totalNutrients.FAT.quantity);
        //    Cals = Convert.ToInt32(f.totalNutrients.ENERC_KCAL.quantity);
        //    CreatorID = 10;
        //}

        [Column("FoodID")]
        public int FoodID { get; set; }
        [Column("ImageURL")]
        public string ImageURL { get; set; }
        [Column("APIFoodID")]
        public string APIFoodID { get; set; }

        [Column("FoodName")]
        public string FoodName { get; set; }
        [Column("Brand")]
        public string Brand { get; set; }
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
