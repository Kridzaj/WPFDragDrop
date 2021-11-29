using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA.DataAccess
{
    public class IARepository<T> : EFRepository<T>
       where T : class
    {
        public IARepository(DbContext context) : base(context) { }
    }
}
