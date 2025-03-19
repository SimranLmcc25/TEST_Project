using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class CTR_CTR_DocFileCreation : System.Web.UI.Page
{
    string _directoryPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillBranch();
            //ddlbranch.SelectedIndex = 1;
            txtfromdate.Attributes.Add("onblur", "return GetToDate();");
            // txtfromdate.Focus();

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
            ddlbranch.DataValueField = "BranchCode";
            ddlbranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        //rowPager.Visible = false;
        ddlbranch.Items.Insert(0, li);
        ddlbranch.Focus();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {

        _directoryPath = Server.MapPath("~/CTR_GeneratedFiles/" + txttodate.Text.Substring(6, 4) + txttodate.Text.Substring(3, 2));
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        string filename = ddlbranch.SelectedValue + "CTRReport" + txttodate.Text.Substring(6, 4) + txttodate.Text.Substring(3, 2) + ".doc";
        string filepath = _directoryPath + "/" + filename;
        StreamWriter sw;
        sw = File.CreateText(filepath);

        TF_DATA BranchData = new TF_DATA();
        string branchquery = "CBWT_GetBranchInfo_DocFile1";
        SqlParameter adcode = new SqlParameter("@adcode", ddlbranch.SelectedValue);

        DataTable branchtable = BranchData.getData(branchquery, adcode);                // To get Branch data

        TF_DATA accountdata = new TF_DATA();
        string accountquery = "CTR_AccountsInfoData_DocFile1";
        SqlParameter p1 = new SqlParameter("@frmdate", txtfromdate.Text);
        SqlParameter p2 = new SqlParameter("@todate", txttodate.Text);

        DataTable acdata = accountdata.getData(accountquery, p1, p2, adcode);           // To get accounts info data

        for (int i = 0; i < acdata.Rows.Count; i++)
        {
            
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("CASH TRANSACTION REPORT (CTR) FOR A BANKING COMPANY");
            sw.WriteLine();
            sw.WriteLine();
            //-------------- Part 1 ------------
            sw.WriteLine("PART 1: DETAILS OF REPORT");
            sw.WriteLine("1.1 Month an Year of Report : " + txttodate.Text.Substring(4, 2) + txttodate.Text.Substring(6, 4));
            sw.WriteLine();
            //-------------- Part 2 ------------
            sw.WriteLine("PART 2: DETAILS OF REPORTING BRANCH / LOCATION");
            sw.WriteLine("2.1 Name of Bank (with Branch) : National Bank of Abu Dhabi, " + branchtable.Rows[0]["BranchName"].ToString());
            sw.WriteLine("2.2 BSR code : " + branchtable.Rows[0]["AuthorizedDealerCode"].ToString());
            sw.WriteLine("2.3 ID allocated by FIU-IND : " + branchtable.Rows[0]["FIU"].ToString());
            sw.WriteLine("2.4 Address (No; Building) : " + branchtable.Rows[0]["add1"].ToString());
            sw.WriteLine("2.5 Street / Road : " + branchtable.Rows[0]["add2"].ToString());
            sw.WriteLine("2.6 Locality : " + branchtable.Rows[0]["add3"].ToString());
            sw.WriteLine("2.7 City / Town : " + branchtable.Rows[0]["add4"].ToString());
            sw.WriteLine("2.8 State, Country : " + branchtable.Rows[0]["add5"].ToString());
            sw.WriteLine("2.9 Pincode : " + branchtable.Rows[0]["Pincode"].ToString());
            sw.WriteLine("2.10 Tel (with STD code) : " + branchtable.Rows[0]["TelephoneNo"].ToString());
            sw.WriteLine("2.11 Fax : " + branchtable.Rows[0]["FaxNo"].ToString());
            sw.WriteLine("2.12 Email : " + branchtable.Rows[0]["EmailID"].ToString());
            sw.WriteLine();

            //-------------- Part 3 ---------------

            TF_DATA accdetails = new TF_DATA();
            string query = "CTR_AccountDetails_DocFile1";
            SqlParameter a1 = new SqlParameter("@AcNo", acdata.Rows[0]["AcNo"].ToString());

            DataTable accdetailstable = accdetails.getData(query, a1, adcode);

            sw.WriteLine("PART 3: DETAILS OF BANK ACCOUNT");
            sw.WriteLine("3.1 Account Number : " + accdetailstable.Rows[0]["AcNo"].ToString());
            sw.WriteLine("3.2 Type of Account : " + accdetailstable.Rows[0]["AcType"].ToString() + "   " + accdetailstable.Rows[0]["AccountDescription"].ToString());
            sw.WriteLine("3.3 Type of Account Holder : " + accdetailstable.Rows[0]["AcHolderType"].ToString() + "   " + accdetailstable.Rows[0]["AccHolderDescription"].ToString());
            sw.WriteLine("3.4 Date of account opening : " + accdetailstable.Rows[0]["AcOpeningDate"].ToString());
            sw.WriteLine("3.5 Risk Category : " + accdetailstable.Rows[0]["RiskCategory"].ToString() + "   " + accdetailstable.Rows[0]["RiskDescription"].ToString());
            sw.WriteLine();

            //-------------- Part 4 ---------------

            sw.WriteLine("PART 4: LIST OF ACCOUNT HOLDERS");
            sw.WriteLine("4.1 Name of first / sole account Holder : " + accdetailstable.Rows[0]["Name"].ToString());
            sw.WriteLine();

            //-------------- Part 5 ---------------

            sw.WriteLine("PART 5: LIST OF OTHER RELATED PERSONS");
            sw.WriteLine("NIL");
            sw.WriteLine();


            //-------------- Part 6 ---------------


            TF_DATA Trndetails = new TF_DATA();
            string Trnquery = "CTR_TransactionDetails_DocFile1";

            SqlParameter t1 = new SqlParameter("@AccNo", acdata.Rows[0]["AcNo"].ToString());
            SqlParameter t2 = new SqlParameter("@BranchCode", accdetailstable.Rows[0]["BranchCode"].ToString());
            SqlParameter t3 = new SqlParameter("@FromDate", txtfromdate.Text);
            SqlParameter t4 = new SqlParameter("@ToDate", txttodate.Text);

            DataTable Trndetailstable = Trndetails.getData(Trnquery, t1, t2, t3, t4);

            if (Trndetailstable.Rows.Count > 0)
            {
                sw.WriteLine("PART 6: DETAILS OF TRANSACTIONS");
                int count = 0;
                for (int n = 0; n < Trndetailstable.Rows.Count; n++)
                {
                    count = count + 1;

                    sw.WriteLine("6." + count + "   " + "Date of Transaction : " + Trndetailstable.Rows[n]["TransDate"].ToString() + "   " + "Debit/ credit : " + Trndetailstable.Rows[n]["DebitCredit"].ToString());
                    sw.WriteLine("        " + "Amount in Rs. : " + Trndetailstable.Rows[n]["AmountINR"].ToString());
                    sw.WriteLine("        " + "Remarks : " + Trndetailstable.Rows[n]["Remarks"].ToString());

                }
                sw.WriteLine();
            }

            //-------------- Part 7 ---------------

            TF_DATA CumAccdetails = new TF_DATA();
            string CumAccquery = "CTR_AccountCumDetails_DocFile1";

            SqlParameter c1 = new SqlParameter("@AccNo", acdata.Rows[0]["AcNo"].ToString());
            SqlParameter c2 = new SqlParameter("@BranchCode", accdetailstable.Rows[0]["BranchCode"].ToString());
            SqlParameter c4 = new SqlParameter("@ToDate", txttodate.Text);

            DataTable Cumdetailstable = CumAccdetails.getData(CumAccquery, c1, c2, c4);

            if (Cumdetailstable.Rows.Count > 0)
            {
                sw.WriteLine("PART 7: CUMULATIVE TOTALS");
                sw.WriteLine("7.1 Total debits in the bank account in the financial year : " + Cumdetailstable.Rows[0]["CumDebit"].ToString());
                sw.WriteLine("7.2 Total credits in the bank account in the financial year : " + Cumdetailstable.Rows[0]["CumCredit"].ToString());
                sw.WriteLine("7.3 Total cash deposits in the bank account in the financial year : " + Cumdetailstable.Rows[0]["CumDeposit"].ToString());
                sw.WriteLine("7.4 Total cash withdrawal in the bank account in the financial year : " + Cumdetailstable.Rows[0]["CumWithdraw"].ToString());
                sw.WriteLine();
            }

            // Annextures

            if (accdetailstable.Rows[0]["AcHolderType"].ToString() == "A")
            {
                TF_DATA Inddetails = new TF_DATA();
                string Indquery = "CTR_IndividualDetails_DocFile1";

                SqlParameter I1 = new SqlParameter("@AccNo", acdata.Rows[0]["AcNo"].ToString());
                SqlParameter I2 = new SqlParameter("@BranchCode", accdetailstable.Rows[0]["BranchCode"].ToString());

                DataTable Inddetailstable = Inddetails.getData(Indquery, I1, I2);


                if (Inddetailstable.Rows.Count > 0)
                {

                    sw.WriteLine("ANNEXTURE A - INDIVIDUAL DETAIL SHEET FOR A BANKING COMPANY");
                    sw.WriteLine();
                    sw.WriteLine("1. Name of bank (with Branch) : National Bank of Abu Dhabi, " + branchtable.Rows[0]["BranchName"].ToString());
                    sw.WriteLine("2. Name of bank (with Branch) : " + branchtable.Rows[0]["AuthorizedDealerCode"].ToString());
                    sw.WriteLine("3. Full Name of Individual : " + Inddetailstable.Rows[0]["IndName"].ToString());
                    sw.WriteLine("4. Account Number / Customer Id : " + Inddetailstable.Rows[0]["AcNo"].ToString());
                    sw.WriteLine("5. Name of Father / Spouse : " + Inddetailstable.Rows[0]["FatherSpouseName"].ToString());
                    sw.WriteLine("6. Occupation : " + Inddetailstable.Rows[0]["Occupation"].ToString());
                    sw.WriteLine("7. Date of Birth : " + Inddetailstable.Rows[0]["BirthDate"].ToString());
                    sw.WriteLine("8. Sex (M/F) : " + Inddetailstable.Rows[0]["Sex"].ToString());
                    sw.WriteLine("9. Nationality : " + Inddetailstable.Rows[0]["Nationality"].ToString());
                    sw.WriteLine("10. Identification Document : " + Inddetailstable.Rows[0]["IdentificationType"].ToString() + "     " + Inddetailstable.Rows[0]["Identification_Description"].ToString());


                    sw.WriteLine("11. Identification Number : " + Inddetailstable.Rows[0]["IdentificationNo"].ToString());
                    sw.WriteLine("12. Issuing Authority : " + Inddetailstable.Rows[0]["IssuingAuth"].ToString());
                    sw.WriteLine("13. Place of issue : " + Inddetailstable.Rows[0]["IssuePlace"].ToString());
                    sw.WriteLine("14. PAN : " + Inddetailstable.Rows[0]["PanNo"].ToString());
                    sw.WriteLine();
                    sw.WriteLine("               " + "COMMUNICATION ADDRESS : ");
                    sw.WriteLine("15. No.; Building : " + Inddetailstable.Rows[0]["CommunicationAddress"].ToString());
                    sw.WriteLine("16. Street / Road : ");
                    sw.WriteLine("17. Locality : ");
                    sw.WriteLine("18. City / Town, District : ");
                    sw.WriteLine("19. State, country : ");
                    sw.WriteLine("20. Pin code : " + Inddetailstable.Rows[0]["Pincode"].ToString());
                    sw.WriteLine("21. Tel (with STD code) : " + Inddetailstable.Rows[0]["Telephone"].ToString());
                    sw.WriteLine("22. Mobile Number : " + Inddetailstable.Rows[0]["Mobile"].ToString());
                    sw.WriteLine("23. Email : " + Inddetailstable.Rows[0]["EmailID"].ToString());
                    sw.WriteLine("24. Name of Organization / Employer : " + Inddetailstable.Rows[0]["WorkPlace"].ToString());
                    sw.WriteLine();
                    sw.WriteLine("               " + "SECOND ADDRESS (Permanent address / place of work) : ");
                    sw.WriteLine("25. No.; Building : " + Inddetailstable.Rows[0]["WAddress"].ToString());
                    sw.WriteLine("26. Street / Road : ");
                    sw.WriteLine("27. Locality : ");
                    sw.WriteLine("28. City / Town, District : ");
                    sw.WriteLine("29. State, country : ");
                    sw.WriteLine("30. Pin code : " + Inddetailstable.Rows[0]["WPincode"].ToString() + "          " + "31. Tel (with STD code) : " + Inddetailstable.Rows[0]["WTelephone"].ToString());
                }
            }

            else if (accdetailstable.Rows[0]["AcHolderType"].ToString() == "B")
            {
                TF_DATA Legdetails = new TF_DATA();
                string Legquery = "CTR_LegalDetails_DocFile1";

                SqlParameter I1 = new SqlParameter("@AccNo", acdata.Rows[0]["AcNo"].ToString());
                SqlParameter I2 = new SqlParameter("@BranchCode", accdetailstable.Rows[0]["BranchCode"].ToString());

                DataTable Legdetailstable = Legdetails.getData(Legquery, I1, I2);


                if (Legdetailstable.Rows.Count > 0)
                {

                    sw.WriteLine("ANNEXTURE B - LEGAL PERSON / ENTITY DETAIL SHEET FOR A BANKING COMPANY");
                    sw.WriteLine();
                    sw.WriteLine("1. Name of bank (with Branch) :  National Bank of Abu Dhabi, " + branchtable.Rows[0]["BranchName"].ToString());
                    sw.WriteLine("2. Name of bank (with Branch) : " + branchtable.Rows[0]["AuthorizedDealerCode"].ToString());
                    sw.WriteLine("3. Name of Legal person / Entity : " + Legdetailstable.Rows[0]["LEname"].ToString());
                    sw.WriteLine("4. Account Number / Customer Id : " + Legdetailstable.Rows[0]["AcNo"].ToString());
                    sw.WriteLine("5. Nature of Business : " + Legdetailstable.Rows[0]["Business"].ToString());
                    sw.WriteLine("6. Date of Incorportaion : " + Legdetailstable.Rows[0]["InCorpDate"].ToString());

                    sw.WriteLine("7. Type of Constitution : " + Legdetailstable.Rows[0]["ConstType"].ToString() + "     " + Legdetailstable.Rows[0]["ConstDescription"].ToString());

                    sw.WriteLine("8. Registration Number : " + Legdetailstable.Rows[0]["RegNo"].ToString());
                    sw.WriteLine("9. Registering Authority : " + Legdetailstable.Rows[0]["RegAuth"].ToString());
                    sw.WriteLine("10. Place of Registration : " + Legdetailstable.Rows[0]["RegPlace"].ToString());
                    sw.WriteLine("11. PAN : " + Legdetailstable.Rows[0]["PanNo"].ToString());
                    sw.WriteLine();

                    sw.WriteLine("               " + "COMMUNICATION ADDRESS : ");
                    sw.WriteLine("12. No.; Building : " + Legdetailstable.Rows[0]["CommunicationAddress"].ToString());
                    sw.WriteLine("13. Street / Road : ");
                    sw.WriteLine("14. Locality : ");
                    sw.WriteLine("15. City / Town, District : ");
                    sw.WriteLine("16. State, country : ");
                    sw.WriteLine("17. Pin code : " + Legdetailstable.Rows[0]["PinCode"].ToString());
                    sw.WriteLine("18. Tel (with STD code) : " + Legdetailstable.Rows[0]["Telephone"].ToString());
                    sw.WriteLine("19. Fax : " + Legdetailstable.Rows[0]["Fax"].ToString());
                    sw.WriteLine("20. Email : " + Legdetailstable.Rows[0]["EmailID"].ToString());

                    sw.WriteLine();
                    sw.WriteLine("               " + "LIST OF DIRECTORS / PARTNERS / MEMBER AND OTHER RELATED PERSON OF LEGAL PERSON / ENTITY");

                    TF_DATA _Inddetails = new TF_DATA();
                    string _Indquery = "CTR_LegIndividualDetails_DocFile1";

                    SqlParameter _I1 = new SqlParameter("@AccNo", acdata.Rows[0]["AcNo"].ToString());
                    SqlParameter _I2 = new SqlParameter("@BranchCode", accdetailstable.Rows[0]["BranchCode"].ToString());

                    DataTable _Inddetailstable = _Inddetails.getData(_Indquery, _I1, _I2);

                    if (_Inddetailstable.Rows.Count > 0)
                    {
                        int count = 0;
                        for (int n = 0; n < Trndetailstable.Rows.Count; n++)
                        {
                            count = count + 1;

                            sw.WriteLine("21." + count + " Name of Individual / Legal Person / Entity : " + _Inddetailstable.Rows[n]["IndName"].ToString());
                            sw.WriteLine("Relation : " + _Inddetailstable.Rows[n]["RelationFlag"].ToString() + "     " + _Inddetailstable.Rows[n]["RelationDescription"].ToString());
                        }
                    }
                }
            }
        }

        sw.Flush();
        sw.Close();
        sw.Dispose();

        TF_DATA objServerName = new TF_DATA();
        string _serverName = objServerName.GetServerName();

        string path = "file://" + _serverName + "/CTR_GeneratedFiles";
        string link = "/CTR_GeneratedFiles";

        LabelMessage.Text = "Doc File Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

    }
}