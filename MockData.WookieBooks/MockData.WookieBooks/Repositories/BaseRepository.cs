using Models.WookieBooks.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MockData.WookieBooks.Repositories
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class, IAppEntity
    {
        protected AppDbContext Context { get; private set; }

        public BaseRepository(AppDbContext appDbContext)
        {
            Context = appDbContext;
        }

        public virtual TEntity Get(int key)
        {
            return Context
                .Set<TEntity>()
                .Where(x => x.Id == key)
                .SingleOrDefault();
        }

        public virtual List<TEntity> GetAll()
        {
            return Context
                .Set<TEntity>()
                .ToList();
        }

        public virtual int Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return Context.SaveChanges();
        }

        public virtual int Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return Context.SaveChanges();
        }

        public virtual int Delete(int key)
        {
            var entity = Context
                .Set<TEntity>()
                .Where(x => x.Id == key)
                .Single();

            return Delete(entity);
        }

        public virtual int Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Context.SaveChanges();
        }
    }
}
