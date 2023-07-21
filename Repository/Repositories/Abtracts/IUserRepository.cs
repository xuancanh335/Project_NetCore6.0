using Repository.EF;
using Repository.EF;

namespace Repository.Repositories
{
    public interface IUserRepository
    {
        Task<List<BCC01_User>> GetListUser();
    }
}