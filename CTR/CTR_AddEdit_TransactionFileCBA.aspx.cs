using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class CTR_CTR_AddEdit_TransactionFileCBA : System.Web.UI.Page
{
    static string mode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {

                    Response.Redirect("CTR_View_TransactionFileCBA.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        
                        txtBranch.Text = Request.QueryString["Branch"].ToString();
                        txttransactionID.Text = Request.QueryString["TransactionNo"].ToString();
                        txtTransactionDate.Text = Request.QueryString["TrnsactionDate"].ToString();
                        txtAccountNo.Text = Request.QueryString["Acno"].ToString();
                       
                        Filldetails();
                        mode = Request.QueryString["mode"].ToString();
                        txtTransactionDate.Focus();
                    }
                    else
                    {
                        mode = Request.QueryString["mode"].ToString();
                        txtBranch.Text = Request.QueryString["Branch"].ToString();
                        txttransactionID.Text = Request.QueryString["TransID"].ToString();
                    }
                    txttransactionID_TextChanged(null, null);
                    txtTransactionDate.Focus();
                    txttransactionID.Enabled = false;
                    txtAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtrate.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtAmtINR.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtrate.Attributes.Add("onblur", "return amt();");
                    btnSave.Attributes.Add("onclick", "return validate_Save();");
                   // txtCurrency.Attributes.Add("onkeydown", "return rate();");
                    txtTransactionDate.Attributes.Add("onblur", "return isValidDate(" + txtTransactionDate.ClientID + ",'Transaction Date');");
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CTR_View_TransactionFileCBA.aspx", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CTR_View_TransactionFileCBA.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        string mode = Request.QueryString["mode"].ToString();
        string _script = "";
        string transmode = "";

        if (rbtcash.Checked == true)
        {
            transmode = "C";
        }

        string DC = "";

        if (rbtdebit.Checked == true)
        {
            DC = "D";

        }
        else if (rbtCeadit.Checked == true)
        {
            DC = "C";
        }

        string TransType = "";

        if (CTR.Checked == true)
        {
            TransType = "CTR";

        }
        else if (STR.Checked == true)
        {
            TransType = "STR";
        }


        string query = "CTR_Update_CBATransactionFile";

        SqlParameter p1 = new SqlParameter("@transid", txttransactionID.Text.Trim());
        SqlParameter p2 = new SqlParameter("@BRANCHNAME", txtBranch.Text.Trim());
        SqlParameter p3 = new SqlParameter("@Acno", txtAccountNo.Text.Trim());
        SqlParameter p4 = new SqlParameter("@transdate", txtTransactionDate.Text.Trim());
        SqlParameter p5 = new SqlParameter("@transmode", transmode);
        SqlParameter p6 = new SqlParameter("@debitcreadit", DC);
        SqlParameter p7 = new SqlParameter("@amt", txtAmt.Text.Trim());
        SqlParameter p8 = new SqlParameter("@amtINR", txtAmtINR.Text.Trim());
        SqlParameter p9 = new SqlParameter("@rate", txtrate.Text.Trim());
        SqlParameter p10 = new SqlParameter("@cur", txtCurrency.Text.Trim());
        SqlParameter p11 = new SqlParameter("@funds", txtFund.Text.Trim());
        SqlParameter p12 = new SqlParameter("@remarks", txtRemark.Text.Trim());
        SqlParameter p13 = new SqlParameter("@username", Session["userName"].ToString().Trim());
        SqlParameter p14 = new SqlParameter("@loaddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter p15 = new SqlParameter("@mode", mode);
        SqlParameter p16 = new SqlParameter("@Trans_Type", TransType);

        string result = objData.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15,p16);

        if (result == "added")
        {
            _script = "window.location='CTR_View_TransactionFileCBA.aspx?result=added&TransactionID=" + txttransactionID.Text.Trim() + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
            if (result == "updated")
            {
                _script = "window.location='CTR_View_TransactionFileCBA.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = result;
    }

    protected void Filldetails()
    {
        TF_DATA objData = new TF_DATA();
        string mode = "edit";
        string query = "CTR_Geet_CBATransactionFile_Details";

        SqlParameter p1 = new SqlParameter("@BRANCHNAME", txtBranch.Text.Trim());
        SqlParameter p2 = new SqlParameter("@transid", txttransactionID.Text.Trim());

        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            mode = "edit";
            txtTransactionDate.Text = dt.Rows[0]["TransDate"].ToString();
            txtAccountNo.Text = dt.Rows[0]["AcNo"].ToString();
            txtCurrency.Text = dt.Rows[0]["Cur"].ToString();
            if (dt.Rows[0]["TransMode"].ToString() == "C")
            {
                rbtcash.Checked = true;
            }
            txtAmt.Text = dt.Rows[0]["Amt"].ToString();
            txtrate.Text = dt.Rows[0]["Rate"].ToString();

            if (dt.Rows[0]["DebitCredit"].ToString() == "C")
            {
                rbtCeadit.Checked = true;
            }
            else
                rbtdebit.Checked = true;

            if (dt.Rows[0]["TransType"].ToString() == "STR")
            {
                STR.Checked = true;
                STR.Enabled = false;
                CTR.Enabled = false;
            }
            else
                CTR.Checked = true;
            CTR.Enabled = false;
            STR.Enabled = false;
            txtAmtINR.Text = dt.Rows[0]["AmountINR"].ToString();
            txtFund.Text = dt.Rows[0]["Funds"].ToString();
            txtRemark.Text = dt.Rows[0]["Remarks"].ToString();
            txtAccountNo_TextChanged(null, null);
            txtCurrency_TextChanged(null, null);
        }
    
    }
    protected void txttransactionID_TextChanged(object sender, EventArgs e)
    {
        Filldetails();
    }
    protected void txtAccountNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        string query = "TF_CBTR_GetAccountMasterDetails";
        SqlParameter p1 = new SqlParameter("@AccountNo", txtAccountNo.Text.ToString());

        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            lblAccount.Text = dt.Rows[0]["Name"].ToString();

        }
        else
        {
            lblAccount.Text = "";
            txtAccountNo.Text = "";
        }
       txtAccountNo.Focus();
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        // string search = "";
        string query = "TF_GetCurrencyMasterDetails";

        SqlParameter p1 = new SqlParameter("@currencyid", txtCurrency.Text.Trim());

        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCurrency.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
            //txtCurrency.Focus();
            if (txtCurrency.Text == "INR")
            {
                txtrate.Text = "1";
            }
        }
        else
        {
            txtCurrency.Text = "";
            lblCurrency.Text = "";
        }
        txtCurrency.Focus();
    }
    protected void CTR_CheckedChanged(object sender, EventArgs e)
    {
         TF_DATA objData = new TF_DATA();
        if (CTR.Checked == true)
        {
            SqlParameter p1 = new SqlParameter("@ReportType","CTR");
            SqlParameter p2 = new SqlParameter("@TransID",txttransactionID.Text);
            DataTable dt = objData.getData("TF_GetSTR_CTRTransType", p1, p2);
            if (dt.Rows.Count > 0)
            {
                txttransactionID.Text = dt.Rows[0]["TransID"].ToString();
            }

        }

    }
    protected void STR_CheckedChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        if (STR.Checked == true)
        {
            SqlParameter p1 = new SqlParameter("@ReportType", "STR");
            SqlParameter p2 = new SqlParameter("@TransID", txttransactionID.Text);
            DataTable dt = objData.getData("TF_GetSTR_CTRTransType", p1, p2);
            if (dt.Rows.Count > 0)
            {
                txttransactionID.Text = dt.Rows[0]["TransID"].ToString();
            }

        }

    }
}