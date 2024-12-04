using LE.Infrastructure.Services;
using LE.Common.Api.Users;
using LE.Infrastructure.Repositories;
using LE.DomainEntities;
using System.Security.Authentication;
using LE.Common.Exceptions;
using LE.Services.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace LE.Services
{
    public class UserService : IUserService
    {
        public const string UserId = "UserId";
        private readonly JwtOption _tokenOptions;

        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IOptions<JwtOption> tokenOptions, IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _tokenOptions = tokenOptions.Value;
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> CreateAsync(UserDto dto)
        {
            var passwordHash = _passwordHasher.HashPassword(dto.Password);

            var user = new User()
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                Username = dto.Username
            };

            await _repository.CreateAsync(user) ;

            return user.Id;
        }

        public async Task<LoginResponse> Login(LoginRequest dto)
        {
            var user = await _repository.GetAsync(dto.Email) ?? throw new RecordNotFoundException();

            if(!_passwordHasher.VerifyHashedPassword(user.PasswordHash, dto.Password))
            {
                throw new AuthenticationException();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(UserId, user.Id.ToString())
            };

            var accessToken = TokenFactory.GenerateToken(claims, _tokenOptions);

            return new LoginResponse()
            {
                AuthToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                UserId = user.Id.ToString()
            };
        }
    }
}
