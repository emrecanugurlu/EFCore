using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

ETradeDbContext context = new();

//Product product1 = new()
//{
//    Name = "12345",
//    Description = "qwert"
//};

//await context.Products.AddAsync(product1);
//await context.SaveChangesAsync();

#region En Temel Basit Bir Sorgulama Nasıl Yapılır
#region Method Syntax
//List<Product> products =  await context.Products.ToListAsync();
#endregion
#region Query Syntax
//List<Product> products = await (from product in context.Products select product).ToListAsync();
#endregion
#endregion

#region IQueryable ve IEnumerable Nedir? 

//var product = await (from p in context.Products select p).ToListAsync();
#region IQueryable
//Sorguya karşılık gelir
//EFCore üzerinden yapılmış olan sorgunun execute edilmemiş halini ifade eder.
#endregion
#region IEnumerable
//Sorgunun çalıştırılıp/execute edilip, in memorye yüklenmiş halini ifade eder.
#endregion
#endregion

#region Sorguyu execute etmek için ne yapmamız gerekmektedir
#region ToListAsync
//Sorguyu execute etmek için kullanılan en basit yöntemlerden biridir.
//var products = await context.Products.ToListAsync();
#endregion
#region Deferred Execution Nedir?
//Deferred execution, bir sorgunun, sorgunun yapılandırıldığı anda değil, sonuçlarının ihtiyaç duyulduğu anda çalıştırılması anlamına gelir. Yani sorgu oluşturulur ve ihtiyaç duyulduğu anda execute edilir.

var productId = 1;


var products = from p in context.Products
               where p.Id > productId
               select p;

productId = 5;

foreach (var p in products)
{
    Console.WriteLine(p.Name);
}

//Burada deferred execution çalışma mantığından dolayı productId değerini 5 olarak kabul edilecektir.


#endregion
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