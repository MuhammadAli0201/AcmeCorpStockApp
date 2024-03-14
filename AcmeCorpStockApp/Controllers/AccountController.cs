using AcmeCorpStockApp.BLL.Interfaces;
using AcmeCorpStockApp.CustomElements;
using AcmeCorpStockApp.Dtos;
using AcmeCorpStockApp.Models;
using Microsoft.AspNetCore.Mvc;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeCorpStockApp.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IManageStockAppUserService _manageStockAppUserService;

        public AccountController(IManageStockAppUserService manageStockAppUserService
            )
        {
            _manageStockAppUserService = manageStockAppUserService;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(userDTO);
            }

            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Captcha answer cannot be empty.");
                return View(userDTO);
            }

            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError("", "Incorrect captcha answer.");
                return View(userDTO);
            }
            else
            {
                ResultState result = await _manageStockAppUserService.Login(userDTO.Email, userDTO.Password,
                    userDTO.IsRememberMe);

                if (result.Status == ResultStatus.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(userDTO);
                }
            }
        }

        [HttpGet]
        [Route("SignUp")]
        public IActionResult SignUp()
        {
            ViewBag.Action = "signUp";
            UserSignUpDTO userSignUpDTO = new UserSignUpDTO
            {
                UserType = "Registrar"
            };
            return View(userSignUpDTO);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpDTO userSignUpDTO)
        {
            ViewBag.Action = "signUp";
            if (!ModelState.IsValid)
            {
                return View(userSignUpDTO);
            }
            else
            {
                StockAppUser stockAppUser = new StockAppUser
                {
                    Name = userSignUpDTO.Name,
                    Email = userSignUpDTO.Email,
                    Password = userSignUpDTO.Password,
                    Type = userSignUpDTO.UserType,
                };

                ResultState result = await _manageStockAppUserService.Register(stockAppUser);

                if (result.Status == ResultStatus.Succeeded)
                {
                    ResultState loggedIn = await _manageStockAppUserService.Login(userSignUpDTO.Email, userSignUpDTO.Password, false);
                    if (loggedIn.Status == ResultStatus.Succeeded)
                    {
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        return RedirectToAction("login", "account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(userSignUpDTO);
                }
            }
        }

        [HttpGet]
        [Route("RecoverPasswordUsingEmail")]
        public IActionResult RecoverPasswordUsingEmail()
        {
            return View();
        }

        [HttpPost]
        [Route("RecoverPasswordUsingEmail")]
        public async Task<IActionResult> RecoverPasswordUsingEmail(UserPasswordRecoverByEmailDTO model)
        {
            if (ModelState.IsValid)
            {
                ResultState result = await _manageStockAppUserService.PasswordRecovery(model.Email);

                if (result.Status == ResultStatus.Succeeded)
                {

                    return View("AcmePasswordRecovered");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("ChangePassword")]
        [CustomAuthAttribute]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ChangePassword")]
        [CustomAuthAttribute]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordDTO);
            }
            else
            {
                var result = await _manageStockAppUserService.ChangePassword(_manageStockAppUserService.GetCurrentUserFromCookie().Email, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);

                if (result.Status == ResultStatus.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View(changePasswordDTO);
                }
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        [CustomAuthAttribute]
        public async Task<IActionResult> GetUsers()
        {
            List<StockAppUser> users = await _manageStockAppUserService.GetAllUsers();
            List<ShowUserDTO> userDTOs = new List<ShowUserDTO> { };
            StockAppUser loggedInUser = _manageStockAppUserService.GetCurrentUserFromCookie();
            users.ForEach(user =>
            {
                if (user.RegistrarEmail == loggedInUser.Email)
                {
                    userDTOs.Add(new ShowUserDTO
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Role = user.Type
                    });
                }
            });

            return View(userDTOs);
        }

        [HttpGet]
        [Route("AddUserByRegistrar")]
        [CustomAuthAttribute]
        public IActionResult AddUserByRegistrar()
        {
            ViewBag.Action = "addUserByRegistrar";
            return View("signup");
        }

        [HttpPost]
        [Route("AddUserByRegistrar")]
        [CustomAuthAttribute]
        public async Task<IActionResult> AddUserByRegistrar(UserSignUpDTO userSignUpDTO)
        {

            ViewBag.Action = "addUserByRegistrar";
            if (!ModelState.IsValid)
            {
                return View("signup", userSignUpDTO);
            }
            else
            {
                StockAppUser currentUser = _manageStockAppUserService.GetCurrentUserFromCookie();

                StockAppUser user = new StockAppUser
                {
                    Email = userSignUpDTO.Email,
                    Password = userSignUpDTO.Password,
                    Name = userSignUpDTO.Name,
                    Type = userSignUpDTO.UserType,
                    RegistrarEmail = currentUser.Email
                };

                ResultState result = await _manageStockAppUserService.Register(user);

                if (result.Status == ResultStatus.Succeeded)
                {
                    return RedirectToAction("getUsers", "account");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    return View("signUp", userSignUpDTO);
                }
            }
        }
    }
}
