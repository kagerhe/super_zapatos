namespace Data_Access.Migrations
{
    using Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data_Access.ConexionB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data_Access.ConexionB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Stores.AddOrUpdate(new Store()
            {
                Id = 1,
                Name = "Somewhere over the rainbow",
                Address = "Super Store"
            }, new Store()
            {
                Id = 2,
                Name = "Zacaris",
                Address = "Zacaris store"
            }
);
            context.Articles.AddOrUpdate(new Article()
            {
                Id = 1,
                StoreId = 1,
                Name = "green shoes",
                Description = "shoes VansThe best quality of shoes in a green color",
                Price = 20,
                Total_In_Shelf = 25,
                Total_In_Vault = 40
            }, new Article()
            {
                Id = 2,
                StoreId = 1,
                Name = "Skechers",
                Description = "shoes Skechers",
                Price = 80,
                Total_In_Shelf = 3,
                Total_In_Vault = 40
            }, new Article()
            {
                Id = 3,
                StoreId = 2,
                Name = "Converse",
                Description = "shoes Converse",
                Price = 150,
                Total_In_Shelf = 2,
                Total_In_Vault = 50
            }
        );
        }
    }
}
