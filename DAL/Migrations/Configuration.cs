namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.KodiDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DAL.KodiDB";
        }

        protected override void Seed(DAL.KodiDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
