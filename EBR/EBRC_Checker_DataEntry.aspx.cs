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

public partial class EBR_EBRC_Checker_DataEntry : System.Web.UI.Page
{
    TF_DATA objData = new TF_DATA();
    public static string IRMststus_at = "";
    Encryption objEnc = new Encryption();
    bool access_flag = true;
    string ipAddressW = GetIPAddress();
    string Log_Query = "TF_Audit_ApplicationLogs";
    public static string status_at = "";
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
        //string irmno = Request.QueryString["irmno"].ToString();
        //string irmstatus_ = Request.QueryString["irmstatus"].ToString();
        //string status_ = Request.QueryString["status"].ToString();
        //string uniquetxid_ = Request.QueryString["uniquetxid"].ToString();

        string irmno = objEnc.URLIDDecription(HttpContext.Current.Items["irmno"].ToString());
        string irmstatus_ = objEnc.URLIDDecription(HttpContext.Current.Items["irmstatus"].ToString());
        string status_ = objEnc.URLIDDecription(HttpContext.Current.Items["status"].ToString());
        string uniquetxid_ = objEnc.URLIDDecription(HttpContext.Current.Items["uniquetxid"].ToString());

         SqlParameter p1 = new SqlParameter("@IRMNumber", irmno);
       // SqlParameter p2 = new SqlParameter("@mode", "edit");
        SqlParameter p2 = new SqlParameter("@irmstatus", irmstatus_);
        SqlParameter p3 = new SqlParameter("@status", status_);
        SqlParameter p4 = new SqlParameter("@uniqutxid", uniquetxid_);
      //  txtDocNo.Text = Request.QueryString["irmno"].ToString();
        txtDocNo.Text = objEnc.URLIDDecription(HttpContext.Current.Items["irmno"].ToString());
        DataTable dt = objData.getData("Tf_Checker_IRMFileUpload_FillDetails", p1,p2,p3,p4);
        if (dt.Rows.Count > 0)
        {
            
            txtDocDate.Text = dt.Rows[0]["Remittancedate"].ToString();
            txtBankuniqueTextId.Text = dt.Rows[0]["BankUniqueTransactionId"].ToString();
            txtirmIssueDate.Text = dt.Rows[0]["IRMissueDate"].ToString().Trim();
            txtremittanceFCC.Text = dt.Rows[0]["RemittanceFCC"].ToString().Trim();
            txtinrCreditAmount.Text = dt.Rows[0]["INRCreditAmount"].ToString().Trim();
            txtPanNumber.Text = dt.Rows[0]["PanNumber"].ToString().Trim();
            txtPurposeCode.Text = dt.Rows[0]["PurposeofRemittance"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["RemittanceFCCAmount"].ToString().Trim();
            txtRemitterName.Text = dt.Rows[0]["RemitterName"].ToString().Trim();
            txtRemitterCountry.Text = dt.Rows[0]["Remittercountry"].ToString().Trim();
            txtIECcode.Text = dt.Rows[0]["IECCode"].ToString().Trim();
            txtIFSCcode.Text = dt.Rows[0]["IFSCCode"].ToString().Trim();
            txtRemittanceADCode.Text = dt.Rows[0]["RemittanceADCode"].ToString().Trim();

            txtbanaccnumber.Text = dt.Rows[0]["BankAccountNumber"].ToString().Trim();
            txtbanrefnumber.Text = dt.Rows[0]["BankReferencenumber"].ToString().Trim();
            string IRMstatus = dt.Rows[0]["IRMStatus"].ToString().Trim();
            if (IRMstatus == "F" || IRMstatus == "A" || IRMstatus == "C")
            {

                ddlIRMstatus.SelectedIndex = ddlIRMstatus.Items.IndexOf(ddlIRMstatus.Items.FindByText(dt.Rows[0]["IRMStatus"].ToString().Trim()));
            }
            else
            {
                ddlIRMstatus.SelectedIndex = 0;
            }
            IRMststus_at = dt.Rows[0]["IRMStatus"].ToString().Trim();

            string Status = dt.Rows[0]["Status"].ToString().Trim();

            if (Status == "Approve" || Status == "Reject")
            {

                ddlApproveReject.SelectedIndex = ddlApproveReject.Items.IndexOf(ddlApproveReject.Items.FindByText(dt.Rows[0]["Status"].ToString().Trim()));

            }
            else
            {
                ddlApproveReject.SelectedIndex = 0;

            }
            status_at = dt.Rows[0]["Status"].ToString().Trim();


        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
      //Response.Redirect("TF_EBRC_Checker.aspx", true);
        Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "gHtyj4Jw9M1DZHRCAepArtcJHvnHP4BXBKA5T0nA", true);

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
            string Approved_Date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            SqlParameter IRMNumber = new SqlParameter("@IRMNumber", txtDocNo.Text);
            SqlParameter irm_status = new SqlParameter("@irmstatus",ddlIRMstatus.SelectedItem.Text.ToString().Trim());
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_StatusDate = new SqlParameter("@ApprovedsDate", Approved_Date);
            SqlParameter P_User = new SqlParameter("@ApproveUser", Session["userName"].ToString());
            string result_;
            string Result = obj.SaveDeleteData("TF_EBRC_IRMFileUpload_ChekerApprove", IRMNumber, P_Status, P_StatusDate,P_User,irm_status);
           
            //-----------------------------------------------------------------------------------------------------------------//

            SqlParameter J2 = new SqlParameter("@BankReferencenumber", txtbanrefnumber.Text.Trim());
            SqlParameter J3 = new SqlParameter("@IRMissueDate", txtirmIssueDate.Text.Trim());
            SqlParameter J4 = new SqlParameter("@IRMNumber", txtDocNo.Text.Trim());
            SqlParameter J5 = new SqlParameter("@irmstatus", ddlIRMstatus.SelectedItem.Text.ToString().Trim());
            SqlParameter J6 = new SqlParameter("@IFSCCode",  txtIFSCcode.Text.Trim());
            SqlParameter J7 = new SqlParameter("@RemittanceADCode", txtRemittanceADCode.Text.Trim());
            SqlParameter J8 = new SqlParameter("@Remittancedate", txtDocDate.Text.Trim());
            SqlParameter J9 = new SqlParameter("@RemittanceFCC",txtremittanceFCC.Text.Trim());
            SqlParameter J10 = new SqlParameter("@RemittanceFCCAmount", txtAmount.Text.Trim());
            SqlParameter J11 = new SqlParameter("@INRCreditAmount", txtinrCreditAmount.Text.Trim());
          // SqlParameter J12 = new SqlParameter("@exchangerate", txt_exchagerate.Text.Trim());
            SqlParameter J13 = new SqlParameter("@IECCode", txtIECcode. Text.Trim());
            SqlParameter J14 = new SqlParameter("@PanNumber",txtPanNumber.Text.Trim());
            SqlParameter J15 = new SqlParameter("@RemitterName", txtRemitterName.Text.Trim());
            SqlParameter J16 = new SqlParameter("@Remittercountry", txtRemitterCountry.Text.Trim());
            SqlParameter J17 = new SqlParameter("@PurposeofRemittance", txtPurposeCode.Text.Trim());
          //SqlParameter J18 = new SqlParameter("@modeofpayment", txtmodeofpayment.Text.Trim());
            SqlParameter J19 = new SqlParameter("@Bankaccno",txtbanaccnumber.Text.Trim());
            SqlParameter J20 = new SqlParameter("@ADD_USER", Session["userName"].ToString());
            SqlParameter J21 = new SqlParameter("@ADD_DATE", DateTime.Now.Date.ToString("dd/MM/yyyy"));
            SqlParameter J22 = new SqlParameter("@status",AR);
            result_ = objData.SaveDeleteData("TF_EBRC_IRM_Fileupload_JsonCreationInsert", J2, J3, J4, J5, J6, J7, J8, J9, J10, J11, J13, J14, J15, J16, J17, J19, J20, J21, J22);
            getjson(result_);
            
           

            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "showalert", "alert('This transaction is move to DGFT server');", true);
            //Response.Write("<script>alert('This transaction is move to DGFT server');</script>");
            // Response.Redirect("TF_EBRC_Checker.aspx");
            //if (result_ == "added")
            //{
            //    string message = "This Transaction is Approved";
            //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('This Transaction is Approved'); window.open('TF_EBRC_Checker.aspx');", true);
            //    string _script = "window.location='TF_EBRC_Checker.aspx?result=" + message + "'";
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            //} 
            //if (result_ == "added")
            //{
            //    string _script = "window.location='TF_EBRC_Checker.aspx'";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "alert('This Transaction is Approved.')", true);
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), " alert('This Transaction is Approved');", _script, true);

            //}

        }
        

        else
        {
            string Rejected_date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            SqlParameter IRMNumber = new SqlParameter("@IRMNumber", txtDocNo.Text);
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_Rejectionreason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter P_statusDate = new SqlParameter("@RejectDate", Rejected_date);
            SqlParameter P_User = new SqlParameter("@Rejecteduser", Session["userName"].ToString());

            string Result = obj.SaveDeleteData("TF_EBRC_IRMFileUpload_ChekerReject", IRMNumber, P_Status, P_Rejectionreason, P_statusDate,P_User);

            //Response.Write("<script>alert('This transaction is move to Maker');</script>");
           //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This transaction is move to Maker');", true);

            //if (Result == "Updated")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Data has been send to Maker'); window.open('TF_EBRC_Checker.aspx');", true);

            //}
            if (Result == "Updated")
            {
                SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
                p1.Value = Session["userName"].ToString().Trim();
                SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
                p2.Value = ipAddressW;
                SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
                p3.Value = System.DateTime.Now;
                SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
                p4.Value = "EBRC IRM Data Entry View - Checker";
                SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
                p5.Value = "Rejected By Checker:IRMno: " + txtDocNo.Text.Trim();
                string store_logs = obj.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);

                string _script;
               // _script = "window.location='TF_EBRC_Checker.aspx?result=" + Result + "'";
                _script = "window.location='" + ConfigurationManager.AppSettings["webpath"] + "gFI0hJGmTJcQ6D5G8DQhTgsHY7XfiFgmLknUdnWjw/" + objEnc.URLIDEncription(Result) + @"'";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
             


            }
       
           

        }


   
    }
    protected void Readonly()
    {
        txtDocNo.Enabled = false;
        txtDocDate.Enabled = false;
        txtPurposeCode.Enabled = false;
        txtRemitterName.Enabled = false;
        txtRemitterCountry.Enabled = false;
        txtAmount.Enabled = false;
        txtBankuniqueTextId.Enabled = false;
        txtirmIssueDate.Enabled = false;
        txtremittanceFCC.Enabled = false;
        txtinrCreditAmount.Enabled = false;
        txtPanNumber.Enabled = false;
        txtRemittanceADCode.Enabled = false;
        txtIFSCcode.Enabled = false;
        txtIECcode.Enabled = false;
        txtbanrefnumber.Enabled = false;
        txtbanaccnumber.Enabled = false;
        ddlIRMstatus.Enabled = false;

        if (ddlApproveReject.SelectedItem.Text == "Approve")
        {
            ddlApproveReject.Enabled = false;
            //lblmessage.Visible = true;
            //lblmessage.Text = "Json File Created for this IRM Number";
        }

    }

    private void getjson(string result_)
    {
        JsonCreation();
    }

    protected void JsonCreation()
    {
        IrmCreation IrmCreation = new IrmCreation();
        irmList irmObj = new irmList();
        Root root = new Root();
        SqlParameter p1 = new SqlParameter("@IRMNumber", txtDocNo.Text);
        SqlParameter p2 = new SqlParameter("@irmstatus", ddlIRMstatus.SelectedItem.Text);
        DataTable dt_ = objData.getData("TF_EBRC_IRM_Fileupload_getuniqueTxId",p1,p2);
        string uniqueid = "";

        if (dt_.Rows.Count > 0)
        {
            IrmCreation.uniqueTxId = dt_.Rows[0]["BankUniqueTransactionId"].ToString().Trim();
            uniqueid = dt_.Rows[0]["BankUniqueTransactionId"].ToString().Trim();
        }
        SqlParameter p01_ = new SqlParameter("@irmno", txtDocNo.Text);
        SqlParameter p02_ = new SqlParameter("@irmstatus", ddlIRMstatus.SelectedItem.Text);
        SqlParameter p03_ = new SqlParameter("@uniquetxid", uniqueid);
        string result__ = objData.SaveDeleteData("TF_EBRC_IRM_MAINTabUpdate", p01_, p02_, p03_);

        DataTable dt = objData.getData("TF_EBRC_IRM_JSONFileCreation", p1,p02_);
        List<irmList> irmList1 = new List<irmList>();
      
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
              
                string issuedate = dt.Rows[0]["IRMissueDate"].ToString().Trim();
                irmObj.irmIssueDate = issuedate.Replace("/", "").Replace("/", "");
                irmObj.irmNumber = txtDocNo.Text;
                irmObj.irmStatus = dt.Rows[0]["IRMStatus"].ToString().Trim();
                irmObj.bankRefNumber = dt.Rows[0]["BankReferencenumber"].ToString().Trim();
                irmObj.ifscCode = dt.Rows[0]["IFSCCode"].ToString().Trim();
                irmObj.remittanceAdCode = dt.Rows[0]["RemittanceADCode"].ToString().Trim();
                string remdate = dt.Rows[0]["Remittancedate"].ToString().Trim();
                irmObj.remittanceDate = remdate.Replace("/", "").Replace("/", "");
                irmObj.remittanceFCC = dt.Rows[0]["RemittanceFCC"].ToString().Trim();
                irmObj.remittanceFCAmount = Math.Round(System.Convert.ToDecimal(dt.Rows[0]["RemittanceFCCAmount"].ToString().Trim()), 2);
                irmObj.inrCreditAmount = Math.Round(System.Convert.ToDecimal(dt.Rows[0]["INRCreditAmount"].ToString().Trim()), 2);
                irmObj.iecCode = dt.Rows[0]["IECCode"].ToString().Trim();
                irmObj.panNumber = dt.Rows[0]["PanNumber"].ToString().Trim();
                irmObj.remitterName = dt.Rows[0]["RemitterName"].ToString().Trim();
                irmObj.remitterCountry = dt.Rows[0]["Remittercountry"].ToString().Trim();
                irmObj.purposeOfRemittance = dt.Rows[0]["PurposeofRemittance"].ToString().Trim();
                irmObj.bankAccountNo = dt.Rows[0]["BankAccountNumber"].ToString().Trim();

                irmList1.Add(irmObj);
            }
        }

        IrmCreation.irmList = irmList1;

        var json = JsonConvert.SerializeObject(IrmCreation,
        new JsonSerializerSettings()
       {
    NullValueHandling = NullValueHandling.Ignore,
    Formatting = Formatting.Indented,
    Converters = { new Newtonsoft.Json.Converters.StringEnumConverter() }
        });

        string JSONresult = JsonConvert.SerializeObject(IrmCreation);

        JObject jsonFormat = JObject.Parse(JSONresult);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string FileName = "";

        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Decrypted/") + todaydt;
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

     //   string FileName = "IRM_Json_Decrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        string irmStatus_ = dt.Rows[0]["IRMStatus"].ToString();
        if (irmStatus_ == "F")
        {
            FileName = "IRM_Json_Decrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "C")
        {
            FileName = "IRM_Json_Decrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "A")
        {
            FileName = "IRM_Json_Decrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        } 

        string _filePath = _directoryPath + "/" + FileName + ".json";

        SqlParameter j1 = new SqlParameter("@uniquetxid", uniqueid);
        SqlParameter j2 = new SqlParameter("@actualdata", json);
        SqlParameter j3 = new SqlParameter("@encodeddata", "");
        SqlParameter j4 = new SqlParameter("@encrypteddata", "");
        SqlParameter j5 = new SqlParameter("@operation", "Insert");
        SqlParameter j6 = new SqlParameter("@mode", "IRM");
        string query = "TF_EBRC_IRMORM_JSONOutput";
        string res = objData.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

        using (var tw = new StreamWriter(_filePath, true))
        {
            tw.WriteLine(json);
            tw.Close();
        }
        Base64EncodedJsonFile(_filePath,uniqueid,irmStatus_);
    }

    protected void Base64EncodedJsonFile(string _filePath, string uniquetxid, string irmStatus_)
    {
        string inputFile = _filePath;
        string id = uniquetxid;
        byte[] bytes = File.ReadAllBytes(inputFile);
        string file = Convert.ToBase64String(bytes);

        string todaydt = System.DateTime.Now.ToString("ddMMyyyy");
        string FileName = "";

        string outputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Base/") + todaydt;

        if (!Directory.Exists(outputFile))
        {
            Directory.CreateDirectory(outputFile);
        }

      //  string FileName = "IRM_Json_Base64Encrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        if (irmStatus_ == "F")
        {
            FileName = "IRM_Json_Base64Encrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "C")
        {
            FileName = "IRM_Json_Base64Encrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }
        if (irmStatus_ == "A")
        {
            FileName = "IRM_Json_Base64Encrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
        }


        string _path = outputFile + "/" + FileName + ".json";

        File.WriteAllText(_path, file);

        SqlParameter j1 = new SqlParameter("@uniquetxid", id);
        SqlParameter j2 = new SqlParameter("@actualdata", "");
        SqlParameter j3 = new SqlParameter("@encodeddata", file);
        SqlParameter j4 = new SqlParameter("@encrypteddata", "");
        SqlParameter j5 = new SqlParameter("@operation", "Update1");
        SqlParameter j6 = new SqlParameter("@mode", "IRM");
        string query = "TF_EBRC_IRMORM_JSONOutput";
        string res = objData.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);

       
        EncryptAndEncode(file, _path, id ,irmStatus_);

      //AESEncryptJsonFile(_path);
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
    public void EncryptAndEncode(string file, string _path, string uniquetxid, string irmStatus_)//--- encoded64data from database
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

                string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Eecrypted/") + todaydt;

                if (!Directory.Exists(AESoutputFile))
                {
                    Directory.CreateDirectory(AESoutputFile);
                }
             //   string FileName = "IRM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                if (irmStatus_ == "F")
                {
                    FileName = "IRM_Json_AESEncrypted_F" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }
                if (irmStatus_ == "C")
                {
                    FileName = "IRM_Json_AESEncrypted_C" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }
                if (irmStatus_ == "A")
                {
                    FileName = "IRM_Json_AESEncrypted_A" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");
                }

                string _AESpath = AESoutputFile + "/" + FileName + ".json";

                File.WriteAllText(_AESpath, base64String);

                //SqlParameter j1 = new SqlParameter("@JSON_FileOutput", base64String);
                //SqlParameter j2 = new SqlParameter("@Date_Time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //SqlParameter j3 = new SqlParameter("@JSON_Filename", FileName);
                //string query = "TF_EBRC_IRM_jsonOutputAESEncrypted";
                //string res = objData.SaveDeleteData(query, j1, j2, j3);

                SqlParameter j1 = new SqlParameter("@uniquetxid", id);
                SqlParameter j2 = new SqlParameter("@actualdata", "");
                SqlParameter j3 = new SqlParameter("@encodeddata", "");
                SqlParameter j4 = new SqlParameter("@encrypteddata", base64String);
                SqlParameter j5 = new SqlParameter("@operation", "Update2");
                SqlParameter j6 = new SqlParameter("@mode", "IRM");
                string query = "TF_EBRC_IRMORM_JSONOutput";
                string res = objData.SaveDeleteData(query, j1, j2, j3, j4, j5, j6);
            }
            //readonlytext();
           
            SqlParameter p1 = new SqlParameter("@userID", SqlDbType.VarChar);
            p1.Value = Session["userName"].ToString().Trim();
            SqlParameter p2 = new SqlParameter("@IP", SqlDbType.VarChar);
            p2.Value = ipAddressW;
            SqlParameter p3 = new SqlParameter("@timestamp", System.Data.SqlDbType.DateTime);
            p3.Value = System.DateTime.Now;
            SqlParameter p4 = new SqlParameter("@type", SqlDbType.VarChar);
            p4.Value = "EBRC IRM Data Entry View - Checker";
            SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
            p5.Value = "Approved By Checker:uniquetxid: " + uniquetxid;
            string store_logs = objData.SaveDeleteData(Log_Query, p1, p2, p3, p4, p5);
            string result = "Approved";

          //  Response.Redirect("~/EBR/TF_EBRC_Checker.aspx?result_=" + result + "'", true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "gFI0hJGmTJcQ6D5G8DQhRUkLxp3kEPCrWZHqxFemmA/" + result, true);
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

    //    string AESoutputFile = Server.MapPath("~/TF_GeneratedFiles/EBRC/IRM/Json_Eecrypted/") + todaydt;

    //    if (!Directory.Exists(AESoutputFile))
    //    {
    //        Directory.CreateDirectory(AESoutputFile);
    //    }
    //    string FileName = "IRM_Json_AESEncrypted" + System.DateTime.Now.ToString("_ddMMyyyy_HHmmss");

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
        menuName.Value = "EBRC IRM Data Entry View - Checker";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "EBRC IRM Data Entry View - Checker")
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