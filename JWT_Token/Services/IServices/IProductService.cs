using JWT_Token.DTO;

namespace JWT_Token.Services.IServices
{
    public interface IProductService
    {
        Task<ProductResponseDto> Create(ProductRegisterDto productRegisterDto);
        Task<IEnumerable<ProductResponseDto>> GetAll();
        Task<ProductResponseDto> GetById(int id);
        Task<ProductResponseDto> Update(int id, ProductRegisterDto productRegisterDto);
        Task<bool> Delete(int id);
    }
}
