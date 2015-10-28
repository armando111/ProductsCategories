namespace ProductsCategories.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        Task<T> GetOne(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> expression);
        void Update(Expression<Func<T, bool>> filterExtention, T entity);
        void Delete(int id);
        Task<int> GetNextId();
    }
}
