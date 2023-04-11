using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ETradeDbContext context = new();

#region Veri Ekleme


#region context.AddAsync Fonksiyonu

Product product = new()
{
    Description = "Test",
    Name = "Ayakkabı",
};
await context.AddAsync(product);
#endregion

#region context.DbSet.AddAsync Fonksiyonu
await context.Products.AddAsync(product);
#endregion

#region Birden Fazla Veri Eklerken Nelere Dikkat Etmeliyiz?

Product product1 = new Product()
{
    Description = "Deneme 1",
    Name = "Ürün 1",
};
Product product2 = new Product()
{
    Description = "Deneme 2",
    Name = "Ürün 2",
};
Product product3 = new Product()
{
    Description = "Deneme 3",
    Name = "Ürün 3",
};

await context.Products.AddAsync(product1);
await context.Products.AddAsync(product2);
await context.Products.AddAsync(product3);
await context.SaveChangesAsync();
//await context.SaveChangesAsync();

#endregion

#region AddRange Fonksiyonu

Product product5 = new()
{
    Description = "Bu bir deneme verisidir...",
    Name = "AddRange Ürünü 1"
};
Product product6 = new()
{
    Description = "Bu bir deneme verisidir...",
    Name = "AddRange Ürünü 2"
};
Product product7 = new()
{
    Description = "Bu bir deneme verisidir...",
    Name = "AddRange Ürünü 3"
};
Product product8 = new()
{
    Description = "Bu bir deneme verisidir...",
    Name = "AddRange Ürünü 4"
};

await context.AddRangeAsync(product5, product6, product7,product8);
await context.SaveChangesAsync();
#endregion


#endregion


public class ETradeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=1q2w3e+!;TrustServerCertificate=True;Encrypt=False;");
        }
        public DbSet<Product> Products { get; set; }
    }

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}