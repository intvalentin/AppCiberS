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

namespace App.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly appciberContext _context;
        public HomeController(appciberContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login model)
        {
            string email = null, password = null;

            var user = from person in _context.Employee
                       join admin in _context.Administrator on person.EmployeeId equals admin.EmployeId where person.Email == model.Email
                       select new
                       {
                           email = person.Email,
                           password = admin.Password
                            
                       };
            foreach(var x in user)
            {
                email = x.email;
                password = x.password;
            }
            if (email != null && password != null)
            {

                var authClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authnetication"));

                var token = new JwtSecurityToken(
                    issuer: "http://dotnetdetail.net",
                    audience: "http://dotnetdetail.net",
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}