using FileTrackingSystem.DAL.Context;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.BaseClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.DAL.Repository
{
    public class GenericDbService<T> : IGenericDbService<T> where T : CommonModel
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public GenericDbService(ApplicationDbContext context)
        {
            this._context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> AsQueryable()
        {
            return entities.AsQueryable();
        }

        public void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.AddAsync(entity);
            _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChangesAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return entities.Where(expression);
        }

        public T FindById(int Id)
        {
            return entities.SingleOrDefault(s => s.Id == Id);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.SaveChangesAsync();
        }
    }
}
