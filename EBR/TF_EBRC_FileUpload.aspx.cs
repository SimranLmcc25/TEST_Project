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

public partial class EBR_TF_EBRC_FileUpload : System.Web.UI.Page
{
    int norecinexcel, cntrec;
    string fname;
    string result;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedUserId"] == null)
        {
            Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
        }
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillBranch();
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

        ddlBranch.Items.Insert(0, li);
        ddlBranch.SelectedIndex = 1;
        ddlBranch_SelectedIndexChanged(null, null);
        ddlBranch.Focus();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "", _query = ""; lblHint.Text = "";

        TF_DATA objdata = new TF_DATA();
        _query = "TF_EBRC_FileUploadDelete";
        SqlParameter p1 = new SqlParameter("@DocType", "Realized");

        SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
        DataTable res = objdata.getData(_query, p1, p2);

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
      
    }
    private void GetExcelSheets(string FilePath, string Extension, string isHDR)
    {
        try
        {
            string conStr = "";
            labelMessage.Text = "";

            string Branch = "", docno = "", TRANSACTIONDT = "", PORTCODE = "", SHIPPINGBILL_NO = "", SHIPPING_BILLDT = "",
                   CUR = "", Amount = "", INRAMOUNT = "", CUSTID = "", REALISEDAMT = "", EX_RT = "", BILL_NO = "", Status = "",
                   FrightValue = "", Export_category = "", Insurance = "", CommissionValue = "", SacCode = "", Isforfeiting = "", Isfactoring = "", Isvostro = "",
                   rmtbank = "", rmtcity = "", rmtcontry = "";


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
            connExcel.Close();

            SqlParameter BranchCode = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            SqlParameter SRNO = new SqlParameter("@SRNO", SqlDbType.VarChar);
            SqlParameter DOCNO = new SqlParameter("@DOCNO", SqlDbType.VarChar);
            SqlParameter TRANSACTION_DT = new SqlParameter("@TRANSACTION_DT", SqlDbType.VarChar);
            SqlParameter PORT_CODE = new SqlParameter("@PORT_CODE", SqlDbType.VarChar);
            SqlParameter SHIPPING_BILL_NO = new SqlParameter("@SHIPPING_BILL_NO", SqlDbType.VarChar);
            SqlParameter SHIPPING_BILL_DT = new SqlParameter("@SHIPPING_BILL_DT", SqlDbType.VarChar);
            SqlParameter CURR = new SqlParameter("@CURR", SqlDbType.VarChar);
            SqlParameter AMOUNT = new SqlParameter("@AMOUNT", SqlDbType.VarChar);
            SqlParameter INR_AMOUNT = new SqlParameter("@INR_AMOUNT", SqlDbType.VarChar);
            SqlParameter CUST_ID = new SqlParameter("@CUST_ID", SqlDbType.VarChar);
            //SqlParameter REMMITERNAME = new SqlParameter("@REMMITERNAME", SqlDbType.VarChar);
            SqlParameter REALISED_AMT = new SqlParameter("@REALISED_AMT", SqlDbType.VarChar);
            //SqlParameter FULL_PART_FLAG = new SqlParameter("@FULL_PART_FLAG", SqlDbType.VarChar);
            SqlParameter EXRT = new SqlParameter("@EXRT", SqlDbType.VarChar);
            SqlParameter BILLNO = new SqlParameter("@BILLNO", SqlDbType.VarChar);
            SqlParameter STATUS = new SqlParameter("@STATUS", SqlDbType.VarChar);
            //SqlParameter CANCELDATE = new SqlParameter("@CANCELDATE",SqlDbType.VarChar);
            //SqlParameter ExcelFlag = new SqlParameter("@ExcelFlag",SqlDbType.VarChar);
            SqlParameter ADD_USER = new SqlParameter("@ADD_USER", SqlDbType.VarChar);
            SqlParameter ADD_DATE = new SqlParameter("@ADD_DATE", SqlDbType.VarChar);
            SqlParameter UPDATE_USER = new SqlParameter("@UPDATE_USER", SqlDbType.VarChar);
            SqlParameter UPDATE_DATE = new SqlParameter("@UPDATE_DATE", SqlDbType.VarChar);

            //----------------------- NEW FIELDS -------------------------------------

            SqlParameter FREIGHT_VALUE = new SqlParameter("@FREIGHT_VALUE", SqlDbType.VarChar);
            SqlParameter CATEGORY_OF_EXPORTS = new SqlParameter("@CATEGORY_OF_EXPORTS", SqlDbType.VarChar);
            SqlParameter INSURANCE_VALUE = new SqlParameter("@INSURANCE_VALUE", SqlDbType.VarChar);
            SqlParameter COMMISSION_VALUE = new SqlParameter("@COMMISSION_VALUE", SqlDbType.VarChar);
            SqlParameter SAC_CODE = new SqlParameter("@SAC_CODE", SqlDbType.VarChar);
            SqlParameter FORFEITING = new SqlParameter("@FORFEITING", SqlDbType.VarChar);
            SqlParameter FACTORING = new SqlParameter("@FACTORING", SqlDbType.VarChar);
            SqlParameter VOSTRO = new SqlParameter("@VOSTRO", SqlDbType.VarChar);

            SqlParameter RMT_BANK = new SqlParameter("@RMT_BANK", SqlDbType.VarChar);
            SqlParameter RMT_CITY = new SqlParameter("@RMT_CITY", SqlDbType.VarChar);
            SqlParameter RMT_COUNTRY = new SqlParameter("@RMT_COUNTRY", SqlDbType.VarChar);

            int dt1count = 0; int dtcount = 0; int noofrecinexcel = 0;

            string _query = "TF_EBRC_FileUpload";

            if (dt.Rows.Count > 1)
            {
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    noofrecinexcel++;
                    SRNO.Value = "";
                    Branch = dt.Rows[i][0].ToString();
                    docno = dt.Rows[i][1].ToString();
                    TRANSACTIONDT = dt.Rows[i][2].ToString();
                    PORTCODE = dt.Rows[i][3].ToString();
                    SHIPPINGBILL_NO = dt.Rows[i][4].ToString();
                    SHIPPING_BILLDT = dt.Rows[i][5].ToString();
                    BILL_NO = dt.Rows[i][6].ToString();
                    CUR = dt.Rows[i][7].ToString();
                    Amount = dt.Rows[i][8].ToString();
                    EX_RT = dt.Rows[i][9].ToString();
                    INRAMOUNT = dt.Rows[i][10].ToString();
                    REALISEDAMT = dt.Rows[i][11].ToString();
                    CUSTID = dt.Rows[i][12].ToString();
                    //REMMITER_NAME=dt.Rows[i][11].ToString();
                    //FULLPART_FLAG = "";
                    Status = "";
                    //CANCEL_DATE="";
                    //Excel_Flag = "";
                    //SRNO.Value = "";

                    //--------------- NEW FIELDS -----------------

                    FrightValue = dt.Rows[i][13].ToString();
                    Export_category = dt.Rows[i][14].ToString();
                    Insurance = dt.Rows[i][15].ToString();
                    CommissionValue = dt.Rows[i][16].ToString();
                    SacCode = dt.Rows[i][17].ToString();
                    Isforfeiting = dt.Rows[i][18].ToString();
                    Isfactoring = dt.Rows[i][19].ToString();
                    Isvostro = dt.Rows[i][20].ToString();

                    rmtbank = dt.Rows[i][21].ToString();
                    rmtcity = dt.Rows[i][22].ToString();
                    rmtcontry = dt.Rows[i][23].ToString();

                    //string qry;
                    //TF_DATA objdata1 = new TF_DATA();
                    //qry = "TF_EBRC_FileUpload_Get_Valid_Branch";

                    //SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                    //DataTable dt2 = objdata1.getData(qry, p2);

                    ////if (dt2.Rows.Count > 0)
                    ////{
                    //hdnbranchcode.Value = dt2.Rows[0]["BranchCode"].ToString();

                    //if (Branch != hdnbranchcode.Value)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('All the transaction data does not belong to the selected branch.')", true);
                    //}
                    //else
                    //{
                    BranchCode.Value = Branch;
                    DOCNO.Value = docno;
                    TRANSACTION_DT.Value = TRANSACTIONDT;
                    PORT_CODE.Value = PORTCODE;
                    SHIPPING_BILL_NO.Value = SHIPPINGBILL_NO;
                    SHIPPING_BILL_DT.Value = SHIPPING_BILLDT;
                    BILLNO.Value = BILL_NO;
                    CURR.Value = CUR;
                    AMOUNT.Value = Amount;
                    EXRT.Value = EX_RT;
                    INR_AMOUNT.Value = INRAMOUNT;
                    REALISED_AMT.Value = REALISEDAMT;
                    CUST_ID.Value = CUSTID;
                    //REMMITERNAME.Value = REMMITER_NAME;
                    //FULL_PART_FLAG.Value = FULLPART_FLAG;
                    STATUS.Value = Status;
                    //CANCELDATE.Value=CANCEL_DATE;
                    //ExcelFlag.Value=Excel_Flag;
                    ADD_USER.Value = Session["UserName"].ToString();
                    ADD_DATE.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    UPDATE_USER.Value = Session["UserName"].ToString();
                    UPDATE_DATE.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    //----------------------- NEW FIELDS -------------------------------------

                    FREIGHT_VALUE.Value = FrightValue;
                    CATEGORY_OF_EXPORTS.Value = Export_category;
                    INSURANCE_VALUE.Value = Insurance;
                    COMMISSION_VALUE.Value = CommissionValue;
                    SAC_CODE.Value = SacCode;
                    FORFEITING.Value = Isforfeiting;
                    FACTORING.Value = Isfactoring;
                    VOSTRO.Value = Isvostro;

                    RMT_BANK.Value = rmtbank;
                    RMT_CITY.Value = rmtcity;
                    RMT_COUNTRY.Value = rmtcontry;

                    TF_DATA objSave = new TF_DATA();
                    result = objSave.SaveDeleteData(_query, BranchCode, SRNO, DOCNO, TRANSACTION_DT, PORT_CODE, SHIPPING_BILL_NO, SHIPPING_BILL_DT, CURR, AMOUNT, INR_AMOUNT, CUST_ID, REALISED_AMT, EXRT, BILLNO, STATUS,
                                                            FREIGHT_VALUE, CATEGORY_OF_EXPORTS, INSURANCE_VALUE, COMMISSION_VALUE, SAC_CODE, FORFEITING, FACTORING, VOSTRO,
                                                            RMT_BANK, RMT_CITY, RMT_COUNTRY, ADD_USER, ADD_DATE, UPDATE_USER, UPDATE_DATE);

                    if (result == "Uploaded")
                    {
                        dtcount = dtcount + 1;
                        // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Records Uploaded')", true);
                    }
                    else
                    {
                        string _Remarks = result;
                        SqlParameter p37 = new SqlParameter("@Remarks", SqlDbType.VarChar);
                        p37.Value = _Remarks;
                        SqlParameter p38 = new SqlParameter("@SRNO", SqlDbType.VarChar);
                        p38.Value = i;
                        errorcount++;
                        //labelMessage.Text = result;
                        string qryInput1 = "TF_EBRC_NotUploaded";
                        result = objSave.SaveDeleteData(qryInput1, BranchCode, p38, DOCNO, TRANSACTION_DT, PORT_CODE, SHIPPING_BILL_NO, SHIPPING_BILL_DT, CURR, AMOUNT, INR_AMOUNT, CUST_ID, REALISED_AMT, EXRT, BILLNO, STATUS,
                                                                   FREIGHT_VALUE, CATEGORY_OF_EXPORTS, INSURANCE_VALUE, COMMISSION_VALUE, SAC_CODE, FORFEITING, FACTORING, VOSTRO,
                                                                   RMT_BANK, RMT_CITY, RMT_COUNTRY, ADD_USER, ADD_DATE, UPDATE_USER, UPDATE_DATE, p37);
                    }
                    if (lblHint.Text == "")
                    {
                        //labelMessage.Text = "<font color='red'>" + dtcount + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records Uploaded out of " + "<font color='red'>" + noofrecinexcel + "</font>" + " from file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                        if (errorcount > 0)
                        {
                            lblHint.Text = "<font color='red'>" + "Please Correct All Errors." + "</font>";
                            string script = "window.open('View_EBRC_NotUploaded.aspx?','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
                        }
                    }
                    // }
                    //}
                }
                if (dtcount == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('File Aborted.');", true);
                }
                else
                {
                    string record = dtcount + " records uploaded out of " + noofrecinexcel + " records from the file " + System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('" + record + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            labelMessage.Text = "Upload Correct File Format.";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Correct File Format File')", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Upload Correct File Format.');", true);
            this.LogError(ex);
        }
    }
    private void LogError(Exception ex)
    {
        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        message += string.Format("Message: {0}", ex.Message);
        message += Environment.NewLine;
        message += string.Format("StackTrace: {0}", ex.StackTrace);
        message += Environment.NewLine;
        message += string.Format("Source: {0}", ex.Source);
        message += Environment.NewLine;
        message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
        message += Environment.NewLine;
        message += "-----------------------------------------------------------";
        message += Environment.NewLine;
        string path = Server.MapPath("~/GeneratedFiles/ErrorLog.txt");
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
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
                DataTable dt = objdata.getData("TF_EBRC_FileUpload_Validate", p1);
                if (dt.Rows.Count > 0)
                {
                    //  lblHint.Text = "<font color='red'>" + "Please Correct All Errors Then You Can Process Data.." + "</font>";
                    //script = "window.open('EBRC_Rpt_Data_Validation.aspx?mode=R','_blank','height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);

                    string result = objdata.SaveDeleteData("EBRC_Temp_Invalid_RowCount", p1);
                    string[] splitresult = result.Split('/');
                    string record = splitresult[0] + " Errors Found In Input File.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Validate('" + record + " Please click on OK to view the error report.');", true);
                }
                else
                {
                    //script = "No Error Records for " + ddlBranch.SelectedItem.Text + " Branch.";

                    script = "No Error Records";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", "Alert('" + script + "')", true);
                    lblHint.Text = "";
                }
            }
            else
            {
                //  lblHint.Text = lblHint.Text;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "Alert('Please Correct All Errors.')", true);
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
                SqlParameter BName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
                SqlParameter FileName = new SqlParameter("@FileName", txtInputFile.Text.Trim());
                SqlParameter UserName = new SqlParameter("@UserName", Session["userName"].ToString().Trim());
                TF_DATA objdata = new TF_DATA();
                //DataTable dt = objdata.getData("Rreturn_Delete_CSV_File_transection", p1, p2, p3);
                string result = objdata.SaveDeleteData("EBRC_FileProcess_Transaction", BName, UserName, FileName);

                if (result.Substring(0, 8) == "Uploaded")
                {
                    //labelMessage.Text = "<font color='red'>" + result.Substring(8) + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;Valid Records Processed ";
                   // string record = result.Substring(8) + " Valid Records Processed Successfully for " + ddlBranch.SelectedItem.Text + " Branch."; ;

                    string record = result.Substring(8) + " Valid Records Processed Successfully"; 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "uploadfile", "Alert('" + record + "')", true);
                }
                else if (result == "Records Alredy Exists")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "Alert('" + result + "')", true);
                }
                else
                {
                    labelMessage.Text = " <font color='red'>" + "0 " + "</font>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Records processed ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "Alert('No Records Processed.')", true);
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
}