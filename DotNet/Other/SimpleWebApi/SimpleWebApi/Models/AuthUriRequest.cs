using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Models
{
    public class AuthUriRequest
    {
        [Required]
        [JsonProperty("device_guid")]
        public string DeviceGuid { get; set; }
        [Required]
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
