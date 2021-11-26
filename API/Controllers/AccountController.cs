using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
     
         private readonly DataContext _context;
        public AccountController(DataContext context, ITokenService tokenService )
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

            if (await UserExists(registerDto.userName)) return BadRequest("Username already exists!");

            using var hmac = new HMACSHA512(); 
            
            var user = new AppUser{
                userName = registerDto.userName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

              
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // make asynchronous call with database and save the user

            return new UserDto{
                userName = user.userName,
                Token = _tokenService.CreateToken(user)
            };

        }

    
        // -----------------------------------------------
        // Validates whether the username exists in the database
        // and returns the appropriate bool expression 
        // -----------------------------------------------
        private async Task<bool> UserExists (string username){

            return await _context.Users.AnyAsync( x=> x.userName ==username.ToLower() );

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto ){
          
            var user = await _context.Users
                    .SingleOrDefaultAsync( x=> x.userName == loginDto.userName );

            if (user == null) return Unauthorized("Invalid Username!!");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i =0; i < computedHash.Length; i ++){
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password!");
            }
            
            return new UserDto{
                userName = user.userName,
                Token = _tokenService.CreateToken(user)
            };

        }
    }
}