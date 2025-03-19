using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for ErrorDetails
/// </summary>
public class ErrorDetails
{
    [JsonProperty("errorCode")]
    public string errorCode { get; set; }

    [JsonProperty("errorDetails")]
    public string errorDetails { get; set; }
	public ErrorDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}