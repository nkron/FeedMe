using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{
    public class APIFoods
    {
        public string text { get; set; }
        public Hint[] hints { get; set; }
        public _Links _links { get; set; }
    }

    public class _Links
    {
        public Next next { get; set; }
    }

    public class Next
    {
        public string title { get; set; }
        public string href { get; set; }
    }

    public class Hint
    {
        public FoodForApi food { get; set; }
        public Measure[] measures { get; set; }
    }

    public class FoodForApi
    {
        public string foodId { get; set; }
        public string label { get; set; }
        public Nutrients nutrients { get; set; }
        public string category { get; set; }
        public string categoryLabel { get; set; }
        public string image { get; set; }
        public string foodContentsLabel { get; set; }
        public string brand { get; set; }
        public Servingsize[] servingSizes { get; set; }
        public float servingsPerContainer { get; set; }
    }

    public class Nutrients
    {
        public float ENERC_KCAL { get; set; }
        public float PROCNT { get; set; }
        public float FAT { get; set; }
        public float CHOCDF { get; set; }
        public float FIBTG { get; set; }
    }

    public class Servingsize
    {
        public string uri { get; set; }
        public string label { get; set; }
        public float quantity { get; set; }
    }

    public class Measure
    {
        public string uri { get; set; }
        public string label { get; set; }
        public Qualified[] qualified { get; set; }
    }

    public class Qualified
    {
        public Qualifier[] qualifiers { get; set; }
    }

    public class Qualifier
    {
        public string uri { get; set; }
        public string label { get; set; }
    }


}
