using AutoMapper;
using Common.Commons;
using Example_Project.Config.Abtracts;


namespace Example_Project.Services.Implement
{
    public class BaseService
    {
        protected readonly IConfigManager _config;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        protected BaseService(IConfigManager config, ILogger logger, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
        }       
    }
}
