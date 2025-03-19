using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TRF_CTR_Cummulative_Acc_View : System.Web.UI.Page
{
    static string yearmonth;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/PS_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                //txtmontYear.Text = "01/2015";
                txtmontYear.Text = System.DateTime.Now.ToString("MM/yyyy");
           
              
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }

                btnAdd.Attributes.Add("onclick", "return val_MonthYear();");
            }
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
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
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        rowPager.Visible = false;
        ddlBranch.Items.Insert(0, li);
    }
    protected void fillGrid()
    {
        yearmonth = (txtmontYear.Text).Substring(3, 4) + (txtmontYear.Text).Substring(0, 2);
        string search = txtSearch.Text.Trim();

        SqlParameter p1 = new SqlParameter("@RefNo", ddlBranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@yearMonth", yearmonth);
        SqlParameter p3 = new SqlParameter("@Search", search);

        string _query = "CTR_CummulativeAccount_GetList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2,p3);
        GridViewCummTotalEntry.DataSource = dt.DefaultView;
        GridViewCummTotalEntry.DataBind();
        GridViewCummTotalEntry.Visible = true;
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCummTotalEntry.DataSource = dt.DefaultView;
            GridViewCummTotalEntry.DataBind();
            GridViewCummTotalEntry.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCummTotalEntry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
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
            if (GridViewCummTotalEntry.PageCount != GridViewCummTotalEntry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCummTotalEntry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCummTotalEntry.PageIndex + 1) + " of " + GridViewCummTotalEntry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCummTotalEntry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCummTotalEntry.PageIndex != (GridViewCummTotalEntry.PageCount - 1))
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
        GridViewCummTotalEntry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCummTotalEntry.PageIndex > 0)
        {
            GridViewCummTotalEntry.PageIndex = GridViewCummTotalEntry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCummTotalEntry.PageIndex != GridViewCummTotalEntry.PageCount - 1)
        {
            GridViewCummTotalEntry.PageIndex = GridViewCummTotalEntry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCummTotalEntry.PageIndex = GridViewCummTotalEntry.PageCount - 1;
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void GridViewCummTotalEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAcNo = new Label();
            Label lblReportTYpe = new Label();
            Button btnDelete = new Button();
            lblAcNo = (Label)e.Row.FindControl("lblAcNo");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            lblReportTYpe = (Label)e.Row.FindControl("lblReportType");
            


            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (Session["userRole"].ToString().Trim() == "Supervisor")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only Supervisor can delete all records.');return false;");
           
            
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='CTR_Cummulative_Account.aspx?mode=edit&AcNo=" + lblAcNo.Text.Trim() + "&RefNo=" + ddlBranch.SelectedValue + "&YearMonth=" + txtmontYear.Text.Trim() +"&ReportType=" +lblReportTYpe.Text.Trim()+"'";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewCummTotalEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _AcNo = e.CommandArgument.ToString();
        yearmonth = (txtmontYear.Text).Substring(3, 4) + (txtmontYear.Text).Substring(0, 2);

        string AccountNo = "", ReportType = "";
    
        string[] values_p;
        if (_AcNo != "")
        {
            char[] splitchar = { ';' };
            values_p = _AcNo.Split(splitchar);
            AccountNo = values_p[0].ToString();
            ReportType = values_p[1].ToString();

        }




        SqlParameter p1 = new SqlParameter("@RefNo", ddlBranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@yearMonth", yearmonth);
        SqlParameter p3 = new SqlParameter("@AcNo", AccountNo);
        SqlParameter p4= new SqlParameter("@TransType",ReportType);

        string _query = "CTR_CummulativeAccount_Delete";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2, p3,p4);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex.ToString() == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select Branch.');", true);
            ddlBranch.Focus();
        }
        else
        Response.Redirect("CTR_Cummulative_Account.aspx?mode=add&RefNo=" + ddlBranch.SelectedValue+"&monthyear="+txtmontYear.Text.Trim());
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtmontYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}