using Common.Commons;
using Repository.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example_Project.Services.Implement
{
    public interface IUserService
    {
        Task<ResponseService<List<BCC01_User>>> GetAll();
    }
}