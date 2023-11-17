using ifood_core_api_7.Interfaces;
using ifood_core_api_7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ifood_core_api_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var data = await _unitOfWork.ProductRepository.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Product product)
        {
            var data = await _unitOfWork.ProductRepository.Insert(product);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(Product product)
        {
            var data = await _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Product product)
        {
            var data = await _unitOfWork.ProductRepository.Delete(product.Id);
            await _unitOfWork.CompleteAsync();
            return Ok(data);
        }
    }
}
