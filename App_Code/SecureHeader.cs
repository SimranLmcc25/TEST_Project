using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hideservername
{
    public class SecureHeader : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;           
        }
        public void Dispose() { }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var response = application.Context.Response;

            //Remove server information from response headers
            //response.Headers["Content-Type"] = "";  // not done            
            response.Headers.Remove("Content-Type");
            response.Headers.Remove("Content-Encoding");
            response.Headers.Remove("Vary");
            response.Headers.Remove("Server");
            response.Headers.Remove("X-SourceFiles");
            response.Headers.Remove("X-UA-Compatible");
            response.Headers.Remove("Content-Length");
            response.Headers.Remove("Date");
            response.Headers.Remove("Connection");

            //response.Cookies["ASP.NET_SessionId"].Secure = true;
            //
            response.Headers["Content-Type"] = "";  // not done
            response.Headers["Content-Encoding"] = "";
            response.Headers["Vary"] = "";
            response.Headers["Server"] = "";
            response.Headers["X-SourceFiles"] = "";
            response.Headers["X-UA-Compatible"] = ""; //not done
            response.Headers["Content-Length"] = ""; //not done
            response.Headers["Date"] = "";
            response.Headers["Connection"] = "";

        }
        
    }
}