namespace TestGPBA.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int SupplierId { get; set; }
        public  Supplier Supplier { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

}
