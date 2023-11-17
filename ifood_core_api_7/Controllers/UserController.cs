using BCrypt.Net;
using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;
using ifood_core_api_7.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ifood_core_api_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
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
            return Ok(validateUser);

        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(User user)
        {
            var data = await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(User user)
        {
            var data = await _unitOfWork.UserRepository.Delete(user.Id);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }
    }
}
