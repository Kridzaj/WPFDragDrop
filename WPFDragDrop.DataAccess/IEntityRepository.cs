using IA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA.DataAccess
{
    public interface IEntityRepository : IRepository<Entity>
    {
    }
}
