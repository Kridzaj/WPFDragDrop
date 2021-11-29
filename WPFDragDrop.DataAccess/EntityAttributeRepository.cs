using WPFDragDrop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.DataAccess
{
    public class EntityAttributeRepository : EFRepository<EntityAttribute>
    {
        public EntityAttributeRepository(DbContext context) : base(context)
        {
        }

    }
}
