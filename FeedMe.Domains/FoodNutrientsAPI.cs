using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{

    public class FoodNutrientsAPI
    {

        public class Rootobject
        {
            public string uri { get; set; }
            public float yield { get; set; }
            public int calories { get; set; }
            public float totalWeight { get; set; }
            public object[] dietLabels { get; set; }
            public string[] healthLabels { get; set; }
            public string[] cautions { get; set; }
            public Totalnutrients totalNutrients { get; set; }
            public Totaldaily totalDaily { get; set; }
            public Ingredient[] ingredients { get; set; }
        }

        public class Totalnutrients
        {
            public ENERC_KCAL ENERC_KCAL { get; set; }
            public FAT FAT { get; set; }
            public CHOCDF CHOCDF { get; set; }
            public FIBTG FIBTG { get; set; }
            public SUGAR SUGAR { get; set; }
            public PROCNT PROCNT { get; set; }            
        }

        public class ENERC_KCAL
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class FAT
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class CHOCDF
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class FIBTG
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class SUGAR
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class PROCNT
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class Totaldaily
        {
            public ENERC_KCAL1 ENERC_KCAL { get; set; }
            public FAT1 FAT { get; set; }
            public FASAT1 FASAT { get; set; }
            public CHOCDF1 CHOCDF { get; set; }
            public FIBTG1 FIBTG { get; set; }
            public PROCNT1 PROCNT { get; set; }
        }

        public class ENERC_KCAL1
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class FAT1
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class FASAT1
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class CHOCDF1
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class FIBTG1
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class PROCNT1
        {
            public string label { get; set; }
            public float quantity { get; set; }
            public string unit { get; set; }
        }

        public class Parsed
        {
            public float quantity { get; set; }
            public string measure { get; set; }
            public string food { get; set; }
            public string foodId { get; set; }
            public string foodContentsLabel { get; set; }
            public float weight { get; set; }
            public float retainedWeight { get; set; }
            public string measureURI { get; set; }
            public string status { get; set; }
        }

    }
}
