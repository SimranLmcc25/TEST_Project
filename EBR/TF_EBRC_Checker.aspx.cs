using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Net;


public partial class EBR_TF_EBRC_Checker: System.Web.UI.Page
{
    TF_DATA objdata = new TF_DATA();
    Encryption objEnc = new Encryption();
    string ipAddressW = GetIPAddress();
    string Log_Query = "TF_Audit_ApplicationLogs";
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
                PageAccess();

                ddlrecordperpage.SelectedValue = "20";
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlIRMstatus.SelectedValue = "1";
                fillGrid();
                
                btnapprove.Visible = true; 
                btnapprove.Attributes.Add("onclick", "return Confirm();");
                Page.DataBind();

                //if (Request.QueryString["result_"] != null)
                if (HttpContext.Current.Items["result_"] != null)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction is Approved')", true);
                }

              //if (Request.QueryString["result"] != null)
                if (HttpContext.Current.Items["result"] != null)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Transaction has been sent to Maker.');", true);

                }

             
            }
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
    protected void fillGrid()
    {
        DGFTStatusList();
        string _status = "";
     
        if (rdb_Approved.Checked == true)
        {
            _status = "Approve";
        }
        if (rdb_Pending.Checked == true)
        {
            _status = "Pending";
        } 
        if (rdb_Reject.Checked == true) 
        {
            _status = "Reject";
        
        }

        string search = txtSearch.Text.Trim();

        //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        //dateInfo.ShortDatePattern = "dd/MM/yyyy";

        //DateTime newDate = new DateTime();

        //bool isdate = false;

        //try
        //{
        //    newDate = Convert.ToDateTime(txtSearch.Text.Trim(), dateInfo);
        //    isdate = true;
        //}
        //catch { }

        //if (isdate)
        //{ search = newDate.ToString(); }

        //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        //dateInfo.ShortDatePattern = "dd/MM/yyyy";
        //DateTime From_Date = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        //DateTime To_Date = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        string _IRMstatus = "";
        if (ddlIRMstatus.SelectedValue == "1")
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
        //else if (ddlIRMstatus.SelectedValue == "0")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select irm status')", true);
        //    ddlIRMstatus.Focus();
        //}
        SqlParameter p2 = new SqlParameter("@fromdate", txtfromDate.Text);
        SqlParameter p3 = new SqlParameter("@todate", txtToDate.Text);

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p_Status = new SqlParameter("@status", SqlDbType.VarChar);
        p_Status.Value = _status;
       
        SqlParameter i_Status = new SqlParameter("@irmstatus", SqlDbType.VarChar);
        i_Status.Value = _IRMstatus;

        TF_DATA objData = new TF_DATA();
     
        DataTable dt = objData.getData("EBRC_IRM_FileUpload_displayCheckerData", p_Status, p1, p2, p3, i_Status);
       
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
        
    }
    protected void rdb_Approved_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
      
        GridViewReturnData.Columns[9].Visible = false;
        GridViewReturnData.Columns[10].Visible = false;
        btnapprove.Enabled = false;
        //if (labelMessage.Text == "No record(s) found.")
        //{
        //    txtfromDate.Visible = false;
        //    txtToDate.Visible = false;
       //     trid.Visible = false;
        //    btnSave.Visible = false;
        //    tddata.attributes.add("style", "display:none");
        //}

    }
    protected void GridViewReturnData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) 
        {
            Label lblIRMNumber = new Label();
            lblIRMNumber = (Label)e.Row.FindControl("lblIRMNumber");

            Label labStatus = new Label();
            Button btnDelete = new Button();
            Label uniquetxid = new Label();
            Label irmstatus = new Label();
            labStatus = (Label)e.Row.FindControl("lblstatus");
            uniquetxid = (Label)e.Row.FindControl("lblBankUniqueTransactionId");
            irmstatus = (Label)e.Row.FindControl("lblIRMStatus");

            string status_ = "";

            if (rdb_Approved.Checked == true)
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
                status_ = "Approve";
            }
            if(rdb_Pending.Checked==true)
            {
                status_ = "Pending";
            }
            if (rdb_Reject.Checked == true)
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
                status_ = "Reject";
            }


            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";

            if (Session["userRole"].ToString().Trim() == "Supervisor")
            {
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            }
            else 
            {
                btnDelete.Attributes.Add("onclick", "alert('Only Supervisor can delete all records.');return false;");
            }

            CheckBox chk = (CheckBox)e.Row.FindControl("chkselect");
            string result;
            SqlParameter p1 = new SqlParameter("@irmno", lblIRMNumber.Text.Trim());
            SqlParameter p2 = new SqlParameter("@irmstatus", irmstatus.Text.Trim());
            result = objdata.SaveDeleteData("TF_EBRC_IRM_EnableDisableChkGrid", p1, p2);
          
                int i = 0;
                foreach (TableCell cell in e.Row.Cells)
                {
                  //  string pageurl = "window.location='EBRC_Checker_DataEntry.aspx?mode=edit&irmno=" + lblIRMNumber.Text.Trim() + "&status=" + status_ + "&uniquetxid=" + uniquetxid.Text.Trim() + "&irmstatus=" + irmstatus.Text.Trim() + "'";
                    string pageurl = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "qhTSR5ZZhC5HI7RcOdTwUvpZcRe0qh9jjDOParoWXTJVXRiRm5QFu2MY1baW86sdOV2WKyIWYKKusOSNsDQ/" + objEnc.URLIDEncription("edit") + "/" + objEnc.URLIDEncription(lblIRMNumber.Text.Trim()) + "/" + objEnc.URLIDEncription(status_) + "/" + objEnc.URLIDEncription(uniquetxid.Text.Trim()) + "/" + objEnc.URLIDEncription(irmstatus.Text.Trim()) + @"'";

                    if (i !=9 && i!=10)
                        cell.Attributes.Add("onclick", pageurl);

                    else
                        if (result == "Exists")
                        {
                            chk.Enabled = false;
                            chk.Checked = false;

                        }
                        else {
                            cell.Style.Add("cursor", "default");
                        
                        }
                      
                    i++;


                }
        }
     

    }
    protected void GridViewReturnData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";

        string Irmno = "";
        string[] values_p;

        string str = e.CommandArgument.ToString();


        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            Irmno = values_p[0].ToString();
        }


        SqlParameter P1 = new SqlParameter("@IRMno", SqlDbType.VarChar);
        P1.Value = Irmno;
      
       
        TF_DATA objData = new TF_DATA();

        result = objData.SaveDeleteData("TF_EBRC_DeleteIRMData", P1);
        fillGrid();
        if (result == "deleted")
        {

            SqlParameter userid = new SqlParameter("@userID", SqlDbType.VarChar);
            userid.Value = Session["userName"].ToString().Trim();
            SqlParameter IP = new SqlParameter("@IP", SqlDbType.VarChar);
            IP.Value = ipAddressW;
            SqlParameter Timestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
            Timestamp.Value = System.DateTime.Now;
            SqlParameter ptype = new SqlParameter("@type", SqlDbType.VarChar);
            ptype.Value = "EBRC IRM Data Entry View - Checker";
            SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
            pstatus.Value = "Record Deleted: " + Irmno;
            string store_logs = objdata.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        
        }
                            
        //else if (result == "locked")
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('EBRC Certificate already generated for this Sr No,You cannot Delete.');", true);
        //else
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);




    }
    protected void rdb_Pending_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
      
        GridViewReturnData.Columns[10].Visible = true;
        GridViewReturnData.Columns[9].Visible = true;
        btnapprove.Visible = true;
      
        //if (labelMessage.Text == "No record(s) found.")
        //{
        //    txtfromDate.Visible = false;
        //    txtToDate.Visible = false;
        //    btnSave.Visible = false;
        //}
    }
    protected void rdb_Reject_CheckedChanged(object sender, EventArgs e)
    {
          
     
        fillGrid();
        GridViewReturnData.Columns[9].Visible = false;
        GridViewReturnData.Columns[10].Visible = false;
        btnapprove.Enabled = false;
       
        //if (labelMessage.Text == "No record(s) found.")
        //{
        //    btnSave.Visible = false;
        //    txtfromDate.Visible = false;
        //    txtToDate.Visible = false;
            
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtfromDate.Text=="")
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select From Upload date')", true);

        }
        else 
        {
            fillGrid();
        
          //txtfromDate.Text = "";
        }
    }
    protected void ddlIRMstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIRMstatus.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select irm status')", true);
            ddlIRMstatus.Focus();
        }
        if (ddlIRMstatus.SelectedValue == "1")
        {
            fillGrid();
            btnapprove.Visible = true;
        }
        else if (ddlIRMstatus.SelectedValue == "2")
        {
            fillGrid();
            btnapprove.Visible = true;
        }
        else if (ddlIRMstatus.SelectedValue == "3")
        {
            fillGrid();
            btnapprove.Visible = true;
        }

        //fillGrid();

    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        Label lblirmno = new Label();
        Label irmstatus = new Label();
        foreach (GridViewRow gvrow in GridViewReturnData.Rows)
        {
            if (gvrow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_ = (CheckBox)gvrow.FindControl("chkall");
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk.Checked)
                {
                    lblirmno = (gvrow.Cells[0].FindControl("lblIRMNumber") as Label);
                    irmstatus = (gvrow.Cells[0].FindControl("lblIRMStatus") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@irmno", lblirmno.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@irmstatus", irmstatus.Text.Trim());
                    result = objdata.SaveDeleteData("TF_EBRC_IRM_EnableDisableChkGrid", p1, p2);
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                    else
                    {
                        btnapprove.Enabled = true;
                    }
                }
                else
                {
                    //CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    lblirmno = (gvrow.Cells[0].FindControl("lblIRMNumber") as Label);
                    irmstatus = (gvrow.Cells[0].FindControl("lblIRMStatus") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@irmno", lblirmno.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@irmstatus", irmstatus.Text.Trim());
                    result = objdata.SaveDeleteData("TF_EBRC_IRM_EnableDisableChkGrid", p1, p2);
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                    else
                    {
                         btnapprove.Enabled = true;
                    }
                }
            }
        }
    }
    //public void DGFTStatusList()
    //{
    //    //getuniquetxid list
    //    TF_DATA objdata = new TF_DATA();
    //    SqlParameter s1 = new SqlParameter("@startdate", txtfromDate.Text);
    //    SqlParameter s2 = new SqlParameter("@enddate", txtToDate.Text);
    //    SqlParameter s3 = new SqlParameter("@type", "IRM");
    //    DataTable dt1 = objdata.getData("TF_EBRC_IRMORM_Uniquetxid_list", s1, s2, s3);//uniqueidlist

    //    if (dt1.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt1.Rows.Count; i++)
    //        {
    //            string uniquetxid = dt1.Rows[i]["BkUniqueTxId"].ToString();
    //            SqlParameter a1 = new SqlParameter("@uniquetxid", uniquetxid);
    //            //DataTable dt_1 = objdata.getData("tf_EBRC_IRM_GETValidated", a1);
    //            //string status_1 = dt_1.ToString();
    //            //if (status_1 == "Validated")
    //            //{

    //            //}

    //            DataTable dt2 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_list", a1);//IRMORMStatusList
    //            if (dt2.Rows.Count > 0)
    //            {
    //                for (int j = 0; j < dt2.Rows.Count; j++)
    //                {
    //                    string json = dt2.Rows[j].ItemArray.GetValue(0).ToString();
    //                    //Creates DataTable 
    //                    DataTable dt11 = new DataTable();
    //                    dt11.Columns.Add("irmNumber", typeof(string));
    //                    dt11.Columns.Add("irmIssueDate", typeof(string));
    //                    dt11.Columns.Add("ackStatus", typeof(string));
    //                    dt11.Columns.Add("errorCode", typeof(string));
    //                    dt11.Columns.Add("errorDetails", typeof(string));

    //                    List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json);

    //                    #region Not use
    //                    //foreach (var item in jsonDataList)
    //                    //{
    //                    //    DataRow row = dt11.NewRow();
    //                    //    row["irmNumber"] = item["irmNumber"].ToString();
    //                    //    row["irmIssueDate"] = item["irmIssueDate"].ToString();
    //                    //    row["ackStatus"] = item["ackStatus"].ToString();

    //                    //    // Handle errorDetails as needed (e.g., convert it to a string)
    //                    //    row["errorDetails"] = item["errorDetails"].ToString();
    //                    //    string errordetails = item["errorDetails"].ToString();
    //                    //    List<JObject> jsonDataList2 = JsonConvert.DeserializeObject<List<JObject>>(errordetails);
    //                    //    foreach (var item2 in jsonDataList2)
    //                    //    {
    //                    //        DataRow row1 = dt11.NewRow();
    //                    //        row1["errorCode"] = item2["errorCode"].ToString();
    //                    //        row1["errorDetails"] = item2["errorDetails"].ToString();
    //                    //        dt11.Rows.Add(row1);
    //                    //    }
    //                    //    dt11.Rows.Add(row);

    //                    //}
    //                    #endregion
    //                    JArray jsonArray = JArray.Parse(json);

    //                    foreach (var item in jsonArray)
    //                    {

    //                        JObject jsonObject = (JObject)item;

    //                        string irmNumber = jsonObject["irmNumber"].ToString();
    //                        string irmIssueDate = jsonObject["irmIssueDate"].ToString();
    //                        string ackStatus = jsonObject["ackStatus"].ToString();
    //                        string errorCode = "";
    //                        string errorDetails = "";
    //                        JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

    //                        foreach (var errorDetailsItem in errorDetailsArray)
    //                        {
    //                            JObject errorDetailsObject = (JObject)errorDetailsItem;

    //                            errorCode = errorDetailsObject["errorCode"].ToString();
    //                            errorDetails = errorDetailsObject["errorDetails"].ToString();
    //                            dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails);

    //                        }
    //                        dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails);

    //                    }


    //                    //insert datatatble into table
    //                    string result = "";
    //                    string irmno = "", irmissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
    //                    if (dt11.Rows.Count > 0)
    //                    {
    //                        for (int k = 0; k < dt11.Rows.Count; k++)
    //                        {
    //                            irmno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
    //                            irmissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
    //                            ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
    //                            errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
    //                            errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
    //                            string irmunique = irmno + uniquetxid + errorcode;
    //                            SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                            SqlParameter i2 = new SqlParameter("@irmno", irmno);
    //                            SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate.Replace("/", ""));
    //                            SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
    //                            SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
    //                            SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
    //                            SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
    //                            result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

    //                        }
    //                    }
    //                }
    //            }
    //            DataTable dt3 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_ErrorDetaillist", a1);//IRMORMErrorDetailsList
    //            if (dt3.Rows.Count > 0)
    //            {
    //                for (int p = 0; p < dt3.Rows.Count; p++)
    //                {
    //                    string json1 = dt3.Rows[p].ItemArray.GetValue(0).ToString();
    //                    //Creates DataTable 
    //                    DataTable dt12 = new DataTable();
    //                    dt12.Columns.Add("errorCode", typeof(string));
    //                    dt12.Columns.Add("errorDetails", typeof(string));
    //                    List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json1);

    //                    JArray jsonArray1 = JArray.Parse(json1);

    //                    foreach (var item in jsonArray1)
    //                    {

    //                        JObject jsonObject1 = (JObject)item;

    //                        string errorCode = jsonObject1["errorCode"].ToString();
    //                        string errorDetails = jsonObject1["errorDetails"].ToString();

    //                        dt12.Rows.Add(errorCode, errorDetails);
    //                    }

    //                    //insert datatatble into table
    //                    //get irm number for this errordetails list
    //                    DataTable dt4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_IRMNo", a1);
    //                    string irmno_ = "";
    //                    string irmissuedate_ = "";
    //                    if (dt4.Rows.Count > 0)
    //                    {
    //                        for (int q = 0; q < dt4.Rows.Count; q++)
    //                        {
    //                            irmno_ = dt4.Rows[q]["IRMNumber"].ToString();
    //                            irmissuedate_ = dt4.Rows[q]["IRMIssueDate"].ToString();


    //                            string result = "";
    //                            string irmno = "", irmissuedate = "", errorcode = "", errordetails_ = "";
    //                            if (dt12.Rows.Count > 0)
    //                            {
    //                                for (int s = 0; s < dt12.Rows.Count; s++)
    //                                {
    //                                    irmno = irmno_;
    //                                    irmissuedate = irmissuedate_;
    //                                    errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
    //                                    errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
    //                                    string irmunique = irmno + uniquetxid + errorcode;
    //                                    SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                                    SqlParameter i2 = new SqlParameter("@irmno", irmno);
    //                                    SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate.Replace("/", ""));
    //                                    SqlParameter i4 = new SqlParameter("@ackstatus", "");
    //                                    SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
    //                                    SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
    //                                    SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
    //                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }


    //        }
    //    }

    //    string result_ = "";
    //    DataTable dt_d = objdata.getData("TF_EBRC_IRM_DGFT_Uniquetxid_List");// get uniquetxid and irm no
    //    if (dt_d.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_d.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_d.Rows[d]["UniqueTxId"].ToString();
    //            string irmno_ = dt_d.Rows[d]["irmormNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@irmno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus", d1, d2);
    //        }
    //    }

    //    //string result_1 = "";
    //    DataTable dt_d1 = objdata.getData("TF_EBRC_IRM_DGFT_Uniquetxid_List");// get uniquetxid and irm no
    //    if (dt_d.Rows.Count > 0)
    //    {
    //        for (int r = 0; r < dt_d1.Rows.Count; r++)
    //        {
    //            string uniquetxid_ = dt_d1.Rows[r]["UniqueTxId"].ToString();
    //            string irmno_ = dt_d1.Rows[r]["irmormNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@irmno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_Injsoncreated", d1, d2);
    //        }
    //    }

    //    DataTable dt_v = objdata.getData("TF_EBRC_IRM_DGFT_Uniquetxid_List_Validated");// get uniquetxid and irm no
    //    if (dt_v.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_v.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_v.Rows[d]["UniqueTxId"].ToString();
    //            string irmno_ = dt_v.Rows[d]["IRMNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@irmno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_ProcessedOrValidated", d1, d2);
    //        }
    //    }

    //    DataTable dt_v1 = objdata.getData("TF_EBRC_IRM_DGFT_Uniquetxid_List_ValidatedGet");// get uniquetxid and irm no
    //    if (dt_v1.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_v1.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_v1.Rows[d]["UniqueTxId"].ToString();
    //            string irmno_ = dt_v1.Rows[d]["IRMNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@irmno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_ProcessedOrValidated", d1, d2);
    //        }
    //    }

    //    DataTable dt_p = objdata.getData("TF_EBRC_IRM_DGFT_Uniquetxid_List_Processed");// get uniquetxid and irm no
    //    if (dt_p.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_p.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_p.Rows[d]["UniqueTxId"].ToString();
    //            string irmno_ = dt_p.Rows[d]["irmormNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@irmno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_ProcessedOrValidated", d1, d2);
    //        }
    //    }

    //}
    public void DGFTStatusList()
    {
        string Result = "";
        if (rdb_Approved.Checked == true)
        {
            TF_DATA objdata = new TF_DATA();
            SqlParameter s1 = new SqlParameter("@fromdate", txtfromDate.Text);
            SqlParameter s2 = new SqlParameter("@todate", txtToDate.Text);
            DataTable dt1 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_PushValidate", s1, s2);

            if (dt1.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt1.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt1.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Validated";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt2 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_PushFailed", s1, s2);
            if (dt2.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt2.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt2.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt3 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_GetValidated", s1, s2);
            if (dt3.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt3.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt3.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "ValidatedGet";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt4 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_GetProcessedorFailed", s1, s2);
            if (dt4.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt4.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt4.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Processed/failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            SqlParameter b1 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter b2 = new SqlParameter("@type", "IRM");
            DataTable dt_2 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_list", b1, b2);// list of IRMORMStatusList_Get from TF_EBRC_IRM_ORM_JSONOutput where  UniqueTxId=a1 and RMORMStatusList_Get is not null and IRMORMStatus_Get!='Validated'
            if (dt_2.Rows.Count > 0)
            {
                for (int j = 0; j < dt_2.Rows.Count; j++)
                {
                    string json = dt_2.Rows[j].ItemArray.GetValue(0).ToString(); // It retrieves a value from the first column of the current row (at index j) in the dt2 DataTable and converts it to a string. This value appears to be in JSON format.
                    string uniquetxid = dt_2.Rows[j].ItemArray.GetValue(1).ToString();
                    SqlParameter q1 = new SqlParameter("@uniquetxtid", uniquetxid);
                    if (json == "[]")
                    {
                        Result = objdata.SaveDeleteData("TF_EBRC_getstatus_Failed_IRM", q1);
                        Result = objdata.SaveDeleteData("TF_EBRC_getstatus_Failed_IRM_JsonCreatedTab", q1);
                    }
                    else
                    {
                        //Creates DataTable 
                        DataTable dt11 = new DataTable(); // creates a new DataTable named as 'dt11'  with columns "irmNumber," "irmIssueDate," "ackStatus," "errorCode," and "errorDetails". This DataTable is intended to store structured data from the JSON content.
                        dt11.Columns.Add("irmNumber", typeof(string));
                        dt11.Columns.Add("irmIssueDate", typeof(string));
                        dt11.Columns.Add("ackStatus", typeof(string));
                        dt11.Columns.Add("errorCode", typeof(string));
                        dt11.Columns.Add("errorDetails", typeof(string));

                        JArray jsonArray = JArray.Parse(json); // It parses the JSON string json using the Newtonsoft.Json library (specifically JArray.Parse) into a JArray named jsonArray.

                        foreach (var item in jsonArray) // it extracts values from the JSON objects (elements) in jsonArray to populate variables like irmNumber, irmIssueDate, ackStatus, and extracts an array of error details.
                        {

                            JObject jsonObject = (JObject)item;

                            string irmNumber = jsonObject["irmNumber"].ToString();
                            string irmIssueDate = jsonObject["irmIssueDate"].ToString();
                            string ackStatus = jsonObject["ackStatus"].ToString();
                            string errorCode = "";
                            string errorDetails = "";
                            JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

                            foreach (var errorDetailsItem in errorDetailsArray) // It iterates through the error details array and extracts errorCode and errorDetails for each item.
                            {
                                JObject errorDetailsObject = (JObject)errorDetailsItem;

                                errorCode = errorDetailsObject["errorCode"].ToString();
                                errorDetails = errorDetailsObject["errorDetails"].ToString();
                                dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails); // The extracted values are added as a new row to the dt11 DataTable for each iteration.

                            }
                            dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails); // The extracted values are added as a new row to the dt11 DataTable for each iteration.

                        }


                        //insert datatatble into table
                        string result = "";
                        string irmno = "", irmissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
                        if (dt11.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt11.Rows.Count; k++)
                            {
                                irmno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
                                irmissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
                                ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
                                errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
                                errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
                                string irmunique = irmno + uniquetxid + errorcode;
                                SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                                SqlParameter i2 = new SqlParameter("@irmno", irmno);
                                SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate.Replace("/", ""));
                                SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
                                SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                                SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                                SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                                result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7); // If no matching record is found in TF_EBRC_IRMORM_DGFT_List table where irmormNumber+uniquetxid+errorCode=@irmunique, a new record is inserted into the [TF_EBRC_IRMORM_DGFT_List] table. 

                                result = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTUpdateMainTable", i2, i1, i4, i5, i6); // If no matching record is found in TF_EBRC_IRMORM_DGFT_List table where irmormNumber+uniquetxid+errorCode=@irmunique, a new record is inserted into the [TF_EBRC_IRMORM_DGFT_List] table. 

                                if (ackstatus == "Errored")
                                {
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus", i2, i1);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                                }
                            }
                        }
                    }
                }
            }

            DataTable dt_3 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_ErrorDetaillist", b1, b2);//IRMORMErrorDetailsList
            if (dt_3.Rows.Count > 0)
            {
                for (int p = 0; p < dt_3.Rows.Count; p++)
                {
                    string json1 = dt_3.Rows[p].ItemArray.GetValue(0).ToString();
                    string uniquetxid = dt_3.Rows[p].ItemArray.GetValue(1).ToString();
                    //Creates DataTable 
                    DataTable dt12 = new DataTable();
                    dt12.Columns.Add("errorCode", typeof(string));
                    dt12.Columns.Add("errorDetails", typeof(string));
                    List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json1);

                    JArray jsonArray1 = JArray.Parse(json1);

                    foreach (var item in jsonArray1)
                    {

                        JObject jsonObject1 = (JObject)item;

                        string errorCode = jsonObject1["errorCode"].ToString();
                        string errorDetails = jsonObject1["errorDetails"].ToString();

                        dt12.Rows.Add(errorCode, errorDetails);
                    }

                    //insert datatatble into table
                    //get irm number for this errordetails list
                    SqlParameter z1 = new SqlParameter("@uniquetxid", uniquetxid);
                    DataTable dt_4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_IRMNo", z1);
                    string irmno_ = "";
                    string irmissuedate_ = "";
                    if (dt_4.Rows.Count > 0)
                    {
                        for (int q = 0; q < dt_4.Rows.Count; q++)
                        {
                            irmno_ = dt_4.Rows[q]["IRMNumber"].ToString();
                            irmissuedate_ = dt_4.Rows[q]["IRMIssueDate"].ToString();


                            string result = "";
                            string irmno = "", irmissuedate = "", errorcode = "", errordetails_ = "";
                            if (dt12.Rows.Count > 0)
                            {
                                for (int s = 0; s < dt12.Rows.Count; s++)
                                {
                                    irmno = irmno_;
                                    irmissuedate = irmissuedate_;
                                    errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
                                    errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
                                    string irmunique = irmno + uniquetxid + errorcode;
                                    SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                                    SqlParameter i2 = new SqlParameter("@irmno", irmno);
                                    SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate.Replace("/", ""));
                                    SqlParameter i4 = new SqlParameter("@ackstatus", "");
                                    SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                                    SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                                    SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTUpdateMainTable", i2, i1, i4, i5, i6);

                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus", i2, i1);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                                }
                            }
                        }
                    }
                }
            }


        }
    }
    protected void btnapprove_Click(object sender, EventArgs e)
    {

        int count = 0;
        string count_ = "";
        foreach (GridViewRow gvrow_ in GridViewReturnData.Rows)
        {
            if (gvrow_.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)gvrow_.FindControl("chkSelect");
                if (chk != null & chk.Checked)
                {
                    count++;
                }
                count_ = count.ToString();
            }
        }
        if (count != 0)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string data = "";
                Label lblirmno = new Label();
                Label irmstatus = new Label();
                string _result;
                string result_;
                string uniquetxid;
              //  SqlParameter p0 = new SqlParameter("@branchcode", ddlbranch.SelectedValue);
                string _qry = "TF_EBRC_IRM_GenerateUniqueTxId";
                DataTable dt = objdata.getData(_qry);
                uniquetxid = dt.Rows[0]["uniquetxtid"].ToString().Trim();

                foreach (GridViewRow gvrow in GridViewReturnData.Rows)
                {
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        lblirmno = (gvrow.Cells[0].FindControl("lblIRMNumber") as Label);
                        irmstatus = (gvrow.Cells[0].FindControl("lblIRMStatus") as Label);
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                        if (chk != null & chk.Checked)
                        {
                            string storid = lblirmno.Text;
                            string storname = irmstatus.Text;
                            data = data + storid + " ,  " + storname + " , " + "<br>";

                            SqlParameter p1 = new SqlParameter("@Addedby", Session["userName"].ToString());
                            SqlParameter p2 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
                            SqlParameter p3 = new SqlParameter("@status", "Approve");
                            SqlParameter p4 = new SqlParameter("@irmno", lblirmno.Text);
                            SqlParameter p5 = new SqlParameter("@irmstatus", irmstatus.Text);
                            SqlParameter p6 = new SqlParameter("@bulkflag", "B");
                            SqlParameter p7 = new SqlParameter("@uniquetxid", uniquetxid);
                            _result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateStatusGrid", p1, p2, p3, p4, p5, p6, p7);

                            if (_result == "Updated")
                            {
                               // SqlParameter J1 = new SqlParameter("@BranchCode", ddlbranch.SelectedValue);
                                SqlParameter J1 = new SqlParameter("@irmno", lblirmno.Text);
                                SqlParameter J2 = new SqlParameter("@irmstatus", irmstatus.Text);
                                SqlParameter J3 = new SqlParameter("@uniquetxid", uniquetxid);
                                result_ = objdata.SaveDeleteData("TF_EBRC_IRM_BulkJsonInsert",J1 ,J2 ,J3);

                                SqlParameter userid = new SqlParameter("@userID", SqlDbType.VarChar);
                                userid.Value = Session["userName"].ToString().Trim();
                                SqlParameter IP = new SqlParameter("@IP", SqlDbType.VarChar);
                                IP.Value = ipAddressW;
                                SqlParameter Timestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                                Timestamp.Value = System.DateTime.Now;
                                SqlParameter ptype = new SqlParameter("@type", SqlDbType.VarChar);
                                ptype.Value = "EBRC IRM Data Entry View - Checker";
                                SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
                                pstatus.Value = "Approved By Checker :uniquetxid: " + uniquetxid;
                                string store_logs = objdata.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
                            }
                            else if (_result != "Updated")
                            {
                                SqlParameter userid = new SqlParameter("@userID", SqlDbType.VarChar);
                                userid.Value = Session["userName"].ToString().Trim();
                                SqlParameter IP = new SqlParameter("@IP", SqlDbType.VarChar);
                                IP.Value = ipAddressW;
                                SqlParameter Timestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                                Timestamp.Value = System.DateTime.Now;
                                SqlParameter ptype = new SqlParameter("@type", SqlDbType.VarChar);
                                ptype.Value = "EBRC IRM Data Entry View - Checker";
                                SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
                                pstatus.Value = _result + " uniquetxid: " + uniquetxid;
                                string store_logs = objdata.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
                            }

                        }
                    }
                }
                //lbl.Text = data;
                JsonCreation(uniquetxid, count_);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Row')", true);
        }

    }
    protected void JsonCreation(string uniquetxid, string count_)
    {
        string id = uniquetxid;
        string count = count_;
        IrmCreation IrmCreation = new IrmCreation();
        irmList irmObj = new irmList();
        IrmCreation.uniqueTxId = id;

        SqlParameter p1 = new SqlParameter("@uniqutxid", id);
        DataTable dt = objdata.getData("TF_EBRC_IRM_JSONFileCreationGrid", p1);

        List<irmList> irmList1 = new List<irmList>();
        irmList1 = (from DataRow dr in dt.Rows

                    select new irmList()
                    {
                        irmIssueDate = dr["IRMIssueDate"].ToString().Trim(),
                        irmNumber = dr["IRMNumber"].ToString().Trim(),
                        irmStatus = dr["IRMStatus"].ToString().Trim(),
                        bankRefNumber = dr["BankReferencenumber"].ToString().Trim(),
                        ifscCode = dr["IFSCCode"].ToString().Trim(),
                        remittanceAdCode = dr["RemittanceADCode"].ToString().Trim(),
                        remittanceDate = dr["RemittanceDate"].ToString().Trim(),
                        remittanceFCC = dr["RemittanceFCC"].ToString().Trim(),
                        remittanceFCAmount = Math.Round(System.Convert.ToDecimal(dr["RemittanceFCCAmount"].ToString().Trim()), 2),
                        inrCreditAmount = Math.Round(System.Convert.ToDecimal(dr["INRCreditAmount"].ToString().Trim()), 2),
                        iecCode = dr["IECCode"].ToString().Trim(),
                        panNumber = dr["PanNumber"].ToString().Trim(),
                        remitterName = dr["RemitterName"].ToString().Trim(),
                        remitterCountry = dr["RemitterCountry"].ToString().Trim(),
                        purposeOfRemittance = dr["PurposeofRemittance"].ToString().Trim(),
                        bankAccountNo = dr["BankAccountNumber"].ToString().Trim()
                    }).ToList();
        #region not use
        //foreach (DataRow dr in dt.Rows)
        //{
        //    //irmObj.bankRefNumber = "";
        //    string issuedate = dr["IRMIssueDate"].ToString().Trim();
        //    irmObj.irmIssueDate = issuedate.Replace("/", "").Replace("/", "");
        //    irmObj.irmNumber = dr["IRMNo"].ToString().Trim();
        //    //irmObj.irmStatus = "";
        //    irmObj.irmStatus = dr["IRMStatus"].ToString().Trim();
        //    irmObj.bankRefNumber = dr["BkRefNo"].ToString().Trim();
        //    irmObj.ifscCode = dr["IFSCCode"].ToString().Trim();
        //    irmObj.remittanceAdCode = dr["RemittanceADCode"].ToString().Trim();
        //    string remdate = dr["RemittanceDate"].ToString().Trim();
        //    irmObj.remittanceDate = remdate.Replace("/", "").Replace("/", "");
        //    irmObj.remittanceFCC = dr["RemittanceFCC"].ToString().Trim();
        //    string remamt = dr["RemittanceFCCAmt"].ToString().Trim();
        //    irmObj.remittanceFCAmount = Convert.ToDouble(remamt);
        //    string inrcredamt = dr["INRCreditAmt"].ToString().Trim();
        //    irmObj.inrCreditAmount = Convert.ToDouble(inrcredamt);
        //    //string exrt = dt.Rows[0]["ExchangeRate"].ToString().Trim();
        //    //irmObj.exchangeRate = Convert.ToDouble(exrt);
        //    irmObj.iecCode = dr["IECCode"].ToString().Trim();
        //    irmObj.panNumber = dr["PanNo"].ToString().Trim();
        //    irmObj.remitterName = dr["RemitterName"].ToString().Trim();
        //    irmObj.remitterCountry = dr["RemitterCountry"].ToString().Trim();
        //    irmObj.purposeOfRemittance = dr["PurposeCode"].ToString().Trim();
        //    //irmObj.modeOfPayment = dt.Rows[0]["ModeofPayment"].ToString().Trim();
        //    //irmObj.bankAccountNo = "";
        //    irmObj.bankAccountNo = dr["BkAcNo"].ToString().Trim();
        //    //dt.Rows.Add(irmList1.ToArray<irmList>());
        //    irmList1.Add(irmObj);
        //}
        #endregion


        IrmCreation.irmList = irmList1;

        var json = JsonConvert.SerializeObject(IrmCreation,
    new JsonSerializerSettings()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
    });
        //var jsonFromSerialization = json.Replace(": ", ":");
        string JSONresult = JsonConvert.SerializeObject(IrmCreation);

        JObject jsonFormat = JObject.Parse(JSONresult);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string FileName = "";
      
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Decrypted/") + todaydt;
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
          
        string irmStatus_ = dt.Rows[0]["IRMStatus"].ToString();
        if(irmStatus_=="F")
        {
             FileName = "IRM_Json_Decrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "C")
        {
            FileName = "IRM_Json_Decrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        } 
        if (irmStatus_ == "A")
        {
            FileName = "IRM_Json_Decrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        } 

       // string FileName = "IRM_Json_Decrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");

        string _filePath = _directoryPath + "/" + FileName + ".json";

        SqlParameter j1 = new SqlParameter("@uniquetxid", id);
        SqlParameter j2 = new SqlParameter("@actualdata", json);
        SqlParameter j3 = new SqlParameter("@encodeddata", "");
        SqlParameter j4 = new SqlParameter("@encrypteddata", "");
        SqlParameter j5 = new SqlParameter("@operation", "Insert");
        SqlParameter j6 = new SqlParameter("@mode", "IRM");
        string query = "TF_EBRC_IRMORM_JSONOutput";
        string res = objdata.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

        using (var tw = new StreamWriter(_filePath, true))
        {
            tw.WriteLine(json);
            tw.Close();
        }
        Base64EncodedJsonFile(_filePath,count,id,irmStatus_);
    }
    protected void Base64EncodedJsonFile(string _filePath, string count, string uniquetxid, string irmStatus_)
    {
        string id = uniquetxid;
        string inputFile = _filePath;
        string count_ = count;

        byte[] bytes = File.ReadAllBytes(inputFile);
        string file = Convert.ToBase64String(bytes);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string outputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Base/") + todaydt;
        string FileName = "";

        if (!Directory.Exists(outputFile))
        {
            Directory.CreateDirectory(outputFile);
        }

     //   string FileName = "IRM_Json_Base64Encrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        if(irmStatus_=="F")
        {
            FileName = "IRM_Json_Base64Encrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "C")
        {
            FileName = "IRM_Json_Base64Encrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "A")
        {
            FileName = "IRM_Json_Base64Encrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }

        string _path = outputFile + "/" + FileName + ".json";

        File.WriteAllText(_path, file);

        //SqlParameter j1 = new SqlParameter("@JSON_FileOutput", file);
        //SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        //SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
        //string query = "TF_EBRC_IRM_jsonOutputBase64Encrypted";
        //string res = objData.SaveDeleteData(query, j1, j2, j3);
        SqlParameter j1 = new SqlParameter("@uniquetxid", id);
        SqlParameter j2 = new SqlParameter("@actualdata", "");
        SqlParameter j3 = new SqlParameter("@encodeddata", file);
        SqlParameter j4 = new SqlParameter("@encrypteddata", "");
        SqlParameter j5 = new SqlParameter("@operation", "Update1");
        SqlParameter j6 = new SqlParameter("@mode", "IRM");
        string query = "TF_EBRC_IRMORM_JSONOutput";
        string res = objdata.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

        //AESEncryptJsonFile(_path,count_);
        EncryptAndEncode(file, count_, _path, id,irmStatus_);
    }
    //protected void AESEncryptJsonFile(string _path,string count_)
    //{
    //    string inputFile = _path;
    //    string count=count_;
    //    string todaydt = System.DateTime.Now.ToString("ddMMyyyy");

    //    string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Eecrypted/") + todaydt;

    //    if (!Directory.Exists(AESoutputFile))
    //    {
    //        Directory.CreateDirectory(AESoutputFile);
    //    }
    //    string FileName = "IRM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");

    //    string _AESpath = AESoutputFile + "/" + FileName + ".json";

    //    byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
    //    string cryptFile = _AESpath;

    //    using (FileStream fileCrypt = new FileStream(cryptFile, FileMode.Create))
    //    {
    //        using (AesManaged encrypt = new AesManaged())
    //        {
    //            using (CryptoStream cs = new CryptoStream(fileCrypt, encrypt.CreateEncryptor(key, key), CryptoStreamMode.Write))
    //            {
    //                using (FileStream fileInput = new FileStream(inputFile, FileMode.Open))
    //                {
    //                    encrypt.KeySize = 256;
    //                    encrypt.BlockSize = 128;
    //                    int data;
    //                    while ((data = fileInput.ReadByte()) != -1)
    //                    {
    //                        cs.WriteByte((byte)data);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    StreamReader streamReader = File.OpenText(_AESpath);
    //    string file = streamReader.ReadToEnd();
    //    SqlParameter j1 = new SqlParameter("@JSON_FileOutput", file);
    //    SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
    //    SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
    //    string query = "TF_EBRC_IRM_jsonOutputAESEncrypted";
    //    string res = objData.SaveDeleteData(query, j1, j2, j3);
    //    //string record = count + " records are Aporoved";
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + "');", true);
    //    //Response.Redirect("~/EBR/TF_EBRC_IRM_CheckerView.aspx", true);
    //    string msg = count + " records are Aporoved";
    //    string aspx = "TF_EBRC_IRM_CheckerView.aspx";
    //    var page = HttpContext.Current.CurrentHandler as Page;
    //    ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');window.location ='" + aspx + "';", true);

    //    //readonlytext();
    //}
    private static byte[] generateIVandSalt()
    {
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            byte[] nonce = new byte[16];
            rng.GetBytes(nonce);
            return nonce;
        }
    }
    private static byte[] IV = generateIVandSalt();
    private static string PASSWORD = "i2MubJH9";
    private static byte[] SALT = generateIVandSalt();
    public void EncryptAndEncode(string file, string count_, string _path, string uniquetxid, string irmStatus_)//--- encoded64data from database
    {
        try
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                //string PASSWORD = "";
                var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), SALT, 65536);
                byte[] key = spec.GetBytes(32);

                byte[] encryptedBytes;
                using (var encryptor = GetCryptoTransform(csp, true, key))
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(file);
                    encryptedBytes = encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length);
                }

                byte[] combined = new byte[SALT.Length + IV.Length + encryptedBytes.Length];
                Array.Copy(SALT, 0, combined, 0, SALT.Length);
                Array.Copy(csp.IV, 0, combined, SALT.Length, IV.Length);
                Array.Copy(encryptedBytes, 0, combined, SALT.Length + IV.Length, encryptedBytes.Length);

                string base64String = Convert.ToBase64String(combined);
                //lblEncryptedValue.Text = "Encoded and Encrypted value : " + base64String;
                //HiddenField1.Value = base64String;
                string id = uniquetxid;
                string inputFile = _path;
                string count = count_;
                string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
                string FileName = "";
               
                string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Eecrypted/") + todaydt;

                if (!Directory.Exists(AESoutputFile))
                {
                    Directory.CreateDirectory(AESoutputFile);
                }
                //string FileName = "IRM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                if(irmStatus_=="F")
                {
                    FileName = "IRM_Json_AESEncrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }
                if (irmStatus_ == "C")
                {
                    FileName = "IRM_Json_AESEncrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }
                if (irmStatus_ == "A")
                {
                    FileName = "IRM_Json_AESEncrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }


                string _AESpath = AESoutputFile + "/" + FileName + ".json";

                File.WriteAllText(_AESpath, base64String);           

                SqlParameter j1 = new SqlParameter("@uniquetxid", id);
                SqlParameter j2 = new SqlParameter("@actualdata", "");
                SqlParameter j3 = new SqlParameter("@encodeddata", "");
                SqlParameter j4 = new SqlParameter("@encrypteddata", base64String);
                SqlParameter j5 = new SqlParameter("@operation", "Update2");
                SqlParameter j6 = new SqlParameter("@mode", "IRM");
                string query = "TF_EBRC_IRMORM_JSONOutput";
                string res = objdata.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

                SqlParameter userid = new SqlParameter("@userID", SqlDbType.VarChar);
                userid.Value = Session["userName"].ToString().Trim();
                SqlParameter IP = new SqlParameter("@IP", SqlDbType.VarChar);
                IP.Value = ipAddressW;
                SqlParameter Timestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                Timestamp.Value = System.DateTime.Now;
                SqlParameter ptype = new SqlParameter("@type", SqlDbType.VarChar);
                ptype.Value = "EBRC IRM Data Entry View - Checker";
                SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
                pstatus.Value = "Approved By Checker :uniquetxid: " + uniquetxid;
                string store_logs = objdata.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);

                string msg = count + " Records are Approved";
              //string aspx = "TF_EBRC_Checker.aspx";
                string aspx = ConfigurationManager.AppSettings["webpath"] + "gHtyj4Jw9M1DZHRCAepArtcJHvnHP4BXBKA5T0nA";
                var page = HttpContext.Current.CurrentHandler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alert", "alert('" + msg + "');window.location ='" + aspx + "';", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting, byte[] encrypted)
    {

        if (encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), SALT, 65536);
            byte[] key = spec.GetBytes(32);
            csp.IV = IV;
            csp.Key = key;
        }
        else
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            byte[] salt = new byte[16];
            Array.Copy(encrypted, 0, salt, 0, 16);
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), salt, 65536);
            byte[] key = spec.GetBytes(32);

            byte[] IV = new byte[16];
            Array.Copy(encrypted, 16, IV, 0, 16);
            csp.IV = IV;
            csp.Key = key;
        }
        if (encrypting)
        {
            return csp.CreateEncryptor();
        }
        return csp.CreateDecryptor();
    }
    public void PageAccess()
    {
        int C = 0;
        TF_DATA objData = new TF_DATA();
        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = Session["userName"].ToString();
        SqlParameter menuName = new SqlParameter("@menuName", SqlDbType.VarChar);
        menuName.Value = "EBRC IRM Data Entry View - Checker";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "EBRC IRM Data Entry View - Checker")
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