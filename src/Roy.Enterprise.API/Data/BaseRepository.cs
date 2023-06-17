using Roy.Enterprise.API.Helpers;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Roy.Enterprise.API.Data
{
    public abstract class BaseRepository
    {
        protected readonly DataContext Context;

        protected BaseRepository(DataContext context)
        {
            Context = context;
        }

        protected void DatabaseAccess(Action<DataContext> action, bool saveChanges = false)
        {
            if (Context.Database.CurrentTransaction != null)
            {
                action(Context);

                if (saveChanges)
                {
                    Context.SaveChanges();
                }
            }
            else
            {
                using var transaction = Context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable);
                action(Context);

                if (saveChanges)
                {
                    Context.SaveChanges();
                }

                transaction.Commit();
            }
        }
        public List<T> Get<T>(Expression<Func<T, bool>> whereClause) where T : class
        {
            var rv = new List<T>();

            DatabaseAccess(c =>
            {
                var queryable = Context.Set<T>().AsNoTracking();

                if (whereClause != null)
                    queryable = queryable.Where(whereClause);

                rv = queryable.ToList();

            }, false);

            return rv;
        }
        public void Create<T>(T entity) where T : class
        {
            DatabaseAccess(c =>
            {
                Context.Set<T>().Add(entity);
            }, true);
        }

        public void Create<T>(List<T> entity) where T : class
        {
            DatabaseAccess(c =>
            {
                Context.Set<T>().AddRange(entity);
            }, true);
        }

        public void Update<T>(T entity) where T : class
        {
            DatabaseAccess(c =>
            {
                Context.Set<T>().Update(entity);
            }, true);
        }
        public void Update<T>(List<T> entity) where T : class
        {
            DatabaseAccess(c =>
            {
                Context.Set<T>().UpdateRange(entity);
            }, true);
        }


        public void Remove<T>(T entity) where T : class
        {
            DatabaseAccess(c =>
            {
                Context.Set<T>().Remove(entity);
            }, true);
        }
        public void Remove<T>(List<T> entity) where T : class
        {
            DatabaseAccess(c =>
            {
                Context.Set<T>().RemoveRange(entity);
            }, true);
        }


        public List<T> GetAll<T>(T entity) where T : class
        {
            var rv = new List<T>();

            DatabaseAccess(c =>
            {
                var queryable = Context.Set<T>().AsNoTracking();
                rv = queryable.ToList();

            }, false);

            return rv;
        }
    }
}
