using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.DataAccess
{
    public class EFRepository<TEntity> : IRepository<TEntity>
         where TEntity : class
    {
        private readonly DbContext _context;

        public EFRepository(DbContext context)
        {
            _context = context;
        }


        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }
        public IQueryable<TEntity> QueryInclude(params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return query;
        }

        public virtual TEntity Create(TEntity entity)
        {
            TEntity created = _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return created;
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public virtual void UpdateExisting(TEntity exitingEntity, TEntity entity)
        {
            _context.Entry<TEntity>(exitingEntity).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public TEntity GetByIdWithInclude(PropertyInfo keyColumn, object id, params string[] includes)
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(PropertyEquals<TEntity, int>(keyColumn, (int)id));
            return query.SingleOrDefault<TEntity>();
        }
        public TEntity Refresh(TEntity entity)
        {
            _context.Entry(entity).Reload();
            return entity;
        }
        public TEntity ReloadNavigationProperty(TEntity entity, string navigationProperty)
        {
            _context.Entry(entity).Reference(navigationProperty).Query().Load();
            return entity;
        }
        public Expression<Func<TItem, bool>> PropertyEquals<TItem, TValue>(PropertyInfo property, TValue value)
        {

            var param = Expression.Parameter(typeof(TItem));
            if (property.PropertyType == typeof(Int16))
            {
                Int16 t = Convert.ToInt16(value);
                var body = Expression.Equal(Expression.Property(param, property),
                    Expression.Constant(t));

                return Expression.Lambda<Func<TItem, bool>>(body, param);

            }
            if (property.PropertyType == typeof(long))
            {
                long t = Convert.ToInt64(value);
                var body = Expression.Equal(Expression.Property(param, property),
                    Expression.Constant(t));

                return Expression.Lambda<Func<TItem, bool>>(body, param);

            }
            else
            {
                var body = Expression.Equal(Expression.Property(param, property),
                    Expression.Constant(value));

                return Expression.Lambda<Func<TItem, bool>>(body, param);
            }
        }
    }
}
