using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly DataContext _Context;
        public UsersController(DataContext Context ){
            _Context = Context;
        }
        [HttpGet]
       [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser() => await _Context.User.ToListAsync();


        //api/user/3
        [HttpGet("{id}")]
         [Authorize]
        public async Task< ActionResult<AppUser>> GetById(int id){
            return await _Context.User.FindAsync(id);

        }
    }
}