using Microsoft.AspNetCore.Identity;

namespace ASPCourseProjects.Auth.Repositories
{
    public interface IUserRepo
    {
        Task<IEnumerable<IdentityUser>> UserList();
    }
}
