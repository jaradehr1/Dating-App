using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.Dtos.Users;
using DatingApp.API.Data.Repository.General;
using DatingApp.API.Data.Repository.Likes;
using DatingApp.API.Data.Repository.Users;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly ILikeRepository _repo2;
        private readonly IDatingRepository _grepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repo, ILikeRepository repo2, IDatingRepository grepo, IMapper mapper)
        {
            _mapper = mapper;
            _grepo = grepo;
            _repo = repo;
            _repo2 = repo2;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _repo.GetUser(currentUserId);
            userParams.UserId = currentUserId;
            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
            }
            var users = await _repo.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            if (user == null)
            {
                NotFound("User not found");
            }

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdate)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(id);
            _mapper.Map(userForUpdate, userFromRepo);

            if(await _grepo.SaveChangesAsync())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var like = await _repo2.GetLike(id, recipientId);

            if (like != null)
            {
                return BadRequest("You already liked this user!");
            }

            if (await _repo.GetUser(recipientId) == null)
            {
                return NotFound();
            }

            like = new Like
            {
                LikerId = id,
                LikeeId = recipientId
            };

            _grepo.Add<Like>(like);

            if(await _grepo.SaveChangesAsync())
                return Ok();

            return BadRequest("Failed to like user");
        }
    }
}