namespace grad_proV1.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }

     
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
          public string Description { get; set; }
    
        public string Address { get; set; }

        public Service service  { get; set; }
        public int ServiceId { get; set; }
    }
}
