using AcmeCorpStockApp.BLL.Algorithms;
using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.DAL.Interfaces;
using AcmeCorpStockApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.BLL.Services
{
    public class ManageStockAppUserService : IManageStockAppUserService
    {
        private readonly IManageStockAppUserRepo _manageStockAppUserRepo;
        private readonly IManageStockAppCookiesRepo _manageStockAppCookiesRepo;

        public ManageStockAppUserService(IManageStockAppUserRepo manageStockAppUserRepo,
            IManageStockAppCookiesRepo manageStockAppCookiesRepo)
        {
            _manageStockAppUserRepo = manageStockAppUserRepo;
            _manageStockAppCookiesRepo = manageStockAppCookiesRepo;
        }

        public async Task<ResultState> ChangePassword(string email, string oldPassword, string newPassword)
        {
            StockAppUser user = _manageStockAppUserRepo.GetByAttrAsync(u => u.Email == email);
            if (user != null)
            {
                if (oldPassword == user.Password)
                {
                    user.Password = newPassword;
                    StockAppUser updatedUser = new StockAppUser(user);

                    StockAppUser isUpdated = await _manageStockAppUserRepo.UpdateAsync(user, updatedUser);
                    if (isUpdated != null)
                    {
                        return new ResultState {
                        Message= "Updated",
                        Status=ResultStatus.Succeeded
                        };
                    }
                    else
                    {
                        return new ResultState { 
                        Message= "Opps. There is Server issue, Unable to update",
                        Status=ResultStatus.Error
                        };
                    }
                }
                else
                {
                    return new ResultState { 
                    Message= "Incorrect Old Password",
                    Status=ResultStatus.Error
                    };
                }
            }
            else
            {
                return new ResultState { 
                Message= "Opps. We cannot find your account.",
                Status=ResultStatus.Error
                };
            }
        }

        public async Task<List<StockAppUser>> GetAllUsers()
        {
            return await _manageStockAppUserRepo.GetAllAsync();
        }

        public StockAppUser GetUser(string email)
        {
            StockAppUser user = _manageStockAppUserRepo.GetByAttrAsync(user => user.Email == email);
            return user;
        }

        public StockAppUser GetCurrentUserFromCookie()
        {
            bool isCookieFound = _manageStockAppCookiesRepo.Find("Login_Id");
            if (isCookieFound)
            {
                string email = _manageStockAppCookiesRepo.Get("Login_Id");

                StockAppUser user = GetUser(email);
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<ResultState> Login(string email, string password, bool isRememberMe)
        {
            email = email.ToLower();
            StockAppUser user = GetUser(email);
            if (user != null)
            {
                if (password == user.Password)
                {
                    _manageStockAppCookiesRepo.Delete("Login_Id");
                    _manageStockAppCookiesRepo.Delete("LoggedIn");
                    _manageStockAppCookiesRepo.Delete("Password");
                    _manageStockAppCookiesRepo.Delete("Remember_Me");

                    _manageStockAppCookiesRepo.Add("Login_Id", email, new CookieOptions { });
                    _manageStockAppCookiesRepo.Add("LoggedIn", "true", new CookieOptions { });

                    if (isRememberMe)
                    {
                        _manageStockAppCookiesRepo.Delete("Login_Id");

                        CookieOptions rememberMeCookieOption = new CookieOptions { Expires = DateTime.Now.AddDays(1) };
                        _manageStockAppCookiesRepo.Add("Login_Id", email, rememberMeCookieOption);
                        _manageStockAppCookiesRepo.Add("Password", password, rememberMeCookieOption);
                        _manageStockAppCookiesRepo.Add("Remember_Me", isRememberMe.ToString(), rememberMeCookieOption);
                    }
                    return new ResultState { Message= "LoggedIn",Status=ResultStatus.Succeeded };
                }
                else
                {
                    return new ResultState { Message = "Error with login, please check + retry.", Status = ResultStatus.Error };
                }
            }
            else
            {
                return new ResultState { Message = "Error with login, please check + retry.", Status = ResultStatus.Error };
            }
        }

        public bool LogOut()
        {
            try
            {
                _manageStockAppCookiesRepo.Delete("Login_Id");
                _manageStockAppCookiesRepo.Delete("LoggedIn");
                _manageStockAppCookiesRepo.Delete("Password");
                _manageStockAppCookiesRepo.Delete("Remember_Me");
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<ResultState> PasswordRecovery(string email)
        {
            StockAppUser user = GetUser(email);
            if (user != null)
            {
                EmailSender emailSender = new EmailSender();
                string password = RandomTextGenerator.CreatePassword(10);

                bool isSucceeded = emailSender.SendEmail(email, "AcmeCorpStockApp password recovery", password);
                
                if (isSucceeded)
                {
                    await _manageStockAppUserRepo.UpdateAsync(user, new StockAppUser
                    {
                        Email = email,
                        Password = password,
                        Type = user.Type,
                        Name = user.Name,
                        RegistrarEmail = user.RegistrarEmail
                    });
                    return new ResultState
                    {
                        Message = "Updated",
                        Status = ResultStatus.Succeeded
                    };
                }
                else
                {
                    return new ResultState
                    {
                        Message = "Error in sending mail",
                        Status = ResultStatus.Error
                    };
                }
            }
            else
            {
                return new ResultState
                {
                    Message = "User not Found",
                    Status = ResultStatus.Error
                };
            }
        }

        public async Task<ResultState> Register(StockAppUser user)
        {
            user.Email = user.Email.ToLower();

            StockAppUser findUser = GetUser(user.Email);
            if (findUser != null)
            {
                return new ResultState
                { 
                Message= "Email is already taken, try another or use forgot option.",
                Status=ResultStatus.Error
                };
            }
            else
            {
                StockAppUser result = await _manageStockAppUserRepo.CreateAsync(user);
                if (result != null)
                {
                    return new ResultState {
                    Message= "Added",
                    Status=ResultStatus.Succeeded
                    };
                }
                else
                {
                    return new ResultState {
                    Message= "Sorry an Exception Occured",
                    Status=ResultStatus.Error
                    };
                }
            }
        }
    }
}