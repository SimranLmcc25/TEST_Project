using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class EBR_EBRC_CancellationDataEntry : System.Web.UI.Page
{
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
                //if (Session["userRole"].ToString() == "Supervisor")
                //{
                //    btnSave.Enabled = false;
                //    lblSupervisormsg.Visible = true;
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //    lblSupervisormsg.Visible = false;
                //}
                ddlBranch.Focus();
                fillBranch();
                btnCustAcNo.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                btnEBRCNo.Attributes.Add("onclick", "return OpenEBRCNoList('mouseClick');");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");
                txtStatus.Attributes.Add("onblur", "toUpper_Case();");
                txtYear.Text = System.DateTime.Now.ToString("yyyy");
                //btnSave.Attributes.Add("onclick", "return ConfirmSave();");

                //btnSave.Attributes["onclick"] = "javascript:return ConfirmSave();";   
 
            }
        }
    }

    private void fillCustomerCodeDescription()
    {
        lblCustName.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustAcNo.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
           
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustName.Text = "Invalid Id";
        }

    }
    protected void btnCustomerCode_Click(object sender, EventArgs e)
    {
        if (hdnCustomerCode.Value != "")
        {
            txtCustAcNo.Text = hdnCustomerCode.Value;
            fillCustomerCodeDescription();
            txtCustAcNo.Focus();
        }
    }

    protected void btnBRCNumber_Click(object sender, EventArgs e)
    {
        if (hdnEBRCNo.Value != "")
        {
            txtEBRCNo.Text = hdnEBRCNo.Value;
            fillDetails();
            lblEBRCNum.Text = "";
            txtEBRCNo.Focus();
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Focus();

    }
 
    protected void clear()
    {
        ddlBranch.SelectedIndex = 0;
        txtCustAcNo.Text = "";
        lblCustName.Text = "";
        txtYear.Text = System.DateTime.Now.ToString("yyyy"); 
        txtEBRCNo.Text = "";
        lblEBRCNum.Text = "";
        txtSrNo.Text = "";
        txtDocNo.Text = "";
        txtDocumentDate.Text = "";
        txtCurrency.Text = "";
        txtRealisedAmt.Text = "";
        txtAmtinINR.Text = "";
        txtStatus.Text = "";
        txtShippingBillDate.Text = "";
        txtShippingBillNo.Text = "";
        txtPortCode.Text = "";
    }

    protected void fillDetails()
    {
        SqlParameter pBranch = new SqlParameter("@Branch", SqlDbType.VarChar);
        if (ddlBranch.SelectedIndex > 0)
            pBranch.Value = ddlBranch.SelectedItem.Text;
        else
            pBranch.Value = "";

        SqlParameter pCustAcNo = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        pCustAcNo.Value = txtCustAcNo.Text.Trim();

        SqlParameter pYear = new SqlParameter("@Year", SqlDbType.VarChar);
        pYear.Value = txtYear.Text.Trim();
        
        
        SqlParameter pEBRCNo = new SqlParameter("@EBRCNO", SqlDbType.VarChar);
        pEBRCNo.Value = txtEBRCNo.Text.Trim();



        string _query = "TF_EBRC_GetCancellationEntryDetails";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query,pBranch,pCustAcNo,pEBRCNo,pYear);

        if (dt.Rows.Count > 0)
        {
            txtSrNo.Text = dt.Rows[0]["SRNO"].ToString().Trim();
            txtDocNo.Text = dt.Rows[0]["DOCNO"].ToString().Trim();
            txtDocumentDate.Text = dt.Rows[0]["TRANSACTION_DT"].ToString().Trim();
            txtCurrency.Text = dt.Rows[0]["CURR"].ToString().Trim();
            txtRealisedAmt.Text = dt.Rows[0]["REALISED_AMT"].ToString().Trim();
            txtAmtinINR.Text = dt.Rows[0]["INR_AMOUNT"].ToString().Trim();
            txtShippingBillNo.Text = dt.Rows[0]["SHIPPING_BILL_NO"].ToString().Trim();
            txtShippingBillDate.Text = dt.Rows[0]["SHIPPING_BILL_DT"].ToString().Trim();
            txtPortCode.Text = dt.Rows[0]["PORT_CODE"].ToString().Trim();
            lblEBRCNum.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "NumberFormat", "numberFormat();", true);
        }
        else

            txtEBRCNo.Text = "";
            lblEBRCNum.Text = "Invalid ERBC No";



    }
    protected void txtCustAcNo_TextChanged(object sender, EventArgs e)
    {
        fillCustomerCodeDescription();
        txtCustAcNo.Focus();
    }

    protected void txtEBRCNo_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
        txtEBRCNo.Focus();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
       

        string _CancelDate = System.DateTime.Now.ToString("dd/MM/yyyy");

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_EBRC_UpdateCancellationEntryDetails";

        SqlParameter p1 = new SqlParameter("@SrNo", SqlDbType.VarChar);
        p1.Value = txtSrNo.Text.Trim();

        SqlParameter pBranch = new SqlParameter("@Branch", SqlDbType.VarChar);
        if (ddlBranch.SelectedIndex > 0)
            pBranch.Value = ddlBranch.SelectedItem.Text;
        else
            pBranch.Value = "";

        SqlParameter pStatus = new SqlParameter("@status", SqlDbType.VarChar);
        pStatus.Value = txtStatus.Text.Trim();

        SqlParameter pCancelDate = new SqlParameter("@CancelDate", SqlDbType.VarChar);
        pCancelDate.Value = _CancelDate;

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;


        string _script = "";

        string _result = objSave.SaveDeleteData(_query, p1, pBranch, pStatus, pCancelDate, pUser, pUploadDate);

        _script = "";
        if (_result == "updated")
        {
            _script = _result;

            //DialogResult dr = MessageBox.Show("Do you want to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('This E-BRC Certificate is Cancelled.');", true);
            clear();
            ddlBranch.Focus();
        }
        else
            labelMessage.Text = _result;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
}