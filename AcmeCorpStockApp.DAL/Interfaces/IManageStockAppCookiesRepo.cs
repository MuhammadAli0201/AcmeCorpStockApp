using Microsoft.AspNetCore.Http;

namespace AcmeCorpStockApp.DAL.Interfaces
{
    public interface IManageStockAppCookiesRepo
    {
        public bool Add(string key, string value, CookieOptions cookieOptions);
        public bool Update(string key, string value, CookieOptions cookieOptions);
        public bool Delete(string key);
        public string Get(string key);
        public bool Find(string key);
        public bool DeleteAll();
    }
}
