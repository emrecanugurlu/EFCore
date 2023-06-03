
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Deneme");
#region EfCore'da neden yapılandırmalara ihtiyaç duyulur?
//Default davranışları yeri geldiğinde geçersiz kılmak ve özelleştirmek isteyebiliriz. Bundan dolayı yapılandırmalara ihtiyacımız olacaktır.
#endregion

#region OnModelCreating metodu
//Bu metod kullanılarak modellardaki temel konfigurasyonlar yapılandırılabilir.
#region GetEntityTypes
//EfCore'da kullanılan entityleri elde etmek, programatik olarak öğrenmek istiyorsak eğer GetEntityTypes fonksiyonunu kullanabiliriz.
#endregion
#endregion


#region Configuration | Data Annotations & Fluent API
#region Table - ToTable
//Generate edilecek tablonun ismini belirlememizi sağlayan yapılanmalardır.
//Normal şartlarda generate edilecek olan entity tablo ismini DbSet property'si üzerinden alır biz bunu özelleştirmek ister isek eğer bu yapılanmaları kullanmalıyız.
#endregion
#region Column - HasColumnName, HasColumnType, HasColumnOrder
//Ef Core'da tabloların isimleri entity sınıflarının içerisindeki propertylere denk gelmektedir.
//Default olarak propertylerin adı kolon adıyken, türleri veya tipleri propertylerin tipidir.
//Eğer ki genarate edilecek kolon isimlerine ve türlerine müdahele etmek istiyorsak bu konfigürasyon kullanılır.
#endregion
#region ForeignKey - HasForeignKey
//İlişkisel tablo tasarımlarında, bağımlı tabloda esas tabloya karşılık gelecek verilerin tutulduğu kolanu foreign key olarak temsil etmekteyiz.
//EF Core'da foreign key kolonu genellikle entity tanımlama kuralları gereği default yapılanmalarla oluşturulur.
//Data Annatations Attribute'unu direkt kullanabilirken. Fluent api ile yapılan foreign key tanımlamasında tablolar arasındaki ilişkiyi bildirmemiz gerekecektir.
#endregion
#region NotMapped - Ignore
//EF Core entity sınıfları içerisindeki tüm propertyleri default olarak modellenen tabloya kolon şeklinde migrate eder.
//Bazen bizler entity sınıfları içerisinde tabloda bir kolona karşılık gelmeyen propertyler tanımlamak mecburiyetinde kalabiliriz.
//Bu propertylerin ef core tarafından kolon olarak map edilmesini istemediğimizi bildirmek için NotMapped ya da Ignore kullanabiliriz.
#endregion
#region Key - HasKey
//EF Core'da default convention olarak bir entity'nin içerisinde Id, ID, EntityId, EntityID vs. şeklinde tanımlanan tüm propertylere varsayılan olarak primary key constraint uygulanır.
//Key ya da HasKey yapılanmalarıyla istediğimiz herhangi bir property'e default convention dışında primary key uygulayabilmekteyiz.
//EF Core da bir entity içerisinde kesinlikle PK'i temsil edecek olan property bulunmalıdır. Aksi takdirde EF Core migration oluştururken hata verecektir. Eğer ki tablonun PK'i yoksa bunun bildirilmesi gerekmektedir.
#endregion
#region Required - IsRequired
//Bir kolonun nullable veya not null olup olmamasını bu konfigürasyon ile belirleyebilmekteyiz.
#endregion
#region MaxLength |StringLength - HasMaxLength
//Bir property'nin maximum karakter sayısını belirtmemiz için kullanılır.
#endregion
#endregion

//[Table("Departman")]
class Department
{
    //[Key]
    public int Id { get; set; }
    //[Column("Deneme",TypeName ="ali",Order =2)]
    public string Name { get; set; }
    
    //[NotMapped]
    public int Deneme { get; set; }
    public ICollection<Person> People { get; set; }

}

class Person
{
    public int Id { get; set; }
    //[ForeignKey(nameof(Department))]
    public int DFK { get; set; }
    //[Required]
    public string Surname { get; set; }
    //[MaxLength(100)]
    //[StringLength(100)]
    public string Name { get; set; }
    public Department Department { get; set; }

}


class ApplicationDbContex : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server : localhost , 1433 ; User Id: sa; Password : 1Q2W3E4R+; Database : ApplicationDb; Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region GetEntityType()
        //var entities = modelBuilder.Model.GetEntityTypes();
        //foreach (var entityType in entities)
        //{
        //    Console.WriteLine(entityType);
        //}
        #endregion
        #region ToTable()
        //modelBuilder.Entity<Person>().ToTable("Kişiler");
        #endregion
        #region Column
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name)
        //    .HasColumnName("Departman")
        //    .HasColumnType("Int")
        //    .HasColumnOrder(12);
        #endregion
        #region Foreign Key
        //modelBuilder.Entity<Person>()
        //    .HasOne(p => p.Department)
        //    .WithMany(p => p.People)
        //    .HasForeignKey(p => p.DFK);
        #endregion
        #region Ignore
        //modelBuilder.Entity<Department>()
        //    .Ignore(d => d.Deneme);
        #endregion
        #region HasKey()
        //modelBuilder.Entity<Department>()
        //    .HasKey(d => d.DepartmenyPrimaryKey);
        #endregion
        #region IsRequired
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Surname).IsRequired(required: false);
        #endregion
        #region HasMaxLength
        //modelBuilder.Entity<Person>()
        //    .Property(p => p.Name).HasMaxLength(50);
        #endregion


    }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Person> People { get; set; }
}