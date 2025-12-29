using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class UserModel
    {
        public UserModel()
        {
            Attribute = new Dictionary<string, string>();
        }
        public string UserName { get; set; }
        public Dictionary<string,string> Attribute { get; set; }

        public string GetUserAttribute(string property)
        {
            if (Attribute is not null && Attribute.ContainsKey(property))
            {
                return Attribute[property];
            }
            return null;
        }
        public string this[string property]
        {
            get
            {
                if (Attribute is not null && Attribute.ContainsKey(property))
                {
                    return Attribute[property];
                }
                return null;
            }
        }
    }
}
