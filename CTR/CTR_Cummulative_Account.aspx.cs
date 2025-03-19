using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class CTR_Cummulative_Account : System.Web.UI.Page
{
    static string mode;
    string ReportType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mode"].ToString() == "edit")
            {
                mode=Request.QueryString["mode"].ToString();
                txtMonthYear.Text = Request.QueryString["YearMonth"].ToString();
                txtAccountNo.Text = Request.QueryString["AcNo"].ToString();
                ReportType = Request.QueryString["ReportType"].ToString();
                txtAccountNo_TextChanged(null, null);
                fillDetails();
            }
            else
            {
                
                //txtMonthYear.Text = System.DateTime.Now.ToString("MM/yyyy");
                mode = Request.QueryString["mode"].ToString();
                txtMonthYear.Text = Request.QueryString["monthyear"].ToString();
            }
            txtMonthYear.Enabled = false;
            //txtAccountNo_TextChanged(null, null);
            txtCummCashDeposit.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtCummCashWithdrawal.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtCummCredit.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtCummDebit.Attributes.Add("onkeydown", "return validate_Number(event);");
            btnSave.Attributes.Add("onclick", "return validate_save();");

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CTR_Cummulative_Acc_View.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "CTR_CummulativeAccount_AddEdit";
        string RefNo = Request.QueryString["RefNo"].ToString();
        string yearmonth = (txtMonthYear.Text).Substring(3, 4) + (txtMonthYear.Text).Substring(0, 2);  
        //SqlParameter p1= new SqlParameter("@BankID",);

        string TransType = "";

        if (CTR.Checked == true)
        {
            TransType = "CTR";

        }
        else if (STR.Checked == true)
        {
            TransType = "STR";
        }

        
        
        SqlParameter p1 = new SqlParameter("@RefNo", RefNo);
        SqlParameter p2 = new SqlParameter("@AcNo", txtAccountNo.Text.Trim());
        SqlParameter p3 = new SqlParameter("@YearMonth", yearmonth);
        SqlParameter p4 = new SqlParameter("@CumCredit", txtCummCredit.Text.Trim());
        SqlParameter p5 = new SqlParameter("@CumDebit", txtCummDebit.Text.Trim());
        SqlParameter p6 = new SqlParameter("@CumDeposit", txtCummCashDeposit.Text.Trim());
        SqlParameter p7 = new SqlParameter("@CumWithdraw", txtCummCashWithdrawal.Text.Trim());
        SqlParameter p8 = new SqlParameter("@AddedBy", Session["userName"].ToString());
        SqlParameter p9 = new SqlParameter("@AddedDate", System.DateTime.Now);
        SqlParameter p10 = new SqlParameter("@mode", mode);
        SqlParameter p11 = new SqlParameter("@TransType", TransType);

        string result= objData.SaveDeleteData(_query,p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,p11);
        if (result == "added")
        {
            string _script = "window.location='CTR_Cummulative_Acc_View.aspx?result=" + result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);        
        }
        else
        {
            if (result == "updated")
            {
                string _script = "window.location='CTR_Cummulative_Acc_View.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);       
            }   
        }
    }

    protected void fillDetails()
    {  
        string RefNo = Request.QueryString["RefNo"].ToString();
        string yearmonth = (txtMonthYear.Text).Substring(3, 4) + (txtMonthYear.Text).Substring(0, 2);    

        TF_DATA objData = new TF_DATA();
        string _query = "CTR_CummulativeAccount_GetDetails";
        SqlParameter p1 = new SqlParameter("@RefNo", RefNo);      
        SqlParameter p2 = new SqlParameter("@yearMonth",yearmonth);
        SqlParameter p3= new SqlParameter("@AcNo",txtAccountNo.Text.Trim());
        SqlParameter p4 = new SqlParameter("@TransType",ReportType);

        DataTable dt = objData.getData(_query,p1,p2,p3,p4);
        if (dt.Rows.Count > 0)
        {
            txtCummCredit.Text = dt.Rows[0]["CumCredit"].ToString();
            txtCummDebit.Text = dt.Rows[0]["CumDebit"].ToString();
            txtCummCashDeposit.Text = dt.Rows[0]["CumDeposit"].ToString();
            txtCummCashWithdrawal.Text = dt.Rows[0]["CumWithdraw"].ToString();
            if (dt.Rows[0]["TransType"].ToString() == "STR")
            {
                STR.Checked = true;
            }
            else
                CTR.Checked = true;

            //txtAccountNo_TextChanged(null, null);
        }
    }

    protected void txtAccountNo_TextChanged(object sender, EventArgs e)
    {

        TF_DATA objData = new TF_DATA();
        string query = "TF_GetCustomerMasterDetails";
        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtAccountNo.Text.Trim();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {

            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtAccountNo.Text = "";
            lblCustName.Text = "";
           
        }
        txtAccountNo.Focus();
       //fillDetails();
    }
}