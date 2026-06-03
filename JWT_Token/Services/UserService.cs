using JWT_Token.DTO;
using JWT_Token.Entities;
using JWT_Token.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Token.Services
{
    public class UserService : UserInterface
    {

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationDbContext context ,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //mannual mapping
        public async Task<UserResponseDto> Register(UserRegisterDto userRegisterDto)
        {
            //new to create user object

            var user = new User
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Username = userRegisterDto.Username,
                Password = userRegisterDto.Password,
                ConfirmPassword = userRegisterDto.ConfirmPassword,
                Role = userRegisterDto.Role
            };

            //Add the user to the database
            await _context.Users.AddAsync(user);
            // then we can save the changes to the database

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role
            };
        }


        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            //we  find the user with the help of username

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequestDto.Username);


            // if user is not found then we will return null


            if (user == null)
            {
                return null;
            }


            var token= GenerateToken(user);


            return new LoginResponseDto
            {
                Token = token,
                User = new UserResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Role = user.Role
                }
            };
        }


        #region private methods

        private string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("jwt");


            //here we can convert the secret key to byte array


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));


            // then we can create signing credentials using the secret key and the security algorithm


            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



            //payload is the information that we want to include in the token, we can include the user id and role in the payload




            //claims


            var claims = new[]
            {

                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            //create the token//signature algorithm and the payload


            var token = new JwtSecurityToken(

                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            //then we can write the token to a string and return it


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        #endregion
    }
}
