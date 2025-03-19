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

public partial class EBR_EBRC_Checker_ORM_DataEntry : System.Web.UI.Page
{
    TF_DATA objData = new TF_DATA();
    public static string ORMststus_at = "";
    Encryption objEnc = new Encryption();
    bool access_flag = true;
    public static string status_at = "";
    string ipAddressW = GetIPAddress();
    string Log_Query = "TF_Audit_ApplicationLogs";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            // Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription(lbl.Value));
        }
        if (!IsPostBack)
        {
            PageAccess();
            if(access_flag==true)
            {
                txtBranchCode.Text = "01";
                fillDetails();
                Readonly();
                ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
                Page.DataBind();
            }
            


        }

        

    }

    protected void fillDetails()
    {
       // string status = Request.QueryString["status"].ToString();
       // string o_status = Request.QueryString["Ormstatus"].ToString();
       //string ormno = Request.QueryString["Ormno"].ToString();
       // string uniqueid = Request.QueryString["uniquetxid"].ToString();

        string status = objEnc.URLIDDecription(HttpContext.Current.Items["status"].ToString());
        string o_status = objEnc.URLIDDecription(HttpContext.Current.Items["Ormstatus"].ToString());
        string ormno = objEnc.URLIDDecription(HttpContext.Current.Items["Ormno"].ToString());
        string uniqueid = objEnc.URLIDDecription(HttpContext.Current.Items["uniquetxid"].ToString());


        SqlParameter p1 = new SqlParameter("@ORMNumber", ormno);
        SqlParameter p2 = new SqlParameter("@ormstatus", o_status);
        SqlParameter p3 = new SqlParameter("@status", status);
        SqlParameter p4 = new SqlParameter("@uniqutxid", uniqueid);

        

        DataTable dt = objData.getData("Tf_Checker_ORMFileUpload_FillDetails", p1, p2,p3,p4);
        if (dt.Rows.Count > 0)
        {
            txtormNo.Text = dt.Rows[0]["ORMNumber"].ToString();
            txtpaymentDate.Text = dt.Rows[0]["Paymentdate"].ToString();
            txtORMissuedt.Text = dt.Rows[0]["ORMissueDate"].ToString();
            txtbeneficiaryName.Text = dt.Rows[0]["BeneficiaryName"].ToString();
            txtbeneficiaryCountry.Text = dt.Rows[0]["Beneficiarycountry"].ToString();
            txtBankuniqueTextId.Text = dt.Rows[0]["BankUniqueTransactionId"].ToString();
            txtADCode.Text = dt.Rows[0]["OrnADCode"].ToString();
            txtornFCC.Text = dt.Rows[0]["OrnFCC"].ToString();
            txtAmount.Text = dt.Rows[0]["OrnFCCAmount"].ToString();
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString();
            txtIFSCcode.Text = dt.Rows[0]["IFSCCode"].ToString();
            txtIECcode.Text = dt.Rows[0]["IECCode"].ToString();
            txtornINRAmt.Text = dt.Rows[0]["INRpayableAmount"].ToString();
            txtPurposeCode.Text = dt.Rows[0]["PurposeofOutward"].ToString();
            txtrefIRM.Text = dt.Rows[0]["ReferenceIRM"].ToString();

            string ORMstatus = dt.Rows[0]["ORMStatus"].ToString();
            if (ORMstatus == "F" || ORMstatus == "A" || ORMstatus == "C")
            {
                ddlORMstatus.SelectedIndex = ddlORMstatus.Items.IndexOf(ddlORMstatus.Items.FindByText(dt.Rows[0]["ORMStatus"].ToString()));

            }
            else 
            {
                ddlORMstatus.SelectedIndex = 0;
            
            }

            ORMststus_at = dt.Rows[0]["ORMStatus"].ToString();

            string status1 = dt.Rows[0]["Status"].ToString();

            if (status1 == "Approve" || status1 == "Reject")
            {
                ddlApproveReject.SelectedIndex = ddlApproveReject.Items.IndexOf(ddlApproveReject.Items.FindByText(dt.Rows[0]["Status"].ToString()));
            }
            else 
            {
                ddlApproveReject.SelectedIndex = 0;
            }
            status_at = dt.Rows[0]["Status"].ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
       // Response.Redirect("TF_EBRC_ORM_Checker.aspx", true);
        Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "HQFvGmmYi1k3fwo4blywjKhVmtCOW4diCYF177Cn98", true);

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string UserName = Session["userName"].ToString();
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "Approve";
        }
        else if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "Reject";

        }
        if (AR == "Approve")
        {
            string Approve_Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            SqlParameter ORMNumber = new SqlParameter("@ORMnumber",txtormNo.Text);
            SqlParameter o_status = new SqlParameter("@ormstatus", ddlORMstatus.SelectedItem.Text.Trim());
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_statusDate = new SqlParameter("@ApproveDate", Approve_Date);
            SqlParameter P_User = new SqlParameter("@Approveduser", Session["userName"].ToString());

            string Result = obj.SaveDeleteData("TF_EBRC_ORMFileUpload_ChekerApprove", ORMNumber, P_Status, P_statusDate, P_User,o_status);

            //-----------------------------------------------------------------------------------------------------------------//
            string result_;
            SqlParameter J0 = new SqlParameter("@ORMissueDate",txtORMissuedt.Text.Trim());
            SqlParameter J1 = new SqlParameter("@ORMNumber", txtormNo.Text.Trim());
            SqlParameter J2 = new SqlParameter("@IFSCCode", txtIFSCcode.Text.Trim());
            SqlParameter J3 = new SqlParameter("@OrnADCode", txtADCode.Text.Trim());
            SqlParameter J4 = new SqlParameter("@Paymentdate", txtpaymentDate.Text.Trim());
            SqlParameter J5 = new SqlParameter("@OrnFCC",txtornFCC.Text.Trim());
            SqlParameter J6 = new SqlParameter("@OrnFCCAmount", txtAmount.Text.Trim());
            SqlParameter J7 = new SqlParameter("@INRAmount",txtornINRAmt.Text.Trim());
            SqlParameter J8 = new SqlParameter("@IECCode", txtIECcode.Text.Trim());
            SqlParameter J9 = new SqlParameter("@PanNumber", txtPanNumber.Text.Trim());
            SqlParameter J10 = new SqlParameter("@beneficiaryName", txtbeneficiaryName.Text.Trim());
            SqlParameter J11 = new SqlParameter("@beneficiarycountry", txtbeneficiaryCountry.Text.Trim());
            SqlParameter J12 = new SqlParameter("@Purposeofoutward", txtPurposeCode.Text.Trim());
            SqlParameter J13 = new SqlParameter("@ormstatus",ddlORMstatus.SelectedItem.Text.Trim());
            SqlParameter J14 = new SqlParameter("@refirm", txtrefIRM.Text.Trim());
           SqlParameter J15 = new SqlParameter("@ADD_USER", Session["userName"].ToString());
           SqlParameter J16 = new SqlParameter("@ADD_DATE", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter J17 = new SqlParameter("@status", AR);
            result_ = objData.SaveDeleteData("TF_EBRC_ORM_Fileupload_JsonCreationInsert", J0 ,J1 ,J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J12 , J13, J14, J15,J16 ,J17);
            getjson(result_);



            //if (result_ == "added")
            //{
            //     string _script = "window.location='TF_EBRC_ORM_Checker.aspx'";
            //     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('This Transaction is Approved.')", true);
            //     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " alert('This Transaction is Approved');", _script, true);

            //}

        }
        else
        {
           string Rejected_Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            SqlParameter ORMNumber = new SqlParameter("@ORMnumber", txtormNo.Text);
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_Rejectionreason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter P_statusDate = new SqlParameter("@RejectedDate", Rejected_Date);
            SqlParameter P_User = new SqlParameter("@Rejecteduser", Session["userName"].ToString());

            string Result = obj.SaveDeleteData("TF_EBRC_ORMFileUpload_ChekerReject", ORMNumber, P_Status, P_Rejectionreason, P_statusDate, P_User);

            if (Result == "Updated")
            {
                SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
                p1.Value = Session["userName"].ToString().Trim();
                SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
                p2.Value = ipAddressW;
                SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                p3.Value = System.DateTime.Now;
                SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
                p4.Value = "EBRC ORM Data Entry View - Checker";
                SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
                p5.Value = "Rejected By Checker:ORMno: " + txtormNo.Text.Trim();
                string store_logs = obj.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);
                string _script ;
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('This Transaction has been sent to Maker.')", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " alert('This Transaction has been sent to Maker.');", _script, true);
                //_script = "window.location='TF_EBRC_ORM_Checker.aspx?result=" + Result + "'";
                _script = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "HQFvGmmYi1k3fwo4blywjKnlGP3hNVH6zb5erGbZEKM/" + objEnc.URLIDEncription(Result) + @"'";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);

            }


        }



    }
    protected void Readonly()
    {
        txtormNo.Enabled = false;
        txtORMissuedt.Enabled = false;
        txtornFCC.Enabled = false;
        txtornINRAmt.Enabled = false;
        txtPanNumber.Enabled = false;
        txtpaymentDate.Enabled = false;
        txtPurposeCode.Enabled = false;
        txtrefIRM.Enabled = false;
        txtIFSCcode.Enabled = false;
        txtIECcode.Enabled = false;
        txtBankuniqueTextId.Enabled = false;
        txtbeneficiaryCountry.Enabled = false;
        txtbeneficiaryName.Enabled = false;
        ddlORMstatus.Enabled = false;
        txtADCode.Enabled = false;
        txtAmount.Enabled = false;

        if (ddlApproveReject.SelectedItem.Text == "Approve")
        {
            ddlApproveReject.Enabled = false;
            lblmessage.Visible = true;
          //lblmessage.Text = "Json File Created for this ORM Number";
        }

    }

    private void getjson(string result_)
    {
        JsonCreation();
    }

    protected void JsonCreation()
    {
      
        OrmCreation OrmCreation = new OrmCreation();
        ormDataLst ormObj = new ormDataLst();

        SqlParameter p1 = new SqlParameter("@ormNumber", txtormNo.Text);
        SqlParameter p2 = new SqlParameter("@ormstatus", ddlORMstatus.SelectedItem.Text);
        DataTable dt_ = objData.getData("TF_EBRC_ORM_Fileupload_getuniqueTxId", p1, p2);
        string uniqueid = "";
        if (dt_.Rows.Count > 0)
        {
           //OrmCreation.uniqueTxId = dt_.Rows[0]["BankUniqueTransactionId"].ToString().Trim();
            OrmCreation.uniqueTxId = dt_.Rows[0]["BankUniqueTransactionId"].ToString().Trim();
            uniqueid = dt_.Rows[0]["BankUniqueTransactionId"].ToString().Trim();
          
        }

        SqlParameter p01_ = new SqlParameter("@ormno", txtormNo.Text);
        SqlParameter p02_ = new SqlParameter("@ormstatus", ddlORMstatus.SelectedItem.Text);
        SqlParameter p03_ = new SqlParameter("@uniquetxid", uniqueid);
        string result__ = objData.SaveDeleteData("TF_EBRC_ORM_MAINTabUpdate", p01_, p02_, p03_);

        DataTable dt = objData.getData("TF_EBRC_ORM_JSONFileCreation", p1);
        List<ormDataLst> ormList1 = new List<ormDataLst>();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string issudate = dt.Rows[0]["ORMissueDate"].ToString().Trim();
                ormObj.ormIssueDate = issudate.Replace("/", "").Replace("/","");
                ormObj.ormNumber = txtormNo.Text;
                ormObj.ormStatus = dt.Rows[0]["ORMStatus"].ToString().Trim();
                ormObj.ifscCode = dt.Rows[0]["IFSCCode"].ToString().Trim();
                ormObj.ornAdCode = dt.Rows[0]["OrnADCode"].ToString().Trim();
                string paymentdate = dt.Rows[0]["Paymentdate"].ToString().Trim();
                ormObj.paymentDate = paymentdate.Replace("/", "").Replace("/","");
                ormObj.ornFCC = dt.Rows[0]["OrnFCC"].ToString().Trim();

                string ornFCAmt = dt.Rows[0]["OrnFCCAmount"].ToString().Trim();
                ormObj.ornFCAmount = Math.Round(System.Convert.ToDecimal(dt.Rows[0]["OrnFCCAmount"].ToString().Trim()), 2);

                string inrPayamt = dt.Rows[0]["INRpayableAmount"].ToString().Trim();
                ormObj.ornINRAmount = Math.Round(System.Convert.ToDecimal(dt.Rows[0]["INRpayableAmount"].ToString().Trim()), 2);

                ormObj.iecCode = dt.Rows[0]["IECCode"].ToString();
                ormObj.panNumber = dt.Rows[0]["PanNumber"].ToString();
                ormObj.beneficiaryName = dt.Rows[0]["BeneficiaryName"].ToString();
                ormObj.beneficiaryCountry = dt.Rows[0]["Beneficiarycountry"].ToString();
                ormObj.purposeOfOutward = dt.Rows[0]["PurposeofOutward"].ToString();
                ormObj.referenceIRM = dt.Rows[0]["ReferenceIRM"].ToString();

                ormList1.Add(ormObj);
            }
        }


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
        if (ormStatus_ == "F")
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

   
        SqlParameter j1 = new SqlParameter("@uniquetxid", uniqueid);
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
        Base64EncodedJsonFile(_filePath, uniqueid,ormStatus_);
    }

    protected void Base64EncodedJsonFile(string _filePath, string uniquetxid, string ormStatus_)
    {
        string inputFile = _filePath;

        byte[] bytes = File.ReadAllBytes(inputFile);
        string file = Convert.ToBase64String(bytes);
        string id = uniquetxid;
        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string FileName = "";

        string outputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Base/") + todaydt;

        if (!Directory.Exists(outputFile))
        {
            Directory.CreateDirectory(outputFile);
        }

       // string FileName = "ORM_Json_Base64Encrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
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

        EncryptAndEncode(file, _path,id,ormStatus_);


     //   AESEncryptJsonFile(_path);

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
    public void EncryptAndEncode(string file, string _path, string uniquetxid, string ormStatus_)//--- encoded64data from database
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
                string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
                string FileName = "";

                string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Eecrypted/") + todaydt;

                if (!Directory.Exists(AESoutputFile))
                {
                    Directory.CreateDirectory(AESoutputFile);
                }
               // string FileName = "ORM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
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
            }
            SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
            p1.Value = Session["userName"].ToString().Trim();
            SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
            p2.Value = ipAddressW;
            SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
            p3.Value = System.DateTime.Now;
            SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
            p4.Value = "EBRC ORM Data Entry View - Checker";
            SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
            p5.Value = "Approved By Checker:uniquetxid: " + uniquetxid;
            string store_logs = objData.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);
            string result1 = "Approved";
            //Response.Redirect("~/EBR/TF_EBRC_ORM_Checker.aspx?result_=" + result1 + "'", true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "HQFvGmmYi1k3fwo4blywjPqOtlmeYWYPyd9COePz6U/" + result1, true);
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

    //protected void AESEncryptJsonFile(string _path)
    //{
    //    string inputFile = _path;

    //    string todaydt = System.DateTime.Now.ToString("ddMMyyyy");

    //    string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/ORM/Json_Eecrypted/") + todaydt;

    //    if (!Directory.Exists(AESoutputFile))
    //    {
    //        Directory.CreateDirectory(AESoutputFile);
    //    }
    //    string FileName = "ORM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");

    //    string _AESpath = AESoutputFile + "/" + FileName + ".json";

    //    byte[] key = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
    //    string cryptFile = _AESpath;

    //    using (FileStream fileCrypt = new FileStream(cryptFile, FileMode.Create))
    //    {
    //        using (AesManaged encrypt = new AesManaged())
    //        {
    //            using (CryptoStream cs = new CryptoStream(fileCrypt, encrypt.CreateEncryptor(key, key), CryptoStreamMode.Write))
    //            {
    //                using (FileStream fileInput = new FileStream(inputFile, FileMode.Open))
    //                {
    //                    encrypt.KeySize = 256;
    //                    encrypt.BlockSize = 128;
    //                    int data;
    //                    while ((data = fileInput.ReadByte()) != -1)
    //                    {
    //                        cs.WriteByte((byte)data);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    Readonly();
    //}

    protected void txtBankuniqueTextId_TextChanged(object sender, EventArgs e)
    {
        fillDetails();
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
            access_flag = false;
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