using Domain.Common;
using Domain.UnitOfWork.Uow;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Application.IntegrationTests
{
    using static TestingInfrastructure;

    public class TestBase
    {

        protected IUnitOfWorkManager uowManager;
        protected IUnitOfWork uow;
        /// <summary>
        /// This method is called before every test
        /// </summary>
        /// <returns></returns>
        [SetUp]
        public void TestSetUp()
        {
            ResetState().Wait();
            uowManager = ServiceLocator.ServiceProvider.GetService<IUnitOfWorkManager>();
            uow = uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true }, true);
        }


        /// <summary>
        /// this method called after every test
        /// </summary>
        [TearDown]
        public void TestTearDown()
        {
            uow.RollbackAsync().Wait();
            uow.Dispose();
        }

        /// <summary>
        /// this method called after all test
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            uowManager = null;
        }

        /// <summary>
        /// this method called to update scopeFactory and create new unit of work because scope is changed
        /// </summary>
        public void UpdateScopeFactory(ServiceCollection serviceCollection)
        {
            TestingInfrastructure.UpdateScopeFactory(serviceCollection);
            RefreshUoW(rollback: true);
        }

        /// <summary>
        /// this method called to create new unit of work and rollback previous 
        /// </summary>
        public void RefreshUoW(bool rollback = false)
        {

            if (rollback)
            {
                uow.RollbackAsync().Wait();
                uow.Dispose();
            }
            else
            {
                uow.CompleteAsync().Wait();
                uow.Dispose();
            }

            uowManager = ServiceLocator.ServiceProvider.GetService<IUnitOfWorkManager>();
            uow = uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true }, true);
        }

    }
}
