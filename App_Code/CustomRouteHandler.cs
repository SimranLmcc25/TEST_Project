using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

public class CustomRouteHandler : IRouteHandler
{
    string _virtualPath;
    public CustomRouteHandler(string virtualPath)
    {
        _virtualPath = virtualPath;
    }

    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        foreach (var value in requestContext.RouteData.Values)
        {
            requestContext.HttpContext.Items[value.Key] = value.Value;
        }
        return (Page)BuildManager.CreateInstanceFromVirtualPath(_virtualPath, typeof(Page));
    }
}