using LE.Common.Api.Users;
using LE.Common.Exceptions;
using LE.DomainEntities;
using LE.Infrastructure.Repositories;
using LE.Infrastructure.Services;
using LE.Services.Options;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

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
            var normalizeEmail = dto.Email.ToLower();
            var normalizeUsername = dto.Username.ToLower();

            if (await _repository.IsReservedAsync(normalizeEmail, normalizeUsername))
                throw new ReservedFieldException($"Email or username reserved");

            var user = new User()
            {
                Email = normalizeEmail,
                PasswordHash = passwordHash,
                Username = normalizeUsername
            };

            await _repository.CreateAsync(user);

            return user.Id;
        }

        public async Task<LoginResponse> Login(LoginRequest dto)
        {
            var user = await _repository.GetAsync(dto.Email) ?? throw new RecordNotFoundException($"No user with email: '{dto.Email}'");

            if (!_passwordHasher.VerifyHashedPassword(user.PasswordHash, dto.Password))
            {
                throw new AuthenticationException("Wrong password");
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
