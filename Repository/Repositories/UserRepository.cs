using Common.Params.Base;
using Microsoft.EntityFrameworkCore;
using Repository.CustomModel;
using Repository.EF;
using Repository.Queries;

namespace Repository.Repositories
{
    public class UserRepository : BaseRepositorySql<BCC01_User>, IUserRepository
    {
        private Query<BCC01_User> _query;
        public UserRepository() : base()
        {
            _query = new Query<BCC01_User>(_db);
        }
        public async Task<List<BCC01_User>> GetListUser()
        {
            return await _db.BCC01_User.AsNoTracking().ToListAsync();
        }
    }
}
