using JWT_Token.DTO;
using JWT_Token.Entities;
using JWT_Token.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace JWT_Token.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<ProductResponseDto> Create(ProductRegisterDto productRegisterDto)
        {
            var product = new Product
            {
                Name = productRegisterDto.Name
            };
            await _applicationDbContext.Products.AddAsync(product);
            await _applicationDbContext.SaveChangesAsync();
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _applicationDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            _applicationDbContext.Products.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAll()
        {
            return await _applicationDbContext.Products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
        }

        public async Task<ProductResponseDto> GetById(int id)
        {
           var product = await _applicationDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name
            };
        }

        public async Task<ProductResponseDto> Update(int id, ProductRegisterDto productRegisterDto)
        {
            var product = await _applicationDbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            product.Name = productRegisterDto.Name;
            _applicationDbContext.Products.Update(product);
            await _applicationDbContext.SaveChangesAsync();
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name
            };
        }
    }
}
