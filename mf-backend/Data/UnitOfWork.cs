using instaapp_backend.Core.IConfiguration;
using Microsoft.AspNetCore.Identity;

namespace mf_backend.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ILogger _logger;

        public UnitOfWork(ILoggerFactory logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger.CreateLogger("logs");
        }

        public async Task CompleteAsync()
        {
            //await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            //_context.Dispose();
        }
    }
}
