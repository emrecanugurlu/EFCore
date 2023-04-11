using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("Veri Güncelleme...");

ETradeDbContext context = new();

#region Veri Nasıl Güncellenir...

Product product = await context.Products.FirstOrDefaultAsync(p => p.Id == 8);
product.Name = "Güncellenen Ürün...";
await context.SaveChangesAsync();
#endregion

#region ChangeTracker Mekanizması Tarafından Takip Edilmeyen Nesneler Nasıl Güncellenir

Product product1 = new()
{
    Id = 5,
    Name = "Güncellenen Ürün İsmi...",
    Description = "Denemeye Devam Ediyorum..."
};

#region Update Fornksiyonu
//ChangeTracker mekanizması tarafından takip edilmeyen nesneler için Update() fonksiyonu kullanılır.
//Update() fonksiyonunu kullanabilmek için ilgili nesnede kesinlikle id değeri olmalıdır.
context.Products.Update(product1);
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
