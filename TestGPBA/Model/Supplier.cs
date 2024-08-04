namespace TestGPBA.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }
}
