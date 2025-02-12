using System;
using System.ComponentModel.DataAnnotations;

namespace Ai_Inside.Models
{
    public class License
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } // FK to Identity User

        [Required]
        public string Plan { get; set; } // "Standard" or "Premium"

        [Required]
        public int Years { get; set; } // Number of years purchased

        [Required]
        public int TotalPrice { get; set; } // Final amount paid

        [Required]
        public string LicenseKey { get; set; } // Unique license key

        [Required]
        public DateTime ExpiryDate { get; set; } // Expiry based on purchase date

        public bool IsActive { get; set; } = true; // License status

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow; // Date of purchase
    }
}