using Extensions;
using System;
using System.Collections.Generic;

namespace Domain.UnitOfWork.Uow
{
    [Serializable]
    public class ConnectionStrings : Dictionary<string, string>
    {
        public const string DefaultConnectionStringName = "Default";

        public string Default
        {
            get => this.GetOrDefault(DefaultConnectionStringName);
            set => this[DefaultConnectionStringName] = value;
        }
    }

    public class SedDbConnectionOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }

        public SedDbConnectionOptions()
        {
            ConnectionStrings = new ConnectionStrings();
        }
    }
}