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
                db.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));

                db.Customers.Add(new Customer {Cnum = 1, City = "London", Cname = "John", Rating = 100});
                db.SaveChanges();


                var c1 =db.Customers.Where(x => x.Orders.Count > 1);
                foreach (var c in c1)
                {
                    Console.WriteLine($"{c.Cname} {c.City} {c.Rating}");
                }

                // Вывести имена покупателей 
                // вместе с датой и суммой их наибольшей покупки,
                // отсортированные по убыванию величины этой наибольшей покупки

                var c2 = from o in db.Orders
                    where o.Amt == (from max in db.Orders where max.Cnum == o.Cnum select max.Amt).Max()
                    select new {o.Customer.Cname, o.Amt, o.Odate};


                //var c3 = db.Database.SqlQuery<string>("Select cname from Customers");

                foreach (var c in c2)
                {
                    Console.WriteLine(c);
                }


                var orders = db.Orders.Include("Salesperson").Include("Customer").ToList();

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Odate} {order.Salesperson.Sname} {order.Customer.Cname}");
                }


            }
        }
    }
}
