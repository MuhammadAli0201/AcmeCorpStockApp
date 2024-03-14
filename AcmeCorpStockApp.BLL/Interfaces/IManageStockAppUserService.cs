using AcmeCorpStockApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.BLL.Interfaces
{
    public interface IManageStockAppUserService
    {
        public Task<ResultState> Login(string email, string password, bool rememberMe);
        public Task<ResultState> Register(StockAppUser user);
        public Task<ResultState> ChangePassword(string email, string oldPassword, string newPassword);
        public Task<List<StockAppUser>> GetAllUsers();
        public StockAppUser GetCurrentUserFromCookie();
        public StockAppUser GetUser(string email);
        public Task<ResultState> PasswordRecovery(string email);
        public bool LogOut();
    }
}
