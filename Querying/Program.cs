using Azure.Core;
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

//var productId = 1;


//var products = from p in context.Products
//               where p.Id > productId
//               select p;

//productId = 5;

//foreach (var p in products)
//{
//    Console.WriteLine(p.Name);
//}

//Burada deferred execution çalışma mantığından dolayı productId değerini 5 olarak kabul edilecektir.


#endregion
#endregion


#region Çogul Veri Getiren Sorgulama Fonksiyonları
#region ToListAsync
//Oluşturulan sorguyu execute etmemizi sağlayan fonksiyondur.
#region Method Syntax
//var urunler = await context.Products.ToListAsync();
//Console.WriteLine();
#endregion
#region Query Syntax
//var products = await (from p in context.Products
//               select p).ToListAsync();
#endregion
#endregion
#region Where
//WHERE ifadesi, belirli bir koşula uyan kayıtları seçmek için kullanılır.
#region Method Syntax
//var products = await context.Products.Where(p => p.Id>3).ToListAsync();
//Console.WriteLine();
#endregion
#region Query Syntax
//var products = await (from p in context.Products
//               where p.Id > 5 
//               select p).ToListAsync();
//Console.WriteLine();
#endregion
#endregion
#region OrderBy
//Sorgu sonucunu belirli bir sütuna göre sıralamak için kullanılır.
#region Method Syntax 
//var products = await context.Products.Where(p => p.Id >=6).OrderBy(p => p.Name).ToListAsync();
//Console.WriteLine();
#endregion
#region Query Syntax
//var products = from p in context.Products
//               where p.Id >= 6
//               orderby p.Name descending
//               select p;
#endregion
#endregion
#region ThenBy
// ThenBy metodu, ORDER BY ifadesinde sıralama yaparken birden fazla sıralama kriteri belirtmek için kullanılır.
#region Method Syntax
//var products = await context.Products.Where(p => p.Id >= 6).OrderBy(p => p.Name).ThenBy(p => p.Description).ToListAsync();
#endregion
#region Query Syntax
//var products  = await (from p in context.Products
//                      orderby p.Id,p.Name descending
//                      select p).ToListAsync();
#endregion
#endregion
#endregion
#region Tekil Veri Getiren Sorgulama Fonksiyonları
#region SingleAsync ve SingleOrDefault Fonksiyonları
//Single() metodu, sorgudan yalnızca bir sonuç döndürdüğünde kullanılır. Bu metot, bir koleksiyondan tek bir öğe seçmenize izin verir ve yalnızca bir öğe seçmeniz gerektiğinde kullanmanız gereken bir metottur. Eğer sorgunuz birden fazla sonuç döndürürse, Single() metodu InvalidOperationException fırlatır.

//SingleOrDefault() metodu, Single() metodu gibi yalnızca bir öğe döndürmeyi bekler, ancak farkı, sorgudan hiçbir sonuç döndürülmediği durumlarda varsayılan bir değer döndürür. SingleOrDefault() metodu, koleksiyonda yalnızca bir öğe bulunmasını beklediğiniz durumlarda kullanılabilir, ancak koleksiyonda hiç öğe bulunmaması durumunda null veya varsayılan bir değer döndürür.

//Bu nedenle, Single() metodu yalnızca bir sonuç döndüren sorgularda kullanılmalıdır, SingleOrDefault() metodu ise bir sonuç döndürmeyen durumları da ele almak için kullanılabilir.

//var product = await context.Products.SingleAsync(p => p.Id == 1);
//var product1 = await context.Products.SingleOrDefaultAsync(p => p.Id == 1);
//Console.WriteLine();
#endregion
#region FirstAsync ve FirstOrDefaultAsync Fonksiyonları

//First() metodu, sorgudan elde edilen sonuçlar listesindeki ilk öğeyi alır ve eğer liste boşsa bir InvalidOperationException fırlatır. Bu nedenle, First() metodunu kullanırken, liste boş olmadığından emin olmanız gerekir.

