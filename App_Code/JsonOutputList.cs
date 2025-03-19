using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JsonOutputList
/// </summary>
public class JsonOutputList
{
    public string irmNumber { get; set; }
    public string irmIssueDate { get; set; }
    public string ackStatus { get; set; }
    public List<Option> errorDetails { get; set; }

    public class Option
    {
        public string errorCode { get; set; }
        public string errorDetails { get; set; }
    }

	public JsonOutputList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}