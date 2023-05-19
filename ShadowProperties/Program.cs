using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

ApplicationDbContext context = new ApplicationDbContext();

#region Shadow Properties
//Entity sınıflarında fiziksel olarak tanımlanmayan/modellenmeyen fakat Ef Core tarafından ilgili entity için var olan propertilerdir.
//Tabloda gösterilmesini istemediğimiz/lüzumlu görmediğimiz/entity intence'sı üzerinde işlem yapmayacağımız kolonlar için shadow property'ler kullılabilir.
//Shadow propertylerin değerleri ve stateleri Change tarcker tarafından kontrol edilmektedir.
#endregion

#region Foreign Key Shadow Properties
//İlişkisel senaryolarda foreign key propertisini tanımlamadığımız halde Ef Core tarafından depndent entity'e eklenmektedir. Buna da shadow property denilir.
#endregion

#region Shadow Property Oluturma
//Bir entity üzerinde shadow property oluşturmak istiyorsak Fluent API'ı kullanmamız gerekmektedir. 
//          modelBuilder.Entity<Blog>()
//                  .Property<DateTime>("CreatedDate");

#endregion


#region Shadow property'e erişim sağlama

#region Change Tracker ile erişim
//Blog? blog = await context.Blogs.FirstAsync();
//PropertyEntry createdDated = context.Entry(blog).Property("CreatedDate");
//createdDated.CurrentValue = DateTime.UtcNow;
//await context.SaveChangesAsync();
#endregion

#region EF.Property ile erişim

//Blogları "CreatedDate" property'sine göre artan olarak sıralama
var blogs = await context.Blogs.OrderBy(b => EF.Property<DateTime>(b,"CreatedDate")).ToListAsync();

var blogs1 = await context.Blogs.Where(b => EF.Property<DateTime>(b, "CreatedDate").Year > 2020).ToListAsync();


#endregion
#endregion


class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime LastUpdate { get; set; }
    public Blog Blog { get; set; }
}
class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=Application; User Id=sa; Password=1Q2W3E4R+; TrustServerCertificate=True; Encrypt=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Blog>()
            .Property<DateTime>("CreatedDate");
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}