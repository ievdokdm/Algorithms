using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Models
{
    public class AuthUriResponse

    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
        [JsonProperty("pin")]
        public string Pin { get; set; }
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
    }
}
