using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");
ETradeDbContext context = new();

#region AsNoTracking Nedir?
    //AsNoTracking, Entity Framework'te bir sorgu sonucundaki varlıkların (entities) takibini yapmamak için kullanılan bir yöntemdir. Bu yöntem, bir sorgu sonucundaki varlıkların değişikliklerini takip etmek ve performansı artırmak için kullanılır.

    //Varsayılan olarak, Entity Framework, bir sorgu sonucundaki varlıkların takibini yapar ve değişiklikleri izler. Ancak, bazı durumlarda, bir sorgu sonucundaki varlıkların takibi gereksiz olabilir. Örneğin, bir sorgu sonucu, veri tabanındaki verilerin bir görüntüsü olarak alınmak istenebilir. Bu durumda, AsNoTracking yöntemi kullanılabilir.

    //AsNoTracking yöntemi, bir sorgu sonucundaki varlıkların takibini yapmaz ve bunları Entity Framework'ten bağımsız hale getirir. Bu yöntemi kullanarak, bir sorgu sonucu elde edilen varlıkların değiştirilmesi veya silinmesi gibi işlemler Entity Framework tarafından takip edilmez. Bunun yerine, değişikliklerin doğrudan veri tabanına yansıtılması gerekir.

    //AsNoTracking yöntemi, özellikle performansı artırmak için kullanışlıdır. Bir sorgu sonucundaki varlıkların takibi yapmak, bellek kullanımını artırır ve uygulamanın yavaşlamasına neden olabilir. AsNoTracking yöntemi kullanarak, sorgu sonucundaki varlıkların bellekte saklanması gerekmez ve performans artar.

    //Burada gelen tüm product verileri ChangeTracker mekanizması tarafından takip edilmez.
    var products = await context.Products.AsNoTracking().ToListAsync();
    foreach (var product in products)
    {
        Console.WriteLine(product.Name);
    }
#endregion
#region AsNoTrackingWithIdentiyResolution Nedir?
    //AsNoTrackingWithIdentityResolution, Entity Framework'te bir sorgu sonucundaki varlıkların takibini yapmayıp, ancak aynı veri kaynağından gelen kayıtları aynı nesne örneğiyle eşleştirerek Identity Resolution (Kimlik Çözümleme) yapmayı sağlayan bir yöntemdir.

    //AsNoTrackingWithIdentityResolution yöntemi, AsNoTracking yöntemine benzer şekilde çalışır, ancak Entity Framework, sorgu sonucundaki varlıkları bellekte saklar ve bir varlık değiştirildiğinde, bu değişikliklerin diğer varlıklara da yansımasını sağlar.

    //Örneğin, bir veri tabanı sorgusu sonucu, farklı kaynaklardan gelen verileri içerebilir. Bu durumda, aynı kaynaklardan gelen verilerin farklı nesne örnekleriyle temsil edilmesi, verilerin doğru şekilde işlenmesini zorlaştırabilir. AsNoTrackingWithIdentityResolution yöntemi, aynı veri kaynağından gelen kayıtları aynı nesne örneğiyle eşleştirerek, verilerin daha tutarlı ve doğru şekilde işlenmesini sağlar.

    var products1 = await context.Products.AsNoTrackingWithIdentityResolution().ToListAsync();
#endregion
#region AsTracking
    //AsTracking yöntemi, bir sorgu sonucundaki varlıkların takibini yapar ve bu varlıklar üzerinde yapılacak değişiklikleri takip etmeye hazır hale getirir. Bu yöntemi kullanarak, bir sorgu sonucu elde edilen varlıkların değiştirilmesi veya silinmesi gibi işlemler Entity Framework tarafından takip edilir ve değişikliklerin veri tabanına yansıtılması sağlanır.
    var products2 = await context.Products.AsTracking().ToListAsync();
#endregion
#region UseQueryTrackingBehovier
//UseQueryTrackingBehavior, Entity Framework Core'da bir sorgunun takibini yapıp yapmayacağını ayarlamak için kullanılan bir yöntemdir. Bu yöntem, bir DbContext nesnesi için belirli bir varsayılan sorgu takip davranışı belirlemek için kullanılır.

//Varsayılan olarak, Entity Framework Core, bir sorgu sonucundaki varlıkları takip eder ve değişiklikleri izler. Ancak, bazı durumlarda, performansı artırmak için bir sorgunun takibi yapılmamalıdır. Bu durumda, UseQueryTrackingBehavior yöntemi kullanılabilir.

//Bu yöntem, DbContext nesnesi için bir varsayılan sorgu takip davranışı belirler. Bu davranış, Entity Framework Core tarafından varsayılan olarak kullanılacaktır. Bu davranış, AsTracking veya AsNoTracking yöntemleri ile bir sorguda değiştirilebilir.

//UseQueryTrackingBehavior yöntemi, aşağıdaki parametrelerle birlikte kullanılabilir:

//NoTracking: Bir sorgu sonucunda elde edilen varlıkların takibinin yapılmamasını belirtir. Bu seçenek, sorgu performansını artırır.
//TrackAll: Bir sorgu sonucunda elde edilen varlıkların takibinin yapılmasını belirtir. Bu seçenek, değişiklikleri izlemek istediğiniz zaman kullanışlıdır.
//TrackAllWithIdentityResolution: Bir sorgu sonucunda elde edilen varlıkların takibinin yapılmasını belirtir ve aynı kimlik özniteliklerine sahip olan varlıkları birleştirir.

context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
#endregion
Console.WriteLine();
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