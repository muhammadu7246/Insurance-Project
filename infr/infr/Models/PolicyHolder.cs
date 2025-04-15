using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    namespace infr.Models
    {
        public class PolicyHolder
        {
            public int HolderId { get; set; }
            public int UserId { get; set; }
            public int PolicyId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal PremiumAmount { get; set; }
        }
    }
