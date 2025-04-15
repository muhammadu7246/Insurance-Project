using System;
namespace infr.Models  // Update namespace as per your project structure
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int HolderId { get; set; }
        public int UserId { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }
        public string Status { get; set; }
    }
}