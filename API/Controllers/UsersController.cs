using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

     [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService, ILogger<UsersController> logger){
            _userRepository = userRepository;
            _mapper = mapper;
            _photoService=photoService;
            _logger = logger;
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
        [HttpPut]
        public async Task<ActionResult> UpdateUser(memberUpdateDTO memberUpdateDto){
       
            var user= await _userRepository.getUserByUserNameAsync(User.GetUsername());

            if(user==null) return NotFound();

            _mapper.Map(memberUpdateDto,user);
            if(await _userRepository.saveAllAsync()) return NoContent();
            return BadRequest("Failed to update resources");

        
        }
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file){
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            var username = User.GetUsername();
            if (string.IsNullOrEmpty(username))
            {
                _logger.LogError("Username not found in token");
                return Unauthorized();
            }

            var user = await _userRepository.getUserByUserNameAsync(username);
            if (user == null)
            {
                _logger.LogError($"User not found for username: {username}");
                return NotFound("User not found");
            }

            var result = await _photoService.AddPhotoAsync(file);
            if (result == null)
            {
                _logger.LogError("Photo upload failed - result is null");
                return BadRequest("Failed to upload photo");
            }

            if (result.Error != null)
            {
                _logger.LogError($"Photo upload failed: {result.Error.Message}");
                return BadRequest(result.Error.Message);
            }

            if (string.IsNullOrEmpty(result.SecureUrl?.AbsoluteUri))
            {
                _logger.LogError("Photo upload succeeded but URL is null");
                return BadRequest("Failed to get photo URL");
            }

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = user.Photos?.Count == 0
            };  
            user.Photos.Add(photo);
            if(await _userRepository.saveAllAsync()) return CreatedAtAction(nameof(GetUser),new {username=user.UserName},_mapper.Map<PhotoDTO>(photo));  
            
            return BadRequest("Problem adding photo");
        }
    }
}