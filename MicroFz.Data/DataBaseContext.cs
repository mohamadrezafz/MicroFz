using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MicroFz.Data.Model;

namespace MicroFz.Data
{
    public class DataBaseContextFactory : IDbContextFactory<DataBaseContext>
    {
        public DataBaseContext Create()
        {
            return new DataBaseContext("Data Source=.;Initial Catalog=MicroFz;Persist Security Info=True;Integrated Security=true;");
        }
    }
    public class DataBaseContext : DbContext 
    {
        public DataBaseContext(string connectionString) : base(connectionString)
        {
        }
        public DbSet<Person> Person { get; set; }
    }
}
