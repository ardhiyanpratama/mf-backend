using mf_backend.Core.IRepositories;
using mf_backend.Models;

namespace mf_backend.Core.Repositories
{
    public class BpkbRepository : GenericRepository<TrBpkb>, IBpkbRepository
    {
        public BpkbRepository(MfContext mfContext, ILogger logger) : base(mfContext, logger)
        {

        }

    }
}
