using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Deneme.DAL.Class.UserClasses;
using Deneme.DAL.DataContexts;
using Deneme.DAL.Enums;
using Deneme.WepApi.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Deneme.WepApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext db;
        private readonly IConfiguration configuration;
        public UsersController(DataContext _db, IConfiguration _configuration)
        {
            db = _db;
            configuration = _configuration;
        }
        [HttpPost]
        public IActionResult Register(UserClass user,string Password)
        {
            byte[] passwordHash, passwordSald;

            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSald = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.userPassword));
            }
            user.PasswordSalt = passwordSald;
            user.PasswordHash = passwordHash;
            user.CreatedBy = 0;
            user.CreatedDate = DateTime.Now;
            user.LastUpdateBy = 1;
            user.LastUpdateDate = DateTime.Now;
            user.ObjectStatus =ObjectStatus.NonDeleted;
            user.Status = Status.Active;

            db.Add(user);
            db.SaveChanges();
            

            return Ok();
        }
        [HttpPost]
        public IActionResult Login(UserLoginModel userLogin)
        {
            var user = db.Users.FirstOrDefault(t => t.UserName == userLogin.UserName);
            if (user==null)
            {
                return null;
            }
            //Pasport Kontrol
            

            using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userLogin.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        //HAslanmiş şifreler birbirine uymuyorsa
                        return null;
                    }
                }
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            //token üretme kodu!!
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("UserJSON", JsonSerializer.Serialize(user)),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDecriptor);
            var tokenstring = tokenHandler.WriteToken(token);
            return Ok(tokenstring);
        }
    }
}