using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedMe.Domains
{
    public class CatFact
    {
        public bool used { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public bool deleted { get; set; }
        public string _id { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
        public Status status { get; set; }
    }
    public class Status
    {
        public bool verified { get; set; }
        public int sentCount { get; set; }
    }



}
