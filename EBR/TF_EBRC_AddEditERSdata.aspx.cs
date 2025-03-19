using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;


public partial class EBR_TF_EBRC_AddEditERSdata : System.Web.UI.Page
{
    Encryption objEnc = new Encryption();
    //Boolean fileGenerated = false;

    //start
    Boolean fileGenerated = false;
    public static string docno_at;
    public static string serialno_at = "";
    public static string CustId_at = "";
    public static string shipingbill_at = "";
    public static string portcode_at = "";
    public static string fcamount_at = "";
    public static string RealAmount_at = "";
    public static string DocDate = "";
    public static string currency_at = "";
    public static string billno_at = "";
    public static string shipingbildate_at = "";
    public static string overseasbankid_at = "";
    public static string exchangerate_at = "";

   //added by simran
    public static string freightvalue_at = "";
    public static string categoryofexports_at = "";
    public static string insurance_at = "";
    public static string commission_at = "";
    public static string sac_at = "";
    public static string forfeighting_at = "";
    public static string factoring_at = "";
    public static string vostro_at = "";
  
    //end


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
            //if (Request.QueryString["mode"] == null)
            if (HttpContext.Current.Items["mode"] == null)
            {
              //  Response.Redirect("TF_EBRC_ViewExRdateEntrylist.aspx", true);
                Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "47xn7JmalF5hzvymVLpQ8GIuNoDJOhMxkzOdYLZDF8oK8IHW1rPcROrBgpKtmXJ", true);
            }
            else
            {
                string _srno;
                //if (Request.QueryString["mode"].Trim() != "add")
                if (objEnc.URLIDDecription(HttpContext.Current.Items["mode"].ToString()) != "add")
                {
                   // _srno = Request.QueryString["srno"].Trim();
                    _srno = (objEnc.URLIDDecription(HttpContext.Current.Items["srno"].ToString()));
                }
                else _srno = "";
                SqlParameter P1 = new SqlParameter("@srno", SqlDbType.VarChar);
                P1.Value = _srno;
                SqlParameter P2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
                //P2.Value = Request.QueryString["BranchName"];
                P2.Value = (objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));
                TF_DATA objCheck = new TF_DATA();
                DataTable dt = objCheck.getData("TF_EBRC_checkForFileGenerated", P1, P2);
                if (dt.Rows.Count > 0)
                {
                    labelMessage.Text = "EBRC Certifiacte already generated for this Sr No,You cannot Update.";
                    fileGenerated = true;
                }
            }
            if (!IsPostBack)
            {
                //if (Session["userRole"].ToString() == "Supervisor")
                //{
                //    btnSave.Enabled = false;
                //    lblSupervisormsg.Visible = true;
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //    lblSupervisormsg.Visible = false;
                //}
                clearControls();
                txtDocumentNumber.Focus();

                btnSave.Attributes.Add("onclick", "return validateSave();");
                //if (Request.QueryString["mode"] == null)
                //{
                //    Response.Redirect("TF_EBRC_ViewExRdateEntrylist.aspx", true);
                //}
                if (HttpContext.Current.Items["mode"] == null)
                {
                    //  Response.Redirect("TF_EBRC_ViewExRdateEntrylist.aspx", true);
                    Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "47xn7JmalF5hzvymVLpQ8GIuNoDJOhMxkzOdYLZDF8oK8IHW1rPcROrBgpKtmXJ", true);
                }
                else
                {
                    fillCurrency();
                    fillPortCode();
                   // if (Request.QueryString["mode"].Trim() != "add")
                    if (objEnc.URLIDDecription(HttpContext.Current.Items["mode"].ToString()) != "add")
                    {
                       // txtBranchName.Text = Request.QueryString["BranchName"].Trim();
                        txtBranchName.Text = (objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));
                        txtSerialNumber.Enabled = false;
                      //fillDetails(Request.QueryString["srno"].Trim(), Request.QueryString["BranchName"].Trim());
                        fillDetails((objEnc.URLIDDecription(HttpContext.Current.Items["srno"].ToString())),(objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString())));
                    }
                    else
                    {

                       // txtBranchName.Text = Request.QueryString["BranchName"].Trim();
                        txtBranchName.Text =(objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));
                        getSrialNo();
                        txtCopyFromDocNo.Visible = true;
                        lblCopyFrom.Visible = true;
                        btnCopy.Visible = true;
                        btnDocNoListtoCopy.Visible = true;

                    }

                    txtDocumentDate.Attributes.Add("onkeypress", "return false;");
                    txtShippingBillDate.Attributes.Add("onkeypress", "return false;");

                    txtDocumentDate.Attributes.Add("oncut", "return false;");
                    txtShippingBillDate.Attributes.Add("oncut", "return false;");

                    txtDocumentDate.Attributes.Add("oncopy", "return false;");
                    txtShippingBillDate.Attributes.Add("oncopy", "return false;");

                    txtDocumentDate.Attributes.Add("onpaste", "return false;");
                    txtShippingBillDate.Attributes.Add("onpaste", "return false;");

                    txtDocumentDate.Attributes.Add("oncontextmenu", "return false;");
                    txtShippingBillDate.Attributes.Add("oncontextmenu", "return false;");

                    btnSave.Attributes.Add("onclick", "return validateSave();");


                    txtExchangeRate.Attributes.Add("onblur", "calculateINR();");

                    txtBillNumber.Attributes.Add("onblur", "formatBillNumber();");

                    txtFCAmount.Attributes.Add("onblur", "AddCoomaToTextboxes();");

                    txtINRAmount.Attributes.Add("onkeydown", "return validate_date(event);");
                    txtFCAmount.Attributes.Add("onkeydown", "return validate_date(event);");
                    txtRealisedAmount.Attributes.Add("onkeydown", "return validate_date(event);");
                    txtExchangeRate.Attributes.Add("onkeydown", "return validate_date(event);");

                 //   txtBillNumberPrefix.Attributes.Add("onblur", "toUpper_Case();");

                    //txtShippingBillDate.Attributes.Add("onblur", "validate_dateRange();");
                    txtFCAmount.Attributes.Add("onblur", "return validateAmt();");
                    txtRealisedAmount.Attributes.Add("onblur", "return validateRealisedAmt();");
                    txtRealisation.Attributes.Add("onkeydown", "return onlyFP(event);");
                    txtRealisation.Attributes.Add("onblur", "toUpper_Case();");
                    dropDownListCurrency.Attributes.Add("onchange", "return changeCurrDesc();");
                    dropDownListPortCode.Attributes.Add("onchange", "return changePortCodeDesc();");

                    txtDocumentDate.Attributes.Add("onblur", "return isValidDate(" + txtDocumentDate.ClientID + "," + "'Documemt Date'" + " );");
                    txtShippingBillDate.Attributes.Add("onblur", "return isValidDate(" + txtShippingBillDate.ClientID + "," + "'Shipping Date'" + " );");

                    //start
                    txtFreightValue.Attributes.Add("onblur", "return validateAmt();");
                    txtInsuranceValue.Attributes.Add("onblur", "return validateAmt();");
                    txtCommissionValue.Attributes.Add("onblur", "return validateAmt();");
                    txtFreightValue.Attributes.Add("onkeypress", "return numeric(event);");
                    txtInsuranceValue.Attributes.Add("onkeypress", "return numeric(event);");
                    txtCommissionValue.Attributes.Add("onkeypress", "return numeric(event);");
                    txtSACcode.Attributes.Add("onkeypress", "return numeric(event);");
                   
                    //end

                }

                btnSave.Attributes.Add("onclick", "return validateSave();");
                btncurrList.Attributes.Add("onclick", "return OpenCurrencyList('1');");
                btnPortCodeList.Attributes.Add("onclick", "return PortHelp();");
                btnDocNoListtoCopy.Attributes.Add("onclick", "return OpenCopySRNoList('mouseClick');");
                btnCustList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                btnBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");



            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "toogleDisplayHelp", "return toogleDisplayHelp();", true);
        }

    }


    protected void getSrialNo()
    {

        TF_DATA objData = new TF_DATA();
        String _query = "TF_EBRC_GetSerialNumbers";

        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        //p1.Value = "6600001";//change by simran Mumbai branch   AuthorizedDealerCode=@BranchName(6600001)
        p1.Value = "3700001";

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int newsrno = Convert.ToInt16(dt.Rows[0]["SrNo"].ToString().Trim()) + 1;
            txtSerialNumber.Text = newsrno.ToString();

        }
        else
            txtSerialNumber.Text = "1";
    }

    protected void btnOverseasBank_Click(object sender, EventArgs e)
    {
        if (hdnOverseasId.Value != "")
        {
            txtBankID.Text = hdnOverseasId.Value;
            fillOverseasBankDescription();
            txtBankID.Focus();
        }

    }

    private void fillOverseasBankDescription()
    {
        lblBankName.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtBankID.Text;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblBankName.Text.Length > 20)
            {
                lblBankName.ToolTip = lblBankName.Text;
                lblBankName.Text = lblBankName.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtBankID.Text = "";
            lblBankName.Text = "INVALID CUSTOMER A/C No.";
        }

    }

    protected void btnCustomerCode_Click(object sender, EventArgs e)
    {
        if (hdnCustomerCode.Value != "")
        {
            txtCustomerID.Text = hdnCustomerCode.Value;
            fillCustomerCodeDescription();
            txtCustomerID.Focus();
        }
    }

    public void fillCustomerCodeDescription()
    {
        lblCustomerName.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustomerID.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtIECode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString().Trim();
        }
        else
        {
            txtCustomerID.Text = "";
            lblCustomerName.Text = "INVALID CUSTOMER A/C No.";
        }

    }

    protected void fillCurrency()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        dropDownListCurrency.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            dropDownListCurrency.DataSource = dt.DefaultView;
            dropDownListCurrency.DataTextField = "C_Code";
            dropDownListCurrency.DataValueField = "C_DESCRIPTION";
            dropDownListCurrency.DataBind();

        }
        else
            li.Text = "No record(s) found";

        dropDownListCurrency.Items.Insert(0, li);

    }

    protected void btnCurr_Click(object sender, EventArgs e)
    {
        if (hdnCurId.Value != "")
        {

            dropDownListCurrency.SelectedIndex =
                    dropDownListCurrency.Items.IndexOf(dropDownListCurrency.Items.FindByText(hdnCurId.Value));
            txtCurrencyDescription.Text = dropDownListCurrency.SelectedValue;
            dropDownListCurrency.Focus();

        }

    }

    protected void fillPortCode()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetPortCodeMasterList";
        DataTable dt = objData.getData(_query, p1);
        dropDownListPortCode.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            dropDownListPortCode.DataSource = dt.DefaultView;
            dropDownListPortCode.DataTextField = "port_Code";
            dropDownListPortCode.DataValueField = "portName";
            dropDownListPortCode.DataBind();

        }
        else
            li.Text = "No record(s) found";

        dropDownListPortCode.Items.Insert(0, li);

    }

    protected void btnPortCode_Click(object sender, EventArgs e)
    {
        if (hdnPortCode.Value != "")
        {
            dropDownListPortCode.SelectedIndex =
                        dropDownListPortCode.Items.IndexOf(dropDownListPortCode.Items.FindByText(hdnPortCode.Value));
            txtPortCode.Text = dropDownListPortCode.SelectedValue;
            dropDownListPortCode.Focus();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
      //  Response.Redirect("TF_EBRC_ViewExRdateEntrylist.aspx", true);
        Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "47xn7JmalF5hzvymVLpQ8GIuNoDJOhMxkzOdYLZDF8oK8IHW1rPcROrBgpKtmXJ", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        string _documentDate = txtDocumentDate.Text.ToString();
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
      //  string _mode = Request.QueryString["mode"].Trim();
        string _mode = objEnc.URLIDDecription(HttpContext.Current.Items["mode"].ToString());

        string _serialNumber = txtSerialNumber.Text.Trim();
        string _documentNumber = txtDocumentNumber.Text.Trim();

        string _currency = dropDownListCurrency.SelectedItem.ToString();
        string _fcAmount = txtFCAmount.Text.Trim();
        string _inrAmount = txtINRAmount.Text.Trim();
        string _realisedAmount = txtRealisedAmount.Text.Trim();
        string _beneficiaryName = txtCustomerID.Text.Trim();
        string _remitterName = txtBankID.Text.Trim();
        string _portCode = dropDownListPortCode.SelectedItem.ToString();
        string _shippingBillNo = txtShippingBillNumber.Text.Trim();



        string _shippingBillDate = "";

        if (txtShippingBillDate.Text != "")
        {
            _shippingBillDate = txtShippingBillDate.Text.Trim();
        }

        string _exchangeRate = txtExchangeRate.Text.Trim();
       // string _billNo = (txtBillNumberPrefix.Text.Trim() + txtBillNumber.Text.Trim());
        string _billNo = txtBillNumber.Text.Trim();
        string _excelUpload = "";
        string _status = "F";
        string _full_part_flag = txtRealisation.Text.Trim();
        string _cancelDate = "";

        //simran start
        string _freightval = txtFreightValue.Text.Trim();
        string _categoryofexports = ddlCategoryOfExports.SelectedItem.ToString();
        string _insuranceval = txtInsuranceValue.Text.Trim();
        string _commissionval = txtCommissionValue.Text.Trim();
        string _saccode = txtSACcode.Text.Trim();
        string _forfeighting = ddlForfeighting.SelectedItem.ToString();
        string _factoring = ddlFactoring.SelectedItem.ToString();
        string _vostro = ddlVastro.SelectedItem.ToString();
        string _rmtbk = txtRMTBank.Text.Trim();
        string _rmtcity = txtRMTCity.Text.Trim();
        string _rmtcountry = txtRMTCountry.Text.Trim();
        //simran end


        SqlParameter BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar);
      //  BranchName.Value = Request.QueryString["BranchName"];
        BranchName.Value =(objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));

        SqlParameter mode = new SqlParameter("@mode", SqlDbType.VarChar);
        mode.Value = _mode;

        SqlParameter serialNumber = new SqlParameter("@serialnumber", SqlDbType.VarChar);
        serialNumber.Value = _serialNumber;

        SqlParameter documentNumber = new SqlParameter("@documentnumber", SqlDbType.VarChar);
        documentNumber.Value = _documentNumber;

        SqlParameter documentDate = new SqlParameter("@transactiondate", SqlDbType.VarChar);
        documentDate.Value = _documentDate;

        SqlParameter currency = new SqlParameter("@currencyid", SqlDbType.VarChar);
        currency.Value = _currency;

        SqlParameter fcAmount = new SqlParameter("@amount", SqlDbType.VarChar);
        fcAmount.Value = _fcAmount.Replace(",", "");

        SqlParameter inrAmount = new SqlParameter("@amountinr", SqlDbType.VarChar);
        inrAmount.Value = _inrAmount.Replace(",", "");

        SqlParameter realisedAmount = new SqlParameter("@realisedamount", SqlDbType.VarChar);
        realisedAmount.Value = _realisedAmount.Replace(",", "");

        SqlParameter beneficiaryName = new SqlParameter("@beneficiaryname", SqlDbType.VarChar);
        beneficiaryName.Value = _beneficiaryName;

        SqlParameter remitterName = new SqlParameter("@remittername", SqlDbType.VarChar);
        remitterName.Value = _remitterName;

        SqlParameter portCode = new SqlParameter("@portcode", SqlDbType.VarChar);
        portCode.Value = _portCode;

        SqlParameter shippingBillNo = new SqlParameter("@shippingbillnumber", SqlDbType.VarChar);
        shippingBillNo.Value = _shippingBillNo;

        SqlParameter shippingBillDate = new SqlParameter("@shippingbilldate", SqlDbType.VarChar);
        shippingBillDate.Value = _shippingBillDate;

        SqlParameter userName = new SqlParameter("@adduser", SqlDbType.VarChar);
        userName.Value = _userName;

        SqlParameter uploadingDate = new SqlParameter("@adddate", SqlDbType.VarChar);
        uploadingDate.Value = _uploadingDate;

        SqlParameter exchangeRate = new SqlParameter("@exchangerate", SqlDbType.VarChar);
        exchangeRate.Value = _exchangeRate;


        SqlParameter billNo = new SqlParameter("@billnumber", SqlDbType.VarChar);
        billNo.Value = _billNo;

        SqlParameter excelUpload = new SqlParameter("@excelFlag", SqlDbType.VarChar);
        excelUpload.Value = _excelUpload;

        SqlParameter status = new SqlParameter("@status", SqlDbType.VarChar);
        status.Value = _status;

        SqlParameter full_part_flag = new SqlParameter("@full_part_flag", SqlDbType.VarChar);
        full_part_flag.Value = _full_part_flag;


        SqlParameter cancelDate = new SqlParameter("@cancelDate", SqlDbType.VarChar);
        cancelDate.Value = _cancelDate;



        //--add simran start
        SqlParameter freightval = new SqlParameter("@freightval", SqlDbType.VarChar);
        freightval.Value = _freightval.Replace(",", "");

        SqlParameter categoryofexports = new SqlParameter("@categoryofexports", SqlDbType.VarChar);
        categoryofexports.Value = _categoryofexports;

        SqlParameter insuranceval = new SqlParameter("@insuranceval", SqlDbType.VarChar);
        insuranceval.Value = _insuranceval.Replace(",", "");

        SqlParameter commissionval = new SqlParameter("@commissionval", SqlDbType.VarChar);
        commissionval.Value = _commissionval.Replace(",", "");

        SqlParameter saccode = new SqlParameter("@saccode", SqlDbType.VarChar);
        saccode.Value = _saccode;

        SqlParameter forfeighting = new SqlParameter("@forfeighting", SqlDbType.VarChar);
        forfeighting.Value = _forfeighting;

        SqlParameter factoring = new SqlParameter("@factoring", SqlDbType.VarChar);
        factoring.Value = _factoring;

        SqlParameter vostro = new SqlParameter("@vostro", SqlDbType.VarChar);
        vostro.Value = _vostro;

        SqlParameter rmtbk = new SqlParameter("@rmtbank", SqlDbType.VarChar);
        rmtbk.Value = _rmtbk;

        SqlParameter rmtcity = new SqlParameter("@rmtcity", SqlDbType.VarChar);
        rmtcity.Value = _rmtcity;

        SqlParameter rmtcountry = new SqlParameter("@rmtcountry", SqlDbType.VarChar);
        rmtcountry.Value = _rmtcountry;
        //----simran end




        TF_DATA objSave = new TF_DATA();
        if (fileGenerated == false)
        {
            _result = objSave.SaveDeleteData("TF_EBRC_UpdateERSData", BranchName, mode, serialNumber, documentNumber, documentDate, currency, fcAmount, inrAmount, realisedAmount, beneficiaryName, remitterName, portCode,
                      shippingBillNo, shippingBillDate, userName, uploadingDate, exchangeRate, billNo, excelUpload, status, full_part_flag, cancelDate,
                       freightval, categoryofexports, insuranceval, commissionval, saccode, forfeighting, factoring, vostro, rmtbk, rmtcity, rmtcountry
                      
                      );
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML File already generated for this Sr No,You cannot Update.');", true);
        }

        string _script = "";
        if (_result == "added")
        {
            //_script = "window.location='TF_EBRC_ViewExRdateEntrylist.aspx?'";
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "alert('Record " + _result + "');", true);
            _script = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "47xn7JmalF5hzvymVLpQ8GIuNoDJOhMxkzOdYLZDF8p1t7w94iLeGQKAEfXBnIj/" + objEnc.URLIDEncription(_result) + @"'";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "redirect", _script, true);
        }
        else if (_result == "updated")
        {

            //_script = "window.location='TF_EBRC_ViewExRdateEntrylist.aspx?'";
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "alert('Record " + _result + "');", true);
            _script = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "47xn7JmalF5hzvymVLpQ8GIuNoDJOhMxkzOdYLZDF8p1t7w94iLeGQKAEfXBnIj/" + objEnc.URLIDEncription(_result) + @"'";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "redirect", _script, true);

        }
        else
            labelMessage.Text = _result;


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
        //  Response.Redirect("TF_EBRC_ViewExRdateEntrylist.aspx", true);
    }

    protected void clearControls()
    {
        // txtSerialNumber.Text = "";

        getSrialNo();

        txtDocumentNumber.Text = "";
        txtDocumentDate.Text = "";
        dropDownListCurrency.SelectedIndex = -1;
        txtFCAmount.Text = "";
        txtExchangeRate.Text = "";
        txtINRAmount.Text = "";
        txtRealisedAmount.Text = "";
        dropDownListPortCode.SelectedIndex = -1;
        txtBillNumber.Text = "";
        txtShippingBillNumber.Text = "";
        txtShippingBillDate.Text = "";
        txtBankID.Text = "";
        txtCustomerID.Text = "";
        lblBankName.Text = "";
        lblCustomerName.Text = "";
        txtIECode.Text = "";
        //txtBillNumberPrefix.Text = "";
        txtRealisation.Text = "";
        hdnCurrency.Value = "";
        hdnFCAmt.Value = "";
        hdnINRAmt.Value = "";
        hdnExtRate.Value = "";
        hdnRealisedAmt.Value = "";
        hdnCustomerId.Value = "";
        hdnOverseasId.Value = "";
        //   txtCopyFromDocNo.Text = "";


        //--add Simran Start
        txtFreightValue.Text = "";
        txtInsuranceValue.Text = "";
        txtCommissionValue.Text = "";
        txtSACcode.Text = "";
        ddlCategoryOfExports.SelectedIndex = -1;
        ddlFactoring.SelectedIndex = -1;
        ddlForfeighting.SelectedIndex = -1;
        ddlVastro.SelectedIndex = -1;
        txtPortCode.Text = "";
        txtRMTBank.Text = "";
        txtRMTCity.Text = "";
        txtRMTCountry.Text = "";
        //Simran End



    }

    protected void fillDetails(string _serialNumber, string _branchname)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter P1 = new SqlParameter("@serialnumber", SqlDbType.VarChar);
        P1.Value = _serialNumber;

        SqlParameter P2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        P2.Value = _branchname;

        DataTable dt = objData.getData("TF_EBRC_GetERSDataDetails", P1, P2);
        if (dt.Rows.Count > 0)
        {

            txtSerialNumber.Text = dt.Rows[0]["SRNO"].ToString().Trim();
            txtDocumentNumber.Text = dt.Rows[0]["DOCNO"].ToString().Trim();

            txtDocumentDate.Text = dt.Rows[0]["TRANSACTION_DT"].ToString();

            if (dt.Rows[0]["CURR"].ToString().Trim() != "")
            {
                dropDownListCurrency.SelectedIndex = dropDownListCurrency.Items.IndexOf(dropDownListCurrency.Items.FindByText(dt.Rows[0]["CURR"].ToString().Trim()));

            }
            else
                dropDownListCurrency.SelectedIndex = -1;

            txtFCAmount.Text = dt.Rows[0]["AMOUNT"].ToString().Trim();
            hdnFCAmt.Value = dt.Rows[0]["AMOUNT"].ToString().Trim();

            //txtINRAmount.Text = (decimal.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim()).ToString("N")).ToString();
            //hdnINRAmt.Value = (decimal.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim()).ToString("N")).ToString();
            txtINRAmount.Text = dt.Rows[0]["INR_AMOUNT"].ToString().Trim();
            hdnINRAmt.Value = dt.Rows[0]["INR_AMOUNT"].ToString().Trim();

            if (dt.Rows[0]["REALISED_AMT"].ToString() == "0")
            {
                txtRealisedAmount.Text = "";
            }
            else
            {
                txtRealisedAmount.Text = dt.Rows[0]["REALISED_AMT"].ToString().Trim();
            }
            hdnRealisedAmt.Value = txtRealisedAmount.Text;


            if (dt.Rows[0]["CUST_AC"].ToString().Trim() != "")
            {
                txtCustomerID.Text = dt.Rows[0]["CUST_AC"].ToString().Trim();
                fillCustomerCodeDescription();
            }
            else
                txtCustomerID.Text = "";

            if (dt.Rows[0]["REMMITERNAME"].ToString().Trim() != "")
            {
                txtBankID.Text = dt.Rows[0]["REMMITERNAME"].ToString().Trim();
                fillOverseasBankDescription();

            }
            else
                txtBankID.Text = "";



            if (dt.Rows[0]["PORT_CODE"].ToString().Trim() != "")
            {
                dropDownListPortCode.SelectedIndex = dropDownListPortCode.Items.IndexOf(dropDownListPortCode.Items.FindByText(dt.Rows[0]["PORT_CODE"].ToString().Trim()));

            }
            else
                dropDownListPortCode.SelectedIndex = -1;

            txtRealisation.Text = dt.Rows[0]["FULL_PART_FLAG"].ToString().Trim();

            if (dt.Rows[0]["BILLNO"].ToString().Trim() != "")
            {
                //  txtBillNumberPrefix.Text = (dt.Rows[0]["BILLNO"].ToString().Trim()).Substring(0, 1); 
                //txtBillNumber.Text = (dt.Rows[0]["BILLNO"].ToString().Trim()).Substring(1);
                txtBillNumber.Text = (dt.Rows[0]["BILLNO"].ToString().Trim());
            }
            else
            {
                txtBillNumber.Text = "";
              //  txtBillNumberPrefix.Text = "";
            }

            txtShippingBillNumber.Text = dt.Rows[0]["SHIPPING_BILL_NO"].ToString().Trim();

            if (dt.Rows[0]["SHIPPING_BILL_DT"] != null && dt.Rows[0]["SHIPPING_BILL_DT"].ToString() != "")
            {

                txtShippingBillDate.Text = dt.Rows[0]["SHIPPING_BILL_DT"].ToString();
            }
            else
                txtShippingBillDate.Text = "";
            float inrAmount = 0, FCAmount = 0, realisedAmount = 0;
            if (dt.Rows[0]["INR_AMOUNT"].ToString().Trim() != "")
                inrAmount = float.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim());
            if (dt.Rows[0]["AMOUNT"].ToString().Trim() != "")
                FCAmount = float.Parse(dt.Rows[0]["AMOUNT"].ToString().Trim());
            if (dt.Rows[0]["REALISED_AMT"].ToString().Trim() != "")
                realisedAmount = float.Parse(dt.Rows[0]["REALISED_AMT"].ToString().Trim());
            float exChangeRate = 0;
            if (FCAmount > 0)
            {
                exChangeRate = inrAmount / realisedAmount;
            }
            txtExchangeRate.Text = exChangeRate.ToString();
            hdnExtRate.Value = exChangeRate.ToString();


            //start 
            string freightvalue = dt.Rows[0]["FREIGHT_VALUE"].ToString().Trim();
            if (freightvalue == "0")
            {
                txtFreightValue.Text = "";
            }
            else
            {
                txtFreightValue.Text = dt.Rows[0]["FREIGHT_VALUE"].ToString().Trim();
            }
            freightvalue_at = dt.Rows[0]["FREIGHT_VALUE"].ToString().Trim();

            string exportcategory = dt.Rows[0]["CATEGORY_OF_EXPORTS"].ToString().Trim();
            if (exportcategory == "G" || exportcategory == "S")
            {
                ddlCategoryOfExports.SelectedIndex = ddlCategoryOfExports.Items.IndexOf(ddlCategoryOfExports.Items.FindByText(dt.Rows[0]["CATEGORY_OF_EXPORTS"].ToString().Trim()));
            }
            else
            {
                ddlCategoryOfExports.SelectedIndex = 0;
            }
            categoryofexports_at = dt.Rows[0]["CATEGORY_OF_EXPORTS"].ToString().Trim();

            string insuranceval = dt.Rows[0]["INSURANCE_VALUE"].ToString().Trim();
            if (insuranceval == "0")
            {
                txtInsuranceValue.Text = "";
            }
            else
            {
                txtInsuranceValue.Text = dt.Rows[0]["INSURANCE_VALUE"].ToString().Trim();
            }
            insurance_at = dt.Rows[0]["INSURANCE_VALUE"].ToString().Trim();

            string commissionval = dt.Rows[0]["COMMISSION_VALUE"].ToString().Trim();
            if (commissionval == "0")
            {
                txtCommissionValue.Text = "";
            }
            else
            {
                txtCommissionValue.Text = dt.Rows[0]["COMMISSION_VALUE"].ToString().Trim();
            }
            commission_at = dt.Rows[0]["COMMISSION_VALUE"].ToString().Trim();

            txtSACcode.Text = dt.Rows[0]["SAC_CODE"].ToString().Trim();
            sac_at = dt.Rows[0]["SAC_CODE"].ToString().Trim();

            string forfeiting = dt.Rows[0]["FORFEIGHTING"].ToString().Trim();
            if (forfeiting == "Y" || forfeiting == "N")
            {
                ddlForfeighting.SelectedIndex = ddlForfeighting.Items.IndexOf(ddlForfeighting.Items.FindByText(dt.Rows[0]["FORFEIGHTING"].ToString().Trim()));
            }
            else
            {
                ddlForfeighting.SelectedIndex = 0;
            }
            forfeighting_at = dt.Rows[0]["FORFEIGHTING"].ToString().Trim();

            string factoring = dt.Rows[0]["FACTORING"].ToString().Trim();
            if (factoring == "Y" || factoring == "N")
            {
                ddlFactoring.SelectedIndex = ddlFactoring.Items.IndexOf(ddlFactoring.Items.FindByText(dt.Rows[0]["FACTORING"].ToString().Trim()));
            }
            else
            {
                ddlFactoring.SelectedIndex = 0;
            }
            factoring_at = dt.Rows[0]["FACTORING"].ToString().Trim();

            string vostro = dt.Rows[0]["VOSTRO"].ToString().Trim();
            if (vostro == "Y" || vostro == "N")
            {
                ddlVastro.SelectedIndex = ddlVastro.Items.IndexOf(ddlVastro.Items.FindByText(dt.Rows[0]["VOSTRO"].ToString().Trim()));
            }
            else
            {
                ddlVastro.SelectedIndex = 0;
            }
            vostro_at = dt.Rows[0]["VOSTRO"].ToString().Trim();

            txtRMTBank.Text = dt.Rows[0]["RMT_BANK"].ToString().Trim();

            txtRMTCity.Text = dt.Rows[0]["RMT_CITY"].ToString().Trim();

            txtRMTCountry.Text = dt.Rows[0]["RMT_COUNTRY"].ToString().Trim();
            //end

        }
    }

    protected void fillDetailsCopy(string _serialNumber, string _branchname)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter P1 = new SqlParameter("@serialnumber", SqlDbType.VarChar);
        P1.Value = _serialNumber;

        SqlParameter P2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        P2.Value = _branchname;

        DataTable dt = objData.getData("TF_EBRC_GetERSDataDetails", P1, P2);
        if (dt.Rows.Count > 0)
        {

            if (dt.Rows[0]["CURR"].ToString().Trim() != "")
            {
                dropDownListCurrency.SelectedIndex = dropDownListCurrency.Items.IndexOf(dropDownListCurrency.Items.FindByText(dt.Rows[0]["CURR"].ToString().Trim()));


            }
            else
                dropDownListCurrency.SelectedIndex = -1;

            txtFCAmount.Text = dt.Rows[0]["AMOUNT"].ToString().Trim();
            hdnFCAmt.Value = dt.Rows[0]["AMOUNT"].ToString().Trim();


            //if (dt.Rows[0]["REALISED_AMT"].ToString() == "0")
            //{
            //    txtRealisedAmount.Text = "";
            //}
            //else
            //{
            //    txtRealisedAmount.Text = dt.Rows[0]["REALISED_AMT"].ToString().Trim();
            //}
            //hdnRealisedAmt.Value = txtRealisedAmount.Text;


            if (dt.Rows[0]["CUST_AC"].ToString().Trim() != "")
            {
                txtCustomerID.Text = dt.Rows[0]["CUST_AC"].ToString().Trim();
                fillCustomerCodeDescription();
            }
            else
                txtCustomerID.Text = "";

            if (dt.Rows[0]["REMMITERNAME"].ToString().Trim() != "")
            {
                txtBankID.Text = dt.Rows[0]["REMMITERNAME"].ToString().Trim();
                fillOverseasBankDescription();

            }
            else
                txtBankID.Text = "";



            if (dt.Rows[0]["PORT_CODE"].ToString().Trim() != "")
            {
                dropDownListPortCode.SelectedIndex = dropDownListPortCode.Items.IndexOf(dropDownListPortCode.Items.FindByText(dt.Rows[0]["PORT_CODE"].ToString().Trim()));

            }
            else
                dropDownListPortCode.SelectedIndex = -1;

            txtRealisation.Text = dt.Rows[0]["FULL_PART_FLAG"].ToString().Trim();

            if (dt.Rows[0]["BILLNO"].ToString().Trim() != "")
            {
                //txtBillNumberPrefix.Text = (dt.Rows[0]["BILLNO"].ToString().Trim()).Substring(0, 1);
               // txtBillNumber.Text = (dt.Rows[0]["BILLNO"].ToString().Trim()).Substring(1);
                txtBillNumber.Text = (dt.Rows[0]["BILLNO"].ToString().Trim());
            }
            else
            {
                txtBillNumber.Text = "";
             //   txtBillNumberPrefix.Text = "";
            }

            txtShippingBillNumber.Text = dt.Rows[0]["SHIPPING_BILL_NO"].ToString().Trim();

            if (dt.Rows[0]["SHIPPING_BILL_DT"] != null && dt.Rows[0]["SHIPPING_BILL_DT"].ToString() != "")
            {

                txtShippingBillDate.Text = dt.Rows[0]["SHIPPING_BILL_DT"].ToString();
            }
            else
                txtShippingBillDate.Text = "";


            float inrAmount = float.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim());
            float FCAmount = float.Parse(dt.Rows[0]["AMOUNT"].ToString().Trim());
            float realisedAmount = float.Parse(dt.Rows[0]["REALISED_AMT"].ToString().Trim());
            float exChangeRate = 0;
            if (FCAmount > 0)
            {
                exChangeRate = inrAmount / realisedAmount;
            }
            //txtExchangeRate.Text = exChangeRate.ToString();
            hdnExtRate.Value = exChangeRate.ToString();


        }
    }

    public void AddLeadingZero(TextBox currentField)
    {
        //Check if the value length hasn't reach its max length yet
        if (currentField.Text.Length != currentField.MaxLength)
        {
            //Add leading zero(s) in front of the value
            int numToAdd = currentField.MaxLength - currentField.Text.Length;
            string value = "";
            for (int i = 0; i < numToAdd; i++)
            {
                value += "0";
            }
            currentField.Text = value + currentField.Text;
        }
    }


    protected void dropDownListPortCode_SelectedIndexChanged(object sender, EventArgs e)
    {

        dropDownListPortCode.Focus();
    }
    protected void setLabel(object sender, EventArgs e)
    {
        if (txtRealisation.Text != "")
        {
            if (txtRealisation.Text == "F")
            {
                lblFullPartRealisation.Text = "Full Realisation";
            }
            else
            {
                lblFullPartRealisation.Text = "Part Realisation";
            }
        }

    }
    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerCodeDescription();
        txtCustomerID.Focus();
    }
    protected void txtBankID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasBankDescription();
        txtBankID.Focus();
    }
    protected void txtRealisation_TextChanged(object sender, EventArgs e)
    {
        if (txtRealisation.Text == "P")
        {
            txtRealisation.Focus();
        }
        else
        {
            txtExchangeRate.Focus();
        }
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {


        if (hdnCopySRNo.Value != "")
        {
            txtCopyFromDocNo.Text = hdnCopySRNo.Value;

            clearControls();
           // fillDetailsCopy(txtCopyFromDocNo.Text.Trim(), Request.QueryString["BranchName"].Trim());
            fillDetailsCopy(txtCopyFromDocNo.Text.Trim(),objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));
            txtDocumentNumber.Focus();
        }
    }

    protected void btnCopy_Click1(object sender, EventArgs e)
    {


        clearControls();
        //fillDetailsCopy(txtCopyFromDocNo.Text.Trim(), Request.QueryString["BranchName"].Trim());
        fillDetailsCopy(txtCopyFromDocNo.Text.Trim(), objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));
        txtDocumentNumber.Focus();

    }
    protected void txtCopyFromDocNo_TextChanged(object sender, EventArgs e)
    {
        clearControls();
        //fillDetailsCopy(txtCopyFromDocNo.Text.Trim(), Request.QueryString["BranchName"].Trim());
        fillDetailsCopy(txtCopyFromDocNo.Text.Trim(), objEnc.URLIDDecription(HttpContext.Current.Items["BranchName"].ToString()));
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "toogleDisplayHelp", "return toogleDisplayHelp();", true);
        txtDocumentNumber.Focus();
    }
}