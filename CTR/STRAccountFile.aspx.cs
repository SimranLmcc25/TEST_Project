using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;


public partial class CTR_STRAccountFile : System.Web.UI.Page
{
    string _directoryPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillBranch();
            //ddlbranch.SelectedIndex = 1;
            txtfromdate.Attributes.Add("onblur", "return GetToDate();");
            txtMonth.Text = System.DateTime.Now.ToString("MM/yyyy");
            btnHelpReason.Attributes.Add("onclick", "return ReasonHelp();");
            btnHelpPO.Attributes.Add("onclick", "return POHelp();");
            txtPrincOffNo.Attributes.Add("onkeydown", "return POHelp_Key(event);");
            btnGenerate.Attributes.Add("onclick", "return save_validate();");
            txtfromdate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
            InitialState();
            LastSerialNo();

            txtfromdate.Focus();

        }
    }

    //protected void fillBranch()
    //{
    //    TF_DATA objData = new TF_DATA();

    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetBranchDetails";

    //    DataTable dt = objData.getData(_query, p1);
    //    ddlbranch.Items.Clear();
    //    ListItem li = new ListItem();
    //    //li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "--Select--";
    //        ddlbranch.DataSource = dt.DefaultView;
    //        ddlbranch.DataTextField = "BranchCode";
    //        ddlbranch.DataValueField = "BranchCode";
    //        ddlbranch.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    //rowPager.Visible = false;
    //    ddlbranch.Items.Insert(0, li);
    //    ddlbranch.Focus();
    //}


    protected void InitialState()
    {
        //fillBranch();
        fillReportSerialNo();

        txtOgReportSerialNo.Text = "00000000";
        // txtOgBatchNo.Text = "00000000";
        txtReasonForReplace.Text = "";
        txtPrincOffNo.Text = "";
        //txtBatchDate.Text = "";
        lblReason.Text = "";
        lblPO.Text = "";
        //txtDataStruVer.Text = "1";

        txtOgReportSerialNo.Enabled = false;
        //txtOgBatchNo.Enabled = false;
        txtReasonForReplace.Enabled = false;


        btnHelpReason.Enabled = true;
        lblReason.Enabled = true;

        rdbNewReport.Checked = true;
        rdbTestMode.Checked = true;

    }

    protected void branchcount()
    {
        TF_DATA objData = new TF_DATA();

        string query = "CTR_BranchCount";

        DataTable dt = objData.getData(query);


        txtTotalBranches.Text = dt.Rows[0][0].ToString();

    }

    protected void fillReportSerialNo()
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Report", SqlDbType.VarChar);
        p1.Value = "";
        SqlParameter p2 = new SqlParameter("@TransType", "STR");

        string _query = "CTR_Report_SerialNo";

        DataTable dt = objData.getData(_query, p1, p2);

        txtReportNo.Text = dt.Rows[0][0].ToString();
    }


    protected void fillOG_ReportSerialNo()
    {
        string Report = "";
        if (rdbNewReport.Checked == true)
        {
            Report = "New";
            txtReasonForReplace.Text = "";
            lblReason.Text = "";
        }
        else
        {
            Report = "Old";
        }

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@Report", SqlDbType.VarChar);
        p1.Value = Report;

        SqlParameter p2 = new SqlParameter("@TransType", "STR");

        string _query = "CTR_Report_SerialNo";

        DataTable dt = objData.getData(_query, p1,p2);

        txtOgReportSerialNo.Text = dt.Rows[0][0].ToString();
        // txtOgBatchNo.Text = dt.Rows[0][1].ToString();

    }

    protected void InsertReportData()
    {
        string result = "";
        string Report = "";
        string Mode = "";

        string ReportSerialNo = txtReportNo.Text;
        if (rdbNewReport.Checked == true)
        {
            Report = "N";
        }
        else
        {
            Report = "R";
        }

        string Reporttype = Report;
        string MonthYear = txtMonth.Text;
        string ReasonofReplacement = txtReasonForReplace.Text;
        string ogReportNo = txtOgReportSerialNo.Text;

        if (rdbProdMode.Checked == true)
        {
            Mode = "P";
        }
        else
        {
            Mode = "T";
        }

        string Dataver = txtDataStruVer.Text;
        string NoBranch = txtTotalBranches.Text;
        string NoBranchIncNil = txtNoofBranches.Text;
        string NoBranchExcNil = txtBranchesSubmitted.Text;
        string NoCTR = txtNoofCTR.Text;
        string NoTRN = txtNoofTransaction.Text;
        string NoINP = txtIndividual.Text;
        string NoLegal = txtLegalPerson.Text;
        string PrincipleNo = txtPrincOffNo.Text;
        string Ackno = "0";
        string Ackdate = System.DateTime.Now.ToString("dd/MM/yyyy");

        TF_DATA objData = new TF_DATA();

        string query = "CTR_Report_SerialNo_Insert";

        SqlParameter p1 = new SqlParameter("@ReportSerialNo", ReportSerialNo);
        SqlParameter p2 = new SqlParameter("@ReportType", Reporttype);
        SqlParameter p3 = new SqlParameter("@Monthyear", MonthYear);
        SqlParameter p4 = new SqlParameter("@ReplaceReason", ReasonofReplacement);
        SqlParameter p5 = new SqlParameter("@OrgReportNo", ogReportNo);
        SqlParameter p6 = new SqlParameter("@Mode", Mode);
        SqlParameter p7 = new SqlParameter("@DataVer", Dataver);
        SqlParameter p8 = new SqlParameter("@NoBranch", NoBranch);
        SqlParameter p9 = new SqlParameter("@NoBranchIncNil", NoBranchIncNil);
        SqlParameter p10 = new SqlParameter("@NoBranchExcNil", NoBranchExcNil);
        SqlParameter p11 = new SqlParameter("@NoCTR", NoCTR);
        SqlParameter p12 = new SqlParameter("@NoTRN", NoTRN);
        SqlParameter p13 = new SqlParameter("@NoINP", NoINP);
        SqlParameter p14 = new SqlParameter("@NoLPE", NoLegal);
        SqlParameter p15 = new SqlParameter("@AckNo", Ackno);
        SqlParameter p16 = new SqlParameter("@AckDate", Ackdate);
        SqlParameter p17 = new SqlParameter("@OfficerID", PrincipleNo);
        SqlParameter p18 = new SqlParameter("@user", Session["userName"].ToString());
        SqlParameter p19 = new SqlParameter("@loadDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter p20 = new SqlParameter("@TransType", "STR");
        result = objData.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
    }

    protected void filldetails()
    {
        string ReportFlag = "";
        string ModeFlag = "";

        string ReportNo = "00000000" + txtReportNo.Text.Trim();
        txtReportNo.Text = ReportNo.Substring(ReportNo.Length - 8);

        TF_DATA objData = new TF_DATA();

        string query = "CTR_ReportSerialNo_FillDetail";

        SqlParameter p1 = new SqlParameter("@ReportNo", txtReportNo.Text.Trim());
        SqlParameter p2 = new SqlParameter("@TransType", "STR");

        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtMonth.Text = dt.Rows[0]["Monthyear"].ToString();
            txtReasonForReplace.Text = dt.Rows[0]["ReplaceReason"].ToString();
            //txtOgReportSerialNo.Text = dt.Rows[0]["OrgReportNo"].ToString();
            txtPrincOffNo.Text = dt.Rows[0]["OfficerID"].ToString();
            txtDataStruVer.Text = dt.Rows[0]["DataVer"].ToString();
            txtNoofCTR.Text = dt.Rows[0]["NoCTR"].ToString();
            txtTotalBranches.Text = dt.Rows[0]["NoBranch"].ToString();
            txtNoofTransaction.Text = dt.Rows[0]["NoTRN"].ToString();
            txtNoofBranches.Text = dt.Rows[0]["NoBranchIncNil"].ToString();
            txtIndividual.Text = dt.Rows[0]["NoTRN"].ToString();
            txtBranchesSubmitted.Text = dt.Rows[0]["NoBranchExcNil"].ToString();
            txtLegalPerson.Text = dt.Rows[0]["NoLPE"].ToString();
            ReportFlag = dt.Rows[0]["ReportType"].ToString();
            ModeFlag = dt.Rows[0]["Mode"].ToString();

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
                rdbNewReport.Checked = true;
                rdbReplaceReport.Checked = false;


                txtReasonForReplace.Text = "";
                txtOgReportSerialNo.Text = "00000000";
                //txtOgBatchNo.Text = "00000000";

                lblReason.Text = "";

                txtReasonForReplace.Enabled = false;
                txtOgReportSerialNo.Enabled = false;
                //txtOgBatchNo.Enabled = false;
                lblReason.Enabled = false;
                btnHelpReason.Enabled = false;


            }
            else
            {
                rdbNewReport.Checked = false;
                rdbReplaceReport.Checked = true;
                txtReasonForReplace.Text = dt.Rows[0]["ReplaceReason"].ToString();
                txtOgReportSerialNo.Text = dt.Rows[0]["OrgReportNo"].ToString();
                //txtOgBatchNo.Text = dt.Rows[0]["OgBatchNo"].ToString();
                lblReason.Text = dt.Rows[0]["Description"].ToString();

                txtReasonForReplace.Enabled = true;
                txtOgReportSerialNo.Enabled = true;
                //txtOgBatchNo.Enabled = true;
                lblReason.Enabled = true;
                btnHelpReason.Enabled = true;
            }



        }
        else
            txtReportNo.Focus();
    }

    protected string CBAACC()
    {
        string filegenerated = "false";

        TF_DATA objData = new TF_DATA();
        string query = "STR_GenerationofAccountDataFile";

        SqlParameter p1 = new SqlParameter("@fromdate", txtfromdate.Text.Trim());
        SqlParameter p2 = new SqlParameter("@todate", txttodate.Text.Trim());
        // SqlParameter p3 = new SqlParameter("@refno", ddlbranch.SelectedValue.Trim());

        DataTable dt = objData.getData(query, p1, p2);
        string filename = "ARFACC.txt";
        string filepath = _directoryPath + "/" + filename;
        StreamWriter sw;
        sw = File.CreateText(filepath);
        int linenumber = 0;


        if (dt.Rows.Count > 0)
        {

            string ReocordType = "ACC";
            string MonthofReport = "";
            string YearofReport = "";
            string refno = "";
            string Accno = "";
            string name = "";
            string TypeofAccount = "";
            string AccountHolder = "";
            string DateofAccOpening = "";
            string RiskCategory = "";
            string CummCredit = "";
            string CummDebit = "";
            string CummCashDeposit = "";
            string CummCashWithdrawl = "";
            string dDay = "";

            string reportnumber = txtReportNo.Text;
            string originalreportserialnumber = txtOgReportSerialNo.Text;
            string AccountStatus = "A";
            string NoTrasactiontobeReported = "X";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;
                MonthofReport = dt.Rows[i]["MonthOfReport"].ToString().Trim();
                YearofReport = dt.Rows[i]["YearOfReport"].ToString().Trim();
                refno = dt.Rows[i]["RefNo"].ToString().Trim();
                Accno = dt.Rows[i]["AcNo"].ToString().Trim();
                name = dt.Rows[i]["Name"].ToString().Trim();
                TypeofAccount = dt.Rows[i]["AcType"].ToString().Trim();
                AccountHolder = dt.Rows[i]["AcHolderType"].ToString().Trim();
                DateofAccOpening = dt.Rows[i]["AcOpeningDate"].ToString().Trim();
                RiskCategory = dt.Rows[i]["RiskCategory"].ToString().Trim();
                CummCredit = dt.Rows[i]["CumCredit"].ToString().Trim();
                CummDebit = dt.Rows[i]["CumDebit"].ToString().Trim();
                CummCashDeposit = dt.Rows[i]["CumDeposit"].ToString().Trim();
                CummCashWithdrawl = dt.Rows[i]["CumWithdraw"].ToString().Trim();

                if (DateofAccOpening != "")
                {
                    dDay = DateofAccOpening.Substring(6, 4) + '-' + DateofAccOpening.Substring(3, 2) + '-' + DateofAccOpening.Substring(0, 2);
                }
                else
                {
                    dDay = DateofAccOpening;
                }
                sw.WriteLine(linenumber.ToString("000000") + reportnumber.PadLeft(8, '0') +
                    refno.PadRight(20, ' ') + Accno.PadRight(20, ' ') + TypeofAccount.PadRight(2, ' ') + name.PadRight(80, ' ')
                   + AccountHolder.PadRight(1, ' ') + AccountStatus.PadRight(1, ' ') + dDay.PadRight(10, ' ') + RiskCategory.PadRight(2, ' ') +
                    CummCredit.PadLeft(20, '0') + CummDebit.PadLeft(20, '0') + CummCashDeposit.PadLeft(20, '0') + CummCashWithdrawl.PadLeft(20, '0') + NoTrasactiontobeReported);

                filegenerated = "true" + filename;
            }
        }
        txtNoofCTR.Text = linenumber.ToString();
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenerated;
    }

    protected string CBATRN()
    {
        string filegenerated = "false";
        TF_DATA objData = new TF_DATA();

        string query = "STR_GenerationofTansactionFile";

        //SqlParameter p1 = new SqlParameter("@refno", ddlbranch.SelectedValue.Trim());
        SqlParameter p2 = new SqlParameter("@fromdate", txtfromdate.Text.Trim());
        SqlParameter p3 = new SqlParameter("@todate", txttodate.Text.Trim());

        string filename = "ARFTRN.txt";
        string filepath = _directoryPath + "/" + filename;

        StreamWriter sw;
        sw = File.CreateText(filepath);
        DataTable dt = objData.getData(query, p2, p3);
        int linenumber = 0;
        if (dt.Rows.Count > 0)
        {

            string recordtype = "TRN";
            string refno = "";
            string Accno = "";
            string TransID = "";
            string TransDate = "";
            string ModeofTrans = "";
            string DebitCreadit = "";
            string Amount = "";
            string Cur = "";
            string Fund = "";
            string Remark = "";
            string date = "";
            string reportsrno = txtReportNo.Text;

            string ProductType = "XX";
            string Identifier = "";
            string TransactionType = "XX";
            string units = "";
            string rate = "";
            string relAccNo = "";
            string relinsname = "";
            string relinstrefnum = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;
                refno = dt.Rows[i]["RefNo"].ToString().Trim();
                Accno = dt.Rows[i]["AcNo"].ToString().Trim();
                TransID = dt.Rows[i]["TransID"].ToString().Trim();
                TransDate = dt.Rows[i]["TransDate"].ToString().Trim();
                ModeofTrans = dt.Rows[i]["TransMode"].ToString().Trim();
                DebitCreadit = dt.Rows[i]["DebitCredit"].ToString().Trim();
                Amount = dt.Rows[i]["AmountINR"].ToString().Trim();
                Cur = dt.Rows[i]["Cur"].ToString().Trim();
                Fund = dt.Rows[i]["Funds"].ToString().Trim();
                Remark = dt.Rows[i]["Remarks"].ToString().Trim();
                date = TransDate.Substring(6, 4) + '-' + TransDate.Substring(3, 2) + '-' + TransDate.Substring(0, 2);


                sw.WriteLine(linenumber.ToString("000000") + reportsrno.PadRight(8, ' ') + refno.PadRight(20, ' ') + Accno.PadRight(20, ' ') + date +
                    TransID.PadRight(20, ' ') + ModeofTrans + DebitCreadit + Amount.PadLeft(20, '0') + Cur + ProductType + Identifier.PadRight(30, ' ') + TransactionType +
                    units.PadLeft(20, '0') + rate.PadLeft(10, '0') + Fund + relAccNo.PadRight(20, ' ') + relinsname.PadRight(20, ' ') + relinstrefnum.PadRight(20, ' ')
                    + Remark.PadRight(50, ' '));

                filegenerated = "true" + filename;
            }
        }
        txtNoofTransaction.Text = linenumber.ToString();
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenerated;
    }


    protected string CBAINP()
    {
        string filegenerated = "false";

        TF_DATA objData = new TF_DATA();

        string query = "STR_GenerationofIndividualFile";

        //SqlParameter p1 = new SqlParameter("@refno", ddlbranch.SelectedValue.Trim());
        SqlParameter p2 = new SqlParameter("@fromdate", txtfromdate.Text.Trim());
        SqlParameter p3 = new SqlParameter("@todate", txttodate.Text.Trim());

        DataTable dt = objData.getData(query, p2, p3);

        string filename = "ARFINP.txt";
        string filepath = _directoryPath + "/" + filename;
        StreamWriter sw;
        sw = File.CreateText(filepath);
        int linenumber = 0;

        if (dt.Rows.Count > 0)
        {

            string RecordType = "INP";
            string monthofreport = "";
            string yearofreport = "";
            string refno = "";
            string Accno = "";
            string relationflag = "";
            string IndName = "";
            string Fathername = "";
            string Occupation = "";
            string Birthdate = "";
            string Sex = "";
            string Nationality = "";
            string TypeofIdentification = "";
            string Identificationno = "";
            string IssuingAuth = "";
            string PlaceofIssue = "";
            string PanNo = "";
            string address = "";
            string pincode = "";
            string Telephone = "";
            string mobileno = "";
            string email = "";
            string workplace = "";
            string wAddress = "";
            string Wpincode = "";
            string Wtelephone = "";
            string date = "";
            string custid = "";
            string statecode = "";
            string Countrycode = "";
            string wstate = "";
            string Wcountrycode = "";
            string AdCode = "";
            string City = "";
            string WCity = "";
            string FAX = "";
            string UIN = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;
                monthofreport = "NA";
                yearofreport = "NA";
                refno = dt.Rows[i]["BranchCode"].ToString();
                Accno = dt.Rows[i]["AcNo"].ToString();
                relationflag = dt.Rows[i]["RelationFlag"].ToString();
                IndName = dt.Rows[i]["IndName"].ToString();
                Fathername = dt.Rows[i]["FatherSpouseName"].ToString();
                Occupation = dt.Rows[i]["Occupation"].ToString();
                Birthdate = dt.Rows[i]["BirthDate"].ToString();
                Sex = dt.Rows[i]["Sex"].ToString();
                Nationality = dt.Rows[i]["Nationality"].ToString();
                TypeofIdentification = dt.Rows[i]["IdentificationType"].ToString();
                Identificationno = dt.Rows[i]["IdentificationNo"].ToString();
                IssuingAuth = dt.Rows[i]["IssuingAuth"].ToString();
                PlaceofIssue = dt.Rows[i]["IssuePlace"].ToString();
                PanNo = dt.Rows[i]["PanNo"].ToString();
                address = dt.Rows[i]["CommunicationAddress"].ToString();
                pincode = dt.Rows[i]["Pincode"].ToString();
                Telephone = dt.Rows[i]["Telephone"].ToString();
                mobileno = dt.Rows[i]["Mobile"].ToString();
                email = dt.Rows[i]["EmailID"].ToString();
                workplace = dt.Rows[i]["WorkPlace"].ToString();
                wAddress = dt.Rows[i]["WAddress"].ToString();
                Wpincode = dt.Rows[i]["WPincode"].ToString();
                Wtelephone = dt.Rows[i]["WTelephone"].ToString();
                statecode = dt.Rows[i]["StateCode"].ToString();
                Countrycode = dt.Rows[i]["CountryCode"].ToString();
                wstate = dt.Rows[i]["WStateCode"].ToString();
                Wcountrycode = dt.Rows[i]["WCountryCode"].ToString();
                AdCode = dt.Rows[i]["RefNo"].ToString();
                City = dt.Rows[i]["City"].ToString();
                WCity = dt.Rows[i]["WCity"].ToString();
                FAX = dt.Rows[i]["Fax"].ToString();
                UIN = dt.Rows[i]["UIN"].ToString();
                string _ReportSerialNumber = txtReportNo.Text;

                if (Birthdate != "")
                {
                    date = Birthdate.Substring(6, 4) + '-' + Birthdate.Substring(3, 2) + '-' + Birthdate.Substring(0, 2);
                }
                else
                    date = Birthdate;

                sw.WriteLine(linenumber.ToString("000000") + _ReportSerialNumber.PadRight(8, ' ') + AdCode.PadRight(20, ' ') +
                    Accno.PadRight(20, ' ') + IndName.PadRight(80, ' ') + custid.PadRight(10, ' ') + relationflag + address.PadRight(225, ' ') + City.PadRight(50, ' ') + statecode.PadRight(2, ' ') + pincode.PadRight(10, ' ') + Countrycode.PadRight(2, ' ') +
                     wAddress.PadRight(225, ' ') + WCity.PadRight(50, ' ') + wstate.PadRight(2, ' ') + Wpincode.PadRight(10, ' ') + Wcountrycode.PadRight(2, ' ') +
                     Telephone.PadRight(30, ' ') + mobileno.PadRight(30, ' ') + FAX.PadRight(30, ' ') + email.PadRight(50, ' ') + PanNo.PadRight(10, ' ') + UIN.PadRight(30, ' ') + Sex.PadRight(1, ' ') +
                      date.PadRight(10, ' ') + TypeofIdentification.PadRight(1, ' ') + Identificationno.PadRight(20, ' ') + IssuingAuth.PadRight(20, ' ') + PlaceofIssue.PadRight(20, ' ') + Nationality.PadRight(2, ' ') +
                     workplace.PadRight(80, ' ') + Fathername.PadRight(80, ' ') + Occupation.PadRight(50, ' ')

              );

                filegenerated = "true" + filename;
            }
        }
        txtIndividual.Text = linenumber.ToString();
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenerated;
    }

    protected string CBALPE()
    {
        string filegenerated = "true";

        TF_DATA objData = new TF_DATA();

        string query = "STR_GenerationOfLegalPerson";

        //SqlParameter p1 = new SqlParameter("@refno",ddlbranch.SelectedValue.Trim());
        SqlParameter p2 = new SqlParameter("@fromdate", txtfromdate.Text.Trim());
        SqlParameter p3 = new SqlParameter("@todate", txttodate.Text.Trim());

        DataTable dt = objData.getData(query, p2, p3);

        string filename = "ARFLPE.txt";
        string filepath = _directoryPath + "/" + filename;
        StreamWriter sw;
        sw = File.CreateText(filepath);
        int linenumber = 0;
        if (dt.Rows.Count > 0)
        {

            string RecordType = "LPE";
            string monthofreport = "";
            string yearofreport = "";
            string refno = "";
            string Accno = "";
            string relationflag = "";
            string lgname = "";
            string custname = "";
            string business = "";
            string dateincp = "";
            string typeofconstituion = "";
            string Rnumber = "";
            string Rauthority = "";
            string placeReg = "";
            string pan = "";
            string address = "";
            string pincode = "";
            string telephone = "";
            string fax = "";
            string email = "";
            string Raddress = "";
            string Rpincode = "";
            string RTelephone = "";
            string RFax = "";
            string date = "";
            string Stateid = "";
            string Countryid = "";
            string RStateCode = "";
            string RCountryCode = "";
            string BranchRefNum = "";
            string city = "";
            string Rcity = "";
            string mobile = "";
            string OCountry = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;
                monthofreport = "NA";
                yearofreport = "NA";
                refno = dt.Rows[i]["BranchCode"].ToString();
                Accno = dt.Rows[i]["AcNo"].ToString();
                relationflag = dt.Rows[i]["RelationFlag"].ToString();
                lgname = dt.Rows[i]["LEname"].ToString();
                business = dt.Rows[i]["Business"].ToString();
                dateincp = dt.Rows[i]["InCorpDate"].ToString();
                typeofconstituion = dt.Rows[i]["ConstType"].ToString();
                Rnumber = dt.Rows[i]["RegNo"].ToString();
                Rauthority = dt.Rows[i]["RegAuth"].ToString();
                placeReg = dt.Rows[i]["RegPlace"].ToString();
                pan = dt.Rows[i]["PanNo"].ToString();
                address = dt.Rows[i]["CommunicationAddress"].ToString();
                pincode = dt.Rows[i]["PinCode"].ToString();
                telephone = dt.Rows[i]["Telephone"].ToString();
                fax = dt.Rows[i]["Fax"].ToString();
                email = dt.Rows[i]["EmailID"].ToString();
                Raddress = dt.Rows[i]["RAddress"].ToString();
                Rpincode = dt.Rows[i]["RPincode"].ToString();
                RTelephone = dt.Rows[i]["RTelephone"].ToString();
                RFax = dt.Rows[i]["RFax"].ToString();
                Stateid = dt.Rows[i]["StateCode"].ToString();
                Countryid = dt.Rows[i]["CountryCode"].ToString();
                RStateCode = dt.Rows[i]["RStateCode"].ToString();
                RCountryCode = dt.Rows[i]["RCountryCode"].ToString();
                BranchRefNum = dt.Rows[i]["RefNo"].ToString();
                string reportserialno = txtReportNo.Text.Trim();
                city = dt.Rows[i]["City"].ToString();
                Rcity = dt.Rows[i]["RCity"].ToString();
                OCountry = dt.Rows[i]["OCountryCode"].ToString();
                string UIN = "";

                date = dateincp.Substring(6, 4) + '-' + dateincp.Substring(3, 2) + '-' + dateincp.Substring(0, 2);

                sw.WriteLine(linenumber.ToString("000000") + reportserialno.PadRight(8, ' ') + BranchRefNum.PadRight(20, ' ') + Accno.PadRight(20, ' ') + lgname.PadRight(80, ' ') + custname.PadRight(10, ' ') +
                    relationflag.PadRight(1, ' ') + address.PadRight(225, ' ') + city.PadRight(50, ' ') + Stateid.PadRight(2, ' ') + pincode.PadRight(10, ' ') + Countryid.PadRight(2, ' ') +
                    Raddress.PadRight(225, ' ') + Rcity.PadRight(50, ' ') + RStateCode.PadRight(2, ' ') + Rpincode.PadLeft(10, ' ') + RCountryCode.PadRight(2, ' ')
                    + telephone.PadRight(30, ' ') + mobile.PadRight(30, ' ') + fax.PadRight(30, ' ') + email.PadRight(50, ' ') + pan.PadRight(10, ' ') + UIN.PadRight(30, ' ') + typeofconstituion.PadRight(1, ' ') +
                    Rnumber.PadRight(20, ' ') + date.PadRight(10, ' ') + placeReg.PadRight(20, ' ') + OCountry.PadRight(2, ' ') +
                    business.PadRight(50, ' '));

                filegenerated = "true" + filename;
            }
        }
        txtLegalPerson.Text = linenumber.ToString();
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenerated;
    }


    protected string CBABRC()
    {
        string filegenereated = "true";
        TF_DATA objdata = new TF_DATA();

        string query = "CTR_GenerationOfBranchFile";

        DataTable dt = objdata.getData(query);
        string filename = "ARFBRC.txt";
        string filepath = _directoryPath + "/" + filename;

        StreamWriter sw;
        sw = File.CreateText(filepath);
        int linenumber = 0;
        if (dt.Rows.Count > 0)
        {

            string RecordType = "BRC";
            string MonthofRepor = "";
            string YearofReport = "";
            string BranchName = "";
            string BranchRefNo = "";
            string FIU = "";
            string Address = "";
            string City = "";
            string Pincode = "";
            string Telephone = "";
            string Fax = "";
            string Email = "";
            string Stateid = "";
            string Countryid = "";
            string BranchRefNumType = "X";
            string mobileNo = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                linenumber = linenumber + 1;
                MonthofRepor = "NA";
                YearofReport = "NA";
                BranchName = dt.Rows[i]["BranchName"].ToString();
                BranchRefNo = dt.Rows[i]["AuthorizedDealerCode"].ToString();
                FIU = dt.Rows[i]["FIU"].ToString();
                Address = dt.Rows[i]["BranchAddress"].ToString();
                City = dt.Rows[i]["City"].ToString();
                Pincode = dt.Rows[i]["Pincode"].ToString();
                Telephone = dt.Rows[i]["TelephoneNo"].ToString();
                Fax = dt.Rows[i]["FaxNo"].ToString();
                Email = dt.Rows[i]["EmailID"].ToString();
                Stateid = dt.Rows[i]["StateID"].ToString();
                Countryid = dt.Rows[i]["CountryId"].ToString();
                sw.WriteLine(linenumber.ToString("000000") + BranchRefNumType.PadRight(1, ' ') + BranchRefNo.PadRight(20, ' ') + BranchName.PadRight(80, ' ') +
                   Address.PadRight(225, ' ') + City.PadRight(50, ' ') + Stateid.PadRight(2, ' ') + Pincode.PadRight(10, ' ') + Countryid.PadRight(2, ' ') +
                    Telephone.PadRight(30, ' ') + mobileNo.PadRight(30, ' ') + Fax.PadRight(30, ' ') + Email.PadRight(50, ' '));

                filegenereated = "true" + filename;

            }
        }
        txtTotalBranches.Text = linenumber.ToString();
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenereated;
    }

    protected string CBACTL()
    {
        string filegenerated = "true";
        TF_DATA objData = new TF_DATA();

        string filename = "ARFBAT.txt";
        string filepath = _directoryPath + "/" + filename;

        string query = "STR_GenerationOfControlFile";

        SqlParameter p1 = new SqlParameter("@reportNo", txtReportNo.Text.Trim());
        SqlParameter p2 = new SqlParameter("@monthyear", txtMonth.Text.Trim());

        DataTable dt = objData.getData(query, p1, p2);
        StreamWriter sw;
        sw = File.CreateText(filepath);
        //int linenumber = 0;

        if (dt.Rows.Count > 0)
        {
            string ReportName = "CBA";
            string ReportSerialNo = "";
            string recordtype = "STR";
            string MonthOfReport = "";
            string YearOfReport = "";
            string BankName = "";
            string CategoryBank = "";
            string BSRCode = "";
            string FIU = "";
            string POfficerName = "";
            string POfficerDesignaton = "";
            string POfficerAddress = "";
            string POfficerPinCode = "";
            string POfficerTelephone = "";
            string POfficerFax = "";
            string POfficerEmail = "";
            string ReportType = "";
            string ReasonForReplacement = "";
            string OgSerialNo = "";
            string OperationMode = "";
            string DataVer = "";
            string TotalBranch = "";
            string NoBranches = "";
            string NoBranchesSubmitted = "";
            string NoCTR = "";
            string NoTRN = "";
            string NoIND = "";
            string NoLEG = "";
            string AckNo = "";
            string DateOfAck = "";
            string Date = "";
            string StateCode = "";
            string CountryCode = "";
            string MobileNo = "";
            int linenumber = 0;
            string _ReportingEntityName = "";
            string _ReportingEntityCategory = "";
            string _RERegistrationNumber = "";
            string _City = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReportSerialNo = dt.Rows[i]["ReportNo"].ToString();
                MonthOfReport = "NA";
                YearOfReport = "NA";
                BankName = dt.Rows[i]["BankName"].ToString();
                CategoryBank = dt.Rows[i]["Category"].ToString();
                BSRCode = dt.Rows[i]["BSR_Code"].ToString();
                FIU = dt.Rows[i]["FIU_ID"].ToString();
                POfficerName = dt.Rows[i]["OfficerName"].ToString();
                POfficerDesignaton = dt.Rows[i]["OfficerDesignation"].ToString();
                POfficerAddress = dt.Rows[i]["OfficerAddress"].ToString();
                POfficerPinCode = dt.Rows[i]["PinCode"].ToString();
                POfficerTelephone = dt.Rows[i]["Phone"].ToString();
                POfficerFax = dt.Rows[i]["Fax"].ToString();
                POfficerEmail = dt.Rows[i]["OfficerEmail"].ToString();
                ReportType = dt.Rows[i]["ReportType"].ToString();
                ReasonForReplacement = dt.Rows[i]["ReplaceReason"].ToString();
                OgSerialNo = dt.Rows[i]["OrgReportNo"].ToString();
                OperationMode = dt.Rows[i]["Mode"].ToString();
                DataVer = dt.Rows[i]["DataVer"].ToString();
                TotalBranch = dt.Rows[i]["NoBranch"].ToString();
                NoBranches = dt.Rows[i]["NoBranchIncNil"].ToString();
                NoBranchesSubmitted = dt.Rows[i]["NoBranchExcNil"].ToString();
                NoCTR = dt.Rows[i]["NoCTR"].ToString();
                NoTRN = dt.Rows[i]["NoTRN"].ToString();
                NoLEG = dt.Rows[i]["NoLPE"].ToString();
                NoIND = dt.Rows[i]["NoINP"].ToString();
                AckNo = dt.Rows[i]["AckNo"].ToString();
                DateOfAck = dt.Rows[i]["AckDate"].ToString();
                StateCode = dt.Rows[i]["StateCode"].ToString();
                CountryCode = dt.Rows[i]["CountryCode"].ToString();
                MobileNo = dt.Rows[i]["Mobile"].ToString();
                linenumber = linenumber + 1;
                string date = txttodate.Text.Trim();

                Date = date.Substring(6, 4) + '-' + date.Substring(3, 2) + '-' + date.Substring(0, 2);
                _ReportingEntityName = dt.Rows[i]["ReportingEntityName"].ToString().Trim();
                _ReportingEntityCategory = dt.Rows[i]["ReportingEntityCate"].ToString().Trim();
                _City = dt.Rows[i]["OfficerCity"].ToString().Trim();

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
                string _OriginalBatchID = txtOgReportSerialNo.Text.ToString().Trim();
                string _ReasonOfRevision;
                if (_BatchType == "N")
                {
                    _ReasonOfRevision = "N";
                    _OriginalBatchID = "0";
                }
                else
                    _ReasonOfRevision = txtReasonForReplace.Text.ToString().Trim();

                string datastruc = txtDataStruVer.Text.Trim();


                sw.WriteLine(linenumber.ToString("000000") + recordtype.PadRight(3, ' ') + datastruc.PadRight(1, ' ') + _ReportingEntityName.PadRight(80, ' ') + _ReportingEntityCategory.PadRight(5, ' ') + _RERegistrationNumber.PadRight(12, ' ') +
                       FIU.PadRight(10, ' ') + POfficerName.PadRight(80, ' ') + POfficerDesignaton.PadRight(80, ' ') + POfficerAddress.PadRight(225, ' ') +
                       _City.PadRight(50, ' ') + StateCode.PadRight(2, ' ') + POfficerPinCode.PadRight(10, ' ') + CountryCode.PadRight(2, ' ') + POfficerTelephone.PadRight(30, ' ') +
                         MobileNo.PadRight(30, ' ') + POfficerFax.PadRight(30, ' ') + POfficerEmail.PadRight(50, ' ') + ReportSerialNo.PadRight(8, ' ') +
                         Date.PadRight(10, ' ') + MonthOfReport + YearOfReport.PadRight(4,' ') + _OperationalMode.PadRight(1, ' ') + _BatchType.PadRight(1, ' ')
                     + _OriginalBatchID.PadRight(10, ' ') + _ReasonOfRevision.PadRight(1, ' '));

                filegenerated = "true" + filename;

            }

        }
        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenerated;

    }


    protected string CBARPT()
    {

        string filegenerated = "true";
        TF_DATA objData = new TF_DATA();

        string filename = "ARFRPT.txt";
        string filepath = _directoryPath + "/" + filename;

        string query = "CTR_GetPrincipalOfficerEntryList";

        DataTable dt = objData.getData(query);
        StreamWriter sw;
        sw = File.CreateText(filepath);
        //int linenumber = 0;

        int linenumber = 1;
        string reportnumber = txtReportNo.Text;
        string originalreportserialnumber = txtOgReportSerialNo.Text;

        string mainperson = "";
        if (dt.Rows.Count > 0)
        {

            mainperson = dt.Rows[0]["OfficerName"].ToString();

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


            sw.WriteLine(linenumber.ToString("000000") + reportnumber.PadLeft(8, '0') + originalreportserialnumber.PadLeft(8, '0') + mainperson.PadRight(80, ' ')
                     + srcalert.PadRight(2, ' ') + alert1.PadRight(100, ' ') + alert2.PadRight(100, ' ') + alert3.PadRight(100, ' ') + suspisioncrime.PadRight(1, ' ')
                     + suspisionTrans.PadRight(1, ' ') + suspisionrationale.PadRight(1, ' ') + suspisionterrorism.PadRight(1, ' ') + attempttrans.PadRight(1, ' ')
                     + groundsuspision.PadRight(4000, ' ') + detailsinvestigation.PadRight(4000, ' ') + leainformed.PadRight(1, ' ') + leadetails.PadRight(250, ' ')
                     + priorityrating.PadRight(2, ' ') + reportcoverage.PadRight(1, ' ') + additionaldocuments.PadRight(1, ' ')
                    );
            filegenerated = "true" + filename;

        }


        sw.Flush();
        sw.Close();
        sw.Dispose();
        return filegenerated;




    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        _directoryPath = Server.MapPath("~/STR_GeneratedFiles/" + txtfromdate.Text.Substring(6, 4) + txtfromdate.Text.Substring(3, 2));
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        string result = "", files = "";
        int count = 0;

        //=========================================Generating Account Data File===========================================

        result = CBAACC();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Generating Transaction File~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        result = CBATRN();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Generation Individual File @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        result = CBAINP();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";

        }

        //########################################### Generation of Legal File ##################################################

        result = CBALPE();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Generation of Branch File ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        result = CBABRC();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";
        }

        InsertReportData();

        result = CBACTL();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Generation of Report File ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        result = CBARPT();
        if (result.Substring(0, 4) == "true")
        {
            count = count + 1;
            files = result.Substring(4);
            result = "";
        }



        if (count > 0)
        {
            TF_DATA objservername = new TF_DATA();
            string servername = objservername.GetServerName();

            string path = "file://" + servername + "/STR_GeneratedFiles";
            string link = "/STR_GeneratedFiles";

            LabelMessage.Text = "STR File Created Successfully on " + servername + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
            LabelMessage.Text = "No Records Found";

    }
    protected void rdbNewReport_CheckedChanged(object sender, EventArgs e)
    {
        fillOG_ReportSerialNo();
        txtOgReportSerialNo.Enabled = false;
        // txtOgBatchNo.Enabled = false;
        txtReasonForReplace.Enabled = false;

        btnHelpReason.Enabled = false;
        lblReason.Enabled = false;
    }
    protected void rdbReplaceReport_CheckedChanged(object sender, EventArgs e)
    {
        fillOG_ReportSerialNo();

        txtOgReportSerialNo.Enabled = true;
        // txtOgBatchNo.Enabled = true;
        txtReasonForReplace.Enabled = true;
        btnHelpReason.Enabled = true;
        lblReason.Enabled = true;
    }
    protected void txtReportNo_TextChanged(object sender, EventArgs e)
    {
        filldetails();
    }

    protected void LastSerialNo()
    {
        string query = "STR_LastReport_SerialNo";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query);

        if (dt.Rows.Count > 0)
        {
            lblLastSerialNo.Text = dt.Rows[0]["Lreport"].ToString();
            lblLastMonthYear.Text = dt.Rows[0]["Monthyear"].ToString();
        }
        else
        {
            lblLastSerialNo.Text = "";
            lblLastMonthYear.Text = "";
        }
    }
}