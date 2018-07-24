using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class // struct, new()
    {
        protected Context Context { get; private set; }

        // TODO  BaseRepository(context)
        public BaseRepository(Context context)
        {
            Context = context;
        }

        // Get(id)
        public abstract TEntity Get(int id, bool includeRelatedEntities = true);

        // GetList()
        public abstract IList<TEntity> GetList();

        // Add(Entity)
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }
        
        // Update(entity)
        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
        
        // Delete (id)
        public void Delete(int id)
        {
            var set = Context.Set<TEntity>();
            var entity = set.Find(id);
            set.Remove(entity);
            Context.SaveChanges();
        }
    }
}
