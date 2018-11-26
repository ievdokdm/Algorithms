using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Models
{
    public class RefreshTokenRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
