using grad_proV1.Models;

namespace grad_proV1.Dtos
{
    public class BookingItemDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public string EnrollmentCode { get; set; }
    }
}
