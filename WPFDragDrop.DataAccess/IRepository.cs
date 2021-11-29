using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.DataAccess
{
    public interface IRepository<T>
    {
        IQueryable<T> Query();
        IQueryable<T> QueryInclude(params string[] includes);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(object id);
    }
}
