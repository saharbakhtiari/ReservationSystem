using Microsoft.Extensions.Options;
using System.Configuration;

namespace Domain.UnitOfWork.Uow
{
    /// <summary>
    /// Default implementation of <see cref="IConnectionStringResolver"/>.
    /// Get connection string from <see cref="ISedStartupConfiguration"/>,
    /// or "Default" connection string in config file,
    /// or single connection string in config file.
    /// </summary>
    public class DefaultConnectionStringResolver : IConnectionStringResolver
    {
        private readonly SedDbConnectionOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConnectionStringResolver"/> class.
        /// </summary>
        public DefaultConnectionStringResolver(IOptions<SedDbConnectionOptions> options)
        {
            this.options = options.Value;
        }

        public virtual string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            if(args.ContainsKey("ConnectionStringName") && !string.IsNullOrEmpty(args["ConnectionStringName"].ToString()) && options.ConnectionStrings.ContainsKey(args["ConnectionStringName"].ToString()))
            {
                return options.ConnectionStrings[args["ConnectionStringName"].ToString()];
            }
            return options.ConnectionStrings.Default;
        }
    }
}