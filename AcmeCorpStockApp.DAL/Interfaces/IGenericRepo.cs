using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.DAL.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        public Task<T> CreateAsync(T obj);
        public Task<T> UpdateAsync(T oldObj, T newObj);
        public Task<bool> DeleteAsync(T obj);
        public T GetByAttrAsync(Expression<Func<T, bool>> filter);
        public Task<List<T>> GetAllAsync();
    }
}
