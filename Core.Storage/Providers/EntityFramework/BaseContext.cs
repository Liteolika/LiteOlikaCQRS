using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Storage.Providers.EntityFramework
{
    public class BaseContext : DbContext
    {

        public BaseContext(string connectionStringOrName)
            : base(connectionStringOrName)
        {

        }

    }
}
