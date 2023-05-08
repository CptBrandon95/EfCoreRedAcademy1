using EfCoreRedAcademy1.GenericRepository;
using EfCoreRedAcademy1.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EfCoreRedAcademy1.GenRepo
{
    public class GenericRepo<T> : IGenericRepository<T> where T : BaseEntity
    {
        private EfCoreAcademyDbContext _efCoreAcademyDbContext { get; }
        private DbSet<T> _DbSet { get; }

        public GenericRepo(EfCoreAcademyDbContext efCoreAcademyDbContext)
        {
            _efCoreAcademyDbContext = efCoreAcademyDbContext;
            _DbSet = _efCoreAcademyDbContext.Set<T>();
        }
        public void Delete(T entity)
        {
           if(_efCoreAcademyDbContext.Entry(entity).State == EntityState.Detached)
                _DbSet.Attach(entity);
           _DbSet.Remove(entity);
        }

        public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _DbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
                
            if (skip != null)
                query = query.Take(take.Value);

            if(take != null)
                query = query.Skip(take.Value);

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _DbSet;

            query = query.Where(include => include.Id == id);

            foreach (var include in includes)
                query = query.Include(include);
            return await query.SingleOrDefaultAsync();

        }

        public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> values = _DbSet;

            foreach (var filter in filters)
                values = values.Where(filter);

            if (skip != null)
                values= values.Skip(skip.Value);


            if (take != null)
                values = values.Take(take.Value);
            return await values.ToListAsync();
        }

        public async Task<int> InsertAsynv(T entity)
        {
            await _DbSet.AddAsync(entity);
            return entity.Id;
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
          _DbSet.Attach(entity);
            _DbSet.Entry(entity).State= EntityState.Modified;
        }
    }
}
