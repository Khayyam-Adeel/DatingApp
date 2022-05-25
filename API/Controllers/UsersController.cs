using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors(origins: "http://www.example.com", headers: "*", methods: "*")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _Context;
        public UsersController(DataContext Context ){
            _Context = Context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser() => await _Context.User.ToListAsync();


        //api/user/3
        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetById(int id){
            return await _Context.User.FindAsync(id);

        }
    }
}