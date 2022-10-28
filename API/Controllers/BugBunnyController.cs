using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugBunnyController:BaseApiController
    {
          private readonly DataContext _context;
           public BugBunnyController(DataContext context){
                _context=context;
           }
           [Authorize]
           [HttpGet("auth")]
           public ActionResult<string> GetSecreat(){
            return "ABC";
           }
          [HttpGet("Not-Found")]
           public ActionResult<AppUser> GetNotFound(){
             var user=_context.User.Find(-1);
             if(user==null) return NotFound();
             return Ok(user);
           }
    
           [HttpGet("Server-Error")]
           public ActionResult<string> GetServerError(){
            var user=_context.User.Find(-1);
            return user.ToString();
           }
 
           [HttpGet("bad-request")]
           public ActionResult<string> GetbadRequest(){
            return BadRequest("this is a bad request");
           }
    }
}