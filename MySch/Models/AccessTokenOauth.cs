using System;
using System.Collections.Generic;

namespace MySch.Models
{
    public partial class AccessTokenOauth
    {
        public string openid { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public System.DateTime create_time { get; set; }
    }
}
