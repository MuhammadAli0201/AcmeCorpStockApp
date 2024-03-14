using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.DAL.Interfaces;
using AcmeCorpStockApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeCorpStockApp.CustomElements
{
    public class CustomAuthAttribute : ActionFilterAttribute
    {
        public async override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var svc = filterContext.HttpContext.RequestServices;
            var manageStockAppCookiesRepo = svc.GetService<IManageStockAppCookiesRepo>();

            bool result = manageStockAppCookiesRepo.Find("Login_Id");

            if (result == false)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "account",
                    action = "login"
                }));
            }
            else
            {
                var manageStockAppUser = svc.GetService<IManageStockAppUserService>();

                string id = manageStockAppCookiesRepo.Get("Login_Id");
                string password = manageStockAppCookiesRepo.Get("Password");
                string rememberMe = manageStockAppCookiesRepo.Get("Remember_Me");

                if (id != null && rememberMe == "True")
                {
                    ResultState message = await manageStockAppUser.Login(id, password, bool.Parse(rememberMe));

                    if (message.Status==ResultStatus.Error)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "account",
                            action = "login"
                        }));
                    }
                }
            }
        }
    }
}
