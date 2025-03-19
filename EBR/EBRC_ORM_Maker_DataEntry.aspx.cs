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

public partial class EBR_EBRC_ORM_Maker_DataEntry : System.Web.UI.Page
{
      TF_DATA objData = new TF_DATA();
      Encryption objEnc = new Encryption();
    public static string ORMststus_at = "";
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
            if (access_flag == true)
            {
                txtBranchCode.Text = "01";
                fillDetails();
                Page.DataBind();
                btnSave.Attributes.Add("onclick", "return functionvalidateSave();");
                txtAmount.Attributes.Add("onblur", "return validateAmt();");
                txtornINRAmt.Attributes.Add("onblur", "return validateAmt();");
            }
           
        }

    }

    protected void fillDetails()
    {
        //txtormNo.Text = Request.QueryString["Ormno"].ToString();
        //string status = Request.QueryString["status"].ToString();
        //string ormstatus = Request.QueryString["ormstatus"].ToString();

        txtormNo.Text = objEnc.URLIDDecription(HttpContext.Current.Items["Ormno"].ToString());
        string status = objEnc.URLIDDecription(HttpContext.Current.Items["status"].ToString());
        string ormstatus = objEnc.URLIDDecription(HttpContext.Current.Items["ormstatus"].ToString());

        
        SqlParameter p1 = new SqlParameter("@ORMNumber", txtormNo.Text);
        SqlParameter p2 = new SqlParameter("@status", status);
        SqlParameter p3 = new SqlParameter("@Ormstatus", ormstatus);

        DataTable dt = objData.getData("Tf_Maker_ORMFileUpload_FillDetails", p1, p2,p3);
        if (dt.Rows.Count > 0)
        {
            txtpaymentDate.Text = dt.Rows[0]["Paymentdate"].ToString();
            txtORMissuedt.Text = dt.Rows[0]["ORMissueDate"].ToString();
            txtbeneficiaryName.Text = dt.Rows[0]["BeneficiaryName"].ToString();
            txtbeneficiaryCountry.Text = dt.Rows[0]["Beneficiarycountry"].ToString();
            txtBankuniqueTextId.Text = dt.Rows[0]["BankUniqueTransactionId"].ToString();
            txtADCode.Text = dt.Rows[0]["OrnADCode"].ToString();
            txtornFCC.Text = dt.Rows[0]["OrnFCC"].ToString();
            txtAmount.Text = dt.Rows[0]["OrnFCCAmount"].ToString();
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString();
            txtIFSCcode.Text = dt.Rows[0]["IFSCCode"].ToString();
            txtIECcode.Text = dt.Rows[0]["IECCode"].ToString();
            txtornINRAmt.Text = dt.Rows[0]["INRpayableAmount"].ToString();
            txtPurposeCode.Text = dt.Rows[0]["PurposeofOutward"].ToString();
            txtrefIRM.Text = dt.Rows[0]["ReferenceIRM"].ToString();

            string ORMstatus = dt.Rows[0]["ORMStatus"].ToString();
            if (ORMstatus == "F" || ORMstatus == "A" || ORMstatus == "C")
            {
                ddlORMstatus.SelectedIndex = ddlORMstatus.Items.IndexOf(ddlORMstatus.Items.FindByText(dt.Rows[0]["ORMStatus"].ToString()));

            }
            else
            {
                ddlORMstatus.SelectedIndex = 0;

            }

            ORMststus_at = dt.Rows[0]["ORMStatus"].ToString();

            if (dt.Rows[0]["Reject_Remarks"].ToString() != "") 
            {
                lblmessage.Text = "Checker Remark :" + dt.Rows[0]["Reject_Remarks"].ToString();

            }


        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string result_ = "";
        string _script = "";
        //txtormNo.Text = Request.QueryString["Ormno"].ToString();
        txtormNo.Text = objEnc.URLIDDecription(HttpContext.Current.Items["Ormno"].ToString());
        string maker_Date = DateTime.Now.Date.ToString("dd/MM/yyyy");
        SqlParameter Ormno = new SqlParameter("@ORMNumber",txtormNo.Text.ToString());
        SqlParameter Bankuniquetxtid = new SqlParameter("@bankuniqtxtid",txtBankuniqueTextId.Text.Trim());
        SqlParameter Paymentdate = new SqlParameter("@Paymentdate",txtpaymentDate.Text.Trim());
        SqlParameter ORMissueDate = new SqlParameter("@ORMissueDate",txtORMissuedt.Text.Trim());
        SqlParameter OrnFcc = new SqlParameter("@OrnFCC", txtornFCC.Text.Trim());
        SqlParameter Ornfcamt = new SqlParameter("@OrnFCCAmount",txtAmount.Text.Trim());
        SqlParameter INRpayableAmount = new SqlParameter("@INRpayableAmount",txtornINRAmt.Text.Trim());
        SqlParameter PanNumber = new SqlParameter("@PanNumber",txtPanNumber.Text.Trim());
        SqlParameter BeneficiaryName =new SqlParameter("@beneficiaryName",txtbeneficiaryName.Text.Trim());
        SqlParameter Beneficiarycountry = new SqlParameter("@beneficiarycountry",txtbeneficiaryCountry.Text.Trim());
        SqlParameter PurposeOfOutward = new SqlParameter("@purposeOfOutward",txtPurposeCode.Text.Trim());
        SqlParameter OrnADCode = new SqlParameter("@ADCode",txtADCode.Text.Trim());
        SqlParameter IFScode = new SqlParameter("@IFScode",txtIFSCcode.Text.Trim());
        SqlParameter iecode = new SqlParameter("@IEcode",txtIECcode.Text.Trim());
        SqlParameter ORMstatus = new SqlParameter("@ormstatus",ddlORMstatus.SelectedItem.ToString().Trim());
        SqlParameter refirm = new SqlParameter("@Refirm",txtrefIRM.Text.ToString().Trim());
        SqlParameter Maker_user = new SqlParameter("@makeruser", Session["userName"].ToString());
        SqlParameter Maker_Date = new SqlParameter("@makerDate", maker_Date);

        result_=objData.SaveDeleteData("Tf_Maker_ORMFileUpload_Updatedata",Bankuniquetxtid ,Ormno, Paymentdate, ORMissueDate, Ornfcamt, OrnFcc, INRpayableAmount
         ,PanNumber, BeneficiaryName, Beneficiarycountry ,PurposeOfOutward, OrnADCode, IFScode,refirm,iecode, ORMstatus,Maker_Date,Maker_user);
      
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
            p5.Value = "send to Checker:ORMno: " + txtormNo.Text.Trim();
            string store_logs = objData.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);

            //_script = "window.location='TF_EBRC_ORM_Maker.aspx?result=" + result_ + "'";
            _script = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "HQFvGmmYi1k3fwo4blywjDUxn4t0vOYj1YgaYqNDTs/" + objEnc.URLIDEncription(result_) + @"'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else if (result_ == "Updated")
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
            p5.Value = result_ +" ORMno: " + txtormNo.Text.Trim();
            string store_logs = objData.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);

          
        }

        


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
      //  Response.Redirect("TF_EBRC_ORM_Maker.aspx");
        Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "HQFvGmmYi1k3fwo4blywjB6Kv2Csm1DJCfTn63KMNRo", true);
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
    protected void txtbeneficiaryCountry_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@cid", txtbeneficiaryCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
        }
        if (lblCountryDesc.Text == "")
        {
            lblCountryDesc.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Country.')", true);
            txtbeneficiaryCountry.Focus();
        }
    }
    public void PageAccess()
    {
        int C = 0;
        TF_DATA objData = new TF_DATA();
        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = Session["userName"].ToString();
        SqlParameter menuName = new SqlParameter("@menuName", SqlDbType.VarChar);
        menuName.Value = "EBRC ORM Data Entry View - Maker";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "EBRC ORM Data Entry View - Maker")
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