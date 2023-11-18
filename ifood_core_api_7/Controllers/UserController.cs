using BCrypt.Net;
using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;
using ifood_core_api_7.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ifood_core_api_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public UserController(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var data = await _unitOfWork.UserRepository.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Hash Password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;

            var data = await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(User _user)
        {
            List<User> listUser = await _unitOfWork.UserRepository.GetAllAsync();
       
            User validateUser = listUser.FirstOrDefault(p => p.Email == _user.Email && BCrypt.Net.BCrypt.Verify(_user.Password, p.Password));
            if (validateUser==null)
            {
                return BadRequest("Wrong email or password.");
            }

            string token = CreateToken(validateUser);
            return Ok(token);

        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(User user)
        {
            var data = await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(User user)
        {
            var data = await _unitOfWork.UserRepository.Delete(user.Id);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }


        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>{ 
            
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"User"),

            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value!
                    ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }
    }
}
