using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;

namespace frontend.Repository
{
    public class CommonRepository
    {
        protected NpgsqlConnection _con;
 
        public CommonRepository()
        {
            IConfiguration myConfig = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            _con = new NpgsqlConnection(myConfig.GetConnectionString("DefaultConnection"));
        }

    }
}