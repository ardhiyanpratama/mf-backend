﻿using mf_backend.Core.IRepositories;
using mf_backend.Models;

namespace mf_backend.Core.Repositories
{
    public class BpkbRepository : GenericRepository<TrBpkb>, IBpkbRepository
    {
        public BpkbRepository(MfContext mfContext, ILogger logger) : base(mfContext, logger)
        {

        }

        public override async Task<bool> AddAsync(TrBpkb trBpkb)
        {
            try
            {
                trBpkb.BpkbDateIn = DateTime.Now;

                await dbSet.AddAsync(trBpkb);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }
}
