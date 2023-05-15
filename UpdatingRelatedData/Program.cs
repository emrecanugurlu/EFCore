using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new ApplicationDbContext();
#region One to One ilişkisel senaryolarda veri güncelleme
#region Saving
//Person person = new Person()
//{
//    Name = "Emre Can Uğurlu",
//    Addrees = new Addrees() { City = "Hatay" }
//};

//Person person1 = new Person()
//{
//    Name = "Ahmet İlhan"
//};

//await context.AddAsync(person);
//await context.AddAsync(person1);
//await context.SaveChangesAsync();
#endregion
#region 1.Durum -> Esas tablodaki veriye bağımlı veriyi değiştirme
//Person? person = await context.Persons
//    .Include(p => p.Addrees)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//Addrees addrees = person!.Addrees;
//context.Addreeses.Remove(addrees);
//person.Addrees = new Addrees()
//{
//    City = "Yozgat"
//};
//await context.SaveChangesAsync();
#endregion
#region 2.Durum -> Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme
//Person? person = await context.Persons
//    .Include(p => p.Addrees)
//    .FirstOrDefaultAsync(p => p.Id == 1);

//Addrees? addrees = person?.Addrees;
//context.Addreeses.Remove(addrees!);
//await context.SaveChangesAsync();

//Person? person1 = await context.Persons
//    .Include (p => p.Addrees)
//    .FirstOrDefaultAsync(p=>p.Id == 2);

//person1!.Addrees = addrees!;
//await context.SaveChangesAsync();
#endregion
#endregion
#region One to Many ilişkisel senaryolarda veri güncelleme
#region Saving
//Blog blog = new Blog()
//{
//    Name = "emrecnugurlu.com",
//    Posts = new List<Post>()
//    {
//        new Post()
//        {
//            Title = "Post 1",
//        },
//        new Post()
//        {
//            Title = "Post 2",
//        },
//        new Post()
//        {
//            Title = "Post 3",
//        }
//    }
//};

//await context.AddAsync(blog);
//await context.SaveChangesAsync();
#endregion
#region 1.Durum -> Esas tablodaki veriye bağımlı olan verileri güncelleme
//Blog? blog = await context.Blogs.Include(b => b.Posts).FirstOrDefaultAsync(b => b.Id == 1);
//Post? post = await context.Posts.FirstOrDefaultAsync(b => b.Id == 3);
//context.Posts.Remove(post!);
//blog!.Posts.Add(new Post() { Title = "Post 4" });
//await context.SaveChangesAsync();
#endregion
#region 2.Durum -> Bağımlı verilerin ilişkisel olduğu ana veriyi güncelleme
//Post? post = await context.Posts.FirstOrDefaultAsync(p => p.Id == 5);
//post!.Blog = new Blog()
//{
//    Name = "Deneme"
//};
//await context.SaveChangesAsync();
#endregion
#endregion
#region Many to Many ilişkisel senaryolarda veri güncelleme
#region Saving
//Book book1 = new Book() { BookName = "1.Kitap" };
//Book book2 = new Book() { BookName = "2.Kitap" };
//Book book3 = new Book() { BookName = "3.Kitap" };

//Author author1 = new Author() { AuthorName = "1.Yazar" };
//Author author2 = new Author() { AuthorName = "2.Yazar" };
//Author author3 = new Author() { AuthorName = "3.Yazar" };

//author1.Books.Add(book1);
//author1.Books.Add(book2);
//author1.Books.Add(book3);

//author2.Books.Add(book3);

//author3.Books.Add(book1);
//author3.Books.Add(book2);

//await context.Books.AddRangeAsync(book1, book2, book3);
//await context.Authors.AddRangeAsync(author1, author2, author3);

//await context.SaveChangesAsync();
#endregion

#region 1 Id'sine sahip olan kitaba 2 Id'li yazarı ekleme
Book? book1 = await context.Books.FirstOrDefaultAsync(b => b.Id == 1);
Author? author2 = await context.Authors.FirstOrDefaultAsync(author => author.Id == 2);
book1!.Authors.Add(author2!);
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
    }
    public DbSet<Addrees> Addreeses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }



}