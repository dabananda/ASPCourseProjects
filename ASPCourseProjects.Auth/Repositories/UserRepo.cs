using ASPCourseProjects.Auth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASPCourseProjects.Auth.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AuthDbContext _context;

        public UserRepo(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IdentityUser>> UserList()
        {
            var users = await _context.Users.ToListAsync();
            var superAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Email == "superadmin@email.com");
            if (superAdmin != null)
            {
                users.Remove(superAdmin);
            }
            return users;
        }

    }
}
