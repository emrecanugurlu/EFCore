using Microsoft.EntityFrameworkCore;

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
Address address = new()
{
    PersonAddress = "Yenimahalle/Ankara",
    Person = new()
    {
        Name = "Gençay"
    }
};
await dbContext.AddAsync(address);
await dbContext.SaveChangesAsync();
#endregion


class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    
}

class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }
    public Person Person { get; set; }


}

class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=localhost, 1433; Database=ETrade; User Id=sa; Password=1Q2W3E4R+; TrustServerCertificate=True; Encrypt=False;");
    }

    DbSet<Person> Persons { get; set; }
    DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }

}
#endregion
