using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("One to one ilişki yapısı nasıl inşa edilir");

#region Default Convention
//Default Convention, EF Core tarafından varsayılan olarak kullanılan bir dizi kural kümesidir. Bu kurallar, bir sınıfın özelliklerinin veritabanındaki sütunlarla eşleştirilmesiyle ilgilidir. Örneğin, bir sınıfın "Id" özelliği, varsayılan olarak, veritabanındaki "Id" adında bir sütunla eşleştirilir. Benzer şekilde, bir sınıfın "FirstName" özelliği, varsayılan olarak, veritabanındaki "FirstName" adında bir sütunla eşleştirilir.

//Default Convention'ın kullanımı, aşağıdaki özelliklerin tanımlanmasına izin verir:

//Primary Key özelliği: Sınıfın "Id" veya "{ClassName}Id" olarak adlandırılan bir özelliği, varsayılan olarak, veritabanındaki birincil anahtar sütunu ile eşleştirilir.
//İlişkili özellikler: İlişkili sınıflar arasındaki ilişkileri tanımlayan özellikler, varsayılan olarak, veritabanındaki bir dış anahtar sütunuyla eşleştirilir.
//İsimlendirme: Özelliklerin isimleri, varsayılan olarak, veritabanındaki sütunların adlarıyla eşleştirilir.
//Default Convention, belirli bir proje için uygun olmayabilir. Bu durumda, geliştiriciler kendi kurallarını tanımlayabilir ve bu kurallar EF Core ile kullanılabilir hale getirilebilir. Bu, Fluent API kullanılarak yapılabilir.

//public class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set;}
//    public int AddreesId { get; set; }
//    public Addrees Addrees { get; set; }

//}

//public class Addrees
//{
//    public int Id { get; set; }
//    public string Country { get; set; }
//    public string City { get; set; }
//    public string Description { get; set; }
//    public Employee Employee { get; set; }
//}
#endregion
#region Data Annotations
//public class Employee
//{
//    [Key]
//    public int Id { get; set; }
//    public string Name { get; set; }
//    [ForeignKey(nameof(Addrees))]
//    public int FKey { get; set; }
//    public Addrees Addrees { get; set; }

//}

//public class Addrees
//{
//    public int Id { get; set; }
//    public string Country { get; set; }
//    public string City { get; set; }
//    public string Description { get; set; }
//    public Employee Employee { get; set; }
//}
#endregion
#region Fluent API

//Fluent API ile entityler arasında ilişki kurarken Context sınıfı içerisindeki OnModelCreating() metodu içerisinde çalışma gerçekleştirmemiz gerekmektedir. İlgili kodlar context sınıfı içerisinde bulunmaktadır.
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int FKey { get; set; }
    public Addrees Addrees { get; set; }

}

public class Addrees
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public Employee Employee { get; set; }
}

#endregion

public class ECompanyDbContext : DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server = localhost, 1433; Database = ECompanyDb; User Id = sa;Password = 1q2w3e+!;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<Employee>().HasKey(e => e.Id);
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Addrees)
            .WithOne(a => a.Employee)
            .HasForeignKey<Employee>(e=>e.FKey);
    }

    public DbSet<Employee> Employees{ get; set; }
    public DbSet<Addrees> Addreeses { get; set; }
}