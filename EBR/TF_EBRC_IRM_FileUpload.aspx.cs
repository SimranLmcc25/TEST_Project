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
using ClosedXML.Excel;
using System.Net;

public partial class EBR_TF_EBRC_IRM_FileUpload : System.Web.UI.Page
{
    int norecinexcel, cntrec;
    string fname;
    string result;
    Encryption objEnc = new Encryption();
    string ipAddressW = GetIPAddress();
    string Log_Query = "TF_Audit_ApplicationLogs";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedUserId"] == null)
        {
           // Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "AO0gtPK5RIS5S1JzBJeCQ/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription("+"));
        }
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            //Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription(lbl.Value));
        }
        {
            if (!IsPostBack)
            {
                PageAccess();
                fillBranch();
                Page.DataBind();

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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);
        //ddlBranch.Focus();

        //ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //ddlBranch.Focus();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        btnProcess.Enabled = false;
        btnValidate.Enabled = false;
        TF_DATA objdata = new TF_DATA();
        string result = "", _query = ""; lblHint.Text = "";
        _query = "EBRC_IRM_FileUploadDelete";
        DataTable dt = objdata.getData(_query);
        if (FileUpload1.HasFile)
        {
            string fname;
            fname = FileUpload1.FileName;
            txtInputFile.Text = FileUpload1.PostedFile.FileName;

            if (fname.Contains(".xls") == false && fname.Contains(".xlsx") == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload only excel file.')", true);
                FileUpload1.Focus();
            }
            else
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = Server.MapPath("~/Uploaded_Files");

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                FileName = FileName.Replace(" ", "");

                string FilePath = FolderPath + "\\" + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(FilePath);
                GetExcelSheets(FilePath, Extension, "No");

            }
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('Please upload file First.')", true);

        }

    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string conStr = "";
            labelMessage.Text = "";
            string SRNO = "", Branch = "", BankUniqueTransactionId = "", IRMissueDate = "", IRMNumber = "", IFSCCode = "", RemittanceADCode = "", Remittancedate = "",
                RemittanceFCC = "", RemittanceFCCAmount = "", INRCreditAmount = "",IECCode = "", PanNumber = "", RemitterName = "", Remittercountry = "",
                PurposeofRemittance = "", IRMStatus = "", BankRefnumber = "", BanKaccnumber = "";

            int errorcount = 0;

            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                             .ConnectionString;
                    break;
            }

            //    //Get the Sheets in Excel WorkBoo
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
   
            oda.Fill(dt);

            foreach (var column in dt.Columns.Cast<DataColumn>().ToArray()) //This code is use for removing empty columns
            {
                if (dt.AsEnumerable().All(dr => dr.IsNull(column)))
                    dt.Columns.Remove(column);
            }
            dt.AcceptChanges();

            dt = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable(); //This Code is used for Removing Blank Rows 
            connExcel.Close();

            SqlParameter SR_NO = new SqlParameter("@SRNO", SqlDbType.VarChar);
            SqlParameter Bank_Unique_TransactionId = new SqlParameter("@BankUniqueTransactionId", SqlDbType.VarChar);
            SqlParameter IRM_issueDate = new SqlParameter("@IRMissueDate", SqlDbType.VarChar);
            SqlParameter IRM_Number = new SqlParameter("@IRMNumber", SqlDbType.VarChar);
            SqlParameter IFSC_Code = new SqlParameter("@IFSCCode", SqlDbType.VarChar);
            SqlParameter Remittance_ADCode = new SqlParameter("@RemittanceADCode", SqlDbType.VarChar);
            SqlParameter Remittance_date = new SqlParameter("@Remittancedate", SqlDbType.VarChar);
            SqlParameter Remittance_FCC = new SqlParameter("@RemittanceFCC", SqlDbType.VarChar);
            SqlParameter Remittance_FCCAmount = new SqlParameter("@RemittanceFCCAmount", SqlDbType.VarChar);
            SqlParameter INRCredit_Amount = new SqlParameter("@INRCreditAmount", SqlDbType.VarChar);
            SqlParameter IEC_Code = new SqlParameter("@IECCode", SqlDbType.VarChar);
            SqlParameter Pan_Number = new SqlParameter("@PanNumber", SqlDbType.VarChar);
            SqlParameter Remitter_Name = new SqlParameter("@RemitterName", SqlDbType.VarChar);
            SqlParameter Remitter_country = new SqlParameter("@Remittercountry", SqlDbType.VarChar);
            SqlParameter Purpose_ofRemittance = new SqlParameter("@PurposeofRemittance", SqlDbType.VarChar);
            SqlParameter ADD_USER = new SqlParameter("@ADD_USER", SqlDbType.VarChar);
            SqlParameter ADD_DATE = new SqlParameter("@ADD_DATE", SqlDbType.VarChar);
            SqlParameter Bank_refno = new SqlParameter("@BankReferencenumber", SqlDbType.VarChar);
            SqlParameter Bank_accno = new SqlParameter("@Bankaccno", SqlDbType.VarChar);
            SqlParameter Irm_status = new SqlParameter("@irmstatus", SqlDbType.VarChar);


            int dt1count = 0; int dtcount = 0; int noofrecinexcel = 0;
            string _query = "TF_EBRC_IRM_FileUpload";
            if (dt.Rows.Count > 1)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    noofrecinexcel++;
                    SR_NO.Value = "";
                    //Branch = dt.Rows[i][0].ToString();
                    BankUniqueTransactionId = dt.Rows[i][0].ToString();
                    BankRefnumber = dt.Rows[i][1].ToString();
                    IRMissueDate = dt.Rows[i][2].ToString();
                    IRMNumber = dt.Rows[i][3].ToString();
                    IRMStatus = dt.Rows[i][4].ToString();
                    IFSCCode = dt.Rows[i][5].ToString();
                    RemittanceADCode = dt.Rows[i][6].ToString();
                    Remittancedate = dt.Rows[i][7].ToString();
                    RemittanceFCC = dt.Rows[i][8].ToString();
                    RemittanceFCCAmount = dt.Rows[i][9].ToString();
                    INRCreditAmount = dt.Rows[i][10].ToString();
                    IECCode = dt.Rows[i][11].ToString();
                    PanNumber = dt.Rows[i][12].ToString();
                    RemitterName = dt.Rows[i][13].ToString();
                    Remittercountry = dt.Rows[i][14].ToString();
                    PurposeofRemittance = dt.Rows[i][15].ToString();
                    BanKaccnumber = dt.Rows[i][16].ToString();


                    //BranchCode.Value = Branch;
                    Bank_Unique_TransactionId.Value = BankUniqueTransactionId;
                    IRM_issueDate.Value = IRMissueDate;
                    IRM_Number.Value = IRMNumber;
                    IFSC_Code.Value = IFSCCode;
                    Remittance_ADCode.Value = RemittanceADCode;
                    Remittance_date.Value = Remittancedate;
                    Remittance_FCC.Value = RemittanceFCC;
                    Remittance_FCCAmount.Value = RemittanceFCCAmount;
                    INRCredit_Amount.Value = INRCreditAmount;

                    if (Remittance_FCCAmount.Value == "" || Remittance_FCCAmount.Value == null)
                    {
                        string RematAMTWithoutCommas = RemittanceFCCAmount.Replace(",", "");

                        // Check if the resulting string is empty or whitespace
                        if (string.IsNullOrWhiteSpace(RematAMTWithoutCommas))
                        {
                            // If empty or whitespace, assign 0.00
                            Remittance_FCCAmount.Value = "0.00";
                        }
                        else
                        {
                            // If not empty, assign the modified value
                            Remittance_FCCAmount.Value = RematAMTWithoutCommas;
                        }
                    }
                    else
                    {
                        Remittance_FCCAmount.Value = RemittanceFCCAmount.Replace(",", "");
                    }

                    if (INRCredit_Amount.Value == "" || INRCredit_Amount.Value == null)
                    {
                        string INRCreditAMTWithoutCommas = INRCreditAmount.Replace(",", "");

                        // Check if the resulting string is empty or whitespace
                        if (string.IsNullOrWhiteSpace(INRCreditAMTWithoutCommas))
                        {
                            // If empty or whitespace, assign 0.00
                            INRCredit_Amount.Value = "0.00";
                        }
                        else
                        {
                            // If not empty, assign the modified value
                            INRCredit_Amount.Value = INRCreditAMTWithoutCommas;
                        }
                    }
                    else
                    {
                        INRCredit_Amount.Value = INRCreditAmount.Replace(",", "");
                    }


                    IEC_Code.Value = IECCode;
                    Pan_Number.Value = PanNumber;
                    Remitter_Name.Value = RemitterName;
                    Remitter_country.Value = Remittercountry;
                    Purpose_ofRemittance.Value = PurposeofRemittance;
                    ADD_USER.Value = Session["UserName"].ToString();
                    ADD_DATE.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    //UPDATE_USER.Value = Session["UserName"].ToString();
                    //UPDATE_DATE.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    Bank_refno.Value = BankRefnumber;
                    Bank_accno.Value = BanKaccnumber;
                    Irm_status.Value = IRMStatus;

                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, SR_NO, Bank_Unique_TransactionId, IRM_issueDate, IRM_Number, IFSC_Code, Remittance_ADCode, Remittance_date, Remittance_FCC,
                    Remittance_FCCAmount, INRCredit_Amount, IEC_Code, Pan_Number, Remitter_Name, Remitter_country,
                    Purpose_ofRemittance, Bank_refno, Bank_accno, Irm_status, ADD_USER, ADD_DATE);

                    if (result == "Uploaded")
                    {

                        dtcount = dtcount + 1;
                        btnValidate.Enabled = true; 
                        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Records Uploaded')", true);
                    }


                }

                if (dtcount == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('File Aborted.');", true);
                }
                else
                {
                    string record = dtcount + " records uploaded out of " + noofrecinexcel + " records from the file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('" + record + "');", true);


                    TF_DATA objData = new TF_DATA();
                    String _query1 = "TF_EBRC_IRM_GetSerialNumbers_New";

                    SqlParameter p11 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                    p11.Value = ddlBranch.SelectedValue.ToString();

                    DataTable dt1 = objData.getData(_query1, p11);
                    if (dt1.Rows.Count > 0)
                    {
                        int newsrno = Convert.ToInt16(dt1.Rows[0]["SRNO"].ToString().Trim()) + 1;
                        lblLog.Text = newsrno.ToString();

                    }
                    else
                    {
                        lblLog.Text = "1";
                    }

                    //TF_DATA upload = new TF_DATA();
                    //string qry = "TF_EBRC_IRM_FileUpload_LogFile";
                    //string srno = lblLog.Text;
                    //SqlParameter f1 = new SqlParameter("@srno", SqlDbType.VarChar);
                    //f1.Value = srno;
                    //SqlParameter f2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                    //f2.Value = ddlBranch.SelectedValue.ToString();
                    //string File_Name = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    //SqlParameter f3 = new SqlParameter("@filename", SqlDbType.VarChar);
                    //f3.Value = File_Name;
                    //int records = dtcount;
                    //SqlParameter f4 = new SqlParameter("@records", SqlDbType.VarChar);
                    //f4.Value = records + " Records";
                    //string time = System.DateTime.Now.ToString();
                    //SqlParameter f5 = new SqlParameter("@time", SqlDbType.VarChar);
                    //f5.Value = time;
                    //SqlParameter f6 = new SqlParameter("@action", SqlDbType.VarChar);
                    //f6.Value = "Upload";
                    //string username = Session["userName"].ToString();
                    //SqlParameter f7 = new SqlParameter("@username", SqlDbType.VarChar);
                    //f7.Value = username;
                    //string res1 = upload.SaveDeleteData(qry, f1, f2, f3, f4, f5, f6, f7);

                }

            }
           
        }

        catch (Exception ex)
        {
            labelMessage.Text = "Upload Correct File Format.";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Correct File Format File')", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Upload Correct File Format.');", true);

        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        if (txtInputFile.Text != "")
        {
            if (lblHint.Text == "")
            {
                TF_DATA objdata = new TF_DATA();
                string script = "";
                SqlParameter p1 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                DataTable dt = objdata.getData("TF_EBRC_IRM_Fileupload_Validate", p1);
                if (dt.Rows.Count > 0)
                {
                    string result = objdata.SaveDeleteData("EBRC_IRM_Temp_Invalid_RowCount", p1);
                    string[] splitresult = result.Split('/');
                    string record = splitresult[0] + " Errors Found In Input File.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "alert('" + record + " Please click on OK to download error report file.');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);
                    txtInputFile.Text = " ";
                    btnValidate.Enabled = false;

                    TF_DATA objData = new TF_DATA();
                    String _query1 = "TF_EBRC_IRM_GetSerialNumbers_New";

                    SqlParameter p11 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                    p11.Value = ddlBranch.SelectedValue.ToString();

                    DataTable dt1 = objData.getData(_query1, p11);
                    if (dt1.Rows.Count > 0)
                    {
                        int newsrno = Convert.ToInt16(dt1.Rows[0]["SRNO"].ToString().Trim()) + 1;
                        lblLog.Text = newsrno.ToString();

                    }
                    else
                    {
                        lblLog.Text = "1";
                    }

                    //TF_DATA upload = new TF_DATA();
                    //string qry = "TF_EBRC_IRM_FileUpload_LogFile";
                    //string srno = lblLog.Text;
                    //SqlParameter f1 = new SqlParameter("@srno", SqlDbType.VarChar);
                    //f1.Value = srno;
                    //SqlParameter f2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                    //f2.Value = ddlBranch.SelectedValue.ToString();
                    //string File_Name = txtInputFile.Text;
                    //SqlParameter f3 = new SqlParameter("@filename", SqlDbType.VarChar);
                    //f3.Value = File_Name;
                    //string records = record;
                    //SqlParameter f4 = new SqlParameter("@records", SqlDbType.VarChar);
                    //f4.Value = records;
                    //string time = System.DateTime.Now.ToString();
                    //SqlParameter f5 = new SqlParameter("@time", SqlDbType.VarChar);
                    //f5.Value = time;
                    //SqlParameter f6 = new SqlParameter("@action", SqlDbType.VarChar);
                    //f6.Value = "Validate";
                    //string username = Session["userName"].ToString();
                    //SqlParameter f7 = new SqlParameter("@username", SqlDbType.VarChar);
                    //f7.Value = username;
                    //string res1 = upload.SaveDeleteData(qry, f1, f2, f3, f4, f5, f6, f7);


                }
                else
                {
                    script = "No Error Records";
                   
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", "Alert('" + script + "')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('" + script + "');", true);
                    btnProcess.Enabled = true;
                    btnValidate.Enabled = false;
                    lblHint.Text = "";

                    TF_DATA objData = new TF_DATA();
                    String _query1 = "TF_EBRC_IRM_GetSerialNumbers_New";

                    SqlParameter p11 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                    p11.Value = ddlBranch.SelectedValue.ToString();

                    DataTable dt1 = objData.getData(_query1, p11);
                    if (dt1.Rows.Count > 0)
                    {
                        int newsrno = Convert.ToInt16(dt1.Rows[0]["SRNO"].ToString().Trim()) + 1;
                        lblLog.Text = newsrno.ToString();

                    }
                    else
                    {
                        lblLog.Text = "1";
                    }

                    //TF_DATA upload = new TF_DATA();
                    //string qry = "TF_EBRC_IRM_FileUpload_LogFile";
                    //string srno = lblLog.Text;
                    //SqlParameter f1 = new SqlParameter("@srno", SqlDbType.VarChar);
                    //f1.Value = srno;
                    //SqlParameter f2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                    //f2.Value = ddlBranch.SelectedValue.ToString();
                    //string File_Name = txtInputFile.Text;
                    //SqlParameter f3 = new SqlParameter("@filename", SqlDbType.VarChar);
                    //f3.Value = File_Name;
                    ////int records = dtcount;
                    //SqlParameter f4 = new SqlParameter("@records", SqlDbType.VarChar);
                    //f4.Value = "No Error Records";
                    //string time = System.DateTime.Now.ToString();
                    //SqlParameter f5 = new SqlParameter("@time", SqlDbType.VarChar);
                    //f5.Value = time;
                    //SqlParameter f6 = new SqlParameter("@action", SqlDbType.VarChar);
                    //f6.Value = "Validate";
                    //string username = Session["userName"].ToString();
                    //SqlParameter f7 = new SqlParameter("@username", SqlDbType.VarChar);
                    //f7.Value = username;
                    //string res1 = upload.SaveDeleteData(qry, f1, f2, f3, f4, f5, f6, f7);

                }

            }
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "Alert('Please Upload File First.')", true);

        }
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (txtInputFile.Text != "")
        {
            if (lblHint.Text == "")
            {
                TF_DATA objdata = new TF_DATA();

                SqlParameter BName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                SqlParameter FileName = new SqlParameter("@FileName", txtInputFile.Text.Trim());
                SqlParameter UserName = new SqlParameter("@UserName", Session["userName"].ToString().Trim());

              // string _qry2 = "TF_EBRC_IRM_Fileupload_getuniqueTxId";
          
                //DataTable dt2 = objdata.getData(_qry2);
                
                //    string Bankunqiueid = dt2.Rows[0]["BankUniqueTransactionId"].ToString().Trim();

                  //  SqlParameter uniquetxt = new SqlParameter("@BankUniqueTransactionId", Bankunqiueid.ToString().Trim());
                    string result = objdata.SaveDeleteData("TF_EBRC_IRM_FileUpload_Process", BName, UserName, FileName);

                    if (result.Substring(0, 8) == "Uploaded")
                    {
                        //labelMessage.Text = "<font color='red'>" + result.Substring(8) + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;Valid Records Processed ";
                        // string record = result.Substring(8) + " Valid Records Processed Successfully for " + ddlBranch.SelectedItem.Text + " Branch."; 

                        SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
                        p1.Value = Session["userName"].ToString().Trim();
                        SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
                        p2.Value = ipAddressW;
                        SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                        p3.Value = System.DateTime.Now;
                        SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
                        p4.Value = "IRM FileUpload";
                        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
                        p5.Value = "File Uploaded Success: FileName: " + txtInputFile.Text.Trim();
                        string store_logs = objdata.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);

                        txtInputFile.Text = "";
                        string record = result.Substring(8) + " Valid Records Processed Successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('" + record + "')", true);
                        btnProcess.Enabled = false;
                       // lblmessage.Text = "This Transaction is move to checker";

                        TF_DATA objData = new TF_DATA();
                        String _query1 = "TF_EBRC_IRM_GetSerialNumbers_New";

                        SqlParameter p11 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                        p11.Value = ddlBranch.SelectedValue.ToString();

                        DataTable dt1 = objData.getData(_query1, p11);
                        if (dt1.Rows.Count > 0)
                        {
                            int newsrno = Convert.ToInt16(dt1.Rows[0]["SRNO"].ToString().Trim()) + 1;
                            lblLog.Text = newsrno.ToString();

                        }
                        else
                        {
                            lblLog.Text = "1";
                        }

                        //TF_DATA upload = new TF_DATA();
                        //string qry = "TF_EBRC_IRM_FileUpload_LogFile";
                        //string srno = lblLog.Text;
                        //SqlParameter f1 = new SqlParameter("@srno", SqlDbType.VarChar);
                        //f1.Value = srno;
                        //SqlParameter f2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                        //f2.Value = ddlBranch.SelectedValue.ToString();
                        //string File_Name = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        //SqlParameter f3 = new SqlParameter("@filename", SqlDbType.VarChar);
                        //f3.Value = File_Name;
                        ////  int records = dtcount;
                        //SqlParameter f4 = new SqlParameter("@records", SqlDbType.VarChar);
                        //f4.Value = result.Substring(8) + " Records";
                        //string time = System.DateTime.Now.ToString();
                        //SqlParameter f5 = new SqlParameter("@time", SqlDbType.VarChar);
                        //f5.Value = time;
                        //SqlParameter f6 = new SqlParameter("@action", SqlDbType.VarChar);
                        //f6.Value = "Upload";
                        //string username = Session["userName"].ToString();
                        //SqlParameter f7 = new SqlParameter("@username", SqlDbType.VarChar);
                        //f7.Value = username;
                        //string res1 = upload.SaveDeleteData(qry, f1, f2, f3, f4, f5, f6, f7);

                    }
                    else if (result == "Records Alredy Exists")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "Alert('" + result + "')", true);
                        TF_DATA objData = new TF_DATA();
                        String _query1 = "TF_EBRC_IRM_GetSerialNumbers_New";

                        SqlParameter p11 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                        p11.Value = ddlBranch.SelectedValue.ToString();

                        DataTable dt1 = objData.getData(_query1, p11);
                        if (dt1.Rows.Count > 0)
                        {
                            int newsrno = Convert.ToInt16(dt1.Rows[0]["SRNO"].ToString().Trim()) + 1;
                            lblLog.Text = newsrno.ToString();

                        }
                        else
                        {
                            lblLog.Text = "1";
                        }

                        //TF_DATA upload = new TF_DATA();
                        //string qry = "TF_EBRC_IRM_FileUpload_LogFile";
                        //string srno = lblLog.Text;
                        //SqlParameter f1 = new SqlParameter("@srno", SqlDbType.VarChar);
                        //f1.Value = srno;
                        //SqlParameter f2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                        //f2.Value = ddlBranch.SelectedValue.ToString();
                        //string File_Name = txtInputFile.Text;
                        //SqlParameter f3 = new SqlParameter("@filename", SqlDbType.VarChar);
                        //f3.Value = File_Name;
                        ////int records = dtcount;
                        //SqlParameter f4 = new SqlParameter("@records", SqlDbType.VarChar);
                        //f4.Value = result;
                        //string time = System.DateTime.Now.ToString();
                        //SqlParameter f5 = new SqlParameter("@time", SqlDbType.VarChar);
                        //f5.Value = time;
                        //SqlParameter f6 = new SqlParameter("@action", SqlDbType.VarChar);
                        //f6.Value = "Process";
                        //string username = Session["userName"].ToString();
                        //SqlParameter f7 = new SqlParameter("@username", SqlDbType.VarChar);
                        //f7.Value = username;
                        //string res1 = upload.SaveDeleteData(qry, f1, f2, f3, f4, f5, f6, f7);


                    }
                    else
                    {
                        labelMessage.Text = " <font color='red'>" + "0 " + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records processed ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "Alert('No Records Processed.')", true);
                        SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
                        p1.Value = Session["userName"].ToString().Trim();
                        SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
                        p2.Value = ipAddressW;
                        SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                        p3.Value = System.DateTime.Now;
                        SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
                        p4.Value = "IRM FileUpload";
                        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
                        p5.Value = "No Records Processed: FileName: " + txtInputFile.Text.Trim();
                        string store_logs = objdata.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);

                        TF_DATA objData = new TF_DATA();
                        String _query1 = "TF_EBRC_IRM_GetSerialNumbers_New";

                        SqlParameter p11 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                        p11.Value = ddlBranch.SelectedValue.ToString();

                        DataTable dt1 = objData.getData(_query1, p11);
                        if (dt1.Rows.Count > 0)
                        {
                            int newsrno = Convert.ToInt16(dt1.Rows[0]["SRNO"].ToString().Trim()) + 1;
                            lblLog.Text = newsrno.ToString();

                        }
                        else
                        {
                            lblLog.Text = "1";
                        }

                        //TF_DATA upload = new TF_DATA();
                        //string qry = "TF_EBRC_IRM_FileUpload_LogFile";
                        //string srno = lblLog.Text;
                        //SqlParameter f1 = new SqlParameter("@srno", SqlDbType.VarChar);
                        //f1.Value = srno;
                        //SqlParameter f2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                        //f2.Value = ddlBranch.SelectedValue.ToString();
                        //string File_Name = txtInputFile.Text;
                        //SqlParameter f3 = new SqlParameter("@filename", SqlDbType.VarChar);
                        //f3.Value = File_Name;
                        ////int records = dtcount;
                        //SqlParameter f4 = new SqlParameter("@records", SqlDbType.VarChar);
                        //f4.Value = "0 Records Processed.";
                        //string time = System.DateTime.Now.ToString();
                        //SqlParameter f5 = new SqlParameter("@time", SqlDbType.VarChar);
                        //f5.Value = time;
                        //SqlParameter f6 = new SqlParameter("@action", SqlDbType.VarChar);
                        //f6.Value = "Process";
                        //string username = Session["userName"].ToString();
                        //SqlParameter f7 = new SqlParameter("@username", SqlDbType.VarChar);
                        //f7.Value = username;
                        //string res1 = upload.SaveDeleteData(qry, f1, f2, f3, f4, f5, f6, f7);

                    }
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "Alert('Please Correct All Errors Then You Can Process Data..')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "Alert('Please Upload File First.')", true);
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //  lblHint.Text = " ";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
        string script = "TF_EBRC_IRM_Fileupload_Validate";

        DataTable dt = objdata.getData(script, p1);
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "Error_Records");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=EBRC_IRM_DataValidation" + System.DateTime.Now.ToString("ddMMyyyy") + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
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
        menuName.Value = "E-BRC IRM File Upload";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "E-BRC IRM File Upload")
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