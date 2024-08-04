using TestGPBA.Model;

namespace TestGPBA
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Offers.Any())
            {
                return; 
            }

            var suppliers = new Supplier[]
            {
                new Supplier { Name = "Supplier1", CreationDate = DateTime.Now },
                new Supplier { Name = "Supplier2", CreationDate = DateTime.Now },
                new Supplier { Name = "Supplier3", CreationDate = DateTime.Now },
                new Supplier { Name = "Supplier4", CreationDate = DateTime.Now },
                new Supplier { Name = "Supplier5", CreationDate = DateTime.Now }
            };

            foreach (var s in suppliers)
            {
                context.Suppliers.Add(s);
            }

            context.SaveChanges();

            var offers = new Offer[]
            {
                new Offer { Brand = "Brand1", Model = "Model1", SupplierId = suppliers[0].Id, RegistrationDate = DateTime.Now },
                new Offer { Brand = "Brand2", Model = "Model2", SupplierId = suppliers[1].Id, RegistrationDate = DateTime.Now },
                new Offer { Brand = "Brand3", Model = "Model3", SupplierId = suppliers[2].Id, RegistrationDate = DateTime.Now },
                new Offer { Brand = "Brand4", Model = "Model4", SupplierId = suppliers[3].Id, RegistrationDate = DateTime.Now },
                new Offer { Brand = "Brand5", Model = "Model5", SupplierId = suppliers[4].Id, RegistrationDate = DateTime.Now },

            };

            foreach (var o in offers)
            {
                context.Offers.Add(o);
            }

            context.SaveChanges();
        }
    }
}
