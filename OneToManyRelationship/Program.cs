using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine("Hello, World!");

#region Default Convantion
//Bu yöntemi kullanırken one to many ilişkisi kurarken foreignKey kolonuna karşılık gelen bir property oluşturmamıza gerek yoktur. Eğer oluşturmak isteniyor ise default convantion kurallarına uygun bir property oluşturulabilir.

//public class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public Department Department { get; set; }

//}
//public class Department
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees { get; set; }
//}

#endregion
#region Data Annotations

//public class Employee
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Department))]
//    public int FKeyDepartment { get; set; }
//    public string Name { get; set; }
//    public Department Department { get; set; }

//}
//public class Department
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public ICollection<Employee> Employees { get; set; }
//}

#endregion
#region Fluent API
//Bu yapı Context sınıfı içerisinde onModelCreating metodunda kurulmuştur. 

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Department Department { get; set; }

}
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
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
        modelBuilder.Entity<Employee>()
            .HasOne(e=>e.Department)
            .WithMany(a=>a.Employees);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments  { get; set; }

}