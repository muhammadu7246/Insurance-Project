using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace infr.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyType { get; set; } // Life, Medical, Motor, Home
        public string Description { get; set; }
        public decimal PremiumAmount { get; set; }
        public decimal CoverageAmount { get; set; }
        public int DurationInYears { get; set; }
    }
}