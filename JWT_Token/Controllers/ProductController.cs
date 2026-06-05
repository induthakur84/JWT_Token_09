using JWT_Token.DTO;
using JWT_Token.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Token.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(ProductRegisterDto productRegisterDto)
        {
            var product = await _productService.Create(productRegisterDto);
            return Ok(product);

        }
        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ProductRegisterDto productRegisterDto)
        {
            var product = await _productService.Update(id, productRegisterDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
