using mf_backend.Core.IRepositories;

namespace instaapp_backend.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IBpkbRepository Bpkb { get; }
        IStorageRepository Storage { get; }
        Task CompleteAsync();
    }
}
