using LE.DomainEntities;
using Task = System.Threading.Tasks.Task;

namespace LE.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        public Task CreateAsync(User user);

        public Task<User?> GetAsync(string email);
    }
}
