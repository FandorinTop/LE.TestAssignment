using LE.Infrastructure.Services;
using LE.Common.Api.Users;
using LE.Infrastructure.Repositories;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;
using LE.DomainEntities;
using System.Security.Authentication;

namespace LE.Services
{
    public class UserService : IUserService
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HashingIterationsCount = 10101;
        private readonly IUserRepository _repository;

        public async Task<Guid> CreateAsync(UserDto dto)
        {
            var passwordHash = HashPassword(HashPassword(dto.Password));

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

            if(!VerifyHashedPassword(user.PasswordHash, dto.Password))
            {
                throw new AuthenticationException();
            }

            return new LoginResponse()
            {
                AuthToken = ,
                UserId = user.Id.ToString()
            };
        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer;

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HashingIterationsCount))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(HashByteSize);
            }

            byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer, 0, dst, SaltByteSize + 1, HashByteSize);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] _passwordHashBytes;

            int _arrayLen = (SaltByteSize + HashByteSize) + 1;

            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            byte[] src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != _arrayLen) || (src[0] != 0))
            {
                return false;
            }

            byte[] _currentSaltBytes = new byte[SaltByteSize];
            Buffer.BlockCopy(src, 1, _currentSaltBytes, 0, SaltByteSize);

            byte[] _currentHashBytes = new byte[HashByteSize];
            Buffer.BlockCopy(src, SaltByteSize + 1, _currentHashBytes, 0, HashByteSize);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, _currentSaltBytes, HashingIterationsCount))
            {
                _passwordHashBytes = bytes.GetBytes(SaltByteSize);
            }

            return AreHashesEqual(_currentHashBytes, _passwordHashBytes);

        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
