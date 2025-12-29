using System;

namespace Application.Common.Helper
{
    /// <summary>
    /// Specifies the propery this attribute is applied to definition property feature.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PropertyDefinitionAttribute : Attribute
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AuthorizeAttribute"/> class.
        /// </summary>
        public PropertyDefinitionAttribute() { }

        /// <summary>
        /// Gets or sets that should be shown in query biulder
        /// </summary>
        public bool BeShown { get; set; }

        /// <summary>
        /// Gets or sets name of property in entity if its name is not the same as entity property
        /// </summary>
        public string PropertyRepresent { get; set; }
    }
}