//FirstOrDefault() metodu ise, sorgudan elde edilen sonuçlar listesindeki ilk öğeyi alır ve liste boşsa varsayılan değeri (genellikle null) döndürür.Bu nedenle, FirstOrDefault() metodunu kullanırken, liste boş olabilir ve sonucun null olabileceğini kabul etmeniz gerekir.

//Özetle, First() metodu sorgudan elde edilen ilk öğeyi döndürürken, liste boşsa bir hata fırlatır. FirstOrDefault() metodu ise aynı işlevi yerine getirir ancak liste boşsa varsayılan değeri döndürür.

//var product = await context.Products.FirstAsync(p => p.Id == 12);
//var product1 = await context.Products.FirstOrDefaultAsync(p => p.Id == 12);
#endregion
#region FindAsync Fonksiyonu
//FindAsync() yöntemi, belirli bir türdeki varlıklar için birincil anahtar değeri kullanarak tek bir varlık nesnesi döndürür. Birincil anahtar değerine sahip bir varlık nesnesi yoksa null değer döndürür
//var product = await context.Products.FindAsync(2);
#endregion
#region LastAsync ve LastOrDefaultAsync Fonksiyonları
//LastAsync() yöntemi, belirli bir sıraya göre sıralanmış bir veri kümesinde son öğeyi döndürür. LastOrDefaultAsync() yöntemi ise, son öğeyi döndürürken, veri kümesinde son öğe yoksa varsayılan değeri döndürür
//Eğer veri kümesi sıralanmamışsa, LastAsync() ve LastOrDefaultAsync() yöntemleri kullanmadan önce veri kümesini sıralamak için OrderBy() veya OrderByDescending() yöntemlerini kullanmak gerekir.
//var product = await context.Products.OrderBy(p => p.Id).LastAsync(p => p.Id>4);
#endregion
#endregion
#region Diğer Sorgulama Fonksiyonları
#region CountAsync
//CountAsync() yöntemi, bir sorgunun sonucunda döndürülen öğe sayısını elde etmek için oldukça kullanışlıdır. Bu yöntem, performans açısından daha iyi bir seçenek sağlar çünkü yalnızca bir sayı döndürür ve veritabanından tüm öğeleri getirerek sayma işlemini gerçekleştirmez.
//var count = await context.Products.CountAsync();
#endregion
#region LongCountAsync
//CountAsync() yöntemine benzer şekilde çalışır, ancak geri döndürdüğü sonuç veri tipi "long" olduğu için, sayı çok büyük olabilecek veri kümelerinde kullanılır.
//var product = await context.Products.LongCountAsync();
#endregion
#region AnyAsync
//AnyAsync() yöntemi, bir sorgunun sonucunda elde edilebilecek belirli bir öğe veya koleksiyon var mı yok mu, gibi basit koşullar için kullanışlıdır. Bu yöntem, veritabanına ek bir yük getirmeden sorguyu yürütebilir.

//var anyData = await context.Products.AnyAsync(p => p.Id>32);
#endregion
#region MaxAsync
//Verilen kolondaki max veriyi getirir.
//var maxData = await context.Products.MaxAsync(p=>p.Id);
#endregion
#region MinAsync
//Verilen kolondaki min veriyi getirir.
//var minData = await context.Products.MinAsync(p => p.Id);
#endregion
#region Distinct
//Distinct() yöntemi, bir sorgudan dönen sonuçların benzersiz öğelerini elde etmek için kullanılır. Bu yöntem, sorgunun sonucunda dönen veri kümesini filtreleyerek, yalnızca benzersiz öğeleri döndürür.
//var products = await context.Products.Select(p => p.Name).Distinct().ToListAsync();
#endregion
#region AllAsync
    //AllAsync() yöntemi, bir koleksiyon içindeki tüm öğelerin belirli bir koşulu karşılayıp karşılamadığını kontrol eder ve sonucu bir boolean değeri olarak döndürür. Yani, bu yöntemle belirli bir koşulu sağlayan tüm öğelerin koleksiyonda bulunup bulunmadığı kontrol edilebilir.
    //var b = await context.Products.AllAsync(p => p.Id > 10);
#endregion
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