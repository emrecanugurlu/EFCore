using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

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
Person? person = await context.Persons
    .Include(p => p.Addrees)
    .FirstOrDefaultAsync(p => p.Id == 1);

Addrees addrees = person!.Addrees;
context.Addreeses.Remove(addrees);
person.Addrees = new Addrees()
{
    City = "Yozgat"
};
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


}