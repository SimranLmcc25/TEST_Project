using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class CTR_CTR_View_TransactionFileCBA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else {
            if (!IsPostBack)
            {
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                txtyear.Text = System.DateTime.Now.Year.ToString();
                //LastTransID();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "added")
                    {
                        string transID = Request.QueryString["TransactionID"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Transaction ID : "+transID+"');", true);
                }
                else
                    if (Request.QueryString["result"].Trim() == "updated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    }
                }
                txtID.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtyear.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtyear.Attributes.Add("onblur", "return checkYear();");
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
        ddlbranch.Items.Clear();
        ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "--Select--";
            ddlbranch.DataSource = dt.DefaultView;
            ddlbranch.DataTextField = "BranchName";
            ddlbranch.DataValueField = "BranchName";
            ddlbranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        rowPager.Visible = false;
        ddlbranch.Items.Insert(0, li);
        ddlbranch.Focus();
    }

    protected void fillgrid()
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text.Trim());
        SqlParameter p2 = new SqlParameter("@BRANCHNAME", ddlbranch.Text.Trim());
        SqlParameter p3 = new SqlParameter("@Year", txtyear.Text);

        string query = "CTR_Get_CBATransactionFile";
        
        DataTable dt = objData.getData(query, p1, p2,p3);
        if (dt.Rows.Count > 0)
        {
            int record = dt.Rows.Count;
            int pagesize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewTransactionFile.PageSize = pagesize;

            GridViewTransactionFile.DataSource = dt.DefaultView;
            GridViewTransactionFile.DataBind();
            GridViewTransactionFile.Visible = true;

            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(record, pagesize);
        }
        else
        {
            GridViewTransactionFile.Visible = false;
            rowPager.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Visible = true;
            labelMessage.Text = "No Record(s) Found";
        }
    }

    protected void LastTransID()
    {
        TF_DATA objData = new TF_DATA();
        
        SqlParameter p2 = new SqlParameter("@BRANCHNAME", ddlbranch.Text.Trim());
        SqlParameter p3 = new SqlParameter("@Year", txtyear.Text);
        string query = "CTR_Last_TransactionID";

        DataTable dt = objData.getData(query, p2,p3);
        if (dt.Rows.Count > 0)
        {
            txtID.Text = dt.Rows[0]["TransID"].ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewTransactionFile.PageCount != GridViewTransactionFile.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewTransactionFile.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewTransactionFile.PageIndex + 1) + " of " + GridViewTransactionFile.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewTransactionFile.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewTransactionFile.PageIndex != (GridViewTransactionFile.PageCount - 1))
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
        GridViewTransactionFile.PageIndex = 0;
        fillgrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewTransactionFile.PageIndex > 0)
        {
            GridViewTransactionFile.PageIndex = GridViewTransactionFile.PageIndex - 1;
        }
        fillgrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewTransactionFile.PageIndex != GridViewTransactionFile.PageCount - 1)
        {
            GridViewTransactionFile.PageIndex = GridViewTransactionFile.PageIndex + 1;
        }
        fillgrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewTransactionFile.PageIndex = GridViewTransactionFile.PageCount - 1;
        fillgrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string TransID=txtCTR.Text+"/"+txtID.Text+"/"+txtbranch.Text+"/"+txtyear.Text;

        if (ddlbranch.SelectedIndex.ToString() == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select Branch.');", true);
            ddlbranch.Focus();
        }
        else
            Response.Redirect("CTR_AddEdit_TransactionFileCBA.aspx?mode=add&Branch=" + ddlbranch.SelectedValue.Trim() + "&TransID="+TransID+"",true);
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", ddlbranch.Text.Trim());
       
        string query = "TF_GetBranchDetails2";
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtbranch.Text = dt.Rows[0]["Bcode"].ToString().Trim();
            LastTransID();
        }
        else
        {
            txtbranch.Text = "";
        } 
        fillgrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void GridViewTransactionFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTransID = new Label();
            Label lblRefNo = new Label();
            Label lblTransactionDate = new Label();
            Label lblAcNo = new Label();

            Button btnDelete = new Button();
            lblTransID = (Label)e.Row.FindControl("lblTransID");
            lblRefNo = (Label)e.Row.FindControl("lblRefNo");
            lblTransactionDate = (Label)e.Row.FindControl("lblTransactionDate");
            lblAcNo=(Label)e.Row.FindControl("lblAcNo");

            btnDelete = (Button)e.Row.FindControl("btnDelete");

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

                string pageurl = "window.location='CTR_AddEdit_TransactionFileCBA.aspx?mode=edit&TransactionNo=" + lblTransID.Text.Trim() + "&TrnsactionDate=" + lblTransactionDate.Text.Trim() + "&Acno=" + lblAcNo.Text.Trim() + "&Branch=" + ddlbranch.SelectedValue.Trim() + "'";
                
                if (i != 5)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewTransactionFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string branch = ddlbranch.SelectedValue.Trim();
        string transid = "";
        string acno = "";
        string[] values_P;
        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_P = str.Split(splitchar);
            branch = values_P[0].ToString();

            transid = values_P[1].ToString();
            acno = values_P[2].ToString();
        }
        SqlParameter p1 = new SqlParameter("@BRANCHNAME", branch);
        SqlParameter p2 = new SqlParameter("@transid", transid);
        SqlParameter p3 = new SqlParameter("@acno", acno);
        string query = "CTR_Delete_CBATransactionFile";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(query, p1, p2, p3);
        fillgrid();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        }
    }
    protected void txtyear_TextChanged(object sender, EventArgs e)
    {
        fillgrid();
        LastTransID();
    }
}