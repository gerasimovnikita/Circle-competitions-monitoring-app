﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CircleCompetitions.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using CircleCompetitions.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CircleCompetitions.Controllers
{
    public class HomeController : Controller
    {
        DataContext db;
        public HomeController(DataContext dataContext)
        {
            db = dataContext;
        }

        /*Методы на возврат данных*/
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<Competition> GetCompetitions()
        {
            return db.Competition.ToList();
        }

        /*Методы на возвращение представление*/
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        /*Post методы*/
        [HttpPost]
        public async Task<IActionResult> Login(string EMail, string Password)
        {
            User user =  db.User.FirstOrDefault(u => u.E_Mail == EMail && u.Password == Password);
            if (user != null)
            {
                await Authenticate(user);              
                return RedirectToAction("Index", "Home");
            }
            return View("LoginError");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]    
        public async Task<IActionResult> Register(RegisterModel model)
        {
            User us = await db.User.FirstOrDefaultAsync(u => u.E_Mail == model.Email);
            if (us == null)
            {
                // добавляем пользователя в бд
                db.User.Add(new User { UserName = model.Name, Nickname = model.Nickname, E_Mail = model.Email, Password = model.Password, RightsOfUser = Convert.ToString("User") });
                await db.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);
        }
        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }


        //Метод для хранения данных о пользователе в куки
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.E_Mail),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RightsOfUser)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}