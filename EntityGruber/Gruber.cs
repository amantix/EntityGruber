using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;

namespace EntityGruber
{
    [Table("Customers")]
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cnum { get; set; }
        public string Cname { get; set; }
        public string City { get; set; }
        public int Rating { get; set; }

        public int? Snum { get; set; }
        public virtual Salesperson Salesperson { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    [Table("Salespeople")]
    public class Salesperson
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Snum { get; set; }
        public string Sname { get; set; }
        public string City { get; set; }
        public decimal Comm { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    [Table("Orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Onum { get; set; }
        public DateTime Odate { get; set; }
        public decimal Amt { get; set; }

        public int Snum { get; set; }
        public virtual Salesperson Salesperson { get; set; }

        public int Cnum { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public class GruberContextInitializer:DropCreateDatabaseAlways<GruberContext>
    {
        protected override void Seed(GruberContext context)
        {
            var sql = File.ReadAllText("gruber.txt");
            context.Database.ExecuteSqlCommand(sql);
            context.SaveChanges();

        }
    }

    public class GruberContext: DbContext
    {
        static GruberContext()
        {
            Database.SetInitializer<GruberContext>(new GruberContextInitializer());
        }
        public GruberContext()
            :base("EntityGruber")
        {}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Salesperson> Salespeople { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
