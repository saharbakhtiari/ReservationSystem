using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application.Common.Security
{
    /// <summary>
    /// Custom resolver used during logging behaviour, to ommit sensitive information when using JsonConvert.Serialize method
    /// </summary>
    public class IgnoreSensitivePropertiesResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            foreach (JsonProperty prop in properties)
            {
                PropertyInfo pi = type.GetProperty(prop.UnderlyingName);
                if (pi != null)
                {
                    // Do not serialize properties that have the Do Not Log attribute
                    if (pi.GetCustomAttribute<RedactFromLogsAttribute>() != null)
                    {

                        // uncomment below to REMOVE the property and the value from serialization
                        //prop.ShouldSerialize = obj => false;

                        // I would much prefer to override the value with strings
                        prop.ValueProvider = new StringValueProvider(pi, "**REDACTED**");
                    }
                }
            }

            return properties;
        }


    }

    /// <summary>
    /// Newtonsoft.Json string provider to help override the property values used above
    /// </summary>
    public class StringValueProvider : IValueProvider
    {
        private PropertyInfo _targetProperty;
        private string _substitutionValue;

        public StringValueProvider(PropertyInfo targetProperty, string substitutionValue)
        {
            _targetProperty = targetProperty;
            _substitutionValue = substitutionValue;
        }

        // SetValue gets called by Json.Net during deserialization.
        // The value parameter has the original value read from the JSON;
        // target is the object on which to set the value.
        public void SetValue(object target, object value)
        {
            _targetProperty.SetValue(target, value);
        }

        // GetValue is called by Json.Net during serialization.
        // The target parameter has the object from which to read the value;
        // the return value is what gets written to the JSON
        public object GetValue(object target)
        {
            return _substitutionValue;
        }
    }
}
