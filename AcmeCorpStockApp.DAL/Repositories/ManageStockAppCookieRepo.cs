using AcmeCorpStockApp.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace AcmeCorpStockApp.DAL.Repositories
{
    public class ManageStockAppCookieRepo : IManageStockAppCookiesRepo
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public ManageStockAppCookieRepo(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        public bool Add(string key, string value, CookieOptions cookieOptions)
        {
            try
            {
                _HttpContextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Delete(string key)
        {
            bool isFound = Find(key);
            if (isFound)
            {
                _HttpContextAccessor.HttpContext.Response.Cookies.Delete(key);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAll()
        {
            throw new NotImplementedException();
        }

        public bool Find(string key)
        {
            bool result = _HttpContextAccessor.HttpContext.Request.Cookies.Any(cookie => cookie.Key == key);
            return result;
        }

        public string Get(string key)
        {
            bool result = Find(key);
            if (result)
            {
                string cookieValue = _HttpContextAccessor.HttpContext.Request.Cookies[key];
                return cookieValue;
            }
            else
            {
                return null;
            }
        }

        public bool Update(string key, string value, CookieOptions cookieOptions)
        {
            bool result = Find(key);
            if (result)
            {
                Delete(key);
                Add(key, value, cookieOptions);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}