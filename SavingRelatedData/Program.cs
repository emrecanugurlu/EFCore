using Microsoft.EntityFrameworkCore;
using System.Reflection;

var dbContext = new ApplicationDbContext();


#region One to One ilişkisel senaryolarda veri ekleme.
//Principal Entity üzerinden veri ekleme işlemi yaparken Dependent Entity'i belirtmemiz elzem değildir. Fakat Dependent Entity üzerinden veri ekleme işlemi yaparken Principal Entity'i belirtmemiz lazımdır. Çünkü Dependent Entity bağımlı olduğundan tek başına birşey ifade edememektedir.
#region 1.Yöntem -> Principal Entity üzerinden Dependent Entity verisini ekleme.

//Person person = new Person();
//person.Name = "Emre Can";
//person.Address = new(){PersonAddress = "Yayladağı/Hatay"};
//await dbContext.AddAsync(person);
//await dbContext.SaveChangesAsync();

#endregion
#region 2.Yöntem -> Dependent Entity üzerinden Principal Entity verisini ekleme.
//Address address = new()
//{
//    PersonAddress = "Yenimahalle/Ankara",
//    Person = new()
//    {
//        Name = "Gençay"
//    }
//};
//await dbContext.AddAsync(address);
//await dbContext.SaveChangesAsync();
//#endregion


//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Address Address { get; set; }

//}

//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }
//    public Person Person { get; set; }


//}

//class ApplicationDbContext : DbContext
//{
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        base.OnConfiguring(optionsBuilder);
//        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=1Q2W3E4R+; TrustServerCertificate=True; Encrypt=False;");
//    }

//    DbSet<Person> Persons { get; set; }
//    DbSet<Address> Addresses { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        base.OnModelCreating(modelBuilder);
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);
//    }

//}
#endregion
#endregion

#region One to Many ilişkisel senaryolarda veri ekleme

#region 1.Yöntem -> Principal Entity üzerinden Dependent Entity verisi ekleme
#region Nesne referansı üzerinden ekleme
Blog blog = new() { Name = "emrecnugurlu.com" };
blog.Posts.Add(new() { Title = "Post1" });
blog.Posts.Add(new() { Title = "Post2" });
//dbContext.AddAsync(blog);
//dbContext.SaveChanges();
#endregion
#region Object Initializer üzerinden ekleme
Blog blog2 = new Blog()
{
    Name = "yeniblog.com",
    Posts = new List<Post>() {
        new Post() { Title = "Post3" },
        new Post() { Title = "Post4" }
    }
};
//dbContext.AddAsync(blog2);
//dbContext.SaveChanges();
#endregion
#endregion
#region 2.Yöntem -> Dependent Entity üzerinden Principal Entity verisi ekleme
//Bu yöntem ile ekleme işlemi gerçekleştirilebilir fakat, 1 Post'a karşılık 1 Blog eklenmiş olur.
#endregion
#region 3.Yöntem -> Foreign Key kolonu üzerinden veri ekleme 
//Bu yöntem önceden eklenmiş olan bir Principal Entity'e karşılık veri ekleyebiliriz.
Post post = new Post()
{
    BlogId = 1,
    Title = "Post10",
};
//dbContext.AddAsync(post);
//dbContext.SaveChanges();
#endregion
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public Blog Blog { get; set; }
}

//class ApplicationDbContext : DbContext
//{
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        base.OnConfiguring(optionsBuilder);
//        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=1Q2W3E4R+; TrustServerCertificate=True; Encrypt=False;");
//    }

//    DbSet<Blog> Blogs { get; set; }
//    DbSet<Post> Posts { get; set; }

//}
#endregion

#region Many to Many ilişkisel senaryolarda veri ekeleme
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Author> Authors{ get; set; }
}

class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}

class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=1Q2W3E4R+; TrustServerCertificate=True; Encrypt=False;");
    }

    DbSet<Book> Blogs { get; set; }
    DbSet<Author> Posts { get; set; }

}
#endregion