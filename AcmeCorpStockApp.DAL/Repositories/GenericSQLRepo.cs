using AcmeCorpStockApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.DAL.Repositories
{
    public class GenericSQLRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AcmeCorpAppDBContext _acneCorpAppDBContext;
        public GenericSQLRepo(AcmeCorpAppDBContext acneCorpAppDBContext)
        {
            _acneCorpAppDBContext = acneCorpAppDBContext;
        }
        public async Task<T> CreateAsync(T obj)
        {
            try
            {
                await _acneCorpAppDBContext.Set<T>().AddAsync(obj);
                await _acneCorpAppDBContext.SaveChangesAsync();
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteAsync(T obj)
        {
            try
            {
                _acneCorpAppDBContext.Set<T>().Remove(obj);
                await _acneCorpAppDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return _acneCorpAppDBContext.Set<T>().ToList();
        }

        public T GetByAttrAsync(Expression<Func<T, bool>> filter)
        {
            try
            {
                T obj = _acneCorpAppDBContext.Set<T>().FirstOrDefault(filter);
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<T> UpdateAsync(T oldObj, T newObj)
        {
            try
            {
                _acneCorpAppDBContext.Entry<T>(oldObj).CurrentValues.SetValues(newObj);
                await _acneCorpAppDBContext.SaveChangesAsync();
                return newObj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
