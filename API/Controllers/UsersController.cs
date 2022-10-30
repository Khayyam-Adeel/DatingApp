using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

     [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _Mapper;
        public UsersController(IUserRepository userRepository, IMapper Mapper){
            _userRepository = userRepository;
            _Mapper = Mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers() {
            // var user = await _userRepository.getAppUsersAsync();
            // var userToReturn = _Mapper.Map<IEnumerable<MemberDTO>>(user);
            // return Ok(userToReturn);
             var users=await _userRepository.GetMembersAsync();
            return Ok(users);

        }


        //api/user/3
        [HttpGet("{username}")]
        public async Task< ActionResult<MemberDTO>> GetUser(string UserName){
            //var user=await _userRepository.getUserByUserName(UserName);
            return await _userRepository.GetMemberAsync(UserName);

             //return _Mapper.Map<MemberDTO>(user);
            //return await _userRepository.getUserByUserName(UserName);

        }
    }
}