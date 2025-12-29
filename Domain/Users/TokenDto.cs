using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public class TokenDto
    {
       public string access_token { get; set; }
        public string refreshToken { get; set; }

        public int expires_in { get; set; }
       
    }
}
