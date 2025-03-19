using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for irmList
/// </summary>
public class irmList
{
    public string bankRefNumber { get; set; }
    public string irmIssueDate { get; set; }
    public string irmNumber { get; set; }
    public string irmStatus { get; set; }
    public string ifscCode { get; set; }
    public string remittanceAdCode { get; set; }
    public string remittanceDate { get; set; }
    public string remittanceFCC { get; set; }
    public decimal remittanceFCAmount { get; set; }
    public decimal inrCreditAmount { get; set; }
    public string iecCode { get; set; }
    public string panNumber { get; set; }
    public string remitterName { get; set; }
    public string remitterCountry { get; set; }
    public string purposeOfRemittance { get; set; }
    public string bankAccountNo { get; set; }

    public irmList()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}