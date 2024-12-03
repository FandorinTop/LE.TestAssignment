using LE.Common.Api.Users;

namespace LE.Infrastructure.Services
{
    public interface IUserService
    {
        public Task<Guid> CreateAsync(UserDto dto);

        public Task<LoginResponse> Login(LoginRequest dto);
    }
}
