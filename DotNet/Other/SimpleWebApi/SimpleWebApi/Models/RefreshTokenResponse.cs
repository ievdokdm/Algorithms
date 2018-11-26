﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Models
{
    public class RefreshTokenResponse
    {
        [Required]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [Required]
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [Required]
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [Required]
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [Required]
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
