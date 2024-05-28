namespace grad_proV1.Models
{
    public class EnrollmentCode
    {
        public int Id { get; set; }

        //public string UserId { get; set; } // Foreign key referencing ApplicationUser
        //public ApplicationUser User { get; set; } // Navigation property for user details
        //public int ProviderId { get; set; } // Foreign key referencing Provider
        //public Provider Provider { get; set; } // Navigation property for provider details
        public string BookingCode { get; set; } // One-time code for booking
    }
}
