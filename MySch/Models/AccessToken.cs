using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class AccessToken
    {
        public System.DateTime create_time { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
