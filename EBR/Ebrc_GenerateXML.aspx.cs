using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class EBR_Ebrc_GenerateXML : System.Web.UI.Page
{
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

                fillBranch();
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();

                txtCustomer.Attributes.Add("onkeydown", "return CustId(event);");
                btCustList.Attributes.Add("onclick", "return Custhelp();");
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();
                rdbAllCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                rdbSelectedCustomer.Attributes.Add("onclick", "return toogleDisplay();");
                //rdbFresh.Attributes.Add("onclick", "return toogleDisplay();");
                //rdbCancel.Attributes.Add("onclick", "return toogleDisplay();");
                btnSave.Attributes.Add("onclick", "return Generate();");

                rdbAllCustomer.Visible = true;
                rdbAllCustomer.Checked = true;
                //rdbSelectedCustomer.Visible = true;
                rdbFresh.Visible = true;
                rdbCancel.Visible = true;
                rdbFresh.Checked = true;
                //txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                ddlBranch.Focus();

            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "All Branches";
        //ListItem li01 = new ListItem();
        //li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            //li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        //ddlBranch.Items.Insert(1, li01);
    }
    public void CustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomer.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
        }
    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        CustomerIdDescription();
        txtCustomer.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();


        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        SqlParameter p4 = new SqlParameter("@cust", SqlDbType.VarChar);

        if (txtCustomer.Text == "")
            p4.Value = "All Customers";
        else
            p4.Value = txtCustomer.Text.ToString();

        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
        if (rdbFresh.Checked == true)
            p5.Value = "F";
        else
            p5.Value = "C";

        string _qry = "TF_EBRC_GenXMLBranch";
        DataTable dt = objData.getData(_qry, p1, p2, p3, p4, p5);

        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                file_Gen(dt.Rows[j]["BranchName"].ToString());
            }
        }
        else
        {
            //  lblmessage.Text = "No Records";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "alert('No Records');", true);
        }
    }

    protected void file_Gen(string bname)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtfromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = bname;


        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = txtfromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        SqlParameter p4 = new SqlParameter("@cust", SqlDbType.VarChar);

        if (txtCustomer.Text == "")
            p4.Value = "All Customers";
        else
            p4.Value = txtCustomer.Text.ToString();

        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
        if (rdbFresh.Checked == true)
            p5.Value = "F";
        else
            p5.Value = "C";

        string _qry = "TF_EBRC_GenXML";
        DataTable dt = objData.getData(_qry, p1, p2, p3, p4, p5);

        string _strBranchCode;

        if (dt.Rows.Count > 0)
        {

            if (rdbFresh.Checked == true)
            {
                string _directoryPath = "EBRC";
                _directoryPath = Server.MapPath("~/GeneratedFiles/" + _directoryPath);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                //Rem for FileName
                TF_DATA objData1 = new TF_DATA();
                SqlParameter b1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                b1.Value = bname;

                string _qry1 = "TF_EBRC_GetFileName";
                DataTable dt1 = objData.getData(_qry1, b1);

                string GetFileName = dt1.Rows[0]["BrcFileName"].ToString().Trim();
                //Rem End
                string _filePath = _directoryPath + "/" + GetFileName + ".xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sw.WriteLine("<BRCDATA>");
                sw.WriteLine("<FILENAME>" + GetFileName + "</FILENAME>");
                sw.WriteLine("<ENVELOPE>");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    _strBranchCode = dt.Rows[0]["BranchCode"].ToString().Trim();

                    string _strSr = dt.Rows[j]["SRNO"].ToString().Trim();

                    sw.WriteLine("<EBRC>");

                    //Rem for BRCNO
                    TF_DATA objData2 = new TF_DATA();
                    SqlParameter s1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                    s1.Value = bname;

                    string _qry2 = "TF_EBRC_GetBRCNo";
                    DataTable dt2 = objData.getData(_qry2, s1);

                    string GetBrcNo = dt2.Rows[0]["BrcNo"].ToString().Trim();
                    // Rem End

                    sw.WriteLine("<BRCNO>" + GetBrcNo + "</BRCNO>");

                    string _strBrcDt = dt.Rows[j]["TranDt"].ToString().Trim();
                    string _strBrcDtMod = _strBrcDt.Substring(6, 4).ToString() + "-" + _strBrcDt.Substring(3, 2).ToString() + "-" + _strBrcDt.Substring(0, 2).ToString();
                    sw.WriteLine("<BRCDATE>" + _strBrcDtMod + "</BRCDATE>");
                    sw.WriteLine("<STATUS>F</STATUS>");
                    string _strIECode = dt.Rows[j]["IECode"].ToString().Trim();
                    sw.WriteLine("<IEC>" + _strIECode + "</IEC>");
                    string _strExpName = dt.Rows[j]["CustName"].ToString().Trim();
                    string _strCustAc = dt.Rows[j]["CustAc"].ToString().Trim();
                    sw.WriteLine("<EXPNAME>" + _strExpName + "</EXPNAME>");
                    sw.WriteLine("<IFSC>" + GetBrcNo.Substring(0, 11) + "</IFSC>");
                    string _strBillNo = dt.Rows[j]["BillNo"].ToString().Trim();
                    sw.WriteLine("<BILLID>" + _strBillNo + "</BILLID>");
                    string _strsno = dt.Rows[j]["ShipNo"].ToString().Trim();
                    sw.WriteLine("<SNO>" + _strsno + "</SNO>");
                    string _strPort = dt.Rows[j]["Port"].ToString().Trim();
                    sw.WriteLine("<SPORT>" + _strPort + "</SPORT>");
                    string _strShipDt = dt.Rows[j]["ShipDt"].ToString().Trim();
                    string _strShipDtMod = _strShipDt.Substring(6, 4).ToString() + "-" + _strShipDt.Substring(3, 2).ToString() + "-" + _strShipDt.Substring(0, 2).ToString();
                    sw.WriteLine("<SDATE>" + _strShipDtMod + "</SDATE>");
                    string _strCur = dt.Rows[j]["Curr"].ToString().Trim();
                    sw.WriteLine("<SCC>" + _strCur + "</SCC>");
                    string _strSvalue = dt.Rows[j]["Amt"].ToString().Trim();
                    sw.WriteLine("<SVALUE>" + _strSvalue + "</SVALUE>");
                    sw.WriteLine("<RCC>" + _strCur + "</RCC>");
                    string _strRvalue = dt.Rows[j]["RealAmt"].ToString().Trim();
                    sw.WriteLine("<RVALUE>" + _strRvalue + "</RVALUE>");
                    sw.WriteLine("<RDATE>" + _strBrcDtMod + "</RDATE>");
                    string _strRvalueINR = dt.Rows[j]["RealAmtINR"].ToString().Trim();
                    //if (_strRvalueINR != "0.00" || _strRvalueINR != "")
                    //    sw.WriteLine("<RVALUEINR>" + _strRvalueINR + "</RVALUEINR>");

                    if (_strRvalueINR != "0.00")
                    {
                        if (_strRvalueINR != "")
                            sw.WriteLine("<RVALUEINR>" + _strRvalueINR + "</RVALUEINR>");
                    }



                    //Simran start
                    string rmtbk = dt.Rows[j]["RMT_BANK"].ToString().Trim();
                    string _strRmtbk = dt.Rows[j]["RMT_BANK"].ToString().Trim();
                    sw.WriteLine("<RMTBANK>" + _strRmtbk + "</RMTBANK>");

                    string rmtcity = dt.Rows[j]["RMT_CITY"].ToString().Trim();
                    string _strRmtcity = dt.Rows[j]["RMT_CITY"].ToString().Trim();
                    sw.WriteLine("<RMTCITY>" + _strRmtcity + "</RMTCITY>");

                    string rmtcountry = dt.Rows[j]["RMT_COUNTRY"].ToString().Trim();
                    string _strRmtcountry = dt.Rows[j]["RMT_COUNTRY"].ToString().Trim();
                    sw.WriteLine("<RMTCTRY>" + _strRmtcountry + "</RMTCTRY>");

                    string freightval = dt.Rows[j]["FREIGHT_VALUE"].ToString().Trim();
                    string _strFreightval = dt.Rows[j]["FREIGHT_VALUE"].ToString().Trim();
                    sw.WriteLine("<FREIGHT>" + _strFreightval + "</FREIGHT>");

                    string insuranceval = dt.Rows[j]["INSURANCE_VALUE"].ToString().Trim();
                    string _strInsuranceval = dt.Rows[j]["INSURANCE_VALUE"].ToString().Trim();
                    sw.WriteLine("<INSURANCE>" + _strInsuranceval + "</INSURANCE>");


                    string commisiionval = dt.Rows[j]["COMMISSION_VALUE"].ToString().Trim();
                    string _strCommissionval = dt.Rows[j]["COMMISSION_VALUE"].ToString().Trim();
                    sw.WriteLine("<COMMISSION>" + _strCommissionval + "</COMMISSION>");


                    string vostro = dt.Rows[j]["VOSTRO"].ToString().Trim();
                    string _strVostro = dt.Rows[j]["VOSTRO"].ToString().Trim();
                    sw.WriteLine("<ISVOSTRO>" + _strVostro + "</ISVOSTRO>");

                    string exportcategory = dt.Rows[j]["CATEGORY_OF_EXPORTS"].ToString().Trim();
                    string _strCategoryofexport = dt.Rows[j]["CATEGORY_OF_EXPORTS"].ToString().Trim();
                    sw.WriteLine("<EXPORTTYPE>" + _strCategoryofexport + "</EXPORTTYPE>");

                    string saccode = dt.Rows[j]["SAC_CODE"].ToString().Trim();
                    string _strSaccode = dt.Rows[j]["SAC_CODE"].ToString().Trim();
                    sw.WriteLine("<SACCODE>" + _strSaccode + "</SACCODE>");


                    string forfeighting = dt.Rows[j]["FORFEIGHTING"].ToString().Trim();
                    string _strForfeiting = dt.Rows[j]["FORFEIGHTING"].ToString().Trim();
                    sw.WriteLine("<ISFORFEIT>" + _strForfeiting + "</ISFORFEIT>");

                    string factoring = dt.Rows[j]["FACTORING"].ToString().Trim();
                    string _strFactoring = dt.Rows[j]["FACTORING"].ToString().Trim();
                    sw.WriteLine("<ISFACTOR>" + _strFactoring + "</ISFACTOR>");
                    //Simran End


                    sw.WriteLine("</EBRC>");

                    //Rem for inserting data into TF_EBRC_CREATED
                    string _qry3 = "TF_EBRC_Insert_EBRC_Created";

                    SqlParameter i1 = new SqlParameter("@Sr", SqlDbType.Float);
                    i1.Value = _strSr;

                    SqlParameter i2 = new SqlParameter("@FileName", SqlDbType.VarChar);
                    i2.Value = GetFileName;

                    SqlParameter i3 = new SqlParameter("@BrcNo", SqlDbType.VarChar);
                    i3.Value = GetBrcNo;

                    SqlParameter i4 = new SqlParameter("@Status", SqlDbType.VarChar);
                    i4.Value = "F";

                    SqlParameter i5 = new SqlParameter("@IECode", SqlDbType.VarChar);
                    i5.Value = _strIECode;

                    string _userName = Session["userName"].ToString().Trim();
                    string _AddDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    SqlParameter i6 = new SqlParameter("@user", SqlDbType.VarChar);
                    i6.Value = _userName;

                    SqlParameter i7 = new SqlParameter("@AddDate", SqlDbType.VarChar);
                    i7.Value = _AddDate;

                    objData.SaveDeleteData(_qry3, s1, i1, i2, i3, i4, i5, i6, i7);

                }

                sw.WriteLine("</ENVELOPE>");
                sw.Write("</BRCDATA>");
                sw.Flush();
                sw.Close();
                sw.Dispose();

                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();

                lblmessage.Font.Bold = true;

                string path = "file://" + _serverName + "/GeneratedFiles/EBRC";
                string link = "/GeneratedFiles/EBRC/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

            }
            else if (rdbCancel.Checked == true)
            {
                string _directoryPath = "EBRC";
                _directoryPath = Server.MapPath("~/GeneratedFiles/" + _directoryPath);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
                //Rem for FileName
                TF_DATA objData1 = new TF_DATA();
                SqlParameter b1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                b1.Value = bname;

                string _qry1 = "TF_EBRC_GetFileName";
                DataTable dt1 = objData.getData(_qry1, b1);

                string GetFileName = dt1.Rows[0]["BrcFileName"].ToString().Trim();
                //Rem End
                string _filePath = _directoryPath + "/" + GetFileName + ".xml";
                StreamWriter sw;
                sw = File.CreateText(_filePath);

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                sw.WriteLine("<BRCDATA>");
                sw.WriteLine("<FILENAME>" + GetFileName + "</FILENAME>");
                sw.WriteLine("<ENVELOPE>");
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    _strBranchCode = dt.Rows[0]["BranchCode"].ToString().Trim();

                    string _strSr = dt.Rows[j]["SRNO"].ToString().Trim();

                    sw.WriteLine("<EBRC>");

                    //Rem for BRCNO
                    TF_DATA objData2 = new TF_DATA();
                    SqlParameter s1 = new SqlParameter("@Branch", SqlDbType.VarChar);
                    s1.Value = bname;

                    SqlParameter s2 = new SqlParameter("@Sr", SqlDbType.Float);
                    s2.Value = _strSr;

                    string _qry2 = "TF_EBRC_GetBRCNOPrev";
                    DataTable dt2 = objData.getData(_qry2, s1, s2);

                    string GetBrcNo = dt2.Rows[0]["OLDBRCNO"].ToString().Trim();
                    // Rem End

                    sw.WriteLine("<BRCNO>" + GetBrcNo + "</BRCNO>");

                    string _strBrcDt = dt.Rows[j]["TranDt"].ToString().Trim();
                    string _strBrcDtMod = _strBrcDt.Substring(6, 4).ToString() + "-" + _strBrcDt.Substring(3, 2).ToString() + "-" + _strBrcDt.Substring(0, 2).ToString();
                    sw.WriteLine("<BRCDATE>" + _strBrcDtMod + "</BRCDATE>");
                    sw.WriteLine("<STATUS>C</STATUS>");
                    string _strIECode = dt.Rows[j]["IECode"].ToString().Trim();
                    sw.WriteLine("<IEC>" + _strIECode + "</IEC>");
                    string _strExpName = dt.Rows[j]["CustName"].ToString().Trim();
                    string _strCustAc = dt.Rows[j]["CustAc"].ToString().Trim();
                    sw.WriteLine("<EXPNAME>" + _strExpName + "</EXPNAME>");
                    sw.WriteLine("<IFSC>" + GetBrcNo.Substring(0, 11) + "</IFSC>");
                    string _strBillNo = dt.Rows[j]["BillNo"].ToString().Trim();
                    sw.WriteLine("<BILLID>" + _strBillNo + "</BILLID>");
                    string _strsno = dt.Rows[j]["ShipNo"].ToString().Trim();
                    sw.WriteLine("<SNO>" + _strsno + "</SNO>");
                    string _strPort = dt.Rows[j]["Port"].ToString().Trim();
                    sw.WriteLine("<SPORT>" + _strPort + "</SPORT>");
                    string _strShipDt = dt.Rows[j]["ShipDt"].ToString().Trim();
                    string _strShipDtMod = _strShipDt.Substring(6, 4).ToString() + "-" + _strShipDt.Substring(3, 2).ToString() + "-" + _strShipDt.Substring(0, 2).ToString();
                    sw.WriteLine("<SDATE>" + _strShipDtMod + "</SDATE>");
                    string _strCur = dt.Rows[j]["Curr"].ToString().Trim();
                    sw.WriteLine("<SCC>" + _strCur + "</SCC>");
                    string _strSvalue = dt.Rows[j]["Amt"].ToString().Trim();
                    sw.WriteLine("<SVALUE>" + _strSvalue + "</SVALUE>");
                    sw.WriteLine("<RCC>" + _strCur + "</RCC>");
                    string _strRvalue = dt.Rows[j]["RealAmt"].ToString().Trim();
                    sw.WriteLine("<RVALUE>" + _strRvalue + "</RVALUE>");
                    sw.WriteLine("<RDATE>" + _strBrcDtMod + "</RDATE>");
                    string _strRvalueINR = dt.Rows[j]["RealAmtINR"].ToString().Trim();
                    //if (_strRvalueINR != "0.00" || _strRvalueINR != "")
                    //    sw.WriteLine("<RVALUEINR>" + _strRvalueINR + "</RVALUEINR>");
                    if (_strRvalueINR != "0.00")
                    {
                        if (_strRvalueINR != "")
                            sw.WriteLine("<RVALUEINR>" + _strRvalueINR + "</RVALUEINR>");
                    }



                    //Simran start
                    string rmtbk = dt.Rows[j]["RMT_BANK"].ToString().Trim();
                    string _strRmtbk = dt.Rows[j]["RMT_BANK"].ToString().Trim();
                    sw.WriteLine("<RMTBANK>" + _strRmtbk + "</RMTBANK>");

                    string rmtcity = dt.Rows[j]["RMT_CITY"].ToString().Trim();
                    string _strRmtcity = dt.Rows[j]["RMT_CITY"].ToString().Trim();
                    sw.WriteLine("<RMTCITY>" + _strRmtcity + "</RMTCITY>");

                    string rmtcountry = dt.Rows[j]["RMT_COUNTRY"].ToString().Trim();
                    string _strRmtcountry = dt.Rows[j]["RMT_COUNTRY"].ToString().Trim();
                    sw.WriteLine("<RMTCTRY>" + _strRmtcountry + "</RMTCTRY>");

                    string freightval = dt.Rows[j]["FREIGHT_VALUE"].ToString().Trim();
                    string _strFreightval = dt.Rows[j]["FREIGHT_VALUE"].ToString().Trim();
                    sw.WriteLine("<FREIGHT>" + _strFreightval + "</FREIGHT>");

                    string insuranceval = dt.Rows[j]["INSURANCE_VALUE"].ToString().Trim();
                    string _strInsuranceval = dt.Rows[j]["INSURANCE_VALUE"].ToString().Trim();
                    sw.WriteLine("<INSURANCE>" + _strInsuranceval + "</INSURANCE>");


                    string commisiionval = dt.Rows[j]["COMMISSION_VALUE"].ToString().Trim();
                    string _strCommissionval = dt.Rows[j]["COMMISSION_VALUE"].ToString().Trim();
                    sw.WriteLine("<COMMISSION>" + _strCommissionval + "</COMMISSION>");


                    string vostro = dt.Rows[j]["VOSTRO"].ToString().Trim();
                    string _strVostro = dt.Rows[j]["VOSTRO"].ToString().Trim();
                    sw.WriteLine("<ISVOSTRO>" + _strVostro + "</ISVOSTRO>");

                    string exportcategory = dt.Rows[j]["CATEGORY_OF_EXPORTS"].ToString().Trim();
                    string _strCategoryofexport = dt.Rows[j]["CATEGORY_OF_EXPORTS"].ToString().Trim();
                    sw.WriteLine("<EXPORTTYPE>" + _strCategoryofexport + "</EXPORTTYPE>");

                    string saccode = dt.Rows[j]["SAC_CODE"].ToString().Trim();
                    string _strSaccode = dt.Rows[j]["SAC_CODE"].ToString().Trim();
                    sw.WriteLine("<SACCODE>" + _strSaccode + "</SACCODE>");


                    string forfeighting = dt.Rows[j]["FORFEIGHTING"].ToString().Trim();
                    string _strForfeiting = dt.Rows[j]["FORFEIGHTING"].ToString().Trim();
                    sw.WriteLine("<ISFORFEIT>" + _strForfeiting + "</ISFORFEIT>");

                    string factoring = dt.Rows[j]["FACTORING"].ToString().Trim();
                    string _strFactoring = dt.Rows[j]["FACTORING"].ToString().Trim();
                    sw.WriteLine("<ISFACTOR>" + _strFactoring + "</ISFACTOR>");
                    //Simran end


                    sw.WriteLine("</EBRC>");

                    //Rem for inserting data into TF_EBRC_CREATED
                    string _qry3 = "TF_EBRC_Insert_EBRC_Created";

                    SqlParameter i1 = new SqlParameter("@Sr", SqlDbType.Float);
                    i1.Value = _strSr;

                    SqlParameter i2 = new SqlParameter("@FileName", SqlDbType.VarChar);
                    i2.Value = GetFileName;

                    SqlParameter i3 = new SqlParameter("@BrcNo", SqlDbType.VarChar);
                    i3.Value = GetBrcNo;

                    SqlParameter i4 = new SqlParameter("@Status", SqlDbType.VarChar);
                    i4.Value = "C";

                    SqlParameter i5 = new SqlParameter("@IECode", SqlDbType.VarChar);
                    i5.Value = _strIECode;

                    string _userName = Session["userName"].ToString().Trim();
                    string _AddDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    SqlParameter i6 = new SqlParameter("@user", SqlDbType.VarChar);
                    i6.Value = _userName;

                    SqlParameter i7 = new SqlParameter("@AddDate", SqlDbType.VarChar);
                    i7.Value = _AddDate;

                    objData.SaveDeleteData(_qry3, s1, i1, i2, i3, i4, i5, i6, i7);

                }

                sw.WriteLine("</ENVELOPE>");
                sw.Write("</BRCDATA>");
                sw.Flush();
                sw.Close();
                sw.Dispose();

                TF_DATA objServerName = new TF_DATA();
                string _serverName = objServerName.GetServerName();

                lblmessage.Font.Bold = true;

                string path = "file://" + _serverName + "/GeneratedFiles/EBRC";
                string link = "/GeneratedFiles/EBRC/";
                lblmessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            }
        }
        else
        {
            lblmessage.Text = "No Records";
        }
    }
}