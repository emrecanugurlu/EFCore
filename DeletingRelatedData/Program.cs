using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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
#region Saving
//Book book1 = new Book()
//{
//    BookName = "Kitap 1"
//};
//Book book2 = new Book()
//{
//    BookName = "Kitap 2"
//};
//Book book3 = new Book()
//{
//    BookName = "Kitap 3"
//};


//Author author1 = new Author()
//{
//    AuthorName = "Yazar 1",
//    Books = new List<Book>()
//    {
//        book1, book2
//    }
//};
//Author author2 = new Author()
//{
//    AuthorName = "Yazar 2",
//    Books = new List<Book>()
//    {
//        book1, book3
//    }
//};
//Author author3 = new Author()
//{
//    AuthorName = "Yazar 3",
//    Books = new List<Book>()
//    {
//        book3
//    }
//};

//await context.Authors.AddRangeAsync(author1, author2, author3);
//await context.SaveChangesAsync();

#endregion
//Book? book = await context.Books.Include(b => b.Authors).SingleOrDefaultAsync(b => b.Id == 1);
//Author? author = book!.Authors.FirstOrDefault(a => a.Id == 1);
////context.Authors.Remove(author!); Bu yöntem yazar ile birlikte, bu yazarla ilişkisel bağı olan tüm yapıları silecektir. Kullanırken dikkatli olmak gerekmektedir.  
//book.Authors.Remove(author!);
//await context.SaveChangesAsync();
#endregion

#region Cascade Delete
//Bu davranış modelleri Fluent API ile modellenmektedir.
#region Cascade
//Principle tabloda bir veri silindiğinde, eğer ki dependent tabloda bu veriye karşılık bir değer var ise o değeri de silecektir. Bu Entity Framework'te varsayılan davranış olarak karşımıza çıkmaktadır.
#endregion
#region SetNull
//Principle tabloda bir değer silindiğinde, bu değere karşılık dependent tabloda bir değer var ise bu değerin foreign key property'sine "null" değer atanacaktır.
#endregion
#region Restrict
//Principle tabloda bir değer silindiğinde, bu değere karşılık dependent tabloda bir değer var ise Entity Framework bizlere hata döndürecektir.

Blog? blog = await context.Blogs.SingleOrDefaultAsync(b => b.Id == 1);
context.Blogs.Remove(blog!);
await context.SaveChangesAsync();
#endregion

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

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
    public DbSet<Addrees> Addreeses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

}