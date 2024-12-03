using LE.DataAccess;
using LE.DomainEntities;
using LE.Infrastructure.Repositories;
using Task = System.Threading.Tasks.Task;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(item => item.Email == email);
        }
    }
}
