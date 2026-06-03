using JWT_Token.DTO;

namespace JWT_Token.Services.IServices
{

    // interface for user service

    //just declaring the interface for user service, we will implement it in the UserService class
    public interface UserInterface
    {
        Task<UserResponseDto> Register(UserRegisterDto userRegisterDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
