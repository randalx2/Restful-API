namespace Restful_API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Restful_API.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Restful_API.Models.Restful_APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Restful_API.Models.Restful_APIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Contacts.AddOrUpdate(new Contacts[]
            {
                new Contacts(){Id = 0, FirstName = "Andrew", LastName = "Peters" },
                new Contacts(){Id = 1, FirstName = "Brice", LastName = "Lambson"},
                new Contacts(){Id = 2, FirstName = "Rowan", LastName = "Miller" }
            }      
                );
        }
    }
}
