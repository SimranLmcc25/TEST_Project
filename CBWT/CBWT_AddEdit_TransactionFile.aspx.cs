using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class CBWT_AddEdit_TransactionFile : System.Web.UI.Page
{
    static string mode = "";
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
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("CBWT_View_TransactionFile.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtTranRefNo.Focus();
                        txtADCode.Text = Request.QueryString["Branch"].ToString();
                        txtYearMonth.Text = Request.QueryString["yearmonth"].ToString();
                        txtSrNo.Text = Request.QueryString["SrNo"].ToString();
                        txtTranDate.Text = Request.QueryString["TransactionDate"];
                        txtTranRefNo.Text = Request.QueryString["TransactionRefNo"];
                        fillDetails();
                        mode = Request.QueryString["mode"].ToString();
                        txtSrNo.Enabled = false;
                    }
                    else
                    {
                        mode = Request.QueryString["mode"].ToString();
                        txtADCode.Text = Request.QueryString["Branch"];
                        txtYearMonth.Text = Request.QueryString["YearMonth"];
                        ddlInstType.SelectedIndex = 5;
                        txtSrNo.Focus();
                        Srno();
                    }
                    txtCurrency_TextChanged(null, null);
                    txtTranCountry_TextChanged(null, null);
                    txtsecondLagTransCountry_TextChanged(null, null);
                    txtInstCountry_TextChanged(null, null);
                    txtPuposeCode_TextChanged(null, null);
                    txtStateCode_TextChanged(null, null);

                    txtAntFC.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtAmtINR.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtExRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                    btnSave.Attributes.Add("onclick", "return validate_Save();");
                    txtTranDate.Attributes.Add("onblur", "return isValidDate(" + txtTranDate.ClientID + ",'Transaction Date');");
                    txtExRate.Attributes.Add("onblur", "calculate_AmtINR();");
                    txtRemAdd.Attributes.Add("onblur", "return validRemAdd();");
                    txtBenAddress.Attributes.Add("onblur", "return validBenAdd();");
                    txtInstCountry.Text = txtTranCountry.Text = "IN";
                    txtTranCountry_TextChanged(null,null);
                    txtInstCountry_TextChanged(null,null);
                    txtTranRefNo.Focus();
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA objSave = new TF_DATA();
        string query = "CBWT_UpdateTransactionFile";
        string ModeType = "";
        string _script = "";
        string mode = Request.QueryString["mode"].ToString();

        if (rbtExport.Checked == true)
        {
            ModeType = "EXP";
        }
        else if (rbtImport.Checked == true)
        {
            ModeType = "IMP";
        }
        else if (rbtInward.Checked == true)
        {
            ModeType = "INW";
        }
        else if (rbtOutward.Checked == true)
        {
            ModeType = "OTW";
        }
        else
        {
            ModeType = "OTH";
        }

        TF_DATA objdata = new TF_DATA();
        if (mode == "add")
        {
            string q1 = "CBWT_SrNo";
            SqlParameter _yearmonth = new SqlParameter("@yearMonth", txtYearMonth.Text.Trim());
            SqlParameter _refno = new SqlParameter("@RefNo", txtADCode.Text.Trim());
            DataTable dt = objdata.getData(q1, _yearmonth, _refno);
            if (dt.Rows.Count > 0)
            {
                Srno();
            }
        }

        SqlParameter p1 = new SqlParameter("@Refno", txtADCode.Text.Trim());
        SqlParameter p2 = new SqlParameter("@yearmonth", txtYearMonth.Text.Trim());
        SqlParameter p3 = new SqlParameter("@srno", txtSrNo.Text.Trim());
        SqlParameter p4 = new SqlParameter("@TranRefNo", txtTranRefNo.Text.Trim());
        SqlParameter p5 = new SqlParameter("@TranDate", txtTranDate.Text.Trim());
        SqlParameter p6 = new SqlParameter("@RemitterID", txtCustACNo.Text.Trim());
        SqlParameter p7 = new SqlParameter("@RemitterName", txtCustName.Text.Trim());
        SqlParameter p8 = new SqlParameter("@BenID", txtBeneficiaryID.Text.Trim());
        SqlParameter p9 = new SqlParameter("@BenName", txtBenName.Text.Trim());
        SqlParameter p10 = new SqlParameter("@BenAddress", txtBenAddress.Text.Trim());
        SqlParameter p11 = new SqlParameter("@BenAcNo", txtBenACNo.Text.Trim());
        SqlParameter p12 = new SqlParameter("@ModType", ModeType);
        SqlParameter p13 = new SqlParameter("@PurposeCode", txtPuposeCode.Text.ToUpper().Trim());
        SqlParameter p14 = new SqlParameter("@TransCur", txtCurrency.Text.ToUpper().Trim());
        SqlParameter p15 = new SqlParameter("@FrngCur", txtAntFC.Text.Trim());
        SqlParameter p16 = new SqlParameter("@AmtRup", txtAmtINR.Text.Trim());
        SqlParameter p17 = new SqlParameter("@ExRt", txtExRate.Text.Trim());
        SqlParameter p18 = new SqlParameter("@StateCode", txtStateCode.Text.ToUpper().Trim());
        SqlParameter p19 = new SqlParameter("@CountryCode", txtTranCountry.Text.ToUpper().Trim());
        SqlParameter p20 = new SqlParameter("@INT_CountryCode", txtInstCountry.Text.ToUpper().Trim());
        SqlParameter p21 = new SqlParameter("@TransType", ddlTransactionType.SelectedValue.Trim());
        SqlParameter p22 = new SqlParameter("@InstType", ddlInstType.SelectedValue.Trim());
        SqlParameter p23 = new SqlParameter("@RiskRating", ddlRiskCategory.SelectedValue.Trim());
        SqlParameter p24 = new SqlParameter("@Trans_InstituteName", txtInstituteName.Text.Trim());
        SqlParameter p25 = new SqlParameter("@Trans_InstituteRefNo", txtInstituteRefNo.Text.Trim());
        SqlParameter p26 = new SqlParameter("@Remarks", txtRemark.Text.Trim());
        SqlParameter p27 = new SqlParameter("@username", Session["userName"].ToString().Trim());
        SqlParameter p28 = new SqlParameter("@LoadDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter p29 = new SqlParameter("@mode", mode);
        SqlParameter p30 = new SqlParameter("@RemAdd", txtRemAdd.Text.Trim());
        SqlParameter p31 = new SqlParameter("@RemAcNo", txtRemAcNo.Text.Trim());
        SqlParameter p32 = new SqlParameter("@SecondLagTransCountryCode", txtsecondLagTransCountry.Text.Trim());

        string result = objSave.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32);
       
        string _NewValue = "";
        string _OldValue = "";

        SqlParameter A1 = new SqlParameter("@oldvalues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@newvalues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@custacno", txtRemAcNo.Text.Trim());
        SqlParameter A4 = new SqlParameter("@documentno", "");
        SqlParameter A5 = new SqlParameter("@documentdate", "");
        SqlParameter A6 = new SqlParameter("@type", SqlDbType.VarChar);
        SqlParameter A7 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A8 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A9 = new SqlParameter("@menu", "Transaction File");
        SqlParameter A10 = new SqlParameter("@refno", txtADCode.Text.Trim());
        SqlParameter A11 = new SqlParameter("@yearmonth", txtYearMonth.Text.Trim());
        SqlParameter A12 = new SqlParameter("@srno", txtSrNo.Text.Trim());

        if (result == "added")
        {
            _NewValue = "Trans Ref No:" + txtTranRefNo.Text.Trim() + ";Trans Date:" + txtTranDate.Text.Trim() + ";Remitter ID:" + txtCustACNo.Text.Trim() +
                        ";Remitter Name:" + txtCustName.Text.Trim() + ";Bene ID:" + txtBeneficiaryID.Text.Trim() + ";Bene Name:" + txtBenName.Text.Trim() +
                        ";Purpose Code:" + txtPuposeCode.Text.Trim() + ";Currency:" + txtCurrency.Text.Trim() + ";AmtFC:" + txtAntFC.Text.Trim() +
                        ";State Code:" + txtStateCode.Text.Trim() + ";Trans Country:" + txtTranCountry.Text.Trim() + ";Instr Country:" + txtInstCountry.Text.Trim() +
                        ";Trans Type:" + ddlTransactionType.SelectedValue.Trim() + ";Inst Type:" + ddlInstType.SelectedValue.Trim() + ";Risk Category:" + ddlRiskCategory.SelectedValue.Trim();

            A1.Value = "";
            A2.Value = _NewValue;
            A6.Value = "Add";
            string S = objdata.SaveDeleteData("CBWT_AddDataLog", A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12);
            _script = "window.location='CBWT_View_TransactionFile.aspx?result=added&Srno=" + txtSrNo.Text.Trim() + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (result == "updated")
            {
                int isneedtolog = 0;

                _OldValue = "Trans Ref No:" + hdnTransRefNo.Value.Trim() + ";Trans Date:" + hdnTransDate.Value.Trim() + ";Remitter ID:" + hdnRemitterID.Value.Trim() +
                        ";Remitter Name:" + hdnRemitterName.Value.Trim() + ";Bene ID:" + hdnBeneID.Value.Trim() + ";Bene Name:" + hdnBeneName.Value.Trim() +
                        ";Purpose Code:" + hdnPuposeCode.Value.Trim() + ";Currency:" + hdnCurrency.Value.Trim() + ";AmtFC:" + hdnAmtFC.Value.Trim() +
                        ";State Code:" + hdnState.Value.Trim() + ";Trans Country:" + hdnCountry.Value.Trim() + ";Instr Country:" + hdnICountry.Value.Trim() +
                        ";Trans Type:" + hdntransType.Value.Trim() + ";Inst Type:" + hdnInstType.Value.Trim() + ";Risk Category:" + hdnRisk.Value.Trim();

                if (hdnTransRefNo.Value != txtTranRefNo.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = "Trans Ref No :" + txtTranRefNo.Text.Trim();

                }

                if (hdnTransDate.Value != txtTranDate.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Trans Date:" + txtTranDate.Text.Trim();
                }

                if (hdnRemitterID.Value != txtCustACNo.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Remitter ID:" + txtCustACNo.Text.Trim();
                }

                if (hdnRemitterName.Value != txtCustName.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Remitter Name:" + txtCustName.Text.Trim();
                }

                if (hdnBeneID.Value != txtBeneficiaryID.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Bene ID:" + txtBeneficiaryID.Text.Trim();
                }

                if (hdnBeneName.Value != txtBenName.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Bene Name:" + txtBenName.Text.Trim();
                }

                if (hdnPuposeCode.Value != txtPuposeCode.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Purpose Code:" + txtPuposeCode.Text.Trim();
                }

                if (hdnCurrency.Value != txtCurrency.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Currency:" + txtCurrency.Text.Trim();

                }

                if (hdnAmtFC.Value != txtAntFC.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Amt FC:" + txtAntFC.Text.Trim();

                }

                if (hdnState.Value != txtStateCode.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";State Code:" + txtStateCode.Text.Trim();
                }

                if (hdnCountry.Value != txtTranCountry.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Trans Country:" + txtTranCountry.Text.Trim();
                }

                if (hdnICountry.Value != txtInstCountry.Text.Trim())
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Instr Country:" + txtInstCountry.Text.Trim();
                }
                if (hdntransType.Value != ddlTransactionType.SelectedValue)
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Trans Type:" + ddlTransactionType.SelectedValue;
                }
                if (hdnInstType.Value != ddlInstType.SelectedValue)
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Inst Type:" + ddlInstType.SelectedValue;
                }

                if (hdnRisk.Value != ddlRiskCategory.SelectedValue)
                {
                    isneedtolog = 1;
                    _NewValue = _NewValue + ";Risk Category:" + ddlRiskCategory.SelectedValue;
                }
                A1.Value = _OldValue;
                A2.Value = _NewValue;
                A6.Value = "Modify";

                if (isneedtolog == 1)
                {
                    string S = objdata.SaveDeleteData("CBWT_AddDataLog", A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12);
                }
                _script = "window.location='CBWT_View_TransactionFile.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = result;
        }
    }
    protected void fillDetails()
    {
        TF_DATA objData = new TF_DATA();
        string mode = "edit";
        string year = txtYearMonth.Text.Trim();
        string refn = txtADCode.Text.Trim();
        string srno = txtSrNo.Text.Trim();

        string query = "Get_TransactionFile_Details";

        SqlParameter p1 = new SqlParameter("@yearmonth", year);
        SqlParameter p2 = new SqlParameter("@refno", refn);
        SqlParameter p3 = new SqlParameter("@srno", srno);

        DataTable dt = objData.getData(query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            mode = "edit";
            txtTranRefNo.Text = dt.Rows[0]["TRANS_REF_NO"].ToString();
            txtTranDate.Text = dt.Rows[0]["TRAN_DATE"].ToString();
            txtCustACNo.Text = dt.Rows[0]["RemitterID"].ToString();
            txtCustName.Text = dt.Rows[0]["RemitterName"].ToString();
            txtRemAdd.Text = dt.Rows[0]["RemitterAddress"].ToString();
            txtRemAcNo.Text = dt.Rows[0]["RemitterAcNo"].ToString();
            txtBeneficiaryID.Text = dt.Rows[0]["BeneficiaryID"].ToString();
            txtBenName.Text = dt.Rows[0]["BeneName"].ToString();
            txtBenAddress.Text = dt.Rows[0]["BeneAddress"].ToString();
            txtBenACNo.Text = dt.Rows[0]["BeneAcNo"].ToString();
            txtPuposeCode.Text = dt.Rows[0]["PURPOSE_CODE"].ToString();
            txtCurrency.Text = dt.Rows[0]["TRANS_CURR"].ToString();
            txtAntFC.Text = dt.Rows[0]["AMT_FRGN_CURR"].ToString();
            txtAmtINR.Text = dt.Rows[0]["AMT_RUPEE"].ToString();
            txtExRate.Text = dt.Rows[0]["EX_RT"].ToString();
            txtStateCode.Text = dt.Rows[0]["TRANS_STATE_CODE"].ToString();
            txtTranCountry.Text = dt.Rows[0]["TRANS_COUNTRY_CODE"].ToString();
            txtsecondLagTransCountry.Text = dt.Rows[0]["SECOND_LAG_TRANS_COUNTRY_CODE"].ToString();
            txtInstCountry.Text = dt.Rows[0]["INSTRUMENT_COUNTRY_CODE"].ToString();
            ddlTransactionType.SelectedValue = dt.Rows[0]["TRANS_TYPE"].ToString();
            ddlInstType.SelectedValue = dt.Rows[0]["INSTRUMENT_TYPE"].ToString();
            ddlRiskCategory.SelectedValue = dt.Rows[0]["RISKRATING"].ToString();
            txtInstituteName.Text = dt.Rows[0]["TRANS_INSTITUTE_NAME"].ToString();
            txtInstituteRefNo.Text = dt.Rows[0]["TRANS_INSTITUTE_REFNO"].ToString();
            txtRemark.Text = dt.Rows[0]["REMARKS"].ToString();

            if (dt.Rows[0]["MOD_TYPE"].ToString() == "EXP")
            {
                rbtExport.Checked = true;
            }
            else if (dt.Rows[0]["MOD_TYPE"].ToString() == "IMP")
            {
                rbtImport.Checked = true;
            }
            else if (dt.Rows[0]["MOD_TYPE"].ToString() == "INW")
            {
                rbtInward.Checked = true;
            }
            else if (dt.Rows[0]["MOD_TYPE"].ToString() == "OTW")
            {
                rbtOutward.Checked = true;
            }
            else
                rbtOther.Checked = true;
        }


        //-------------------------- audit trail  -------------------------------//
        hdnTransRefNo.Value = dt.Rows[0]["TRANS_REF_NO"].ToString();
        hdnTransDate.Value = dt.Rows[0]["TRAN_DATE"].ToString();
        hdnRemitterID.Value = dt.Rows[0]["RemitterID"].ToString();
        hdnRemitterName.Value = dt.Rows[0]["RemitterName"].ToString();
        hdnBeneID.Value = dt.Rows[0]["BeneficiaryID"].ToString();
        hdnBeneName.Value = dt.Rows[0]["BeneName"].ToString();
        hdnPuposeCode.Value = dt.Rows[0]["PURPOSE_CODE"].ToString();
        hdnCurrency.Value = dt.Rows[0]["TRANS_CURR"].ToString();
        hdnAmtFC.Value = dt.Rows[0]["AMT_FRGN_CURR"].ToString();
        hdnState.Value = dt.Rows[0]["TRANS_STATE_CODE"].ToString();
        hdnCountry.Value = dt.Rows[0]["TRANS_COUNTRY_CODE"].ToString();
        hdnICountry.Value = dt.Rows[0]["INSTRUMENT_COUNTRY_CODE"].ToString();
        hdntransType.Value = dt.Rows[0]["TRANS_TYPE"].ToString();
        hdnInstType.Value = dt.Rows[0]["INSTRUMENT_TYPE"].ToString();
        hdnRisk.Value = dt.Rows[0]["RISKRATING"].ToString();
        //------------------------------------------------------------------------//

    }
    protected void txtSrNo_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CBWT_View_TransactionFile.aspx", true);
    }
    protected void txtTranCountry_TextChanged(object sender, EventArgs e)
    {
        lblCountryName.Text = "";

        string Countryid = txtTranCountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryName.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtTranCountry.Text = "";
            lblCountryName.Text = "";
            txtTranCountry.Focus();
        }
        txtTranCountry.Text= txtTranCountry.Text.ToUpper();
        txtTranCountry.Focus();
    }

    protected void Srno()
    {
        TF_DATA objData = new TF_DATA();
        string query = "Get_Last_SrNo";

        SqlParameter p1 = new SqlParameter("@yearMonth", txtYearMonth.Text.Trim());
        SqlParameter p2 = new SqlParameter("RefNo", txtADCode.Text.Trim());

        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtSrNo.Text = dt.Rows[0]["SRNO"].ToString();

        }
        else
        {
            txtTranCountry.Text = "";
            lblCountryName.Text = "";
            txtSrNo.Focus();
        }
        txtSrNo.Focus();

    }
    protected void txtInstCountry_TextChanged(object sender, EventArgs e)
    {
        lblcountryN.Text = "";

        string Countryid = txtInstCountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblcountryN.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtInstCountry.Text = "";
            lblcountryN.Text = "";
            txtInstCountry.Focus();
        }
        txtInstCountry.Text = txtInstCountry.Text.ToUpper();
        txtInstCountry.Focus();
    }


    protected void txtPuposeCode_TextChanged(object sender, EventArgs e)
    {
        lblPurpose.Text = "";
        string PuposeCode = txtPuposeCode.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@purposecode", SqlDbType.VarChar);
        p1.Value = PuposeCode;
        String query = "TF_GetPurposeCodeMasterDetails";
        DataTable dt = objData.getData(query, p1);

        if (dt.Rows.Count > 0)
        {
            lblPurpose.Text = dt.Rows[0]["description"].ToString().Trim();
        }
        else
        {
            txtPuposeCode.Text = "";
            lblPurpose.Text = "";
            txtPuposeCode.Focus();
        }
        txtPuposeCode.Focus();
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        lblCurrency.Text = "";
        string currency = txtCurrency.Text.Trim();
        TF_DATA objData = new TF_DATA();
        string query = "TF_GetCurrencyMasterDetails";
        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = currency;
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCurrency.Text = dt.Rows[0]["C_Description"].ToString().Trim();
        }
        else
        {
            txtCurrency.Text = "";
            lblCountryName.Text = "";
            txtCurrency.Focus();

        }
        txtCurrency.Focus();
    }
    protected void txtStateCode_TextChanged(object sender, EventArgs e)
    {
        lblState.Text = "";

        string State = txtStateCode.Text.Trim();
        TF_DATA objData = new TF_DATA();
        string query = "TF_GetStateDetails";
        SqlParameter p1 = new SqlParameter("@sid", SqlDbType.VarChar);
        p1.Value = State;
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            lblState.Text = dt.Rows[0]["StateName"].ToString().Trim();

        }
        else
        {
            txtStateCode.Text = "";
            lblState.Text = "";
            txtStateCode.Focus();
        }
        txtStateCode.Focus();
    }
    protected void txtCustACNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        string query = "TF_GetCustomerMasterDetails";
        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtCustACNo.Text.Trim();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {

            txtCustName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtRemAdd.Text = dt.Rows[0]["CustAdd"].ToString().Trim();
        }
        else
        {
            txtCustACNo.Text = "";
            txtCustName.Text = "";
            txtCustACNo.Focus();
        }

        txtCustName.Focus();
    }
    protected void txtBeneficiaryID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        string query = "TF_GetCustomerMasterDetails";
        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtBeneficiaryID.Text.Trim();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {

            txtBenName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtBenAddress.Text = dt.Rows[0]["CustAdd"].ToString().Trim();
        }
        else
        {
            txtBeneficiaryID.Text = "";
            txtBenName.Text = "";
            txtBeneficiaryID.Focus();
        }
        txtBenName.Focus();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CBWT_View_TransactionFile.aspx", true);
    }
    protected void txtADCode_TextChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();
        //string query = "CBWT_GetBranchDetails";
        //SqlParameter p1 = new SqlParameter("@Branchid", SqlDbType.VarChar);
        //p1.Value = txtADCode.Text.Trim();
        //DataTable dt = objData.getData(query, p1);
        //if (dt.Rows.Count > 0)
        //{
        //    lblBranchName.Text = dt.Rows[0]["BranchName"].ToString().Trim();
        //}

    }

    protected void txtsecondLagTransCountry_TextChanged(object sender, EventArgs e)
    {
        lblSecondLagTrsnsCountry.Text = "";

        string Countryid = txtsecondLagTransCountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblSecondLagTrsnsCountry.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtsecondLagTransCountry.Text = "";
            lblSecondLagTrsnsCountry.Text = "";
            txtsecondLagTransCountry.Focus();
        }
        txtsecondLagTransCountry.Text = txtsecondLagTransCountry.Text.ToUpper();
        txtsecondLagTransCountry.Focus();
    }
}
