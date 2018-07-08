

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Evolent.Data
{
    public class DBInitializer : DbMigrationsConfiguration<EvolentContext>
    {
        public DBInitializer()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EvolentContext context)
        {
            var customers = new List<Contact>{
                new Contact  {ContactId = 1, FirstName ="Alejandra Camino", LastName ="Gran Vía, 1", PhoneNumber ="(953) 10956", Status="Active", Email="someemail@hotmail.com"},
                
            };

            customers.ForEach(s => context.Contacts.AddOrUpdate(p => p.FirstName, s));
            context.SaveChanges();

           
        }
    }
}