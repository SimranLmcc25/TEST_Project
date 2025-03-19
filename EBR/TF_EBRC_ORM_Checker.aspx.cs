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
using System.Text;
using System.Configuration;
using System.Net;

public partial class EBR_TF_EBRC_ORM_Checker : System.Web.UI.Page
{
    Encryption objEnc = new Encryption();
    TF_DATA objData = new TF_DATA();
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
                ddlORMstatus.SelectedValue = "1";
                fillGrid();
                btnapprove.Visible = true;
                btnapprove.Attributes.Add("onclick", "return Confirm();");
                DGFTStatusList();
                Page.DataBind();
             
               // if (Request.QueryString["result_"] != null)
                if (HttpContext.Current.Items["result_"] != null)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Transaction is Approved')", true);
                }

               // if (Request.QueryString["result"] != null)
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
        string _ORMstatus = "";
        string search = txtSearch.Text.Trim();
    
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
        if(ddlORMstatus.SelectedValue=="1")
        {
            _ORMstatus = "F";
        
        }else if(ddlORMstatus.SelectedValue=="2")
        {
            _ORMstatus = "A";

        }
        else if (ddlORMstatus.SelectedValue == "3")
        {
            _ORMstatus = "C";
        }
           // else if(_ORMstatus=="")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select orm status')", true);
        //    ddlORMstatus.Focus();
        
        //}

        SqlParameter p4 = new SqlParameter("@search", search);
        SqlParameter p_Status = new SqlParameter("@status",_status);
    
        SqlParameter orm_Status = new SqlParameter("@ORMstatus", _ORMstatus);
      
        SqlParameter p2 = new SqlParameter("@fromdate", txtfromDate.Text);
        SqlParameter p3 = new SqlParameter("@todate", txtToDate.Text);


        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData("EBRC_ORM_FileUpload_displayCheckerData", p_Status, p4,orm_Status,p2,p3);

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
    protected void GridViewReturnData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblORMNumber = new Label();
            Button btnDelete = new Button();
            lblORMNumber = (Label)e.Row.FindControl("lblORMNumber");

            Label uniquetxid = new Label();
            Label ormstatus = new Label();
          //  labStatus = (Label)e.Row.FindControl("lblstatus");
            uniquetxid = (Label)e.Row.FindControl("lblBankUniqueTransactionId");
            ormstatus = (Label)e.Row.FindControl("lblORMStatus");

            Label labStatus = new Label();
            labStatus = (Label)e.Row.FindControl("lblstatus");
            string status = "";

            if (rdb_Approved.Checked == true)
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
                status = "Approve";
               
            }
            if (rdb_Pending.Checked == true)
            {
                status = "Pending";
            }
            if (rdb_Reject.Checked == true)
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
                status = "Reject";
            }
            CheckBox chk = (CheckBox)e.Row.FindControl("chkselect");
            string result;
            SqlParameter p1 = new SqlParameter("@ormno", lblORMNumber.Text.Trim());
            SqlParameter p2 = new SqlParameter("@ormstatus", ormstatus.Text.Trim());
            result = objData.SaveDeleteData("TF_EBRC_ORM_EnableDisableChkGrid", p1, p2);

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
               
                //string pageurl = "window.location='EBRC_Checker_ORM_DataEntry.aspx?mode=edit&Ormno=" + lblORMNumber.Text.Trim() + "&status=" + status + "&uniquetxid=" + uniquetxid.Text.Trim() + "&Ormstatus=" + ormstatus.Text + "'";
                string pageurl = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "WoYr39vxTPIlefeu6cTIInI98k6aAylbZBNHcDGRs/" + objEnc.URLIDEncription("edit") + "/" + objEnc.URLIDEncription(lblORMNumber.Text.Trim()) + "/" + objEnc.URLIDEncription(status) + "/" + objEnc.URLIDEncription(uniquetxid.Text.Trim()) + "/" + objEnc.URLIDEncription(ormstatus.Text.Trim()) + @"'";

                if (i != 9 && i != 10)
                    cell.Attributes.Add("onclick", pageurl);

                else
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;

                    }
                    else
                    {
                        cell.Style.Add("cursor", "default");

                    }

                i++;

            }

        }   
    }
    protected void GridViewReturnData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";

        string Ormno = "";
        string[] values_p;

        string str = e.CommandArgument.ToString();


        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            Ormno = values_p[0].ToString();
        }


        SqlParameter P1 = new SqlParameter("@ORMno", SqlDbType.VarChar);
        P1.Value = Ormno;


        TF_DATA objData = new TF_DATA();

        result = objData.SaveDeleteData("TF_EBRC_DeleteORMData", P1);
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
            ptype.Value = "EBRC ORM Data Entry View - Checker";
            SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
            pstatus.Value = "Record Deleted: " + Ormno;
            string store_logs = objData.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);

        }
              



    }
    //protected void fillGridUpload()
    //{
    //    string _status = "";

    //    if (rdb_Approved.Checked == true)
    //    {
    //        _status = "Approve";
    //    }
    //    if (rdb_Pending.Checked == true)
    //    {
    //        _status = "Pending";
    //    }
    //    if (rdb_Reject.Checked == true)
    //    {
    //        _status = "Reject";

    //    }
    //    SqlParameter p_Status = new SqlParameter("@status", SqlDbType.VarChar);
    //    p_Status.Value = _status;

    //    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
    //    dateInfo.ShortDatePattern = "dd/MM/yyyy";
    //    DateTime From_Date = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
    //    DateTime To_Date = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
    //    SqlParameter p2 = new SqlParameter("@startdate", txtfromDate.Text);
    //    SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);
    //    TF_DATA objData = new TF_DATA();
    //    DataTable dt = objData.getData("TF_EBRC_ORM_Fileupload_showUploadData", p2, p3, p_Status);
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
        fillGrid();
        DGFTStatusList();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
        DGFTStatusList();

    }
    protected void rdb_Pending_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        GridViewReturnData.Columns[10].Visible = true;
        GridViewReturnData.Columns[9].Visible = true;
        btnapprove.Visible = true;
    }
    protected void rdb_Approved_CheckedChanged(object sender, EventArgs e)
    {
      
        fillGrid();
        GridViewReturnData.Columns[9].Visible = false;
        GridViewReturnData.Columns[10].Visible = false;
        btnapprove.Enabled = false;
    }
    protected void rdb_Reject_CheckedChanged(object sender, EventArgs e)
    {
       // GridViewReturnData.Visible = false;
       // rowPager.Visible = false;
       
        fillGrid();
        GridViewReturnData.Columns[10].Visible = false;
        GridViewReturnData.Columns[9].Visible = false;
        GridViewReturnData.Columns[10].Visible = false;
        btnapprove.Enabled = false;
      
    }
    protected void ddlORMstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlORMstatus.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Orm status')", true);
            ddlORMstatus.Focus();
        }
        if (ddlORMstatus.SelectedValue == "1")
        {
            fillGrid();
            btnapprove.Visible = true;
        }
        else if (ddlORMstatus.SelectedValue == "2")
        {
            fillGrid();
            btnapprove.Visible = true;
        }
        else if (ddlORMstatus.SelectedValue == "3")
        {
            fillGrid();
            btnapprove.Visible = true;
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
                Label lblormno = new Label();
                Label ormstatus = new Label();
                string _result;
                string result_;
                string uniquetxid;
             //   SqlParameter p0 = new SqlParameter("@branchcode", ddlbranch.SelectedValue);
                string _qry = "TF_EBRC_ORM_GenerateUniqueTxId";
                DataTable dt = objData.getData(_qry);
                uniquetxid = dt.Rows[0]["uniquetxtid"].ToString().Trim();

                foreach (GridViewRow gvrow in GridViewReturnData.Rows)
                {
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        lblormno = (gvrow.Cells[0].FindControl("lblORMNumber") as Label);
                        ormstatus = (gvrow.Cells[0].FindControl("lblORMStatus") as Label);
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                        if (chk != null & chk.Checked)
                        {
                            string storid = lblormno.Text;
                            string storname = ormstatus.Text;
                            data = data + storid + " ,  " + storname + " , " + "<br>";

                            SqlParameter p1 = new SqlParameter("@Addedby", Session["userName"].ToString());
                            SqlParameter p2 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
                            SqlParameter p3 = new SqlParameter("@status", "Approve");
                            SqlParameter p4 = new SqlParameter("@ormno", lblormno.Text);
                            SqlParameter p5 = new SqlParameter("@ormstatus", ormstatus.Text);
                            SqlParameter p6 = new SqlParameter("@bulkflag", "B");
                            SqlParameter p7 = new SqlParameter("@uniquetxid", uniquetxid);
                            _result = objData.SaveDeleteData("TF_EBRC_ORM_UpdateStatusGrid", p1, p2, p3, p4, p5, p6, p7);

                            if (_result == "Updated")
                            {
                              //  SqlParameter J1 = new SqlParameter("@BranchCode", ddlbranch.SelectedValue);
                                SqlParameter J1 = new SqlParameter("@ormno", lblormno.Text);
                                SqlParameter J2 = new SqlParameter("@ormstatus", ormstatus.Text);
                                SqlParameter J3 = new SqlParameter("@uniquetxid", uniquetxid);
                                result_ = objData.SaveDeleteData("TF_EBRC_ORM_BulkJsonInsert", J1, J2, J3);

                                SqlParameter userid = new SqlParameter("@userID", SqlDbType.VarChar);
                                userid.Value = Session["userName"].ToString().Trim();
                                SqlParameter IP = new SqlParameter("@IP", SqlDbType.VarChar);
                                IP.Value = ipAddressW;
                                SqlParameter Timestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                                Timestamp.Value = System.DateTime.Now;
                                SqlParameter ptype = new SqlParameter("@type", SqlDbType.VarChar);
                                ptype.Value = "EBRC ORM Data Entry View - Checker";
                                SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
                                pstatus.Value = "Approved By Checker :uniquetxid: " + uniquetxid;
                                string store_logs = objData.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
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
                                ptype.Value = "EBRC ORM Data Entry View - Checker";
                                SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
                                pstatus.Value = _result + " uniquetxid: " + uniquetxid;
                                string store_logs = objData.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
                            
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
        OrmCreation OrmCreation = new OrmCreation();
        ormDataLst ormObj = new ormDataLst();
        OrmCreation.uniqueTxId = id;

        SqlParameter p1 = new SqlParameter("@uniqutxid", id);
        DataTable dt = objData.getData("TF_EBRC_ORM_JSONFileCreationGrid", p1);

        List<ormDataLst> ormList1 = new List<ormDataLst>();
        ormList1 = (from DataRow dr in dt.Rows

                    select new ormDataLst()
                    {
                        ormIssueDate = dr["ORMIssueDate"].ToString().Trim(),
                        ormNumber = dr["ORMNumber"].ToString().Trim(),
                        ormStatus = dr["ORMStatus"].ToString().Trim(),
                        ifscCode = dr["IFSCCode"].ToString().Trim(),
                        ornAdCode = dr["OrnADCode"].ToString().Trim(),
                        paymentDate = dr["PaymentDate"].ToString().Trim(),
                        ornFCC = dr["ORNFCC"].ToString().Trim(),
                        ornFCAmount = Math.Round(System.Convert.ToDecimal(dr["OrnFCCAmount"].ToString().Trim()), 2),
                        ornINRAmount = Math.Round(System.Convert.ToDecimal(dr["INRpayableAmount"].ToString().Trim()), 2),
                        iecCode = dr["IECCode"].ToString().Trim(),
                        panNumber = dr["PanNumber"].ToString().Trim(),
                        beneficiaryName = dr["BeneficiaryName"].ToString().Trim(),
                        beneficiaryCountry = dr["Beneficiarycountry"].ToString().Trim(),
                        purposeOfOutward = dr["PurposeofOutward"].ToString().Trim(),
                        referenceIRM = dr["ReferenceIRM"].ToString().Trim()
                    }).ToList();

        OrmCreation.ormDataLst = ormList1;

        var json = JsonConvert.SerializeObject(OrmCreation,
    new JsonSerializerSettings()
    {
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
    });
        string JSONresult = JsonConvert.SerializeObject(OrmCreation);

        JObject jsonFormat = JObject.Parse(JSONresult);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string FileName = "";
    
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Decrypted/") + todaydt;
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

       // string FileName = "ORM_Json_Decrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        string ormStatus_ = dt.Rows[0]["ORMStatus"].ToString();
        if(ormStatus_=="F")
        {
            FileName = "ORM_Json_Decrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (ormStatus_ == "C")
        {
            FileName = "ORM_Json_Decrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (ormStatus_ == "A")
        {
            FileName = "ORM_Json_Decrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }


        string _filePath = _directoryPath + "/" + FileName + ".json";
        SqlParameter j1 = new SqlParameter("@uniquetxid", id);
        SqlParameter j2 = new SqlParameter("@actualdata", json);
        SqlParameter j3 = new SqlParameter("@encodeddata", "");
        SqlParameter j4 = new SqlParameter("@encrypteddata", "");
        SqlParameter j5 = new SqlParameter("@operation", "Insert");
        SqlParameter j6 = new SqlParameter("@mode", "ORM");
        string query = "TF_EBRC_IRMORM_JSONOutput";
        string res = objData.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

        using (var tw = new StreamWriter(_filePath, true))
        {
            tw.WriteLine(json);
            tw.Close();
        }
        Base64EncodedJsonFile(_filePath, count,id,ormStatus_);

    }
    protected void Base64EncodedJsonFile(string _filePath, string count, string uniquetxid, string ormStatus_)
    {
        string inputFile = _filePath;
        string count_ = count;
        string id = uniquetxid;
        byte[] bytes = File.ReadAllBytes(inputFile);
        string file = Convert.ToBase64String(bytes);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string outputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Base/") + todaydt;
        string FileName = "";

        if (!Directory.Exists(outputFile))
        {
            Directory.CreateDirectory(outputFile);
        }

      //  string FileName = "ORM_Json_Base64Encrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        if (ormStatus_ == "F")
        {
            FileName = "ORM_Json_Base64Encrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (ormStatus_ == "C")
        {
            FileName = "ORM_Json_Base64Encrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (ormStatus_ == "A")
        {
            FileName = "ORM_Json_Base64Encrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }

        string _path = outputFile + "/" + FileName + ".json";

        File.WriteAllText(_path, file);
        SqlParameter j1 = new SqlParameter("@uniquetxid", id);
        SqlParameter j2 = new SqlParameter("@actualdata", "");
        SqlParameter j3 = new SqlParameter("@encodeddata", file);
        SqlParameter j4 = new SqlParameter("@encrypteddata", "");
        SqlParameter j5 = new SqlParameter("@operation", "Update1");
        SqlParameter j6 = new SqlParameter("@mode", "ORM");
        string query = "TF_EBRC_IRMORM_JSONOutput";
        string res = objData.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

        EncryptAndEncode(file,count_, _path, id,ormStatus_);

       
    }
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
    public void EncryptAndEncode(string file, string count_, string _path, string uniquetxid, string ormStatus_)//--- encoded64data from database
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
                string inputFile = _path;
                string id = uniquetxid;
                string count = count_;
                string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
                string FileName = "";

                string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Eecrypted/") + todaydt;

                if (!Directory.Exists(AESoutputFile))
                {
                    Directory.CreateDirectory(AESoutputFile);
                }
                //string FileName = "ORM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                if (ormStatus_ == "F")
                {
                    FileName = "ORM_Json_AESEncrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }
                if (ormStatus_ == "C")
                {
                    FileName = "ORM_Json_AESEncrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }
                if (ormStatus_ == "A")
                {
                    FileName = "ORM_Json_AESEncrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }

                string _AESpath = AESoutputFile + "/" + FileName + ".json";

                File.WriteAllText(_AESpath, base64String);

                SqlParameter j1 = new SqlParameter("@uniquetxid", id);
                SqlParameter j2 = new SqlParameter("@actualdata", "");
                SqlParameter j3 = new SqlParameter("@encodeddata", "");
                SqlParameter j4 = new SqlParameter("@encrypteddata", base64String);
                SqlParameter j5 = new SqlParameter("@operation", "Update2");
                SqlParameter j6 = new SqlParameter("@mode", "ORM");
                string query = "TF_EBRC_IRMORM_JSONOutput";
                string res = objData.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

                SqlParameter userid = new SqlParameter("@userID", SqlDbType.VarChar);
                userid.Value = Session["userName"].ToString().Trim();
                SqlParameter IP = new SqlParameter("@IP", SqlDbType.VarChar);
                IP.Value = ipAddressW;
                SqlParameter Timestamp = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                Timestamp.Value = System.DateTime.Now;
                SqlParameter ptype = new SqlParameter("@type", SqlDbType.VarChar);
                ptype.Value = "EBRC ORM Data Entry View - Checker";
                SqlParameter pstatus = new SqlParameter("@status", SqlDbType.VarChar);
                pstatus.Value = "Approved By Checker :uniquetxid: " + uniquetxid;
                string store_logs = objData.SaveDeleteData(Log_Query, userid, IP, Timestamp, ptype, pstatus);
                string msg = count + " records are Approved";
         
                //string aspx = "TF_EBRC_ORM_Checker.aspx";
                string aspx = ConfigurationManager.AppSettings["webpath"] + "HQFvGmmYi1k3fwo4blywjKhVmtCOW4diCYF177Cn98";
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
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        Label lblormno = new Label();
        Label ormstatus = new Label();
        foreach (GridViewRow gvrow in GridViewReturnData.Rows)
        {
            if (gvrow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_ = (CheckBox)gvrow.FindControl("chkall");
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk.Checked)
                {
                    lblormno = (gvrow.Cells[0].FindControl("lblORMNumber") as Label);
                    ormstatus = (gvrow.Cells[0].FindControl("lblORMStatus") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@ormno", lblormno.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@ormstatus", ormstatus.Text.Trim());
                    result = objData.SaveDeleteData("TF_EBRC_ORM_EnableDisableChkGrid", p1, p2);
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
                    //    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    lblormno = (gvrow.Cells[0].FindControl("lblORMNumber") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@ormno", lblormno.Text.Trim());
                    SqlParameter p2 = new SqlParameter("@ormstatus", ormstatus.Text.Trim());
                    result = objData.SaveDeleteData("TF_EBRC_ORM_EnableDisableChkGrid", p1, p2);
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
    //    //getuniquetxid
    //    TF_DATA objdata = new TF_DATA();
    //    SqlParameter s1 = new SqlParameter("@startdate", txtfromDate.Text);
    //    SqlParameter s2 = new SqlParameter("@enddate", txtToDate.Text);
    //    SqlParameter s3 = new SqlParameter("@type", "ORM");
    //    DataTable dt1 = objdata.getData("TF_EBRC_IRMORM_Uniquetxid_list", s1, s2, s3);//uniqueidlist
    //    if (dt1.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dt1.Rows.Count; i++)
    //        {
    //            string uniquetxid = dt1.Rows[i]["BkUniqueTxId"].ToString();
    //            SqlParameter a1 = new SqlParameter("@uniquetxid", uniquetxid);
    //            DataTable dt2 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_list", a1);//IRMORMStatusList
    //            if (dt2.Rows.Count > 0)
    //            {
    //                for (int j = 0; j < dt2.Rows.Count; j++)
    //                {
    //                    string json = dt2.Rows[j].ItemArray.GetValue(0).ToString();
    //                    //Creates DataTable 
    //                    DataTable dt11 = new DataTable();
    //                    dt11.Columns.Add("ormNumber", typeof(string));
    //                    dt11.Columns.Add("ormIssueDate", typeof(string));
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

    //                        string ormNumber = jsonObject["ormNumber"].ToString();
    //                        string ormIssueDate = jsonObject["ormIssueDate"].ToString();
    //                        string ackStatus = jsonObject["ackStatus"].ToString();
    //                        string errorCode = "";
    //                        string errorDetails = "";
    //                        JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

    //                        foreach (var errorDetailsItem in errorDetailsArray)
    //                        {
    //                            JObject errorDetailsObject = (JObject)errorDetailsItem;

    //                            errorCode = errorDetailsObject["errorCode"].ToString();
    //                            errorDetails = errorDetailsObject["errorDetails"].ToString();
    //                            dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);

    //                        }
    //                        dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);
    //                    }


    //                    //insert datatatble into table
    //                    string result = "";
    //                    string ormno = "", ormissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
    //                    if (dt11.Rows.Count > 0)
    //                    {
    //                        for (int k = 0; k < dt11.Rows.Count; k++)
    //                        {
    //                            ormno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
    //                            ormissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
    //                            ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
    //                            errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
    //                            errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
    //                            string irmunique = ormno + uniquetxid + errorcode;
    //                            SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                            SqlParameter i2 = new SqlParameter("@irmno", ormno);
    //                            SqlParameter i3 = new SqlParameter("@irmissuedate", ormissuedate.Replace("/", ""));
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
    //                    DataTable dt4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_ORMNo", a1);
    //                    string ormno_ = "";
    //                    string ormissuedate_ = "";
    //                    if (dt4.Rows.Count > 0)
    //                    {
    //                        for (int q = 0; q < dt4.Rows.Count; q++)
    //                        {
    //                            ormno_ = dt4.Rows[q]["ORMNumber"].ToString();
    //                            ormissuedate_ = dt4.Rows[q]["ORMIssueDate"].ToString();

    //                            string result = "";
    //                            string ormno = "", ormissuedate = "", errorcode = "", errordetails_ = "";
    //                            if (dt12.Rows.Count > 0)
    //                            {
    //                                for (int s = 0; s < dt12.Rows.Count; s++)
    //                                {
    //                                    ormno = ormno_;
    //                                    ormissuedate = ormissuedate_;
    //                                    errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
    //                                    errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
    //                                    string irmunique = ormno + uniquetxid + errorcode;
    //                                    SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                                    SqlParameter i2 = new SqlParameter("@irmno", ormno);
    //                                    SqlParameter i3 = new SqlParameter("@irmissuedate", ormissuedate.Replace("/", ""));
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
    //    DataTable dt_d = objdata.getData("TF_EBRC_ORM_DGFT_Uniquetxid_List");// get uniquetxid and irm no
    //    if (dt_d.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_d.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_d.Rows[d]["UniqueTxId"].ToString();
    //            string ormno_ = dt_d.Rows[d]["irmormNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@ormno", ormno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus", d1, d2);
    //        }
    //    }

    //    //string result_1 = "";
    //    DataTable dt_d1 = objdata.getData("TF_EBRC_ORM_DGFT_Uniquetxid_List");// get uniquetxid and irm no
    //    if (dt_d.Rows.Count > 0)
    //    {
    //        for (int r = 0; r < dt_d1.Rows.Count; r++)
    //        {
    //            string uniquetxid_ = dt_d1.Rows[r]["UniqueTxId"].ToString();
    //            string ormno_ = dt_d1.Rows[r]["irmormNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@ormno", ormno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_Injsoncreated", d1, d2);
    //        }
    //    }

    //    DataTable dt_v = objdata.getData("TF_EBRC_ORM_DGFT_Uniquetxid_List_Validated");// get uniquetxid and irm no
    //    if (dt_v.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_v.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_v.Rows[d]["UniqueTxId"].ToString();
    //            string ormno_ = dt_v.Rows[d]["ORMNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@ormno", ormno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_ProcessedOrValidated", d1, d2);
    //        }
    //    }

    //    DataTable dt_v1 = objdata.getData("TF_EBRC_ORM_DGFT_Uniquetxid_List_ValidatedGet");// get uniquetxid and irm no
    //    if (dt_v1.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_v1.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_v1.Rows[d]["UniqueTxId"].ToString();
    //            string irmno_ = dt_v1.Rows[d]["ORMNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@ormno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_ProcessedOrValidated", d1, d2);
    //        }
    //    }

    //    DataTable dt_p = objdata.getData("TF_EBRC_ORM_DGFT_Uniquetxid_List_Processed");// get uniquetxid and irm no
    //    if (dt_p.Rows.Count > 0)
    //    {
    //        for (int d = 0; d < dt_p.Rows.Count; d++)
    //        {
    //            string uniquetxid_ = dt_p.Rows[d]["UniqueTxId"].ToString();
    //            string irmno_ = dt_p.Rows[d]["irmormNumber"].ToString();

    //            SqlParameter d1 = new SqlParameter("@uniquetxid", uniquetxid_);
    //            SqlParameter d2 = new SqlParameter("@ormno", irmno_);
    //            result_ = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_ProcessedOrValidated", d1, d2);
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
            DataTable dt1 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_PushValidate", s1, s2);

            if (dt1.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt1.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt1.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Validated";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt2 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_PushFailed", s1, s2);
            if (dt2.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt2.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt2.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt3 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_GetValidated", s1, s2);
            if (dt3.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt3.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt3.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "ValidatedGet";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt4 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_GetProcessedorFailed", s1, s2);
            if (dt4.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt4.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt4.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Processed/failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            SqlParameter b1 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter b2 = new SqlParameter("@type", "ORM");
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
                        Result = objdata.SaveDeleteData("TF_EBRC_getstatus_Failed_ORM", q1);
                        Result = objdata.SaveDeleteData("TF_EBRC_getstatus_Failed_ORM_JsonCreatedTab", q1);
                    }
                    else
                    {

                        //Creates DataTable 
                        DataTable dt11 = new DataTable();
                        dt11.Columns.Add("ormNumber", typeof(string));
                        dt11.Columns.Add("ormIssueDate", typeof(string));
                        dt11.Columns.Add("ackStatus", typeof(string));
                        dt11.Columns.Add("errorCode", typeof(string));
                        dt11.Columns.Add("errorDetails", typeof(string));


                        JArray jsonArray = JArray.Parse(json);

                        foreach (var item in jsonArray)
                        {

                            JObject jsonObject = (JObject)item;

                            string ormNumber = jsonObject["ormNumber"].ToString();
                            string ormIssueDate = jsonObject["ormIssueDate"].ToString();
                            string ackStatus = jsonObject["ackStatus"].ToString();
                            string errorCode = "";
                            string errorDetails = "";
                            JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

                            foreach (var errorDetailsItem in errorDetailsArray)
                            {
                                JObject errorDetailsObject = (JObject)errorDetailsItem;

                                errorCode = errorDetailsObject["errorCode"].ToString();
                                errorDetails = errorDetailsObject["errorDetails"].ToString();
                                dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);

                            }
                            dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);
                        }


                        //insert datatatble into table
                        string result = "";
                        string ormno = "", ormissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
                        if (dt11.Rows.Count > 0)
                        {
                            for (int k = 0; k < dt11.Rows.Count; k++)
                            {
                                ormno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
                                ormissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
                                ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
                                errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
                                errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
                                string irmunique = ormno + uniquetxid + errorcode;
                                SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                                SqlParameter i2 = new SqlParameter("@irmno", ormno);
                                SqlParameter i3 = new SqlParameter("@irmissuedate", ormissuedate.Replace("/", ""));
                                SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
                                SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                                SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                                SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                                result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

                                result = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTUpdateMainTable", i2, i1, i4, i5, i6); // If no matching record is found in TF_EBRC_IRMORM_DGFT_List table where irmormNumber+uniquetxid+errorCode=@irmunique, a new record is inserted into the [TF_EBRC_IRMORM_DGFT_List] table. 

                                if (ackstatus == "Errored")
                                {
                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus", i2, i1);
                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
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
                    DataTable dt_4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_ORMNo", z1);
                    string ormno_ = "";
                    string ormissuedate_ = "";
                    if (dt_4.Rows.Count > 0)
                    {
                        for (int q = 0; q < dt_4.Rows.Count; q++)
                        {
                            ormno_ = dt_4.Rows[q]["ORMNumber"].ToString();
                            ormissuedate_ = dt_4.Rows[q]["ORMIssueDate"].ToString();

                            string result = "";
                            string ormno = "", ormissuedate = "", errorcode = "", errordetails_ = "";
                            if (dt12.Rows.Count > 0)
                            {
                                for (int s = 0; s < dt12.Rows.Count; s++)
                                {
                                    ormno = ormno_;
                                    ormissuedate = ormissuedate_;
                                    errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
                                    errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
                                    string irmunique = ormno + uniquetxid + errorcode;
                                    SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                                    SqlParameter i2 = new SqlParameter("@irmno", ormno);
                                    SqlParameter i3 = new SqlParameter("@irmissuedate", ormissuedate.Replace("/", ""));
                                    SqlParameter i4 = new SqlParameter("@ackstatus", "");
                                    SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                                    SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                                    SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTUpdateMainTable", i2, i1, i4, i5, i6);

                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus", i2, i1);
                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                                }
                            }
                        }
                    }
                }
            }



        }
    }
    public void PageAccess()
    {
        int C = 0;
        TF_DATA objData = new TF_DATA();
        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = Session["userName"].ToString();
        SqlParameter menuName = new SqlParameter("@menuName", SqlDbType.VarChar);
        menuName.Value = "EBRC ORM Data Entry View - Checker";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "EBRC ORM Data Entry View - Checker")
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