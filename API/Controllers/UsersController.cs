using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class UsersController : BaseApiController
    {
        
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        
        }
    

    // ---------------------------------------------
    //   Returns a list with the users specified in 
    //   the database
    // ---------------------------------------------
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
    
        
        return await _context.Users.ToListAsync();
        
    }


    // ---------------------------------------------
    //   Returns the user from the database which  
    //   contains the parameter passed as an ID
    // ---------------------------------------------
    // api/users/3
    [Authorize]
    [HttpGet("{Id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id){
        
        // Get users from database
        return await _context.Users.FindAsync(id);
        
    }

}
}