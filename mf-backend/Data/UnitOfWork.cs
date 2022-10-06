using instaapp_backend.Core.IConfiguration;
using mf_backend.Core.IRepositories;
using mf_backend.Core.Repositories;
using mf_backend.Models;
using Microsoft.AspNetCore.Identity;
using Polly;

namespace mf_backend.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MfContext _context;
        private readonly ILogger _logger;
        public IBpkbRepository Bpkb { get; private set; }
        public IStorageRepository Storage { get; private set; }

        public UnitOfWork(MfContext context, ILoggerFactory logger, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _logger = logger.CreateLogger("logs");
            Bpkb = new BpkbRepository(context, _logger);
            Storage = new StorageRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
