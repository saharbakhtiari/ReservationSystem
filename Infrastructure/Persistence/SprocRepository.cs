using AutoMapper;
using Domain.Common;
using Domain.UnitOfWork.Uow;
using Infrastructure.Persistence.Extentions;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class SprocRepository : ISprocRepository
    {
        protected virtual ApplicationDbContext _dbContext => _dbContextProvider.GetDbContext();
        private readonly IDbContextProvider<ApplicationDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SprocRepository(IDbContextProvider<ApplicationDbContext> dbContextProvider, IUnitOfWorkManager unitOfWorkManager)
        {
            _dbContextProvider = dbContextProvider;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public DbCommand GetStoredProcedure(    string name,  params (string, object)[] nameValueParams)
        {
            return _dbContext
                .LoadStoredProcedure(name)
                .WithSqlParams(nameValueParams);
        }
        

        public DbCommand GetStoredProcedure(string name)
        {
            return _dbContext.LoadStoredProcedure(name);
        }

        public Task<IList<T>> ExecuteStoredProcedureAsync<T>(string name, CancellationToken cancellationToken, params (string, object)[] nameValueParams) where T : class
        {
            {
                using (var uow = _unitOfWorkManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }, requiresNew: true))
                {
                    return _dbContext
                     .LoadStoredProcedure(name)
                     .WithSqlParams(nameValueParams).ExecuteStoredProcedureAsync<T>(cancellationToken);
                }
            }
        }
    }
}
