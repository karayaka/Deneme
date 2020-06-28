using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Deneme.Entity.Helpers
{
    public static class JwtExtension
    {
        public static void AddAplicationError(this HttpResponseHeaders httpResponse,string message)
        {
            httpResponse.AddAplicationError(message);
            httpResponse.Add("Access-Control-Allow-Origin", "*");
            httpResponse.Add("Access-Control-Expose-Header", "Application-Error");
        }
    }
}
