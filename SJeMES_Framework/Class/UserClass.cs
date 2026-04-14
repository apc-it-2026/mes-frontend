using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.Class
{
    public class UserClass
    {
        public string UserCode;
        public string UserName;
        public bool MaxWindow;

        public UserClass(string UserCode, string UserName)
        {
            this.UserCode = UserCode;
            this.UserName = UserName;
        }

        public System.Data.DataTable Permissions = new System.Data.DataTable();
    }
}
