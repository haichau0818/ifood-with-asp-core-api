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

        [HttpPost("Create")]
        public async Task<IActionResult> Create(User user)
        {
            var data = await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
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
