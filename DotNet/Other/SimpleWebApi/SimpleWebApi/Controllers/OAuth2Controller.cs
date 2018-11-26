using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.BL;
using SimpleWebApi.Models;

namespace SimpleWebApi.Controllers
{
    [Route("api/brands/{brand}/providers/{provider}/oauth2")]
    [ApiController]
    public class OAuth2Controller : ControllerBase
    {
        [HttpPost("request_auth_uri")]
        public ActionResult<AuthUriResponse> PostAuthUri([FromRoute] string brand, [FromRoute] string provider, [FromBody] AuthUriRequest loginInfo)
        {
            int id = 26 * 26 * 26 * 26 * 26 - 1;
            var pin = PinConverter.IntToPin(id);
            var test = PinConverter.PinToInt(pin);
            return new AuthUriResponse { Pin = pin, Uri = "https://uei.com/login", RedirectUri = "https://uei.com/login?Pin="+pin };
        }

        [HttpPost("refresh_token")]
        public ActionResult<RefreshTokenResponse> PostRefeshToken([FromRoute] string brand, [FromRoute] string provider, [FromBody] RefreshTokenRequest refreshToken)
        {
            return new RefreshTokenResponse { AccessToken = "QWE", ExpiresIn = 12, RefreshToken = "ASD", Scope = "Scope", TokenType = "Barier" };
        }

        [HttpPost("send_token")]
        public void PostSendToken([FromRoute] string brand, [FromRoute] string provider, [FromBody] SendTokenRequest sendToken)
        {
            
        }

        [HttpGet("auth_uri")]
        public ActionResult<AuthUriResponse> GetAuthUri([FromRoute] string brand, [FromRoute] string provider, [FromQuery(Name = "device_uid")] string deviceUid, [FromQuery(Name = "client_id")] string clientId)
        {
            return new AuthUriResponse { Pin="ABCDE", Uri="https://uei.com/login", RedirectUri= "https://uei.com/login?Pin=ABCDE" };
        }
    }
}