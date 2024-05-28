namespace grad_proV1.Models
{
    public class BookingItem
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public string EnrollmentCode { get; set; }
    }
}
