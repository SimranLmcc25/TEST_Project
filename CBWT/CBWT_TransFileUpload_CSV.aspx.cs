using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

public partial class CBWT_TransFileUpload_CSV : System.Web.UI.Page
{
    int norecinexcel;
    int cntrec = 0;
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
                cntrec = 0;
                norecinexcel = 0;
                fillBranch();
                //	txtYearMonth.Attributes.Add("onblur", "return checkSysDate(" + txtYearMonth.ClientID + ");");            
                txtYearMonth.Text = System.DateTime.Now.ToString("MM/yyyy");
                ddlBranch.SelectedIndex = 1;
                ddlBranch.Focus();
            }
        }
        //txtYearMonth_TextChanged(null, null);
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        string result = "", _query = "";
        TF_DATA objdata = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@YearMonth", txtYearMonth.Text.Trim());
        SqlParameter p2 = new SqlParameter("@RefNo", ddlBranch.SelectedValue);
        // txtYearMonth_TextChanged(null,null);
        //if (hiddenFieldId.Value == "1")
        //{
        _query = "CBWT_Delete_TransFileData_CSV";
        objdata.SaveDeleteData(_query, p1, p2);
        //}
        norecinexcel = 0;
        string path = Server.MapPath("~/GeneratedFiles/CBWT/Uploaded_Files");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        result = Upload_File(path + "\\" + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName));
        labelMessage.Font.Size = 10;

        if (result == "fileuploaded")
        {
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            string tempfilepath = "file://" + _serverName + "\\GeneratedFiles\\CBWT\\Uploaded_Files\\temp.txt";
            labelMessage.Text = "<b><font color='red'>" + cntrec + "</font> record(s) " + "Uploaded out of <font color='red'>" + norecinexcel + "</font> from file " + System.IO.Path.GetFileName(fileinhouse.PostedFile.FileName) + " (Size :"+fileinhouse.PostedFile.ContentLength+ "kb)</b>";
            //lbltest.Text = "File name: " +fileinhouse.PostedFile.FileName + "<br>" + fileinhouse.PostedFile.ContentLength + " kb<br>" + "Content type: " +fileinhouse.PostedFile.ContentType;
        }
        else
        {
            labelMessage.Text = result;
        }
    }
    private string Upload_File(string _filepath)
    {
        //lblUploading.Text="Uploading.";

        string uploadresult = "";
        string result = "";
        string _FileSrNo = "";

        string Ref_No = "";
        string TRANSACTION_DT = "";
        string TRANS_REF_NO = "";
        string TRANSACTION_TYPE = "";
        string INSTRUMENT_TYPE = "";
        string TRANS_INSTITUTE_NAME = "";
        string TRANS_INSTITUTE_REF_NO = "";
        string TRANS_STATE_CODE = "";
        string TRANS_COUNTRY_CODE = "";
        string INSTRUMENT_COUNTRY_CODE = "";
        string FC_AMOUNT = "";
        string EXCH_RATE = "";
        string INR_AMOUNT = "";
        string CURR = "";
        string PURPOSE_ID = "";
        string RISK_RATING = "";
        string MOD_TYPE = "";
        string REM_NAME = "";
        string REM_ID = "";
        string REM_ADD = "";
        string REM_ACNO = "";
        string BENEFICIARY_ID = "";
        string BENEFICIARY_NAME = "";
        string BENEFICIARY_ADDRESS = "";
        string BENEFICIARY_ACNO = "";

        FileStream currentFileStream = null;//EDIT

        string path = Server.MapPath("~/GeneratedFiles/CBWT/Uploaded_Files");
        string tempFilePath = path + "\\TEMP.txt";

        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileuploaded";

        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                }
                catch (Exception ex)  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    Thread.Sleep(10000); //Wait for object to be killed. 
                    labelMessage.Text = ex.Message.ToString();
                    File.Delete(_filepath); //File can be now deleted
                }
            }
        }
        catch { }
        try
        {
            fileinhouse.PostedFile.SaveAs(_filepath);
            uploadresult = "fileuploaded";
        }
        catch
        {
            uploadresult = "ioerror";
        }       
            //string fileext = System.IO.Path.GetExtension(fileinhouse.FileName);
            //if (fileext == ".csv")
            //{         
        if (uploadresult == "fileuploaded")        
        {
            StreamReader sr = new StreamReader(_filepath);
            string line = sr.ReadLine();
            string[] value = line.Split(',');
            DataTable dt = new DataTable();
            DataRow row;
            foreach (string dc in value)
            {
                dt.Columns.Add(new DataColumn(dc));
            }

            while (!sr.EndOfStream)
            {
                value = sr.ReadLine().Split(',');
                if (value.Length == dt.Columns.Count)
                {
                    row = dt.NewRow();
                    row.ItemArray = value;
                    dt.Rows.Add(row);
                }
                else
                {
                    _rowData = "File Sr No :" + value[0] + " Not uploaded. [ Due to the presence of comma (,) in the row ]";
                    labelMessage.Text = "File Sr No :" + value[0] + " Not uploaded. [ Due to the presence of comma (,) in the row ]";
                    //list.InnerHtml = list.InnerHtml + "<br>" + "<font color='red'>" + _rowData + "</font>";
                }
                norecinexcel = norecinexcel + 1;
            }
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
            else
            {
                currentFileStream = File.Create(tempFilePath);//creates temp text file
                currentFileStream.Close();//frees the file for editing/reading
            }//if file does not already exist
            File.AppendAllText(tempFilePath, _rowData);

            SqlParameter pREF_NO = new SqlParameter("@REF_NO", SqlDbType.VarChar);
            SqlParameter pYearMonth = new SqlParameter("@YearMonth", txtYearMonth.Text.Trim());
            SqlParameter pdocSrNo = new SqlParameter("@docSrNo", SqlDbType.VarChar);
            SqlParameter pTRANSACTION_DT = new SqlParameter("@TRANSACTION_DT", SqlDbType.VarChar);
            SqlParameter pTRANS_REF_NO = new SqlParameter("@TRANS_REF_NO", SqlDbType.VarChar);
            SqlParameter pTRANSACTION_TYPE = new SqlParameter("@TRANSACTION_TYPE", SqlDbType.VarChar);
            SqlParameter pINSTRUMENT_TYPE = new SqlParameter("@INSTRUMENT_TYPE", SqlDbType.VarChar);
            SqlParameter pTRANS_INSTITUTE_NAME = new SqlParameter("TRANS_INSTITUTE_NAME", SqlDbType.VarChar);
            SqlParameter pTRANS_INSTITUTE_REF_NO = new SqlParameter("@TRANS_INSTITUTE_REF_NO", SqlDbType.VarChar);
            SqlParameter pTRANS_STATE_CODE = new SqlParameter("@TRANS_STATE_CODE", SqlDbType.VarChar);
            SqlParameter pTRANS_COUNTRY_CODE = new SqlParameter("@TRANS_COUNTRY_CODE", SqlDbType.VarChar);
            SqlParameter pINSTRUMENT_COUNTRY_CODE = new SqlParameter("@INSTRUMENT_COUNTRY_CODE", SqlDbType.VarChar);
            SqlParameter pFC_AMOUNT = new SqlParameter("@FC_AMOUNT", SqlDbType.VarChar);
            SqlParameter pEXCH_RATE = new SqlParameter("@EXCH_RATE", SqlDbType.VarChar);
            SqlParameter pINR_AMOUNT = new SqlParameter("@INR_AMOUNT", SqlDbType.VarChar);
            SqlParameter pCURR = new SqlParameter("@CURR", SqlDbType.VarChar);
            SqlParameter pPURPOSE_ID = new SqlParameter("@PURPOSE_ID", SqlDbType.VarChar);
            SqlParameter pRISK_RATING = new SqlParameter("@RISK_RATING", SqlDbType.VarChar);
            SqlParameter pMOD_TYPE = new SqlParameter("@MOD_TYPE", SqlDbType.VarChar);
            SqlParameter pREM_NAME = new SqlParameter("@REM_NAME", SqlDbType.VarChar);
            SqlParameter pREM_ID = new SqlParameter("@REM_ID", SqlDbType.VarChar);
            SqlParameter pREM_ADD = new SqlParameter("@REM_ADD", SqlDbType.VarChar);
            SqlParameter pREM_ACNO = new SqlParameter("@REM_ACNO", SqlDbType.VarChar);




            SqlParameter pBENEFICIARY_ID = new SqlParameter("@BENEFICIARY_ID", SqlDbType.VarChar);
            SqlParameter pBENEFICIARY_NAME = new SqlParameter("@BENEFICIARY_NAME", SqlDbType.VarChar);
            SqlParameter pBENEFICIARY_ADDRESS = new SqlParameter("@BENEFICIARY_ADDRESS", SqlDbType.VarChar);
            SqlParameter pBENEFICIARY_ACNO = new SqlParameter("@BENEFICIARY_ACNO", SqlDbType.VarChar);

            SqlParameter p1 = new SqlParameter("@addedBy", Session["userName"].ToString().Trim());
            SqlParameter p2 = new SqlParameter("@addedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            string _query = "CBWT_TransFileUpload_CSV";
            try
            {
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Trim() != "")
                        {
                            Ref_No = dt.Rows[i][0].ToString();

                            TRANSACTION_DT = dt.Rows[i][2].ToString();
                            TRANS_REF_NO = dt.Rows[i][3].ToString();
                            TRANSACTION_TYPE = dt.Rows[i][4].ToString();
                            INSTRUMENT_TYPE = dt.Rows[i][5].ToString();
                            TRANS_INSTITUTE_NAME = dt.Rows[i][6].ToString();
                            TRANS_INSTITUTE_REF_NO = dt.Rows[i][7].ToString();
                            TRANS_STATE_CODE = dt.Rows[i][8].ToString();
                            TRANS_COUNTRY_CODE = dt.Rows[i][9].ToString();
                            INSTRUMENT_COUNTRY_CODE = dt.Rows[i][10].ToString();
                            FC_AMOUNT = dt.Rows[i][11].ToString();
                            EXCH_RATE = dt.Rows[i][12].ToString();
                            INR_AMOUNT = dt.Rows[i][13].ToString();
                            CURR = dt.Rows[i][14].ToString();
                            PURPOSE_ID = dt.Rows[i][15].ToString();
                            RISK_RATING = dt.Rows[i][16].ToString();
                            MOD_TYPE = dt.Rows[i][17].ToString();
                            REM_NAME = dt.Rows[i][18].ToString();
                            REM_ID = dt.Rows[i][19].ToString().Replace("'", "");
                            REM_ADD = dt.Rows[i][20].ToString().Replace("'", "");
                            REM_ACNO=dt.Rows[i][21].ToString().Replace("'", "");
                            BENEFICIARY_ID = dt.Rows[i][22].ToString().Replace("'", "");
                            BENEFICIARY_NAME = dt.Rows[i][23].ToString();
                            BENEFICIARY_ADDRESS = dt.Rows[i][24].ToString();
                            BENEFICIARY_ACNO = dt.Rows[i][25].ToString();


                            //pREF_NO.Value=Ref_No;
                            pdocSrNo.Value = _FileSrNo;
                            pTRANSACTION_DT.Value = TRANSACTION_DT;
                            pTRANS_REF_NO.Value = TRANS_REF_NO;
                            pTRANSACTION_TYPE.Value = TRANSACTION_TYPE;
                            pINSTRUMENT_TYPE.Value = INSTRUMENT_TYPE;
                            pTRANS_INSTITUTE_NAME.Value = TRANS_INSTITUTE_NAME;
                            pTRANS_INSTITUTE_REF_NO.Value = TRANS_INSTITUTE_REF_NO;
                            pTRANS_STATE_CODE.Value = TRANS_STATE_CODE;
                            pTRANS_COUNTRY_CODE.Value = TRANS_COUNTRY_CODE;
                            pINSTRUMENT_COUNTRY_CODE.Value = INSTRUMENT_COUNTRY_CODE;
                            pFC_AMOUNT.Value = FC_AMOUNT;
                            pEXCH_RATE.Value = EXCH_RATE;
                            pINR_AMOUNT.Value = INR_AMOUNT;
                            pCURR.Value = CURR;
                            pPURPOSE_ID.Value = PURPOSE_ID;
                            pRISK_RATING.Value = RISK_RATING;
                            pMOD_TYPE.Value = MOD_TYPE;
                            pREM_NAME.Value = REM_NAME;
                            pREM_ID.Value = REM_ID;

                            pREM_ADD.Value = REM_ADD;
                            pREM_ACNO.Value = REM_ACNO;

                           

                            pBENEFICIARY_ID.Value = BENEFICIARY_ID;
                            pBENEFICIARY_NAME.Value = BENEFICIARY_NAME;
                            pBENEFICIARY_ADDRESS.Value = BENEFICIARY_ADDRESS;
                            pBENEFICIARY_ACNO.Value = BENEFICIARY_ACNO;

                            if (Ref_No == ddlBranch.SelectedValue)
                            {
                                pREF_NO.Value = Ref_No;
                                TF_DATA objSave = new TF_DATA();
                                result = objSave.SaveDeleteData(_query, pREF_NO, pYearMonth, pTRANSACTION_DT, pTRANS_REF_NO, pTRANSACTION_TYPE,
                                pINSTRUMENT_TYPE, pTRANS_INSTITUTE_NAME, pTRANS_INSTITUTE_REF_NO, pTRANS_STATE_CODE, pTRANS_COUNTRY_CODE,
                                pINSTRUMENT_COUNTRY_CODE, pFC_AMOUNT, pEXCH_RATE, pINR_AMOUNT, pCURR, pPURPOSE_ID, pRISK_RATING, pMOD_TYPE,
                                pREM_NAME, pREM_ID,pREM_ADD,pREM_ACNO,pBENEFICIARY_ID, pBENEFICIARY_NAME, pBENEFICIARY_ADDRESS, pBENEFICIARY_ACNO, p1, p2);

                                if (result.Substring(0, 8) == "Uploaded")
                                {
                                    cntrec = cntrec + 1;
                                    _rowData = "File Sr No :" + _FileSrNo.ToString() + " Uploaded successfully with Sr No.: " + result.Substring(8).ToString();

                                }
                                else
                                {
                                    _rowData = "File Sr No :" + _FileSrNo.ToString() + " Not uploaded. [ " + result.ToString() + " ]";
                                }
                                if (_rowData != "")
                                    File.AppendAllText(tempFilePath, Environment.NewLine + _rowData);//appends all text in temp file
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid. Check Ref No in File.')", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }
                // lblUploading.Text = "";
            }
            catch (Exception ex)
            {
                uploadresult = ex.Message;
                GC.Collect();
            }
            return uploadresult;
        }
        else
            return uploadresult;
    }
    
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("CBWT_Main.aspx", true);
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
            li.Text = "-Select-";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchCode";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }
}


