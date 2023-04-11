using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");
ETradeDbContext context = new();
#region Change Tracking Nedir?
//"Change Tracking", bir nesnenin durumunun takip edilmesini ve nesne üzerinde yapılan değişikliklerin otomatik olarak algılanmasını ifade eder. Change Tracking, bir veritabanı işlemi gerçekleştirildiğinde veya bir nesne üzerinde yapılan değişiklikleri kaydetmek için SaveChanges() yöntemi çağrıldığında, bir değişiklik listesi oluşturur ve bu değişiklikleri veritabanına uygular.
#endregion
#region ChangeTracker Property'si
//Takip edilen nesnelere ulaşabilmemizi ve gerektiği takdirde işlemler gerçekleştirmemizi sağlayan bir property'dir.
//Context sınıfının base class'ı olan DbContext'in bir member'ıdır.

//List<Product> products = await context.Products.ToListAsync();

//products[1].Name = "Tenis Raketi";
//context.Products.Remove(products[2]);
//var datas = context.ChangeTracker.Entries();
#endregion
#region DetectChanges Metodu
//DetectChanges() metodu, Entity Framework Core'un Change Tracker'ının durumunu izleyerek varlık durumlarını günceller ve bu sayede veritabanındaki kayıtların doğru şekilde güncellenmesini sağlar.
#endregion
#region AutoDetectChangesEnabled Metodu 
//AutoDetectChangesEnabled özelliği, Entity Framework Core'un değişiklikleri otomatik olarak algılayıp algılamamasını belirleyen bir özelliktir ve performansı artırmak için kullanılır.

//context.ChangeTracker.AutoDetectChangesEnabled = false;
#endregion
#region Entries Metodu
//Entries() metodu, bir DbContext nesnesinin Change Tracker'ındaki tüm varlık öğelerini döndüren bir yöntemdir ve ilgili varlıkların durumunu, özelliklerini ve ilişkilerini içerir. Bu metot, değiştirilmiş veya eklenmiş olan varlıkların incelemesi veya üzerlerinde işlem yapmak için kullanılabilir.

//var products = await context.Products.ToListAsync();
//products.FirstOrDefault(p => p.Id == 12)!.Name = "Masa Tenisi Topu";

//var entries = context.ChangeTracker.Entries();
//foreach (var entry in entries)
//{
//    Console.WriteLine(entry.State.ToString());
//}
#endregion
#region AcceptAllChanges
//AcceptAllChanges yöntemi, bir varlık öğesinin değiştirilmiş durumunu geri alır ve Unchanged durumuna getirir. Böylece, değiştirilmiş bir varlık öğesi üzerinde yapılan değişiklikler kaydedilmeden önce geri alınabilir. Bu yöntem, bir varlık öğesi üzerindeki değişiklikleri geri almak için kullanılırken, RejectChanges yöntemi de tüm varlık öğelerinin değişikliklerini geri almak için kullanılabilir.
//context.ChangeTracker.AcceptAllChanges();
#endregion
#region HasChanges
    //HasChanges yöntemi, değiştirilmiş varlık öğelerinin var olup olmadığını belirlemek için kullanılır. Bu yöntem, değiştirilmiş varlık öğeleri bulunduğunda, bu değişiklikleri kaydetmek için SaveChanges yöntemi çağrılabilir.
    //var result = context.ChangeTracker.HasChanges();
#endregion

Console.WriteLine();
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