using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{

    public class FoodAPI
    {

        public class Rootobject
        {
            public string Text { get; set; }
            public Parsed[] Parsed { get; set; }
            public Hint[] Hints { get; set; }
            public _Links _links { get; set; }
        }

        public class Next
        {
            public string Title { get; set; }
            public string Href { get; set; }
        }

        public class Parsed
        {
            public Food Food { get; set; }
        }

        public class Food
        {
            public string FoodId { get; set; }
            public string Label { get; set; }
            public Nutrients Nutrients { get; set; }
            public string Category { get; set; }
            public string CategoryLabel { get; set; }
        }

        public class Nutrients
        {
            public float ENERC_KCAL { get; set; }
            public float PROCNT { get; set; }
            public float FAT { get; set; }
            public float CHOCDF { get; set; }
            public float FIBTG { get; set; }
        }

        public class Hint
        {
            public Food1 Food { get; set; }
            public Measure[] Measures { get; set; }
        }

        public class Food1
        {
            public string FoodId { get; set; }
            public string Label { get; set; }
            public Nutrients1 Nutrients { get; set; }
            public string Category { get; set; }
            public string CategoryLabel { get; set; }
            public string FoodContentsLabel { get; set; }
            public string Brand { get; set; }
            public Servingsize[] ServingSizes { get; set; }
            public string Image { get; set; }
            public float ServingsPerContainer { get; set; }
        }

        public class Nutrients1
        {
            public float ENERC_KCAL { get; set; }
            public float PROCNT { get; set; }
            public float FAT { get; set; }
            public float CHOCDF { get; set; }
            public float FIBTG { get; set; }
        }

        public class Servingsize
        {
            public string Uri { get; set; }
            public string Label { get; set; }
            public float Quantity { get; set; }
        }

        public class Measure
        {
            public string Uri { get; set; }
            public string Label { get; set; }
        }
        public class _Links
        {
            public Next Next { get; set; }
        }
    }
}
