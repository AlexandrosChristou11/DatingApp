using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
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
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
    
        
        return await _context.Users.ToListAsync();
        
    }


    // ---------------------------------------------
    //   Returns the user from the database which  
    //   contains the parameter passed as an ID
    // ---------------------------------------------
    // api/users/3
    [HttpGet("{Id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id){
        
        // Get users from database
        return await _context.Users.FindAsync(id);
        
    }

}
}