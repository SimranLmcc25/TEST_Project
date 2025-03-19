using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public partial class EBR_TF_EBRC_ViewExRdateEntrylist : System.Web.UI.Page
{
    Encryption objEnc = new Encryption();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            //Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription(lbl.Value));
        }
        else
        {
            if (!IsPostBack)
            {
                txtYear.Text = System.DateTime.Now.Year.ToString();
                ddlrecordperpage.SelectedValue = "10";
                fillBranch();
                fillGrid();
              //  if (Request.QueryString["result"] != null)
                if (HttpContext.Current.Items["result"] != null)
                {
                    //if (Request.QueryString["result"].Trim() == "locked")
                    if (objEnc.URLIDDecription(HttpContext.Current.Items["result"].ToString()) == "locked")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML File already generated for this Sr No,You cannot Update.');", true);
                    }
                   // if (Request.QueryString["result"].Trim() == "added")
                    if (objEnc.URLIDDecription(HttpContext.Current.Items["result"].ToString()) == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                      //  if (Request.QueryString["result"].Trim() == "updated")
                        if (objEnc.URLIDDecription(HttpContext.Current.Items["result"].ToString()) == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                    ddlBranch.Focus();
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnAdd.Attributes.Add("onClick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewReturnData.PageCount != GridViewReturnData.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewReturnData.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewReturnData.PageIndex + 1) + " of " + GridViewReturnData.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewReturnData.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewReturnData.PageIndex != (GridViewReturnData.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewReturnData.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewReturnData.PageIndex > 0)
        {
            GridViewReturnData.PageIndex = GridViewReturnData.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewReturnData.PageIndex != GridViewReturnData.PageCount - 1)
        {
            GridViewReturnData.PageIndex = GridViewReturnData.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewReturnData.PageIndex = GridViewReturnData.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
       // Response.Redirect("TF_EBRC_AddEditERSdata.aspx?mode=add&&BranchName=" + ddlBranch.Text, true);
        Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "aQ4IHcbW6xwIKa7MSP0RpLajvtgpAy0qDuqiCohb2nsWVwycclfJLE7zzhnYwqb/" + objEnc.URLIDEncription("add") + "/" + objEnc.URLIDEncription(ddlBranch.Text));
    }
    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();

        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime newDate = new DateTime();
        
        bool isdate = false;

        try
        {
            newDate = Convert.ToDateTime(txtSearch.Text.Trim(), dateInfo);
            isdate = true;
        }
        catch { }

        if (isdate)
        { search = newDate.ToString(); }

        SqlParameter p1 = new SqlParameter("@search",SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();

        SqlParameter p3 = new SqlParameter("@year", SqlDbType.VarChar);
        p3.Value = txtYear.Text;

        DataTable dt = objData.getData("TF_EBRC_GetERSDataList",p1,p2,p3);
        
        if (dt.Rows.Count > 0)
        {
            int nextSrNo;
            string srNo = dt.Rows[dt.Rows.Count - 1]["SrNo"].ToString().Trim();
            nextSrNo = Convert.ToInt32(srNo) + 1;
            hdnSrNo.Value = nextSrNo.ToString();
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewReturnData.PageSize = _pageSize;
            GridViewReturnData.DataSource = dt.DefaultView;
            GridViewReturnData.DataBind();
            GridViewReturnData.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewReturnData.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            hdnSrNo.Value = (1).ToString(); ;
            labelMessage.Visible = true;
        }
    }
    protected void GridViewReturnData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialNumber = new Label();
            Button btnDelete = new Button();
            lblSerialNumber = (Label)e.Row.FindControl("lblSerialNumber");

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";

            if (Session["userRole"].ToString().Trim() == "Supervisor")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only Supervisor can delete all records.');return false;");
           
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {

                //string pageurl = "window.location='TF_EBRC_AddEditERSdata.aspx?mode=edit&srno=" + lblSerialNumber.Text.Trim()+ "&BranchName=" + ddlBranch.Text + "'";
                string pageurl = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "aQ4IHcbW6xwIKa7MSP0RpLajvtgpAy0qDuqiCohb2nuVR5fpqTTS0SB8YUPGgZbs/" + objEnc.URLIDEncription("edit") + "/" + objEnc.URLIDEncription(lblSerialNumber.Text.Trim()) + "/" + objEnc.URLIDEncription(ddlBranch.Text.Trim()) + @"'";
                if (i != 11)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewReturnData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";

        string _SrNo = "";
        string[] values_p;

        string str = e.CommandArgument.ToString();


        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            _SrNo = values_p[0].ToString();
        }


        SqlParameter P1 = new SqlParameter("@serialnumber", SqlDbType.VarChar);
        P1.Value = _SrNo;
          SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        TF_DATA objData = new TF_DATA();

        result = objData.SaveDeleteData("TF_EBRC_DeleteERSData",P1,p2);
        fillGrid();
      if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else if (result == "locked")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('EBRC Certificate already generated for this Sr No,You cannot Delete.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
        ddlBranch.Focus();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
  
}