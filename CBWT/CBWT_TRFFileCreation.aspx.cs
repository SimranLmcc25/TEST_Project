using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Globalization;
using System.Drawing;

public partial class INW_INWBranchFileCreation : System.Web.UI.Page
{
    string _directoryPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("onblur", "return GetToDate();");
            btnHelpReason.Attributes.Add("onclick", "return ReasonHelp();");
            btnHelpPO.Attributes.Add("onclick", "return POHelp();");
            //  txtReasonForReplace.Attributes.Add("onkeydown", "return ReasonHelp_Key(event);");
            txtPrincOffNo.Attributes.Add("onkeydown", "return POHelp_Key(event);");

            InitialState();
            txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
            //  txtFromDate.Focus();
            txtBatchNo.Text = "";
            txtFromDate.Focus();
            btnSave.Attributes.Add("onclick", "return validateSave();");
        }

       
    }
    protected void InitialState()
    {
        //fillBranch();
        //fillReportSerialNo();

        //txtOgReportSerialNo.Text = "00000000";
        txtOgBatchNo.Text = "00000000";
        txtReasonForReplace.Text = "";
        txtPrincOffNo.Text = "";
        txtBatchDate.Text = "";
        lblReason.Text = "";
        lblPO.Text = "";
        txtDataStruVer.Text = "2";



        //txtOgReportSerialNo.Enabled = false;
        txtOgBatchNo.Enabled = false;
        txtReasonForReplace.Enabled = false;
        btnHelpReason.Enabled = false;
        lblReason.Enabled = false;

        rdbNewReport.Checked = true;
        rdbTestMode.Checked = true;

    }

    //protected void fillOG_ReportSerialNo()
    //{
    //    string Report = "";
    //    if (rdbNewReport.Checked == true)
    //    {
    //        Report = "New";
    //        txtReasonForReplace.Text = "";
    //        lblReason.Text = "";
    //    }
    //    else
    //    {
    //        Report = "Old";
    //    }

    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Report", SqlDbType.VarChar);
    //    p1.Value = Report;

    //    string _query = "TF_CBTR_ReportSerial_No";

    //    DataTable dt = objData.getData(_query, p1);

    //    //txtOgReportSerialNo.Text = dt.Rows[0][0].ToString();
    //    txtOgBatchNo.Text = dt.Rows[0][1].ToString();

    //}

    //protected void fillBranch()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetBranchDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlBranch.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "All Branches";

    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "All Branches";
    //        ddlBranch.DataSource = dt.DefaultView;
    //        ddlBranch.DataTextField = "BranchCode";
    //        ddlBranch.DataValueField = "BranchName";
    //        ddlBranch.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";

    //    ddlBranch.Items.Insert(0, li);
    //}

    //protected void insertRportData()
    //{
    //    string _result = "";

    //    string ReportSerialNo = txtReportSerialNumber.Text;
    //    string Og_ReportSerialNo = txtOgReportSerialNo.Text;
    //    string Og_BatchNo = txtOgBatchNo.Text;

    //    string Replace_Reason = txtReasonForReplace.Text;
    //    string Principal_Officer = txtPrincOffNo.Text;


    //    string Mode = "";
    //    string Report = "";
    //    string FDate = txtFromDate.Text;
    //    string TDate = txtToDate.Text;
    //    string BDate = txtBatchDate.Text;
    //    string DataVer = txtDataStruVer.Text;

    //    string Addedby = Session["userName"].ToString();
    //    string addedtime = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");



    //    if (rdbProdMode.Checked == true)
    //    {
    //        Mode = "P";
    //    }
    //    else
    //    {
    //        Mode = "T";
    //    }


    //    if (rdbNewReport.Checked == true)
    //    {
    //        Report = "N";
    //    }
    //    else
    //    {
    //        Report = "R";
    //    }

    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@ReportSerialNo", SqlDbType.VarChar);
    //    p1.Value = ReportSerialNo;

    //    SqlParameter p2 = new SqlParameter("@Og_ReportSerialNo", SqlDbType.VarChar);
    //    p2.Value = Og_ReportSerialNo;

    //    SqlParameter p3 = new SqlParameter("@Replace_Reason", SqlDbType.VarChar);
    //    p3.Value = Replace_Reason;

    //    SqlParameter p4 = new SqlParameter("@Report", SqlDbType.VarChar);
    //    p4.Value = Report;

    //    SqlParameter p5 = new SqlParameter("@FDate", SqlDbType.VarChar);
    //    p5.Value = FDate;

    //    SqlParameter p6 = new SqlParameter("@TDate", SqlDbType.VarChar);
    //    p6.Value = TDate;

    //    SqlParameter p7 = new SqlParameter("@Addedby", SqlDbType.VarChar);
    //    p7.Value = Addedby;

    //    SqlParameter p8 = new SqlParameter("@Addedtime", SqlDbType.VarChar);
    //    p8.Value = addedtime;

    //    SqlParameter p9 = new SqlParameter("@PrincOff", SqlDbType.VarChar);
    //    p9.Value = Principal_Officer;

    //    SqlParameter p10 = new SqlParameter("@BDate", SqlDbType.VarChar);
    //    p10.Value = BDate;

    //    SqlParameter p11 = new SqlParameter("@TPMode", SqlDbType.VarChar);
    //    p11.Value = Mode;

    //    SqlParameter p12 = new SqlParameter("@DataVer", SqlDbType.VarChar);
    //    p12.Value = DataVer;

    //    SqlParameter p13 = new SqlParameter("@OgBatchNo", SqlDbType.VarChar);
    //    p13.Value = Og_BatchNo;

    //    SqlParameter p14 = new SqlParameter("@BatchNo", SqlDbType.VarChar);
    //    p14.Value = txtBatchNo.Text;

    //    string _query = "TF_CBTR_ReportSerial_No_Insert";

    //    _result = objData.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14);

    //}

    //protected void fillReportSerialNo()
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@Report", SqlDbType.VarChar);
    //    p1.Value = "";

    //    string _query = "TF_CBTR_ReportSerial_No";

    //    DataTable dt = objData.getData(_query, p1);

    //    txtReportSerialNumber.Text = dt.Rows[0][0].ToString();
    //}

    protected void rdbNewReport_CheckedChanged(object sender, EventArgs e)
    {
        //fillOG_ReportSerialNo();
        //txtOgReportSerialNo.Enabled = false;
        txtOgBatchNo.Enabled = false;
        txtReasonForReplace.Enabled = false;

        btnHelpReason.Enabled = false;
        lblReason.Enabled = false;


    }
    protected void rdbReplaceReport_CheckedChanged(object sender, EventArgs e)
    {
        //fillOG_ReportSerialNo();


        //txtOgReportSerialNo.Enabled = true;
        txtOgBatchNo.Enabled = true;
        txtReasonForReplace.Enabled = true;
        btnHelpReason.Enabled = true;
        lblReason.Enabled = true;
    }
    protected void fillDetails()
    {

        string ReportFlag = "";
        string ModeFlag = "";
        string _BatchNo = "00000000" + txtBatchNo.Text.Trim();
        txtBatchNo.Text = _BatchNo.Substring(_BatchNo.Length - 8);

        TF_DATA objData = new TF_DATA();

        string _query = "TF_CBTR_ReportSerial_No_Fill_Details";

        SqlParameter ReportNo = new SqlParameter("@ReportNo", SqlDbType.VarChar);
        ReportNo.Value = txtBatchNo.Text;

        DataTable dt = objData.getData(_query, ReportNo);

        if (dt.Rows.Count > 0)
        {
            ReportFlag = dt.Rows[0]["ReportType"].ToString();
            txtPrincOffNo.Text = dt.Rows[0]["PrincOfficerId"].ToString();
            lblPO.Text = dt.Rows[0]["PrincOfficerName"].ToString();
            txtBatchDate.Text = dt.Rows[0]["BatchDate"].ToString();
            ModeFlag = dt.Rows[0]["Mode"].ToString();
            txtDataStruVer.Text = dt.Rows[0]["DataVer"].ToString();
            //txtReportSerialNumber.Text = dt.Rows[0]["ReportSerialNo"].ToString();

            if (ModeFlag == "P")
            {
                rdbProdMode.Checked = true;
            }
            else
            {
                rdbTestMode.Checked = true;
            }

            if (ReportFlag == "N")
            {
                rdbReplaceReport.Checked = false;
                rdbNewReport.Checked = true;

                txtReasonForReplace.Text = "";
                //txtOgReportSerialNo.Text = "00000000";
                txtOgBatchNo.Text = "00000000";

                lblReason.Text = "";

                txtReasonForReplace.Enabled = false;
                //txtOgReportSerialNo.Enabled = false;
                txtOgBatchNo.Enabled = false;
                lblReason.Enabled = false;
                btnHelpReason.Enabled = false;


            }
            else
            {
                rdbNewReport.Checked = false;
                rdbReplaceReport.Checked = true;
                txtReasonForReplace.Text = dt.Rows[0]["ReplaceReason"].ToString();
                //txtOgReportSerialNo.Text = dt.Rows[0]["OgReportSerialNo"].ToString();
                txtOgBatchNo.Text = dt.Rows[0]["OgBatchNo"].ToString();
                lblReason.Text = dt.Rows[0]["Description"].ToString();

                txtReasonForReplace.Enabled = true;
                //txtOgReportSerialNo.Enabled = true;
                txtOgBatchNo.Enabled = true;
                lblReason.Enabled = true;
                btnHelpReason.Enabled = true;
            }
        }

        else
        {

            //InitialState();
            //txtReportSerialNumber.Focus();

        }


    }
    protected void fillPODesc()
    {
        TF_DATA objData = new TF_DATA();

        string _query = "TF_CBTR_ReportSerial_No_Fill_Po_Name";

        SqlParameter id = new SqlParameter("@Id", SqlDbType.VarChar);
        id.Value = txtPrincOffNo.Text;

        DataTable dt = objData.getData(_query, id);

        if (dt.Rows.Count > 0)
        {
            lblPO.Text = dt.Rows[0][0].ToString().Trim();
        }
        else
        {
            lblPO.Text = "";
        }
    }
    protected void fillReasonDesc()
    {

        TF_DATA objData = new TF_DATA();

        string _query = "TF_CBTR_ReportSerial_No_Fill_Reason";

        SqlParameter id = new SqlParameter("@Id", SqlDbType.VarChar);
        id.Value = txtReasonForReplace.Text;

        DataTable dt = objData.getData(_query, id);

        if (dt.Rows.Count > 0)
        {
            lblReason.Text = dt.Rows[0][0].ToString().Trim();
        }
    }
    protected void txtReasonForReplace_TextChanged(object sender, EventArgs e)
    {
        fillReasonDesc();
    }
    protected void txtBatchNo_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
    }
    protected void txtPrincOffNo_TextChanged(object sender, EventArgs e)
    {
        fillPODesc();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        _directoryPath = Server.MapPath("~/GeneratedFiles/CBWT/CBTR/" + txtFromDate.Text.Substring(6, 4) + txtFromDate.Text.Substring(3, 2));
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        string _result = "", _files = "";
        int count = 0, ErrorCount = 0;

        if (DataValidation() == "true")
        {
            string script = "window.open('Reports/ViewDataValidation.aspx?frmdate=" + txtFromDate.Text.Trim() + "&toDate=" + txtToDate.Text.Trim() + "' , '_blank' , 'height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Data Validation", script, true);
        }

        //===========Generating Transaction File==================//
        _result = TRFTRN();
        if (_result.Substring(0, 4) == "true")
        {
            if (count == 0)
                _files = _result.Substring(4);
            else
                _files = _files + ", " + _result.Substring(4);

            count = count + 1;
        }
        else
            ErrorCount = 1;


        //===========Generating Batch File==================//
        _result = TRFBAT();
        if (_result.Substring(0, 4) == "true")
        {
            count = count + 1;
            _files = _result.Substring(4);
            _result = "";
        }
        else
            ErrorCount = 1;
        //=======================================================//

        //===========Generating Payment Instrument File==================//
        _result = TRFPIN();
        if (_result.Substring(0, 4) == "true")
        {
            count = count + 1;
            _files = _result.Substring(4);
            _result = "";
        }
        //=======================================================//

        //===========Generating Individual File==================//
        _result = TRFINP();
        if (_result.Substring(0, 4) == "true")
        {
            count = count + 1;
            _files = _result.Substring(4);
            _result = "";
        }
        else
            ErrorCount = 1;
        //=======================================================//

        //===========Generating Legal Person File==================//
        _result = TRFLPE();
        if (_result.Substring(0, 4) == "true")
        {
            if (count == 0)
                _files = _result.Substring(4);
            else
                _files = _files + ", " + _result.Substring(4);

            count = count + 1;
        }
        else
            ErrorCount = 1;
        //=======================================================//


        //=======================================================//

        //===========Generating Report File==================//
        _result = TRFRPT();
        if (_result.Substring(0, 4) == "true")
        {
            if (count == 0)
                _files = _result.Substring(4);
            else
                _files = _files + ", " + _result.Substring(4);

            count = count + 1;
        }
        //=======================================================//


        //===========Generating Branch File==================//
        _result = TRFBRC();
        if (_result.Substring(0, 4) == "true")
        {
            if (count == 0)
                _files = _result.Substring(4);
            else
                _files = _files + ", " + _result.Substring(4);

            count = count + 1;
        }
        //=======================================================//

        //if (count > 0 && ErrorCount == 0)
        if (count > 0)
        {
            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            string path = "file://" + _serverName + "/GeneratedFiles/CBWT/CBTR";
            string link = "/GeneratedFiles/CBWT/CBTR";

            labelMessage.Text = "TRF Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            //labelMessage.Text = "TRF Files Created Successfully on " + _serverName;
        }
        else
            labelMessage.Text = "No Records Found";

        //insertRportData();

    }
    protected string TRFBAT()
    {
        string _fileGenrated = "false";

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Branch", "");
        SqlParameter p2 = new SqlParameter("@POID", txtPrincOffNo.Text.ToString().Trim());

        string _querry = "TF_CBTR_GenerateTRFBAT";

        DataTable dt = objData.getData(_querry, p1, p2);
        
        string _fileName = "TRFBAT.txt";
        string _filePath = _directoryPath + "/" + _fileName;
        
        StreamWriter sw;
        sw = File.CreateText(_filePath);

        if (dt.Rows.Count > 0)
        {
            int linenumber = 0;
            string _ReportType = "NTR";
            string _DataStructureVersion = txtDataStruVer.Text;
            string _ReportingEntityName = "";
            string _ReportingEntityCategory = "";
            string _RERegistrationNumber = "";
            string _FIUREID = "";
            string _POName = "";
            string _PODesignation = "";
            string _Address = "";
            string _City = "";
            string _StateCode = "";
            string _Pincode = "";
            string _CountryCode = "";
            string _Telephone = "";
            string _Mobile = "";
            string _Fax = "";
            string _EMailID = "";
            string _BatchNumber = txtBatchNo.Text.ToString().Trim();

            string _BatchDate = txtBatchDate.Text.ToString().Trim();
            _BatchDate = txtBatchDate.Text.Substring(6, 4) + "-" + txtBatchDate.Text.Substring(3, 2) + "-" + txtBatchDate.Text.Substring(0, 2);  // Batch Date should be YYYY/MM/DD format

            string _MonthOfReport = txtBatchDate.Text.Substring(3, 2);
            string _YearOfReport = txtBatchDate.Text.Substring(6, 4);
            string _OperationalMode = "T";
            if (rdbProdMode.Checked == true)
                _OperationalMode = "P";
            if (rdbTestMode.Checked == true)
                _OperationalMode = "T";

            string _BatchType = "N";
            if (rdbNewReport.Checked == true)
                _BatchType = "N";
            if (rdbReplaceReport.Checked == true)
                _BatchType = "R";
            string _OriginalBatchID = txtOgBatchNo.Text.ToString().Trim();
            string _ReasonOfRevision;
            if (_BatchType == "N")
            {
                _ReasonOfRevision = "N";
                _OriginalBatchID = "0";
            }
            else
                _ReasonOfRevision = txtReasonForReplace.Text.ToString().Trim();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;

                _ReportingEntityName = dt.Rows[i]["ReportingEntityName"].ToString().Trim();
                _ReportingEntityCategory = dt.Rows[i]["ReportingEntityCate"].ToString().Trim();
                _FIUREID = dt.Rows[i]["FIUid"].ToString().Trim();
                _POName = dt.Rows[i]["OfficerName"].ToString().Trim();
                _PODesignation = dt.Rows[i]["OfficerDesignation"].ToString().Trim();
                _Address = dt.Rows[i]["OfficerAddress"].ToString().Trim();
                _City = dt.Rows[i]["OfficerCity"].ToString().Trim();
                _StateCode = dt.Rows[i]["StateCode"].ToString().Trim();
                _Pincode = dt.Rows[i]["Pincode"].ToString().Trim();
                _CountryCode = dt.Rows[i]["CountryCode"].ToString().Trim();
                _Telephone = dt.Rows[i]["Phone"].ToString().Trim();
                _Mobile = dt.Rows[i]["Mobile"].ToString().Trim();
                _Fax = dt.Rows[i]["Fax"].ToString().Trim();
                _EMailID = dt.Rows[i]["OfficerEmail"].ToString().Trim();


                sw.WriteLine(linenumber.ToString("000000") + _ReportType.PadRight(3, ' ') + _DataStructureVersion.PadRight(1, ' ') +
                            _ReportingEntityName.PadRight(80, ' ') + _ReportingEntityCategory.PadRight(5, ' ') + _RERegistrationNumber.PadRight(12, ' ') +
                            _FIUREID.PadRight(10, ' ') + _POName.PadRight(80, ' ') + _PODesignation.PadRight(80, ' ') + _Address.PadRight(225, ' ') +
                            _City.PadRight(50, ' ') + _StateCode.PadRight(2, ' ') + _Pincode.PadRight(10, ' ') +
                            _CountryCode.PadRight(2, ' ') + _Telephone.PadRight(30, ' ') + _Mobile.PadRight(30, ' ') + _Fax.PadRight(30, ' ') +
                            _EMailID.PadRight(50, ' ') + _BatchNumber.PadRight(8, ' ') + _BatchDate.PadRight(10, ' ') + _MonthOfReport.PadRight(2, ' ') + _YearOfReport.PadRight(4, ' ') +
                            _OperationalMode.PadRight(1, ' ') + _BatchType.PadRight(1, ' ') + _OriginalBatchID.PadRight(10, ' ') +
                            _ReasonOfRevision.PadRight(1, ' ')
                            );

                _fileGenrated = "true" + _fileName;
            }
        }
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return _fileGenrated;
    }
    protected string TRFINP()
    {
        string _fileGenrated = "false";

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Branch", "");
        SqlParameter p2 = new SqlParameter("@FromDate", txtFromDate.Text.ToString().Trim());
        SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text.ToString().Trim());

        string _querry = "TF_CBTR_GenerateTRFINP";

        DataTable dt = objData.getData(_querry, p1, p2, p3);
        string _fileName = "TRFINP.txt";
        string _filePath = _directoryPath + "/" + _fileName;
        StreamWriter sw;
        sw = File.CreateText(_filePath);

        if (dt.Rows.Count > 0)
        {
            int linenumber = 0;
            string _ReportSerialNumber = "";
            string _PersonName = "";
            string _CustomerID = "";
            string _RelationFlag = "";
            string _CommunicationAddress = "";
            string _City = "";
            string _StateCode = "";
            string _Pincode = "";
            string _CountryCode = "";
            string _SecondAddress = "";
            string _WCity = "";
            string _WStateCode = "";
            string _WPincode = "";
            string _WCountryCode = "";
            string _WTelephone = "";
            string _Mobile = "";
            string _Fax = "";
            string _EMailID = "";
            string _PAN = "";
            string _UIN = "";
            string _Gender = "";
            string _DateOfBirth = "";
            string _IdentificationType = "";
            string _IdentificationNumber = "";
            string _IssuingAuthority = "";
            string _PlaceOfIssue = "";
            string _Nationality = "";
            string _PlaceOfWork = "";
            string _FatherOrSpouse = "";
            string _Occupation = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;

                _PersonName = dt.Rows[i]["IndName"].ToString().Trim();
                _CustomerID = ""; // dt.Rows[i]["AcNo"].ToString().Trim();
                _RelationFlag = dt.Rows[i]["RelationFlag"].ToString().Trim();
                _CommunicationAddress = dt.Rows[i]["CommunicationAddress"].ToString().Trim();
                _City = dt.Rows[i]["City"].ToString().Trim();
                _StateCode = dt.Rows[i]["StateCode"].ToString().Trim().ToUpper();
                _Pincode = dt.Rows[i]["Pincode"].ToString().Trim();
                _CountryCode = dt.Rows[i]["CountryCode"].ToString().Trim().ToUpper();
                _SecondAddress = dt.Rows[i]["WAddress"].ToString().Trim();
                _WCity = dt.Rows[i]["WCity"].ToString().Trim();
                _WStateCode = dt.Rows[i]["WStateCode"].ToString().Trim().ToUpper();
                _WPincode = dt.Rows[i]["WPincode"].ToString().Trim();
                _WCountryCode = dt.Rows[i]["WCountryCode"].ToString().Trim().ToUpper();
                _WTelephone = dt.Rows[i]["WTelephone"].ToString().Trim();
                _Mobile = dt.Rows[i]["Mobile"].ToString().Trim();
                _EMailID = dt.Rows[i]["EmailID"].ToString().Trim();
                _UIN = dt.Rows[i]["UIN"].ToString().Trim();
                _PAN = dt.Rows[i]["PanNo"].ToString().Trim();
                _Gender = dt.Rows[i]["Sex"].ToString().Trim().ToUpper().ToUpper();
                _DateOfBirth = dt.Rows[i]["BirthDate"].ToString().Trim();
                _IdentificationType = dt.Rows[i]["IdentificationType"].ToString().Trim().ToUpper();
                _IdentificationNumber = dt.Rows[i]["IdentificationNo"].ToString().Trim();
                _IssuingAuthority = dt.Rows[i]["IssuingAuth"].ToString().Trim();
                _PlaceOfIssue = dt.Rows[i]["IssuePlace"].ToString().Trim();
                _Nationality = dt.Rows[i]["Nationality"].ToString().Trim().ToUpper();
                _PlaceOfWork = dt.Rows[i]["WorkPlace"].ToString().Trim();
                _FatherOrSpouse = dt.Rows[i]["FatherSpouseName"].ToString().Trim();
                _Occupation = dt.Rows[i]["Occupation"].ToString().Trim();
                _ReportSerialNumber = dt.Rows[i]["ReportSerialNo"].ToString().Trim();

                sw.WriteLine(linenumber.ToString("000000") + _ReportSerialNumber.PadRight(8, ' ') + _PersonName.PadRight(80, ' ') +
                            _CustomerID.PadRight(10, ' ') + _RelationFlag.PadRight(1, ' ') + _CommunicationAddress.PadRight(225, ' ') +
                            _City.PadRight(50, ' ') + _StateCode.PadRight(2, ' ') + _Pincode.PadRight(10, ' ') + _CountryCode.PadRight(2, ' ') +
                            _SecondAddress.PadRight(225, ' ') + _WCity.PadRight(50, ' ') + _WStateCode.PadRight(2, ' ') + _WPincode.PadRight(10, ' ') +
                            _WCountryCode.PadRight(2, ' ') + _WTelephone.PadRight(30, ' ') + _Mobile.PadRight(30, ' ') + _Fax.PadRight(30, ' ') +
                            _EMailID.PadRight(50, ' ') + _PAN.PadRight(10, ' ') + _UIN.PadRight(30, ' ') + _Gender.PadRight(1, ' ') + _DateOfBirth.PadRight(10, ' ') +
                            _IdentificationType.PadRight(1, ' ') + _IdentificationNumber.PadRight(20, ' ') + _IssuingAuthority.PadRight(20, ' ') +
                            _PlaceOfIssue.PadRight(20, ' ') + _Nationality.PadRight(2, ' ') + _PlaceOfWork.PadRight(80, ' ') + _FatherOrSpouse.PadRight(80, ' ') +
                            _Occupation.PadRight(50, ' ')
                            );

                _fileGenrated = "true" + _fileName;
            }

        }
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return _fileGenrated;
    }
    protected string TRFLPE()
    {
        string _fileGenrated = "false";

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Branch", "");
        SqlParameter p2 = new SqlParameter("@FromDate", txtFromDate.Text.ToString().Trim());
        SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text.ToString().Trim());

        string _querry = "TF_CBTR_GenerateTRFLPE";

        string _fileName = "TRFLPE.txt";
        string _filePath = _directoryPath + "/" + _fileName;
        StreamWriter sw;
        sw = File.CreateText(_filePath);

        DataTable dt = objData.getData(_querry, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {

            int linenumber = 0;
            string _ReportSerialNumber = "";
            string _PersonName = "";
            string _CustomerID = "";
            string _RelationFlag = "";
            string _CommunicationAddress = "";
            string _City = "";
            string _StateCode = "";
            string _Pincode = "";
            string _CountryCode = "";
            string _SecondAddress = "";
            string _RCity = "";
            string _RStateCode = "";
            string _RPincode = "";
            string _RCountryCode = "";
            string _RTelephone = "";
            string _Mobile = "";
            string _Fax = "";
            string _EMailID = "";
            string _PAN = "";
            string _UIN = "";
            string _ConstitutionType = "";
            string _RegistrationNumber = "";
            string _DateOfIncorpation = "";
            string _PlaceOfRegistration = "";
            string _OCountryCode = "";
            string _NatureOfBusiness = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;

                _PersonName = dt.Rows[i]["LEname"].ToString().Trim();
                _CustomerID = ""; // dt.Rows[i]["AcNo"].ToString().Trim();
                _RelationFlag = dt.Rows[i]["RelationFlag"].ToString().Trim();
                _CommunicationAddress = dt.Rows[i]["CommunicationAddress"].ToString().Trim();
                _City = dt.Rows[i]["City"].ToString().Trim();
                _StateCode = dt.Rows[i]["StateCode"].ToString().Trim().ToUpper();
                _Pincode = dt.Rows[i]["Pincode"].ToString().Trim();
                _CountryCode = dt.Rows[i]["CountryCode"].ToString().Trim().ToUpper();
                _SecondAddress = dt.Rows[i]["RAddress"].ToString().Trim();
                _RCity = dt.Rows[i]["RCity"].ToString().Trim();
                _RStateCode = dt.Rows[i]["RStateCode"].ToString().Trim().ToUpper();
                _RPincode = dt.Rows[i]["RPincode"].ToString().Trim();
                _RCountryCode = dt.Rows[i]["RCountryCode"].ToString().Trim().ToUpper();
                _RTelephone = dt.Rows[i]["RTelephone"].ToString().Trim();
                _Mobile = dt.Rows[i]["Mobile"].ToString().Trim();
                _Fax = dt.Rows[i]["Fax"].ToString().Trim();
                _EMailID = dt.Rows[i]["EmailID"].ToString().Trim();
                _PAN = dt.Rows[i]["PanNo"].ToString().Trim();
                _UIN = dt.Rows[i]["UIN"].ToString().Trim();
                _ConstitutionType = dt.Rows[i]["ConstType"].ToString().Trim();
                _RegistrationNumber = dt.Rows[i]["RegNo"].ToString().Trim();
                _DateOfIncorpation = dt.Rows[i]["InCorpDate"].ToString().Trim();
                _PlaceOfRegistration = dt.Rows[i]["RegPlace"].ToString().Trim();
                _OCountryCode = dt.Rows[i]["OCountryCode"].ToString().Trim().ToUpper();
                _NatureOfBusiness = dt.Rows[i]["Business"].ToString().Trim();
                _ReportSerialNumber = dt.Rows[i]["ReportSerialNo"].ToString().Trim();


                sw.WriteLine(linenumber.ToString("000000") + _ReportSerialNumber.PadRight(8, ' ') + _PersonName.PadRight(80, ' ') +
                            _CustomerID.PadRight(10, ' ') + _RelationFlag.PadRight(1, ' ') + _CommunicationAddress.PadRight(225, ' ') +
                            _City.PadRight(50, ' ') + _StateCode.PadRight(2, ' ') + _Pincode.PadRight(10, ' ') + _CountryCode.PadRight(2, ' ') +
                            _SecondAddress.PadRight(225, ' ') + _RCity.PadRight(50, ' ') + _RStateCode.PadRight(2, ' ') + _RPincode.PadRight(10, ' ') +
                            _RCountryCode.PadRight(2, ' ') + _RTelephone.PadRight(30, ' ') + _Mobile.PadRight(30, ' ') + _Fax.PadRight(30, ' ') +
                            _EMailID.PadRight(50, ' ') + _PAN.PadRight(10, ' ') + _UIN.PadRight(30, ' ') + _ConstitutionType.PadRight(1, ' ') + _RegistrationNumber.PadRight(20, ' ') +
                            _DateOfIncorpation.PadRight(10, ' ') + _PlaceOfRegistration.PadRight(20, ' ') + _OCountryCode.PadRight(2, ' ') +
                            _NatureOfBusiness.PadRight(50, ' ')
                            );

                _fileGenrated = "true" + _fileName;
            }

        }
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return _fileGenrated;
    }
    protected string TRFPIN()
    {
        string _fileGenrated = "false";
        string _fileName = "TRFPIN.txt";
        string _filePath = _directoryPath + "/" + _fileName;
        StreamWriter sw;
        sw = File.CreateText(_filePath);

        sw.Flush();
        sw.Close();
        sw.Dispose();
        _fileGenrated = "true" + _fileName;
        return _fileGenrated;
    }
    protected string TRFBRC()
    {
        string _fileGenrated = "false";
        string _Month = txtToDate.Text.Substring(3, 2);
        string _Year = txtToDate.Text.Substring(6, 4);

        string _fileName = "TRFBRC.txt";
        string _filePath = _directoryPath + "/" + _fileName;

        TF_DATA objData = new TF_DATA();

        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime FDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateinfo);
        DateTime TDate = Convert.ToDateTime(txtToDate.Text.Trim(), dateinfo);

        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = "";

        SqlParameter p2 = new SqlParameter("@FDate", SqlDbType.VarChar);
        p2.Value = FDate.ToString("dd/MM/yyyy");

        SqlParameter p3 = new SqlParameter("@TDate", SqlDbType.VarChar);
        p3.Value = TDate.ToString("dd/MM/yyyy");

        StreamWriter sw;
        sw = File.CreateText(_filePath);

        string _qry = "TF_Branch_File";
        DataTable dt = objData.getData(_qry, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int linenumber = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strName = "";
                string strAddress = "";
                string strPincode = "";
                string strTel = "";
                string strEmail = "";
                string strFax = "";
                string strFIUId = "";
                string strRefNo = "";
                string strStateCode = "";
                string strCountryCode = "";
                string strMobileNo = "";
                string strRemarks = "";
                string strBankName = "";
                string reportRole = "X";

                linenumber = linenumber + 1;

                strRefNo = dt.Rows[i]["AuthorizedDealerCode"].ToString().Trim();
                strBankName = "NATIONAL BANK OF ABU DHABI";

                strName = dt.Rows[i]["BranchName"].ToString().Trim();
                strAddress = dt.Rows[i]["BranchAddress"].ToString().Trim();
                strPincode = dt.Rows[i]["Pincode"].ToString().Trim();
                strTel = dt.Rows[i]["TelephoneNo"].ToString().Trim();
                strEmail = dt.Rows[i]["EmailID"].ToString().Trim();
                strFax = dt.Rows[i]["FaxNo"].ToString().Trim();
                strFIUId = dt.Rows[i]["fiu"].ToString().Trim();         //In file fiu means BIC
                strStateCode = dt.Rows[i]["stateid"].ToString().Trim();
                strCountryCode = dt.Rows[i]["countryid"].ToString().Trim();
                strMobileNo = dt.Rows[i]["mobileno"].ToString().Trim();
                strRemarks = dt.Rows[i]["remarks"].ToString().Trim();


                //decimal strPincode1 = Convert.ToDecimal(strPincode);

                sw.WriteLine(linenumber.ToString("000000") + strBankName.PadRight(80, ' ') + strName.PadRight(80, ' ') + strRefNo.PadRight(20, ' ')
                                   + reportRole.PadRight(1, 'X') + strFIUId.PadRight(11, ' ') + strAddress.PadRight(225, ' ') + strName.PadRight(50, ' ')
                                   + strStateCode.PadRight(2, ' ') + strPincode.PadRight(10, ' ') + strCountryCode.PadRight(2, ' ')
                                   + strTel.PadRight(30, ' ') + strMobileNo.PadRight(30, ' ') + strFax.PadRight(30, ' ')
                                   + strEmail.PadRight(50, ' ') + strRemarks.PadRight(30, ' '));

            }
            _fileGenrated = "true" + _fileName;
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        return _fileGenrated;
    }
    protected string TRFTRN()
    {
        string filegenerated = "false";

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = "";
        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text;
        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text;

        string query = "TF_CBTR_GenerateTRFTRN";
        string _fileName = "TRFTRN.txt";

        string _filePath = _directoryPath + "/" + _fileName;

        StreamWriter sw;
        sw = File.CreateText(_filePath);
        DataTable dt = objData.getData(query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int linenumber = 0;
            string reportsrno = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Trans_Date = "";
                string transtime = "";
                string TransRefNo = "";
                string Trans_Type = "";
                string InstrumentType = "";
                string Trans_inst_Name = "";
                string transInstRefnum = "";
                string Trans_State_Code = "";
                string Trans_Country_Code = "";
                string payinstrumentno = "";
                string payinstrumentissuename = "";
                string instrumentrefno = "";
                string Instrument_Country_Code = "";
                string AmountinINR = "";
                string Amount = "";
                string Currency = "";
                string PurposeofTrans = "";
                string PurposeCode = "";
                string RiskRating = "";
                string CustName = "";
                string custid = "";
                string occupation = "";
                string dob = "";
                string gender = "";
                string nationality = "";
                string idtype = "";
                string idno = "";
                string issuingauthority = "";
                string placeofissue = "";
                string pan = "";
                string uin = "";
                string Address = "";
                string City = "";
                string Cust_State_Code = "";
                string PinCode = "";
                string Cust_Country = "";
                string Telephone = "";
                string Mobile = "";
                string Fax = "";
                string Email = "";
                string AccountNumber = "";
                string acinstname = "";
                string acinstrefnum = "";
                string relatedinstname = "";
                string instrelflag = "";
                string relinstrefnum = "";
                string remarks = "";

                linenumber += 1;

                Trans_Date = dt.Rows[i]["TRAN_DATE"].ToString();
                TransRefNo = dt.Rows[i]["TRANS_REF_NO"].ToString();
                Trans_Type = dt.Rows[i]["Trans_Type"].ToString();
                InstrumentType = dt.Rows[i]["INSTRUMENT_TYPE"].ToString().ToUpper();
                Trans_inst_Name = dt.Rows[i]["TRANS_INSTITUTE_NAME"].ToString();
                transInstRefnum = dt.Rows[i]["TRANS_INSTITUTE_REFNO"].ToString();
                Trans_State_Code = dt.Rows[i]["Trans_State_Code"].ToString().ToUpper();
                Trans_Country_Code = dt.Rows[i]["Trans_Country_Code"].ToString().ToUpper();
                Instrument_Country_Code = dt.Rows[i]["Instrument_Country_Code"].ToString().ToUpper();
                AmountinINR = dt.Rows[i]["AMT_RUPEE"].ToString();
                Amount = dt.Rows[i]["AMT_FRGN_CURR"].ToString();
                Currency = dt.Rows[i]["TRANS_CURR"].ToString().ToUpper();
                PurposeofTrans = dt.Rows[i]["PurposeofTrans"].ToString();
                PurposeCode = dt.Rows[i]["PURPOSE_CODE"].ToString();
                RiskRating = dt.Rows[i]["RiskRating"].ToString();
                CustName = dt.Rows[i]["CUST_NAME"].ToString();
                Address = dt.Rows[i]["CUST_ADDRESS"].ToString();
                City = dt.Rows[i]["CUST_CITY"].ToString();
                Cust_State_Code = dt.Rows[i]["StateCode"].ToString().ToUpper();
                PinCode = dt.Rows[i]["CUST_PINCODE"].ToString();
                Cust_Country = dt.Rows[i]["CountryCode"].ToString().ToUpper();
                Telephone = dt.Rows[i]["CUST_TELEPHONE_NO"].ToString();
                Mobile = dt.Rows[i]["CUST_Mobile"].ToString();
                Fax = dt.Rows[i]["CUST_FAX_NO"].ToString();
                Email = dt.Rows[i]["CUST_EMAIL_ID"].ToString();
                AccountNumber = dt.Rows[i]["CUST_AC_NO"].ToString();
                reportsrno = dt.Rows[i]["ReportSerialNo"].ToString();


                sw.WriteLine(linenumber.ToString("000000") + reportsrno.PadLeft(8, '0') + Trans_Date.PadRight(10, ' ') + transtime.PadRight(8, ' ') + TransRefNo.PadRight(20, ' ')
                             + Trans_Type.PadRight(1, ' ') + InstrumentType.PadRight(1, ' ') + Trans_inst_Name.PadRight(80, ' ') + transInstRefnum.PadRight(20, ' ') + Trans_State_Code.PadRight(2, ' ')
                             + Trans_Country_Code.PadRight(2, 'X') + payinstrumentno.PadRight(20, ' ') + payinstrumentissuename.PadRight(80, ' ') + instrumentrefno.PadRight(20, ' ') + Instrument_Country_Code.PadRight(2, ' ')
                             + AmountinINR.PadLeft(20, '0') + Amount.PadLeft(20, '0') + Currency.PadRight(3, 'X') + PurposeofTrans.PadRight(100, ' ') + PurposeCode.PadRight(5, ' ') + RiskRating.PadRight(2, ' ')
                             + CustName.PadRight(80, ' ') + custid.PadRight(10, ' ') + occupation.PadRight(50, ' ') + dob.PadRight(10, ' ') + gender.PadRight(1, 'X') + nationality.PadRight(2, 'X') + idtype.PadRight(1, 'Z')
                             + idno.PadRight(20, ' ') + issuingauthority.PadRight(20, ' ') + placeofissue.PadRight(20, ' ') + pan.PadRight(10, ' ') + uin.PadRight(30, ' ') + Address.PadRight(225, ' ')
                             + City.PadRight(50, ' ') + Cust_State_Code.PadRight(2, ' ') + PinCode.PadRight(10, ' ') + Cust_Country.PadRight(2, 'X') + Telephone.PadRight(30, ' ') + Mobile.PadRight(30, ' ') + Fax.PadRight(30, ' ')
                             + Email.PadRight(50, ' ') + AccountNumber.PadRight(20, ' ') + acinstname.PadRight(80, ' ') + acinstrefnum.PadRight(20, ' ') + relatedinstname.PadRight(80, ' ') + instrelflag.PadRight(1, ' ')
                             + relinstrefnum.PadRight(20, ' ') + remarks.PadRight(50, ' ')
                            );

                filegenerated = "true" + _fileName;

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            if (dt.Rows[0]["InvalidRecords"].ToString().Trim() == "true")
            {
                filegenerated = "Errors" + _fileName;
            }
        }

        return filegenerated;
    }
    protected string TRFRPT()
    {
        string filegenerated = "false";
        TF_DATA objData = new TF_DATA();
        string _fileName = "TRFRPT.txt";
        string _filePath = _directoryPath + "/" + _fileName;
        StreamWriter sw;
        sw = File.CreateText(_filePath);

        int linenumber = 1;

        string reportnumber = "";
        string originalreportserialnumber = "";
        string query = "TF_CBTR_RPT_File";

        DataTable dt = objData.getData(query);
        string mainperson = "";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string query1 = "TF_CBTR_GetPrincipalOfficerEntryList";
                SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
                p1.Value = "";
                string branchname = "";

                SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                p2.Value = branchname;
                DataTable dt1 = objData.getData(query1, p1, p2);
                if (dt1.Rows.Count > 0)
                {
                    mainperson = dt1.Rows[0]["OfficerName"].ToString();
                }

                string srcalert = "XX";
                string alert1 = "X";
                string alert2 = "X";
                string alert3 = "X";
                string suspisioncrime = "X";
                string suspisionTrans = "X";
                string suspisionrationale = "X";
                string suspisionterrorism = "X";
                string attempttrans = "X";
                string groundsuspision = "X";
                string detailsinvestigation = "X";
                string leainformed = "X";
                string leadetails = "";
                string priorityrating = "XX";
                string reportcoverage = "X";
                string additionaldocuments = "X";

                reportnumber = dt.Rows[i]["ReportSerialNo"].ToString();
                if (rdbNewReport.Checked == true)
                    originalreportserialnumber = "0";
                else
                    originalreportserialnumber = dt.Rows[i]["OriginalReportSrNo"].ToString();
                try
                {
                    sw.WriteLine(linenumber.ToString("000000") + reportnumber.PadLeft(8, '0') + originalreportserialnumber.PadLeft(8, '0') + mainperson.PadRight(80, ' ')
                                 + srcalert.PadRight(2, ' ') + alert1.PadRight(100, ' ') + alert2.PadRight(100, ' ') + alert3.PadRight(100, ' ') + suspisioncrime.PadRight(1, ' ')
                                 + suspisionTrans.PadRight(1, ' ') + suspisionrationale.PadRight(1, ' ') + suspisionterrorism.PadRight(1, ' ') + attempttrans.PadRight(1, ' ')
                                 + groundsuspision.PadRight(4000, ' ') + detailsinvestigation.PadRight(4000, ' ') + leainformed.PadRight(1, ' ') + leadetails.PadRight(250, ' ')
                                 + priorityrating.PadRight(2, ' ') + reportcoverage.PadRight(1, ' ') + additionaldocuments.PadRight(1, ' ')
                                );
                }

                catch (Exception ex)
                { }

                linenumber = linenumber + 1;
                filegenerated = "true" + _fileName;
                
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        return filegenerated;
    }
    private string DataValidation()
    {
        string _InvalidData = "false";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@frmDate", txtFromDate.Text);
        SqlParameter p2 = new SqlParameter("@toDate", txtToDate.Text);

        string _querry = "TF_CBTR_DataValidation";
        DataTable dt = objData.getData(_querry, p1, p2);
        if (dt.Rows.Count > 0)
        {
            _InvalidData = "true";
        }
        else
            _InvalidData = "false";

        return _InvalidData;
    }
}
