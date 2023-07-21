using AutoMapper;
using Common.Commons;
using Example_Project.Config.Abtracts;
using Repository.EF;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example_Project.Services.Implement
{
    public class UserService : BaseService, IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(
            ILogger logger,
            IConfigManager config,
            IMapper mapper,
            IUserRepository userRepository
            ) : base(config, logger, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseService<List<BCC01_User>>> GetAll()
        {
            try
            {
                List<BCC01_User> result = await _userRepository.GetListUser();

                return new ResponseService<List<BCC01_User>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseService<List<BCC01_User>>(ex);
            }
        }
    }
}
