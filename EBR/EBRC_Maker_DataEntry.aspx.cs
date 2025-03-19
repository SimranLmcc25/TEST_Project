using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;

public partial class EBR_EBRC_Maker_DataEntry : System.Web.UI.Page
{
    TF_DATA objData = new TF_DATA();
    public static string IRMststus_at = "";
    Encryption objEnc = new Encryption();
    bool access_flag = true;
    string ipAddressW = GetIPAddress();
    string Log_Query = "TF_Audit_ApplicationLogs";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
          //Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription(lbl.Value));
        }
        if (!IsPostBack)
        {
            PageAccess();
            if(access_flag==true)
            {
                txtBranchCode.Text = "01";
                fillDetails();
                Page.DataBind();
                btnSave.Attributes.Add("onclick", "return functionvalidateSave();");
                txtAmount.Attributes.Add("onblur", "return validateAmt();");
                txtinrCreditAmount.Attributes.Add("onblur", "return validateAmt();");
                txtDocNo.Attributes.Add("onblur", "return CheckLength();");
            }
           
        }

      
    }
    protected void fillDetails()
    {
         //string irmno = Request.QueryString["DocNo"].ToString();
         //string status = Request.QueryString["status"].ToString();
         //string i_status = Request.QueryString["irmstatus"].ToString();

        string irmno = objEnc.URLIDDecription(HttpContext.Current.Items["DocNo"].ToString());
        string status = objEnc.URLIDDecription(HttpContext.Current.Items["status"].ToString());
        string i_status = objEnc.URLIDDecription(HttpContext.Current.Items["irmstatus"].ToString());

         SqlParameter p1 = new SqlParameter("@IRMNumber", irmno);
         SqlParameter p2 = new SqlParameter("@status", status);
         SqlParameter p3 = new SqlParameter("@irmstatus", i_status);
         
         DataTable dt = objData.getData("Tf_Maker_IRMFileUpload_FillDetails", p1, p2,p3);
        if(dt.Rows.Count >0)
        {
            txtDocNo.Text = dt.Rows[0]["IRMNumber"].ToString();
            txtDocDate.Text = dt.Rows[0]["Remittancedate"].ToString();
            txtBankuniqueTextId.Text = dt.Rows[0]["BankUniqueTransactionId"].ToString().Trim();
            txtirmIssueDate.Text = dt.Rows[0]["IRMissueDate"].ToString().Trim();
            txtremittanceFCC.Text = dt.Rows[0]["RemittanceFCC"].ToString().Trim();
            txtinrCreditAmount.Text = dt.Rows[0]["INRCreditAmount"].ToString().Trim();
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString().Trim();
            txtPurposeCode.Text = dt.Rows[0]["PurposeofRemittance"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["RemittanceFCCAmount"].ToString().Trim();
            txtRemitterName.Text = dt.Rows[0]["RemitterName"].ToString().Trim();
            txtRemitterCountry.Text = dt.Rows[0]["Remittercountry"].ToString().Trim();
            txtIECcode.Text = dt.Rows[0]["IECCode"].ToString().Trim();
            txtIFSCcode.Text = dt.Rows[0]["IFSCCode"].ToString().Trim();
            txtRemittanceADCode.Text = dt.Rows[0]["RemittanceADCode"].ToString().Trim();

            txtbanaccnumber.Text = dt.Rows[0]["BankAccountNumber"].ToString().Trim();
            txtbanrefnumber.Text = dt.Rows[0]["BankReferencenumber"].ToString().Trim();
            string IRMstatus = dt.Rows[0]["IRMStatus"].ToString().Trim();
            if (IRMstatus == "F" || IRMstatus == "A" || IRMstatus == "C")
            {

                ddlIRMstatus.SelectedIndex = ddlIRMstatus.Items.IndexOf(ddlIRMstatus.Items.FindByText(dt.Rows[0]["IRMStatus"].ToString().Trim()));
            }
            else
            {
                ddlIRMstatus.SelectedIndex = 0;
            }
            IRMststus_at = dt.Rows[0]["IRMStatus"].ToString().Trim();


            //string factoring = dt.Rows[0]["Factoringflag"].ToString().Trim();
            //if (factoring == "Y" || factoring == "N")
            //{
            //    ddlFactoring.SelectedIndex = ddlFactoring.Items.IndexOf(ddlFactoring.Items.FindByText(dt.Rows[0]["Factoringflag"].ToString().Trim()));
            //}
            //else
            //{
            //    ddlFactoring.SelectedIndex = 0;
            //}
            //factoring_at = dt.Rows[0]["Factoringflag"].ToString().Trim();

            if (dt.Rows[0]["Reject_Remarks"].ToString() != "")
            {
             
                labelMessage.Text = "Checker Remark :" + dt.Rows[0]["Reject_Remarks"].ToString();
            }
        
        
    }



    //    txtDocNo.Text = Request.QueryString["DocNo"].ToString();
    //    string status = Request.QueryString["status"].ToString();
    //    SqlParameter p1 = new SqlParameter("@IRMNumber", txtDocNo.Text);
    //    SqlParameter p2 = new SqlParameter("@status", status);

    //    DataTable dt = objData.getData("Tf_Maker_FillDetails", p1, p2);
    //    if (dt.Rows.Count > 0)
    //    {
    //        if (status == "")
    //        {
    //            txtDocDate.Text = dt.Rows[0]["RemittanceDate"].ToString();
    //            txtCustAcNo.Text = dt.Rows[0]["CUST_ID"].ToString().Trim();
    //            txtPurposeCode.Text = dt.Rows[0]["PurposeCode"].ToString().Trim();
    //            ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
    //            txtAmount.Text = dt.Rows[0]["RemittanceAmount"].ToString().Trim();
    //            txtExchangeRate.Text = dt.Rows[0]["ExchangeRate"].ToString().Trim();
    //            txtAmtInINR.Text = dt.Rows[0]["AmountInINR"].ToString().Trim();
    //            txtFIRCNo.Text = dt.Rows[0]["FIRCNo"].ToString().Trim();
    //            txtFIRCDate.Text = dt.Rows[0]["FIRCDate"].ToString().Trim();
    //            txtADCode.Text = dt.Rows[0]["FIRC_ADCode"].ToString().Trim();
    //            txtRemitterName.Text = dt.Rows[0]["RemitterName"].ToString().Trim();
    //            txtRemitterCountry.Text = dt.Rows[0]["RemitterCountry"].ToString().Trim();
    //            txtvalueDate.Text = dt.Rows[0]["ValueDate"].ToString().Trim();
    //            txtremarks.Text = dt.Rows[0]["Remarks"].ToString().Trim();
    //            txtswift.Text = dt.Rows[0]["SwiftOtherBankRefNumber"].ToString().Trim();
    //            txtRemBankCountry.Text = dt.Rows[0]["RemitterBankCountry"].ToString().Trim();
    //            txtRemitterBank.Text = dt.Rows[0]["RemitterBankName"].ToString().Trim();
    //            txtRemitterBankAddress.Text = dt.Rows[0]["RemitterBankAddress"].ToString().Trim();
    //            txtRemitterAddress.Text = dt.Rows[0]["RemitterAddress"].ToString().Trim();
    //        }
    //        else
    //        {
    //            txtDocDate.Text = dt.Rows[0]["RemittanceDate"].ToString();
    //            txtCustAcNo.Text = dt.Rows[0]["CUST_ID"].ToString().Trim();
    //            txtPurposeCode.Text = dt.Rows[0]["PurposeCode"].ToString().Trim();
    //            ddlCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
    //            txtAmount.Text = dt.Rows[0]["RemittanceAmount"].ToString().Trim();
    //            txtExchangeRate.Text = dt.Rows[0]["ExchangeRate"].ToString().Trim();
    //            txtAmtInINR.Text = dt.Rows[0]["AmountInINR"].ToString().Trim();
    //            txtFIRCNo.Text = dt.Rows[0]["FIRCNo"].ToString().Trim();
    //            txtFIRCDate.Text = dt.Rows[0]["FIRCDate"].ToString().Trim();
    //            txtADCode.Text = dt.Rows[0]["FIRC_ADCode"].ToString().Trim();
    //            txtRemitterName.Text = dt.Rows[0]["RemitterName"].ToString().Trim();
    //            txtRemitterCountry.Text = dt.Rows[0]["RemitterCountry"].ToString().Trim();
    //            txtvalueDate.Text = dt.Rows[0]["ValueDate"].ToString().Trim();
    //            txtremarks.Text = dt.Rows[0]["Remarks"].ToString().Trim();
    //            txtswift.Text = dt.Rows[0]["SwiftOtherBankRefNumber"].ToString().Trim();
    //            txtRemBankCountry.Text = dt.Rows[0]["RemitterBankCountry"].ToString().Trim();
    //            txtRemitterBank.Text = dt.Rows[0]["RemitterBankName"].ToString().Trim();
    //            txtRemitterBankAddress.Text = dt.Rows[0]["RemitterBankAddress"].ToString().Trim();
    //            txtRemitterAddress.Text = dt.Rows[0]["RemitterAddress"].ToString().Trim();
    //            txtuniqueTextId.Text = dt.Rows[0]["uniqueTextId"].ToString().Trim();
    //            txtirmIssueDate.Text = dt.Rows[0]["irmIssueDate"].ToString().Trim();
    //            txtremittanceFCC.Text = dt.Rows[0]["remittanceFCC"].ToString().Trim();
    //            txtinrCreditAmount.Text = dt.Rows[0]["inrCreditAmount"].ToString().Trim();
    //            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString().Trim();
    //            txtmodeOfPayment.Text = dt.Rows[0]["modeOfPayment"].ToString().Trim();
    //            string forfeiting = dt.Rows[0]["IsFofeting"].ToString().Trim();
    //            if (forfeiting == "Y" || forfeiting == "N")
    //            {
    //                ddlForfeighting.SelectedIndex = ddlForfeighting.Items.IndexOf(ddlForfeighting.Items.FindByText(dt.Rows[0]["IsFofeting"].ToString().Trim()));
    //            }
    //            else
    //            {
    //                ddlForfeighting.SelectedIndex = 0;
    //            }
    //            forfeighting_at = dt.Rows[0]["IsFofeting"].ToString().Trim();

    //            string factoring = dt.Rows[0]["Isfactoring"].ToString().Trim();
    //            if (factoring == "Y" || factoring == "N")
    //            {
    //                ddlFactoring.SelectedIndex = ddlFactoring.Items.IndexOf(ddlFactoring.Items.FindByText(dt.Rows[0]["Isfactoring"].ToString().Trim()));
    //            }
    //            else
    //            {
    //                ddlFactoring.SelectedIndex = 0;
    //            }
    //            factoring_at = dt.Rows[0]["Isfactoring"].ToString().Trim();
    //        }
    //    }
    }

    //protected void fillCurrency()
    //{
    //    SqlParameter p1 = new SqlParameter("@search", "");
    //    string _query = "TF_GetCurrencyList";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlCurrency.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "Select";
    //        ddlCurrency.DataSource = dt.DefaultView;
    //        ddlCurrency.DataTextField = "C_Code";
    //        ddlCurrency.DataValueField = "C_Code";
    //        ddlCurrency.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    ddlCurrency.Items.Insert(0, li);
    //}

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("TF_EBRC_Maker.aspx", true);
        Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "Z9nPBerecmnxse4UMzigPX3iiLiRMpDkMopUe4THkw", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string result_ = "";
        string _script = "";
      //  txtDocNo.Text = Request.QueryString["DocNo"].ToString();
        txtDocNo.Text = objEnc.URLIDDecription(HttpContext.Current.Items["DocNo"].ToString());
        string maker_Date = DateTime.Now.Date.ToString("dd/MM/yyyy");
        TF_DATA obj = new TF_DATA();
        SqlParameter BankUniqueTranscationID = new SqlParameter("@BankUniqueTransactionId", txtBankuniqueTextId.Text.ToString());
        SqlParameter Remittancedate = new SqlParameter("@Remittancedate", txtDocDate.Text.ToString().Trim());
        SqlParameter IrmNumber = new SqlParameter("@IRMNumber", txtDocNo.Text.ToString().Trim());
        SqlParameter IRMIssueDate = new SqlParameter("@IRMissueDate", txtirmIssueDate.Text.ToString().Trim());
        SqlParameter RemittanceFCC = new SqlParameter("@RemittanceFCC", txtremittanceFCC.Text.ToString().Trim());
        SqlParameter RemittanceFCCAmount = new SqlParameter("@RemittanceFCCAmount", txtAmount.Text.ToString().Trim());
        SqlParameter INRCreditAmount = new SqlParameter("@INRCreditAmount", txtinrCreditAmount.Text.ToString().Trim());
        SqlParameter PanNumber = new SqlParameter("@PanNumber", txtPanNumber.Text.ToString().Trim());
        SqlParameter RemitterName = new SqlParameter("@RemitterName", txtRemitterName.Text.ToString().Trim());
        SqlParameter Remittercountry = new SqlParameter("@Remittercountry", txtRemitterCountry.Text.ToString().Trim());
        SqlParameter PurposeofRemittance = new SqlParameter("@PurposeofRemittance", txtPurposeCode.Text.ToString().Trim());
        SqlParameter BanaccNumber = new SqlParameter("@BankaccNumber",txtbanaccnumber.Text.ToString().Trim() );
        SqlParameter BankRefNumber = new SqlParameter("@BankrefNumber",txtbanrefnumber.Text.ToString().Trim() );
        SqlParameter RemittanceADCode = new SqlParameter("@RemittanceADCode",txtRemittanceADCode.Text.ToString().Trim());
        SqlParameter IFScode = new SqlParameter("@IFScode", txtIFSCcode.Text.ToString().Trim());
        SqlParameter IEcode = new SqlParameter("@IEcode", txtIECcode.Text.ToString().Trim());
        SqlParameter IRMStatus = new SqlParameter("@Irmstatus",ddlIRMstatus.SelectedItem.ToString().Trim());
        SqlParameter Maker_user = new SqlParameter("@makeruser", Session["userName"].ToString());
        SqlParameter Maker_Date = new SqlParameter("@makerDate",maker_Date);
    
      result_=obj.SaveDeleteData("Tf_Maker_IRMFileUpload_Updatedata", BankUniqueTranscationID, Remittancedate, IrmNumber, IRMIssueDate, RemittanceFCC,
        RemittanceFCCAmount, INRCreditAmount, PanNumber, RemitterName, Remittercountry,
        PurposeofRemittance, BanaccNumber, BankRefNumber, IRMStatus, RemittanceADCode, IFScode, IEcode, Maker_user, Maker_Date);
        
      if (result_ == "Updated")
        {
            SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
            p1.Value = Session["userName"].ToString().Trim();
            SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
            p2.Value = ipAddressW;
            SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
            p3.Value = System.DateTime.Now;
            SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
            p4.Value = "EBRC IRM Data Entry View - Maker";
            SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
            p5.Value = "send to Checker:IRMno: "+txtDocNo.Text.Trim();
            string store_logs = obj.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);

           // _script = "window.location='TF_EBRC_Maker.aspx?result=" + result_ + "'";
            _script = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "Cwj7xDHbUlocDLfQxqcp00EDBtzERGsgyNy7jYgYY/" + objEnc.URLIDEncription(result_) + @"'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
      else if (result_ != "Updated")
      {
          SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
          p1.Value = Session["userName"].ToString().Trim();
          SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
          p2.Value = ipAddressW;
          SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
          p3.Value = System.DateTime.Now;
          SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
          p4.Value = "EBRC IRM Data Entry View - Maker";
          SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
          p5.Value = result_;
          string store_logs = obj.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);
          labelMessage.Text = result_+ "IRMno:" + txtDocNo.Text.Trim();
            
      }

   }
    protected void txtPurposeCode_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@purposecode", txtPurposeCode.Text);
        DataTable dt = objData.getData("TF_GetPurposeCodeMasterDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblpurposeCode.Text = dt.Rows[0]["description"].ToString();
        }
        if (lblpurposeCode.Text == "")
        {
            txtPurposeCode.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid PurposeCode.')", true);
            txtPurposeCode.Focus();
        }
    }
    protected void txtRemitterCountry_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@cid", txtRemitterCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
        }
        if (lblCountryDesc.Text == "")
        {
            lblCountryDesc.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Country.')", true);
            txtRemitterCountry.Focus();
        }
    }
    public void PageAccess()
    {
        int C = 0;
        TF_DATA objData = new TF_DATA();
        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = Session["userName"].ToString();
        SqlParameter menuName = new SqlParameter("@menuName", SqlDbType.VarChar);
        menuName.Value = "EBRC IRM Data Entry View - Maker";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "EBRC IRM Data Entry View - Maker")
                {
                    C = 1;
                }
            }
        }
        if (C != 1)
        {
            access_flag = false;
            string result = "Access denied";
            //string redirectUrl = ResolveUrl("~/TF_Logout.aspx");
            string redirectUrl = ResolveUrl(ConfigurationManager.AppSettings["webpath"] + "0rJaTMnF39W4f93iMtXSg/");
            string script = "alert('" + result + "');";
            script += "window.location.href = '" + redirectUrl + "';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "RedirectScript", script, true);

        }
    }

    public static string GetIPAddress()
    {
        string ipAddress = string.Empty;
        foreach (IPAddress item in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        if (!string.IsNullOrEmpty(ipAddress))
        {
            return ipAddress;
        }
        foreach (IPAddress item in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (item.AddressFamily.ToString().Equals("InterNetwork"))
            {
                ipAddress = item.ToString();
                break;
            }
        }
        return ipAddress;
    }
}
