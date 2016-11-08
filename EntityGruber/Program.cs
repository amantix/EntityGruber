using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGruber
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new GruberContext())
            {
                db.Customers.Add(new Customer {Cnum = 1, City = "London", Cname = "John", Rating = 100});
                db.SaveChanges();
            }
        }
    }
}
