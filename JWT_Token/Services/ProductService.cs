using JWT_Token.DTO;
using JWT_Token.Entities;
using JWT_Token.Services.IServices;

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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponseDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductResponseDto> Update(int id, ProductRegisterDto productRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
