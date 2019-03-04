using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.DTOs;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        public IAuthRepository _repo { get; }

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            //validate request

            userForRegisterDTO.Username=userForRegisterDTO.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDTO.Username))
            return BadRequest("Username already exist"); 

            var userToCreate=new User
            {
                   Username=userForRegisterDTO.Username
            };

            var createdUser=await _repo.Register(userToCreate,userForRegisterDTO.Password);

            return StatusCode(201);
        }

    }
}