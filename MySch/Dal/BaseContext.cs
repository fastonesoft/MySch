using MySch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySch.Dal
{
    public class BaseContext : MySchContext
    {
        public BaseContext()
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}