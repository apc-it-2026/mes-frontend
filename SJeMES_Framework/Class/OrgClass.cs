using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.Class
{
    public class OrgClass
    {
        public string Org;
        public string OrgName;
        public string DBType;
        public string DBServer;
        public string DBName;
        public string DBUser;
        public string DBPassword;

        public UserClass User;

        public OrgClass()
        {
            User = new Class.UserClass(string.Empty, string.Empty);
        }
    }
}
