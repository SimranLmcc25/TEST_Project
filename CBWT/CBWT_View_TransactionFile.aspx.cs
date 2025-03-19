using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class CBWT_View_TransactionFile : System.Web.UI.Page
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
                ddlrecordperpage.SelectedValue = "20";
                txtYearMonth.Text = DateTime.Now.Date.ToString("MM/yyyy");
                fillBranch();
                ddlRefNo.SelectedIndex = 1;
                fillgrid();

                ddlRefNo.Focus();
                

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "added")
                    {
                        string Srno = Request.QueryString["SRNO"].ToString();
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Reference No : "+Srno+"');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }

                }
                btnAdd.Attributes.Add("onclick", "return val_MonthYear();");
                //// txtYearMonth.Attributes.Add("onblur", "return validDate(" + txtYearMonth.ClientID + ",'Year Momth');");
                //txtYearMonth.Attributes.Add("onblur", "return isValidDate(" + txtYearMonth.ClientID + ",'Year Month');");
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void GridViewBankCodeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string RefNo = ddlRefNo.SelectedValue.Trim();
        string YEARMONTH = txtYearMonth.Text.Trim();
        string SRNO = "";
        string[] values_P;
        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_P = str.Split(splitchar);
            RefNo = values_P[0].ToString();

            YEARMONTH = values_P[1].ToString();
            SRNO = values_P[2].ToString();

        }
        SqlParameter p1 = new SqlParameter("@refcode", RefNo);
        SqlParameter p2 = new SqlParameter("@yearmonth", YEARMONTH);
        SqlParameter p3 = new SqlParameter("@srno", SRNO);
        string query = "Delete_Transaction_File";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(query, p1, p2, p3);
        fillgrid();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        }

    }
    protected void GridViewBankCodeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTransactionRefNo = new Label();
            Label lblTransactionDate = new Label();
            Label lblSrNo = new Label();
            Button btnDelete = new Button();
            lblTransactionRefNo = (Label)e.Row.FindControl("lblTransactionRefNo");
            lblTransactionDate = (Label)e.Row.FindControl("lblTransactionDate");
            lblSrNo = (Label)e.Row.FindControl("lblSrNo");
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
                string pageurl = "window.location='CBWT_AddEdit_TransactionFile.aspx?mode=edit&TransactionDate=" + lblTransactionDate.Text.Trim() + "&TransactionRefNo=" + lblTransactionRefNo.Text.Trim() + "&SrNo=" + lblSrNo.Text.Trim() + "&Branch=" + ddlRefNo.SelectedValue.ToString() + "&yearmonth=" + txtYearMonth.Text.Trim() + "'";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlRefNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlRefNo.Items.Clear();
        ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "--Select--";
            ddlRefNo.DataSource = dt.DefaultView;
            ddlRefNo.DataTextField = "BranchCode";
            ddlRefNo.DataValueField = "BranchCode";
            ddlRefNo.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlRefNo.Items.Insert(0, li);
        ddlRefNo.Focus();

    }

    //protected void fillBranchNo()
    //{
    //    TF_DATA objData = new TF_DATA();

    //    //SqlParameter p1 = new SqlParameter("@Branchno",SqlDbType.VarChar);
    //    //p1.Value = "";
    //    SqlParameter p1 = new SqlParameter("@Branchno", "37705");
    //    //p1.Value = "37705";
    //    string query = "";
    //    query = "test";
    //    DataTable dt = objData.getData(query,p1);
    //    ddlRefNo.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "--Select--";
    //        ddlRefNo.DataSource = dt.DefaultView;
    //        ddlRefNo.DataTextField = "BranchCode";
    //        ddlRefNo.DataValueField = "BranchCode";
    //        ddlRefNo.DataBind();
    //    }
    //    else
    //        li.Text = "no Record(s) Found";
    //    ddlRefNo.Items.Insert(0, li);
    //    ddlRefNo.SelectedIndex = 0;
    //    ddlRefNo.Focus();
    //}
    protected void fillgrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@refno", SqlDbType.VarChar);
        p2.Value = ddlRefNo.Text.Trim();
        SqlParameter p3 = new SqlParameter("@yearmonth", SqlDbType.VarChar);
        p3.Value = txtYearMonth.Text.Trim();
        string query = "Get_Transaction_FileList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3);

        if (dt.Rows.Count > 0)
        {
            int record = dt.Rows.Count;
            int pagesize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewBankCodeList.PageSize = pagesize;

            GridViewBankCodeList.DataSource = dt.DefaultView;
            GridViewBankCodeList.DataBind();
            GridViewBankCodeList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(record, pagesize);
        }
        else
        {
            GridViewBankCodeList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBankCodeList.PageCount != GridViewBankCodeList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBankCodeList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBankCodeList.PageIndex + 1) + " of " + GridViewBankCodeList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBankCodeList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBankCodeList.PageIndex != (GridViewBankCodeList.PageCount - 1))
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
        GridViewBankCodeList.PageIndex = 0;
        fillgrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBankCodeList.PageIndex > 0)
        {
            GridViewBankCodeList.PageIndex = GridViewBankCodeList.PageIndex - 1;
        }
        fillgrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBankCodeList.PageIndex != GridViewBankCodeList.PageCount - 1)
        {
            GridViewBankCodeList.PageIndex = GridViewBankCodeList.PageIndex + 1;
        }
        fillgrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBankCodeList.PageIndex = GridViewBankCodeList.PageCount - 1;
        fillgrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlRefNo.SelectedIndex.ToString() == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select Reference No.');", true);
            ddlRefNo.Focus();
        }
        else

            Response.Redirect("CBWT_AddEdit_TransactionFile.aspx?mode=add&Branch=" + ddlRefNo.SelectedValue.Trim() + "&YearMonth=" + txtYearMonth.Text.Trim() + "");
    }
    protected void txtYearMonth_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}