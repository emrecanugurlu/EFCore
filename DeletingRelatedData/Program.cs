using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new ApplicationDbContext();

#region One to One ilişkisel senaryolarda veri silme işlemi
#region Saving
//Person person1 = new Person()
//{
//    Name = "Emre", 
//    Addrees = new Addrees() {
//        City = "Hatay"
//    }
//};

//Person person2 = new Person()
//{
//    Name = "Ahmet"
//};

//await context.AddAsync(person1);
//await context.AddAsync(person2);

//await context.SaveChangesAsync();

#endregion

//Person? person = await context.Persons
//                .Include(p => p.Addrees)
//                .FirstOrDefaultAsync();

//context.Addreeses.Remove(person!.Addrees);
//await context.SaveChangesAsync();

#endregion
#region One to Many ilişkisel senaryolarda veri silme işlemi
#region Saving
//Blog blog = new Blog()
//{
//    Name = "emrecnugurlu.com",
//    Posts = new List<Post>()
//    {
//        new Post(){
//        Title = "Post 1"
//        },
//        new Post(){
//        Title = "Post 2"
//        },
//        new Post(){
//        Title = "Post 3"
//        },
//    }
//};

//await context.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion

//Blog? blog = await context.Blogs.Include(b => b.Posts).FirstOrDefaultAsync(b => b.Id ==1);
//foreach (var post in blog!.Posts)
//{
//    if (post.Id == 1 || post.Id == 2)
//    {
//        context.Posts.Remove(post);
//    }
//}
//await context.SaveChangesAsync();
#endregion
#region Many to Many ilişkisel senaryolarda veri silme işlemi

#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Addrees Addrees { get; set; }

}
class Addrees
{
    public int Id { get; set; }
    public string City { get; set; }
    public Person Person { get; set; }
}

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
    public string Title { get; set; }
    public Blog Blog { get; set; }
}

class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }
    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }
    public ICollection<Book> Books { get; set; }
}

class ApplicationDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=1Q2W3E4R+; TrustServerCertificate=True; Encrypt=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Addrees>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Addrees)
            .HasForeignKey<Addrees>(a => a.Id);
    }
    public DbSet<Addrees> Addreeses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

}