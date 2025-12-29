using System;
using System.Data;

namespace Domain.UnitOfWork.Uow
{
    public class SedUnitOfWorkOptions : ISedUnitOfWorkOptions
    {
        /// <summary>
        /// Default: false.
        /// </summary>
        public bool IsTransactional { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public TimeSpan? Timeout { get; set; }

        public SedUnitOfWorkOptions()
        {

        }

        public SedUnitOfWorkOptions(bool isTransactional = false, IsolationLevel? isolationLevel = null, TimeSpan? timeout = null)
        {
            IsTransactional = isTransactional;
            IsolationLevel = isolationLevel;
            Timeout = timeout;
        }

        public SedUnitOfWorkOptions Clone()
        {
            return new SedUnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout
            };
        }
    }
}