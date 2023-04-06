using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("Veri Silme...");

ETradeDbContext context = new();

#region Veri Silme İşlemi Nasıl Gerçekleştirilir

//Product product = await context.Products.FirstOrDefaultAsync(u=>u.Id == 1);
//context.Products.Remove(product);
//await context.SaveChangesAsync();

#endregion


#region EntityState ile veri silme işlemi

//Product prdct = new() { Id = 7 };
//context.Entry(prdct).State = EntityState.Deleted;
//await context.SaveChangesAsync();

#endregion

#region RemoveRange Fonksiyonu
List<Product> products = await context.Products.Where(p => p.Id >= 4).ToListAsync();
context.Products.RemoveRange(products);
await context.SaveChangesAsync();
#endregion

public class ETradeDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=ECURi13974;TrustServerCertificate=True;Encrypt=False;");
    }
    public DbSet<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

