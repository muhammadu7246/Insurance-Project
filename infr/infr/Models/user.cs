using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace infr.Models
{
    public class user
    {
        public int Id { get; set; }  // Auto-incremented in SQL Server
        public string Name { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}