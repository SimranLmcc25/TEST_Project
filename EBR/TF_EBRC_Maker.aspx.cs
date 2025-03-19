using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class EBR_TF_EBRC_Maker : System.Web.UI.Page
{
    Encryption objEnc = new Encryption();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoggedUserId"] == null)
        {
            //Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "AO0gtPK5RIS5S1JzBJeCQ/" + objEnc.URLIDEncription("yes") + "/" + "", true);
        }
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            //Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription(lbl.Value));
        }
       
           
        if(!IsPostBack)     
        {
            PageAccess();
        
            ddlIRMstatus.SelectedValue = "1";
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            fillGrid();
            //if (Request.QueryString["result"] != null)
            if (HttpContext.Current.Items["result"] != null)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction has been sent to checker.');", true);
            }
      
           
        }
        
        btnnavfirst.Visible = false;
        btnnavnext.Visible = false;
        btnnavpre.Visible = false;
        btnnavlast.Visible = false;
        jsScript1.Text = "<script type=\"text/javascript\" src='" + ResolveClientUrl("~/Scripts/jquery-1.4.1.min.js") + "'></script>";
        jsScript2.Text = "<script type=\"text/javascript\" src='" + ResolveClientUrl("~/Scripts/jquerynew.min.js") + "'></script>";
        cssLink.Text = "<link href='" + ResolveClientUrl("../Scripts/JueryUI.css") + "' rel='stylesheet' type='text/css' media='screen' />";
        jsScript3.Text = "<script type=\"text/javascript\" src='" + ResolveClientUrl("~/Scripts/jquery-ui.js") + "'></script>";
        jsScript4.Text = "<script type=\"text/javascript\" src='" + ResolveClientUrl("~/Scripts/MyJquery1.js") + "'></script>";
        jsScript5.Text = "<script type=\"text/javascript\" src='" + ResolveClientUrl("~/Scripts/Enable_Disable_Opener.js") + "'></script>";
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

    //Fetch data button 
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    fillGrid();
    //}

    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();

       SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _IRMstatus = "";
        if(ddlIRMstatus.SelectedValue=="1")
        {
            _IRMstatus = "F";
        }
        else if (ddlIRMstatus.SelectedValue == "2")
        {
            _IRMstatus = "A";
        
        }
        else if (ddlIRMstatus.SelectedValue == "3")
        {
            _IRMstatus = "C";
        }

        SqlParameter p2 = new SqlParameter("@fromdate",txtfromDate.Text);
        SqlParameter p3 = new SqlParameter("@todate", txtToDate.Text);

        SqlParameter i_Status = new SqlParameter("@IRMstatus", SqlDbType.VarChar);
        i_Status.Value = _IRMstatus;
        SqlParameter p4 = new SqlParameter("@status", "Reject");
        DataTable dt = objData.getData("TF_EBRC_IRMFileUpload_Maker_GetDetails", p1, p4, p2, p3, i_Status);
   
        if (dt.Rows.Count > 0)
        {
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
            labelMessage.Visible = true;
        }
        
        //TF_DATA objData = new TF_DATA();

        //DataTable dt1 = objData.getData("TF_EBRC_InsertIntoTable");
        //DataTable dt = objData.getData("TF_EBRC_GetIRMDetails");

        //if (dt.Rows.Count > 0)
        //{
        //    int _records = dt.Rows.Count;
        //    int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
        //    GridViewReturnData.PageSize = _pageSize;
        //    GridViewReturnData.DataSource = dt.DefaultView;
        //    GridViewReturnData.DataBind();
        //    GridViewReturnData.Visible = true;
        //    rowGrid.Visible = true;
        //    rowPager.Visible = true;
        //    labelMessage.Visible = false;
        //    pagination(_records, _pageSize);
        //}
        //else
        //{
        //    GridViewReturnData.Visible = false;
        //    rowGrid.Visible = false;
        //    rowPager.Visible = false;
        //    labelMessage.Text = "No record(s) found.";
        //    labelMessage.Visible = true;
        //}
    }

    protected void GridViewReturnData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        if (e.Row.RowType == DataControlRowType.DataRow) 
        {
            Label lblIRMNumber = new Label();
            lblIRMNumber = (Label)e.Row.FindControl("lblIRMNumber");
            Label labStatus = new Label();
            labStatus=(Label)e.Row.FindControl("lblstatus");

            Label _IRMstatus = new Label();
            _IRMstatus = (Label)e.Row.FindControl("lblIRMStatus");


           // string _IRMstatus = "";
          

            int i = 0;
            if (labStatus.Text == "Reject")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            } 

            foreach (TableCell cell in e.Row.Cells)
            {
              
               //string pageurl = "window.location='EBRC_Maker_DataEntry.aspx?DocNo=" + lblIRMNumber.Text.Trim() + "&status=" + labStatus.Text.Trim() + "&irmstatus=" + _IRMstatus.Text.Trim() + "'";
                string pageurl = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "WXTLdYOWi18XdzrltOkQPtJZE9gkDET7enosSmP3Eo/" + objEnc.URLIDEncription(lblIRMNumber.Text.Trim()) + "/" + objEnc.URLIDEncription(labStatus.Text) + "/" + objEnc.URLIDEncription(_IRMstatus.Text) + @"'";
                if (i != 11)
                    cell.Attributes.Add("onclick", pageurl);
                  
                  
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }

        // TF_DATA objData = new TF_DATA();
        
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //       Label lblDocNo = new Label();
        //    lblDocNo = (Label)e.Row.FindControl("lblDocNo");

        //    Label labStatus=new Label();
        //    labStatus=(Label)e.Row.FindControl("lblstatus");
          
        //    int i = 0;
        //    if (labStatus.Text == "Reject")
        //    {
        //       e.Row.BackColor = System.Drawing.Color.Tomato;
        //    }

        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        string pageurl = "window.location='EBRC_Maker_DataEntry.aspx?status="+labStatus.Text.Trim()+"&DocNo=" + lblDocNo.Text.Trim() + "'";
                
                
        //        if (i != 11)
        //            cell.Attributes.Add("onclick", pageurl);
                  
                  
        //        else
        //            cell.Style.Add("cursor", "default");
        //        i++;
        //    }
        //}
    }

    protected void GridViewReturnData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    //protected void fillGridUpload()
    //{
    //    //string _status = "";

    //    //if (rdb_Approved.Checked == true)
    //    //{
    //    //    _status = "Approve";
    //    //}
    //    //if (rdb_Pending.Checked == true)
    //    //{
    //    //    _status = "Pending";
    //    //}
    //    //if (rdb_Reject.Checked == true)
    //    //{
    //    //    _status = "Reject";

    //    //}
    //    //SqlParameter p_Status = new SqlParameter("@status", SqlDbType.VarChar);
    //    //p_Status.Value = _status;

    //    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
    //    dateInfo.ShortDatePattern = "dd/MM/yyyy";
    //    DateTime From_Date = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
    //    DateTime To_Date = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
    //    SqlParameter p2 = new SqlParameter("@startdate", txtfromDate.Text);
    //    SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);
    //    TF_DATA objData = new TF_DATA();
    //    DataTable dt = objData.getData("TF_EBRC_IRMFileupload_Maker_showUploadData", p2, p3);
    //    if (dt.Rows.Count > 0)
    //    {
    //        int _records = dt.Rows.Count;
    //        int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
    //        GridViewReturnData.PageSize = _pageSize;
    //        GridViewReturnData.DataSource = dt.DefaultView;
    //        GridViewReturnData.DataBind();
    //        GridViewReturnData.Visible = true;
    //        rowGrid.Visible = true;
    //        rowPager.Visible = true;
    //        labelMessage.Visible = false;
    //        pagination(_records, _pageSize);
    //    }
    //    else
    //    {
    //        GridViewReturnData.Visible = false;
    //        rowGrid.Visible = false;
    //        rowPager.Visible = false;
    //        labelMessage.Text = "No record(s) found.";
    //        labelMessage.Visible = true;
    //    }
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtfromDate.Text == "")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select From Upload date')", true);

        }
        else
        {
            fillGrid();
           // fillGridUpload();
           // txtfromDate.Text = "";
        }
    }

    //protected void fillGridIrmStatus()
    //{
    //    string _IRMstatus = ddlIRMstatus.SelectedItem.Text;

    //    SqlParameter i_Status = new SqlParameter("@IRMstatus", SqlDbType.VarChar);
    //    i_Status.Value = _IRMstatus;
    //    TF_DATA objData = new TF_DATA();

    //    DataTable dt = objData.getData("TF_getIRMStatus_Details", i_Status);
    //    if (dt.Rows.Count > 0)
    //    {
    //        int _records = dt.Rows.Count;
    //        int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
    //        GridViewReturnData.PageSize = _pageSize;
    //        GridViewReturnData.DataSource = dt.DefaultView;
    //        GridViewReturnData.DataBind();
    //        GridViewReturnData.Visible = true;
    //        rowGrid.Visible = true;
    //        rowPager.Visible = true;
    //        labelMessage.Visible = false;
    //        pagination(_records, _pageSize);
    //    }
    //    else
    //    {
    //        GridViewReturnData.Visible = false;
    //        rowGrid.Visible = false;
    //        rowPager.Visible = false;
    //        labelMessage.Text = "No record(s) found.";
    //        labelMessage.Visible = true;
    //    }
    //}

    protected void ddlIRMstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
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
            string result = "Access denied";

            //string redirectUrl = ResolveUrl("~/TF_Logout.aspx");
            string redirectUrl = ResolveUrl(ConfigurationManager.AppSettings["webpath"] + "0rJaTMnF39W4f93iMtXSg/");
            string script = "alert('" + result + "');";
            script += "window.location.href = '" + redirectUrl + "';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "RedirectScript", script, true);

        }
    }
} 
    