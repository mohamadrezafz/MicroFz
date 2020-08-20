using CoreBase.Data;
using MicroFz.Common.Config;
using MicroFz.Data.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroFz.Data
{
    public interface IMicroFzUnitOfWork 
    {
        IBaseRepository<DataBaseContext, Person> Person { get; }
    }
    public class MicroFzUnitOfWork : IMicroFzUnitOfWork
    {
        private DataBaseContext applicationDbContext;
        protected readonly IConfiguration configuration;


        public MicroFzUnitOfWork(IConfiguration configuration)
        {
            this.configuration = configuration;
            if (applicationDbContext == null)
            {
                applicationDbContext = new DataBaseContext(configuration.GetSection("Settings:Connection").Value);
            }
        }

        public IBaseRepository<DataBaseContext, Person> Person => new BaseRepository<DataBaseContext, Person>(applicationDbContext);

      //  DataBaseContext IBaseUnitOfWork<DataBaseContext>.Context => applicationDbContext;

      
    }
}
