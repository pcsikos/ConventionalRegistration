using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAssembly.Data
{
    public class DbContext2
    {
        readonly DbConnection connection;

        public DbContext2(DbConnection connection)
        {
            this.connection = connection;
        }

        public DbConnection Connection
        {
            get
            {
                return connection;
            }
        }
    }
}
