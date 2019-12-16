using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace App.Controllers
{
    [ApiController]
    [Route("")]
   
    [Authorize]
    public class HomeController : Controller
    {
        private readonly appciberContext _context;
        public HomeController(appciberContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Bearer")))
            {
                return View();
            }

            return RedirectToAction("Index","Employees");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] Login model)
        {
            string email = null, password = null;
            int id = 0;

            var user = from person in _context.Employee
                       join admin in _context.Administrator on person.EmployeeId equals admin.EmployeId
                       where person.Email == model.Email && admin.Password == model.Password
                       select new
                       {
                           email = person.Email,
                           password = admin.Password,
                           id = person.EmployeeId

                       };
            foreach (var x in user)
            {
                email = x.email;
                password = x.password;
                id = x.id;
            }
            if (email != null && password != null)
            {


                //var authClaims = new[]
                //{
                //    new Claim(JwtRegisteredClaimNames.Sub, email),
                //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //};
                //var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YVBy0OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SBM"));

                //var token = new JwtSecurityToken(

                //    //issuer: "https://localhost:44389",
                //    //audience: "https://localhost:44389",
                //    expires: DateTime.Now.AddHours(3),
                //    claims: authClaims,
                //    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                //    );

                //HttpContext.Session.SetString("Bearer", new JwtSecurityTokenHandler().WriteToken(token));
                //HttpContext.Session.SetString("ID", id.ToString());

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    IssuedUtc = DateTime.Now,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = "/Employees"
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);



                return RedirectToAction("Index", "Employees");
            }
            return Unauthorized();
        }

        [Route("NodeInfo")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}