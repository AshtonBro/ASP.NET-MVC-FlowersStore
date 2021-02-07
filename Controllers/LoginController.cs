using System;
using Microsoft.AspNetCore.Mvc;
using FlowersStore.ViewModels;
using FlowersStore.Helpers;
using System.Linq;
using FlowersStore.Data;
using FlowersStore.Models;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace FlowersStore.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        Crypto myCryptoHasher = new Crypto();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonRedirect> LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var claims = new List<Claim>();
                if (model.LoginUser.Name == "Admin")
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.LoginUser.Name),
                        new Claim(ClaimTypes.Role, "Administrator")
                    };
                } else
                {
                    claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.LoginUser.Name),
                        new Claim(ClaimTypes.Role, "User")
                    };
                }
                
                var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                var claimPrincial = new ClaimsPrincipal(claimIdentity);

                using (StoreDBContext db = new StoreDBContext())
                {
                    var user = db.Users.FirstOrDefault(f => f.Name == model.LoginUser.Name);
                    if (user == null) return new JsonRedirect("A such user isn't registered.");

                    var isPasswordValid = myCryptoHasher.VerifyHashedPassword(user.Password, model.LoginUser.Password);
                    if (isPasswordValid)
                    {
                        await HttpContext.SignInAsync("Cookie", claimPrincial);

                        return new JsonRedirect(new Link(nameof(StoreController), nameof(StoreController.Index)));
                    }
                    return new JsonRedirect("Invalid login or password.");
                }
            }

            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonRedirect RegistrationUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (StoreDBContext db = new StoreDBContext())
                {
                    var user = db.Users.FirstOrDefault(f => (f.Name == model.RegistrationUser.Name) 
                    || (f.Email == model.RegistrationUser.Email));

                    if (user == null)
                    {
                        var passwordHashed = myCryptoHasher.HashPassword(model.RegistrationUser.Password);
                        var userId = Guid.NewGuid();
                        User userRegistration = new User()
                        {
                            UserId = userId,
                            Name = model.RegistrationUser.Name,
                            SecondName = model.RegistrationUser.SecondName,
                            Phone = model.RegistrationUser.Phone,
                            Email = model.RegistrationUser.Email,
                            Password = passwordHashed,
                            DateCreated = DateTime.Now
                        };

                        Basket newBasketForUser = new Basket()
                        {
                            BasketId = Guid.NewGuid(),
                            UserId = userId,
                            DateCreated = DateTime.Now

                        };

                        db.Baskets.Add(newBasketForUser);
                        db.Users.Add(userRegistration);
                        db.SaveChanges();
                        return new JsonRedirect("You successfully registered. Try to login");
                    }
                    return new JsonRedirect("Such User or Email is registered.");
                }

            }
            var error = ModelState.Values.FirstOrDefault(f => f.Errors.Count > 0).Errors.FirstOrDefault();
            return new JsonRedirect(error.ErrorMessage);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookie");
            return View("~/Views/Home/Index.cshtml");
        }

      
    }
}
