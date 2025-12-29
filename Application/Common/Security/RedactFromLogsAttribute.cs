using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Security
{
    /// <summary>
    /// Specifies the property to not be logged, intended for private information to be omitted from logs from example
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class RedactFromLogsAttribute : Attribute
    {
    }
}
