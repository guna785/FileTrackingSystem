using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.DAL.Contract
{
    public interface IGenericDbService<T> where T : class
    {
        IQueryable<T> AsQueryable();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T FindById(int Id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
