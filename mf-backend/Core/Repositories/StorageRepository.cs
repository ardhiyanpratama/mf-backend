using mf_backend.Core.IRepositories;
using mf_backend.Models;

namespace mf_backend.Core.Repositories
{
    public class StorageRepository : GenericRepository<MsStorageLocation>, IStorageRepository
    {
        public StorageRepository(MfContext mfContext, ILogger logger) : base(mfContext, logger)
        {

        }
    }
}
