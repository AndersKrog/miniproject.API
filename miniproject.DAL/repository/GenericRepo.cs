using Microsoft.EntityFrameworkCore;
using miniproject.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniproject.DAL.repository
{
    //ikke god, men har det grundlæggende:
    // https://codingblast.com/entity-framework-core-generic-repository/

    public interface Ientity
    {
        int Id { get; set; }
    }

    public interface IGenericRepo<TEntity>
    {
        Task<List<TEntity>> getAsync();        
        Task<TEntity> getByIdAsync(int id);
        Task<TEntity> createAsync(TEntity entity);
        Task<TEntity> updateAsync(int id, TEntity entity);
        Task<TEntity> deleteAsync(int id);
        bool exists(int id);

    }

    public class GenericRepo<TEntity> : IGenericRepo<TEntity>
        where TEntity : class, Ientity{
        // 
        private readonly AbContext myContext;
        public GenericRepo(AbContext _context)
        {
            myContext = _context;
        }

        public async Task<List<TEntity>> getAsync()
        {
            var list = await myContext.Set<TEntity>().ToListAsync();

            return list;
        }
        public async Task<TEntity> getByIdAsync(int id)
        {
            return await myContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
            return null;
        }
        public async Task<TEntity> createAsync(TEntity entity)
        {
            myContext.Set<TEntity>().Add(entity);
            var result = await myContext.SaveChangesAsync();
            return result != 0 ? entity : null;
        }
        public async Task<TEntity> updateAsync(int id, TEntity entity)
        {
            int result = 0;

            if (exists(id) == true) 
            { 
                myContext.Set<TEntity>().Update(entity);
                result = await myContext.SaveChangesAsync();
            }
            return result == 0 ? null : entity;
        }

        public async Task<TEntity> deleteAsync(int id)
        {
            var entity = await getByIdAsync(id);
            if (entity != null)
            {
                myContext.Set<TEntity>().Remove(entity);
                var result = await myContext.SaveChangesAsync();
                return result != 0 ? entity : null;
            }
            else
            {
                return null;
            }
        }

        public bool exists(int id)
        {
            return myContext.Set<TEntity>().Any(e => e.Id == id);
        }
    }
}
