using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EBR_Ebrc_Generate_TradeData : System.Web.UI.Page
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
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillBranch();
                ddlBranch.Focus();
            }
        }
    }
    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
       
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li01.Text = "No record(s) found";

  
        ddlBranch.Items.Insert(0, li01);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {

        TF_DATA objValid = new TF_DATA();
        SqlParameter v1 = new SqlParameter("@Branch", SqlDbType.VarChar);
          v1.Value = ddlBranch.Text.ToString().Trim();


        SqlParameter v2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        v2.Value = txtFromDate.Text.ToString();

        SqlParameter v3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        v3.Value = txtToDate.Text.ToString();

        string _qryValid = "TF_EBRC_TempDataFile_Generate";
        DataTable dtv = objValid.getData(_qryValid, v1, v2, v3);
        string dv = "Data Validation";
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();


        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        string _qry1 = "TF_EBRC_DATA";
        string result = objData.SaveDeleteData(_qry1, p1, p2, p3);

        labelMessage.Text = result;

        if (dtv.Rows.Count > 0)
        {

            Response.Redirect("~/Reports/EBRReports/View_rptEBR_DataValidationReports.aspx?frm=" + txtFromDate.Text.Trim() + "&to=" + txtToDate.Text.Trim() + "&Branch=" + ddlBranch.Text.ToString().Trim()+"&Report="+dv);
            //window.open('View_rptEBR_DataValidationReports.aspx?frm=' + txtfromDate.value + '&to=' + txtToDate.value + '&Branch=' + ddlBranch +  '&Report=' + Report, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1100,height=550');
        }
        else
        {
                labelMessage.Text = result;
        }
    }

}