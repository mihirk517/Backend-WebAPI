using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPI.Data;
using WebAPI.Migrations;
using WebAPI.Models;

namespace WebAPI.Services.UserAuthService
{
    public class UserAuthService : IUserAuthService
    {     
        
        private readonly DatabaseContext _context;        
        public UserAuthService( DatabaseContext context) 
        {
            
            _context = context;
            
        }
        async Task<User> IUserAuthService.Register(UserDto request)
        {
            if(_context.Users.Any(x=> x.Username == request.Username))
            {
                Debug.WriteLine($"{request.Username} already present");
                return null;
            }
            var user = new User();
            user.Username =  request.Username;
            user.PasswordHash = CreatePasswordHash(request.Password);
            //user.Timestamp= DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
            
        }

        async Task<User> IUserAuthService.Verify(UserDto request)
        {            
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);

            if (user == null)
            {
                Debug.WriteLine($"Invalid credentials");
                return null;  
            }
            if (!VerifyPasswordHash(request.Password,user.PasswordHash))
            {
                Debug.WriteLine("Password Incorrect");
                return null;
            }          
            return user;
        }
       
        private string CreatePasswordHash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return passwordHash;
        }

        private bool VerifyPasswordHash(string password, string? passwordHash)
        {            
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
