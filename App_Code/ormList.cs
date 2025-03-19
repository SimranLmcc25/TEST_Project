using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ormList
/// </summary>
public class ormDataLst
{
    public string ormIssueDate { get; set; }
    public string ormNumber { get; set; }
    public string ormStatus { get; set; }
    public string ifscCode { get; set; }
    public string ornAdCode { get; set; }
    public string paymentDate { get; set; }
    public string ornFCC { get; set; }
    public decimal ornFCAmount { get; set; }
    public decimal ornINRAmount { get; set; }
    public string iecCode { get; set; }
    public string panNumber { get; set; }
    public string beneficiaryName { get; set; }
    public string beneficiaryCountry { get; set; }
    public string purposeOfOutward { get; set; }
    public string referenceIRM { get; set; }

    public ormDataLst()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}