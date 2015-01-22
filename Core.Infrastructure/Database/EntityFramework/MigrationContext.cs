using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.EntityFramework
{
    public class MigrationContext : DbContextBase
    {

        public MigrationContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {

        }

    }
}
