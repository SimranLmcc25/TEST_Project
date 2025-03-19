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
using System.Text;
using System.Configuration;

public partial class CBWT_Account_Master_Upload : System.Web.UI.Page
{
    int cntrec = 0;
    int norecinexcel = 0;
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

            }
            btnupload.Attributes.Add("onclick", "return validateSave();");

        }
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        string path = Server.MapPath("~/GeneratedFiles/CBWT/Uploaded_Files");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string csvPath = path + "\\" + System.IO.Path.GetFileName(FileAccountMaster.PostedFile.FileName);
        Upload_Data_From_CSV_Account_Master(csvPath);


        string IndCSVPath = path + "\\" + System.IO.Path.GetFileName(FileIndividualMaster.PostedFile.FileName);
        Upload_Data_From_CSV_Individual_Master(IndCSVPath);

        string LegCSVPath = path + "\\" + System.IO.Path.GetFileName(FileLegalPerson.PostedFile.FileName);
        Upload_Data_From_CSV_Legal_Master(LegCSVPath);

    }

    private string Upload_Data_From_CSV_Account_Master(string _filepath)
    {
        string path = Server.MapPath("~/GeneratedFiles/CBWT/Uploaded_Files");

        norecinexcel = 0;
        string uploadresult = "";

        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        //  string _DateRecvd = System.DateTime.Now.ToString("dd/MM/yyyy");
        string result = "";
        string _AdCode = "";
        string _AcNo = "";
        string _Name = "";
        string _AcType = "";
        string _AcHolderType = "";
        string _AcOpeningDate = "";
        string _RiskCategory = "";

        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileposted";
        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                    //System.IO.File.Delete(_filepath);
                }
                catch  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    Thread.Sleep(10000); //Wait for object to be killed. 
                    File.Delete(_filepath); //File can be now deleted
                }
            }
        }
        catch { }

        try
        {
            FileAccountMaster.PostedFile.SaveAs(_filepath);
            uploadresult = "fileposted";
        }

        catch
        {
            uploadresult = "ioerror";
        }

        if (uploadresult == "fileposted")
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
                    _rowData = " Not uploaded. [ Due to the presence of comma (,) in the row ]";
                    list.InnerHtml = list.InnerHtml + "<br>" + "<font color='red'>" + _rowData + "</font>";
                }
                norecinexcel = norecinexcel + 1;
            }

            SqlParameter pAdCode = new SqlParameter("@AdCode", SqlDbType.VarChar);
            SqlParameter pAcNo = new SqlParameter("@AcNo", SqlDbType.VarChar);
            SqlParameter pName = new SqlParameter("@Name", SqlDbType.VarChar);
            SqlParameter pAcType = new SqlParameter("@AcType", SqlDbType.VarChar);
            SqlParameter pAcHolderType = new SqlParameter("@AcHolderType", SqlDbType.VarChar);
            SqlParameter pAcOpeningDate = new SqlParameter("@AcOpeningDate", SqlDbType.VarChar);
            SqlParameter pRiskCategory = new SqlParameter("@RiskCategory", SqlDbType.VarChar);

            SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
            pUser.Value = _userName;
            SqlParameter pUploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
            pUploadingDate.Value = _uploadingDate;

            string _query = "CBWT_Account_Master_File_Upload";

            try
            {
                try
                {
                    if (dt.Columns.Count == 7)
                    {
                        int AccUploadCount = 0;
                        int TotalAccUploadCount = 0;
                        // list.InnerHtml = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString().Trim() != "")
                            {
                                _AdCode = dt.Rows[i][0].ToString();
                                _AcNo = dt.Rows[i][1].ToString();
                                _Name = dt.Rows[i][2].ToString();
                                _AcType = dt.Rows[i][3].ToString();
                                _AcHolderType = dt.Rows[i][4].ToString();
                                _AcOpeningDate = dt.Rows[i][5].ToString();
                                _RiskCategory = dt.Rows[i][6].ToString();

                                pAdCode.Value = _AdCode;
                                pAcNo.Value = _AcNo;
                                pName.Value = _Name;
                                pAcType.Value = _AcType;
                                pAcHolderType.Value = _AcHolderType;
                                pAcOpeningDate.Value = _AcOpeningDate;
                                pRiskCategory.Value = _RiskCategory;


                                TF_DATA objSave = new TF_DATA();
                                uploadresult = objSave.SaveDeleteData(_query, pAdCode, pAcNo, pName, pAcType, pAcHolderType, pAcOpeningDate, pRiskCategory, pUser, pUploadingDate);

                                TotalAccUploadCount = TotalAccUploadCount + 1;

                                if (uploadresult == "Uploaded")
                                {
                                    AccUploadCount = AccUploadCount + 1;
                                }
                            }
                        }
                        if (uploadresult == "Uploaded")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Data Uploaded.')", true);
                            list.InnerHtml = "Account Master File Uploaded";
                            lblAccountMaster.Text = AccUploadCount + " Records uploaded of " + TotalAccUploadCount;
                        }
                    }
                    else
                    {
                        list.InnerHtml = "<font color='red'> Invalid File for Account Master.</font>";
                        lblAccountMaster.Text = "";
                    }

                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }
            }
            catch (System.Exception ex)
            {
                // uploadresult = "error";
                uploadresult = ex.Message;
                // oconn.Close();
                GC.Collect();
            }
            return uploadresult;
        }
        else
            lblAccountMaster.Text = "";
            return uploadresult;

    }


    private string Upload_Data_From_CSV_Individual_Master(string _filepath)
    {
        string path = Server.MapPath("~/GeneratedFiles/CBWT/Uploaded_Files");

        norecinexcel = 0;
        string uploadresult = "";

        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        //  string _DateRecvd = System.DateTime.Now.ToString("dd/MM/yyyy");
        string result = "";

        string _BranchCode = "";
        string _AcNo = "";
        string _SerialNo = "";
        string _IndName = "";
        string _FatherSpouseName = "";
        string _Occupation = "";
        string _RelationFlag = "";
        string _BirthDate = "";
        string _Sex = "";
        string _Nationality = "";
        string _CommunicationAddress = "";
        string _CountryCode = "";
        string _StateCode = "";
        string _City = "";
        string _Pincode = "";
        string _Telephone = "";
        string _Mobile = "";
        string _EmailID = "";
        string _Fax = "";
        string _UIN = "";
        string _WorkPlace = "";
        string _WAddress = "";
        string _WCountryCode = "";
        string _WStateCode = "";
        string _WCity = "";
        string _WPincode = "";
        string _WTelephone = "";
        string _IdentificationType = "";
        string _IdentificationNo = "";
        string _IssuingAuth = "";
        string _IssuePlace = "";
        string _PanNo = "";

        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileposted";
        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                    //System.IO.File.Delete(_filepath);
                }
                catch  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    Thread.Sleep(10000); //Wait for object to be killed.
                    File.Delete(_filepath); //File can be now deleted
                }
            }
        }
        catch { }

        try
        {
            FileIndividualMaster.PostedFile.SaveAs(_filepath);
            uploadresult = "fileposted";
        }
        catch
        {
            uploadresult = "ioerror";
        }

        if (uploadresult == "fileposted")
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

                }
                norecinexcel = norecinexcel + 1;
            }

            SqlParameter pBranchCode = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            SqlParameter pAcNo = new SqlParameter("@AcNo", SqlDbType.VarChar);
            SqlParameter pSerialNo = new SqlParameter("@SerialNo", SqlDbType.VarChar);
            SqlParameter pIndName = new SqlParameter("@IndName", SqlDbType.VarChar);
            SqlParameter pFatherSpouseName = new SqlParameter("@FatherSpouseName", SqlDbType.VarChar);
            SqlParameter pOccupation = new SqlParameter("@Occupation", SqlDbType.VarChar);
            SqlParameter pRelationFlag = new SqlParameter("@RelationFlag", SqlDbType.VarChar);
            SqlParameter pBirthDate = new SqlParameter("@BirthDate", SqlDbType.VarChar);
            SqlParameter pSex = new SqlParameter("@Sex", SqlDbType.VarChar);
            SqlParameter pNationality = new SqlParameter("@Nationality", SqlDbType.VarChar);
            SqlParameter pCommunicationAddress = new SqlParameter("@CommunicationAddress", SqlDbType.VarChar);
            SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
            SqlParameter pStateCode = new SqlParameter("@StateCode", SqlDbType.VarChar);
            SqlParameter pCity = new SqlParameter("@City", SqlDbType.VarChar);
            SqlParameter pPincode = new SqlParameter("@Pincode", SqlDbType.VarChar);
            SqlParameter pTelephone = new SqlParameter("@Telephone", SqlDbType.VarChar);
            SqlParameter pMobile = new SqlParameter("@Mobile", SqlDbType.VarChar);
            SqlParameter pEmailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            SqlParameter pFax = new SqlParameter("@Fax", SqlDbType.VarChar);
            SqlParameter pUIN = new SqlParameter("@UIN", SqlDbType.VarChar);
            SqlParameter pWorkPlace = new SqlParameter("@WorkPlace", SqlDbType.VarChar);
            SqlParameter pWAddress = new SqlParameter("@WAddress", SqlDbType.VarChar);
            SqlParameter pWCountryCode = new SqlParameter("@WCountryCode", SqlDbType.VarChar);
            SqlParameter pWStateCode = new SqlParameter("@WStateCode", SqlDbType.VarChar);
            SqlParameter pWCity = new SqlParameter("@WCity", SqlDbType.VarChar);
            SqlParameter pWPincode = new SqlParameter("@WPincode", SqlDbType.VarChar);
            SqlParameter pWTelephone = new SqlParameter("@WTelephone", SqlDbType.VarChar);
            SqlParameter pIdentificationType = new SqlParameter("@IdentificationType", SqlDbType.VarChar);
            SqlParameter pIdentificationNo = new SqlParameter("@IdentificationNo", SqlDbType.VarChar);
            SqlParameter pIssuingAuth = new SqlParameter("@IssuingAuth", SqlDbType.VarChar);
            SqlParameter pIssuePlace = new SqlParameter("@IssuePlace", SqlDbType.VarChar);
            SqlParameter pPanNo = new SqlParameter("@PanNo", SqlDbType.VarChar);
            SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
            pUser.Value = _userName;
            SqlParameter pUploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
            pUploadingDate.Value = _uploadingDate;

            string _query = "CBWT_Individual_Master_File_Upload";

            try
            {
                try
                {
                    if (dt.Columns.Count == 32)
                    {
                        int UploadedIndividualFileCount = 0;
                        int TotalIndividualFileCount = 0;


                        // list.InnerHtml = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString().Trim() != "")
                            {
                                _BranchCode = dt.Rows[i][0].ToString();
                                _AcNo = dt.Rows[i][1].ToString();
                                _SerialNo = dt.Rows[i][2].ToString();
                                _IndName = dt.Rows[i][3].ToString();
                                _FatherSpouseName = dt.Rows[i][4].ToString();
                                _Occupation = dt.Rows[i][5].ToString();
                                _RelationFlag = dt.Rows[i][6].ToString();
                                _BirthDate = dt.Rows[i][7].ToString();
                                _Sex = dt.Rows[i][8].ToString();
                                _Nationality = dt.Rows[i][9].ToString();
                                _CommunicationAddress = dt.Rows[i][10].ToString();
                                _CountryCode = dt.Rows[i][11].ToString();
                                _StateCode = dt.Rows[i][12].ToString();
                                _City = dt.Rows[i][13].ToString();
                                _Pincode = dt.Rows[i][14].ToString();
                                _Telephone = dt.Rows[i][15].ToString();
                                _Mobile = dt.Rows[i][16].ToString();
                                _EmailID = dt.Rows[i][17].ToString();
                                _Fax = dt.Rows[i][18].ToString();
                                _UIN = dt.Rows[i][19].ToString();
                                _WorkPlace = dt.Rows[i][20].ToString();
                                _WAddress = dt.Rows[i][21].ToString();
                                _WCountryCode = dt.Rows[i][22].ToString();
                                _WStateCode = dt.Rows[i][23].ToString();
                                _WCity = dt.Rows[i][24].ToString();
                                _WPincode = dt.Rows[i][25].ToString();
                                _WTelephone = dt.Rows[i][26].ToString();
                                _IdentificationType = dt.Rows[i][27].ToString();
                                _IdentificationNo = dt.Rows[i][28].ToString();
                                _IssuingAuth = dt.Rows[i][29].ToString();
                                _IssuePlace = dt.Rows[i][30].ToString();
                                _PanNo = dt.Rows[i][31].ToString();


                                pBranchCode.Value = _BranchCode;
                                pAcNo.Value = _AcNo;
                                pSerialNo.Value = _SerialNo;
                                pIndName.Value = _IndName;
                                pFatherSpouseName.Value = _FatherSpouseName;
                                pOccupation.Value = _Occupation;
                                pRelationFlag.Value = _RelationFlag;
                                pBirthDate.Value = _BirthDate;
                                pSex.Value = _Sex;
                                pNationality.Value = _Nationality;
                                pCommunicationAddress.Value = _CommunicationAddress;
                                pCountryCode.Value = _CountryCode;
                                pStateCode.Value = _StateCode;
                                pCity.Value = _City;
                                pPincode.Value = _Pincode;
                                pTelephone.Value = _Telephone;
                                pMobile.Value = _Mobile;
                                pEmailID.Value = _EmailID;
                                pFax.Value = _Fax;
                                pUIN.Value = _UIN;
                                pWorkPlace.Value = _WorkPlace;
                                pWAddress.Value = _WAddress;
                                pWCountryCode.Value = _WCountryCode;
                                pWStateCode.Value = _WStateCode;
                                pWCity.Value = _WCity;
                                pWPincode.Value = _WPincode;
                                pWTelephone.Value = _WTelephone;
                                pIdentificationType.Value = _IdentificationType;
                                pIdentificationNo.Value = _IdentificationNo;
                                pIssuingAuth.Value = _IssuingAuth;
                                pIssuePlace.Value = _IssuePlace;
                                pPanNo.Value = _PanNo;


                                TF_DATA objSave = new TF_DATA();
                                uploadresult = objSave.SaveDeleteData(_query, pBranchCode, pAcNo, pSerialNo, pIndName, pFatherSpouseName, pOccupation, pRelationFlag, pBirthDate,
                                                pSex, pNationality, pCommunicationAddress, pCountryCode, pStateCode, pCity, pPincode,
                                                pTelephone, pMobile, pEmailID, pFax, pUIN, pWorkPlace, pWAddress, pWCountryCode, pWStateCode,
                                                pWCity, pWPincode, pWTelephone, pIdentificationType, pIdentificationNo, pIssuingAuth, pIssuePlace, pPanNo, pUser, pUploadingDate);

                                TotalIndividualFileCount = TotalIndividualFileCount + 1;
                                if (uploadresult == "Uploaded")
                                {
                                    UploadedIndividualFileCount = UploadedIndividualFileCount + 1;
                                }
                            }
                        }
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Data Uploaded.')", true);
                        list.InnerHtml = list.InnerHtml + "<br>" + "Individual Master File Uploaded";
                        lblIndividualMaster.Text = UploadedIndividualFileCount + " Records uploaded of " + TotalIndividualFileCount;

                    }
                    else
                    {
                        list.InnerHtml = list.InnerHtml + "<br>" + "<font color='red'> Invalid File for Individual Info. Master.</font>";
                        lblIndividualMaster.Text = "";

                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }

                //if (File.Exists(tempFilePath))
                //    File.Delete(tempFilePath);//delete temp file

            }
            catch (System.Exception ex)
            {
                // uploadresult = "error";
                uploadresult = ex.Message;
                // oconn.Close();
                GC.Collect();
            }
            return uploadresult;
        }
        else
            lblIndividualMaster.Text = "";
            return uploadresult;

    }


    private string Upload_Data_From_CSV_Legal_Master(string _filepath)
    {
        string path = Server.MapPath("~/GeneratedFiles/CBWT/Uploaded_Files");

        norecinexcel = 0;
        string uploadresult = "";

        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        //  string _DateRecvd = System.DateTime.Now.ToString("dd/MM/yyyy");
        string result = "";

        string _BranchCode = "";
        string _AcNo = "";
        string _SerialNo = "";
        string _LEname = "";
        string _Business = "";
        string _InCorpDate = "";
        string _RelationFlag = "";
        string _CommunicationAddress = "";
        string _CountryCode = "";
        string _StateCode = "";
        string _City = "";
        string _Pincode = "";
        string _Telephone = "";
        string _Fax = "";
        string _Mobile = "";
        string _EmailID = "";
        string _UIN = "";
        string _RAddress = "";
        string _RCountryCode = "";
        string _RStateCode = "";
        string _RCity = "";
        string _RPincode = "";
        string _RTelephone = "";
        string _RFax = "";
        string _ConstType = "";
        string _RegNo = "";
        string _RegAuth = "";
        string _RegPlace = "";
        string _PanNo = "";
        string _OCountryCode = "";

        string _rowData = System.DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
        uploadresult = "fileposted";
        try
        {
            System.IO.File.SetAttributes(_filepath, FileAttributes.Normal);
            if (System.IO.File.Exists(_filepath))
            {
                try
                {
                    File.Delete(_filepath);
                    //System.IO.File.Delete(_filepath);
                }
                catch  //or maybe in finally
                {
                    GC.Collect(); //kill object that keep the file. I think dispose will do the trick as well.
                    Thread.Sleep(10000); //Wait for object to be killed. 
                    File.Delete(_filepath); //File can be now deleted
                }
            }
        }
        catch { }

        try
        {
            FileLegalPerson.PostedFile.SaveAs(_filepath);
            uploadresult = "fileposted";
        }
        catch
        {
            uploadresult = "ioerror";
        }

        if (uploadresult == "fileposted")
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

                }
                norecinexcel = norecinexcel + 1;
            }

            SqlParameter pBranchCode = new SqlParameter("@BranchCode", SqlDbType.VarChar);
            SqlParameter pAcNo = new SqlParameter("@AcNo", SqlDbType.VarChar);
            SqlParameter pSerialNo = new SqlParameter("@SerialNo", SqlDbType.VarChar);
            SqlParameter pLEname = new SqlParameter("@LEname", SqlDbType.VarChar);
            SqlParameter pBusiness = new SqlParameter("@Business", SqlDbType.VarChar);
            SqlParameter pInCorpDate = new SqlParameter("@InCorpDate", SqlDbType.VarChar);
            SqlParameter pRelationFlag = new SqlParameter("@RelationFlag", SqlDbType.VarChar);
            SqlParameter pCommunicationAddress = new SqlParameter("@CommunicationAddress", SqlDbType.VarChar);
            SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
            SqlParameter pStateCode = new SqlParameter("@StateCode", SqlDbType.VarChar);
            SqlParameter pCity = new SqlParameter("@City", SqlDbType.VarChar);
            SqlParameter pPincode = new SqlParameter("@Pincode", SqlDbType.VarChar);
            SqlParameter pTelephone = new SqlParameter("@Telephone", SqlDbType.VarChar);
            SqlParameter pFax = new SqlParameter("@Fax", SqlDbType.VarChar);
            SqlParameter pMobile = new SqlParameter("@Mobile", SqlDbType.VarChar);
            SqlParameter pEmailID = new SqlParameter("@EmailID", SqlDbType.VarChar);
            SqlParameter pUIN = new SqlParameter("@UIN", SqlDbType.VarChar);
            SqlParameter pRAddress = new SqlParameter("@RAddress", SqlDbType.VarChar);
            SqlParameter pRCountryCode = new SqlParameter("@RCountryCode", SqlDbType.VarChar);
            SqlParameter pRStateCode = new SqlParameter("@RStateCode", SqlDbType.VarChar);
            SqlParameter pRCity = new SqlParameter("@RCity", SqlDbType.VarChar);
            SqlParameter pRPincode = new SqlParameter("@RPincode", SqlDbType.VarChar);
            SqlParameter pRTelephone = new SqlParameter("@RTelephone", SqlDbType.VarChar);
            SqlParameter pRFax = new SqlParameter("@RFax", SqlDbType.VarChar);
            SqlParameter pConstType = new SqlParameter("@ConstType", SqlDbType.VarChar);
            SqlParameter pRegNo = new SqlParameter("@RegNo", SqlDbType.VarChar);
            SqlParameter pRegAuth = new SqlParameter("@RegAuth", SqlDbType.VarChar);
            SqlParameter pRegPlace = new SqlParameter("@RegPlace", SqlDbType.VarChar);
            SqlParameter pPanNo = new SqlParameter("@PanNo", SqlDbType.VarChar);
            SqlParameter pOCountryCode = new SqlParameter("@OCountryCode", SqlDbType.VarChar);
            SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
            pUser.Value = _userName;
            SqlParameter pUploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
            pUploadingDate.Value = _uploadingDate;

            string _query = "CBWT_Legal_Master_File_Upload";

            try
            {
                try
                {
                    if (dt.Columns.Count == 30)
                    {
                        int UploadedLegalEntity = 0;
                        int TotalLegalEntity = 0;



                        // list.InnerHtml = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString().Trim() != "")
                            {
                                _BranchCode = dt.Rows[i][0].ToString();
                                _AcNo = dt.Rows[i][1].ToString();
                                _SerialNo = dt.Rows[i][2].ToString();
                                _LEname = dt.Rows[i][3].ToString();
                                _Business = dt.Rows[i][4].ToString();
                                _InCorpDate = dt.Rows[i][5].ToString();
                                _RelationFlag = dt.Rows[i][6].ToString();
                                _CommunicationAddress = dt.Rows[i][7].ToString();
                                _CountryCode = dt.Rows[i][8].ToString();
                                _StateCode = dt.Rows[i][9].ToString();
                                _City = dt.Rows[i][10].ToString();
                                _Pincode = dt.Rows[i][11].ToString();
                                _Telephone = dt.Rows[i][12].ToString();
                                _Fax = dt.Rows[i][13].ToString();
                                _Mobile = dt.Rows[i][14].ToString();
                                _EmailID = dt.Rows[i][15].ToString();
                                _UIN = dt.Rows[i][16].ToString();
                                _RAddress = dt.Rows[i][17].ToString();
                                _RCountryCode = dt.Rows[i][18].ToString();
                                _RStateCode = dt.Rows[i][19].ToString();
                                _RCity = dt.Rows[i][20].ToString();
                                _RPincode = dt.Rows[i][21].ToString();
                                _RTelephone = dt.Rows[i][22].ToString();
                                _RFax = dt.Rows[i][23].ToString();
                                _ConstType = dt.Rows[i][24].ToString();
                                _RegNo = dt.Rows[i][25].ToString();
                                _RegAuth = dt.Rows[i][26].ToString();
                                _RegPlace = dt.Rows[i][27].ToString();
                                _PanNo = dt.Rows[i][28].ToString();
                                _OCountryCode = dt.Rows[i][29].ToString();

                                pBranchCode.Value = _BranchCode;
                                pAcNo.Value = _AcNo;
                                pSerialNo.Value = _SerialNo;
                                pLEname.Value = _LEname;
                                pBusiness.Value = _Business;
                                pInCorpDate.Value = _InCorpDate;
                                pRelationFlag.Value = _RelationFlag;
                                pCommunicationAddress.Value = _CommunicationAddress;
                                pCountryCode.Value = _CountryCode;
                                pStateCode.Value = _StateCode;
                                pCity.Value = _City;
                                pPincode.Value = _Pincode;
                                pTelephone.Value = _Telephone;
                                pFax.Value = _Fax;
                                pMobile.Value = _Mobile;
                                pEmailID.Value = _EmailID;
                                pUIN.Value = _UIN;
                                pRAddress.Value = _RAddress;
                                pRCountryCode.Value = _RCountryCode;
                                pRStateCode.Value = _RStateCode;
                                pRCity.Value = _RCity;
                                pRPincode.Value = _RPincode;
                                pRTelephone.Value = _RTelephone;
                                pRFax.Value = _RFax;
                                pConstType.Value = _ConstType;
                                pRegNo.Value = _RegNo;
                                pRegAuth.Value = _RegAuth;
                                pRegPlace.Value = _RegPlace;
                                pPanNo.Value = _PanNo;
                                pOCountryCode.Value = _OCountryCode;


                                TF_DATA objSave = new TF_DATA();
                                uploadresult = objSave.SaveDeleteData(_query, pBranchCode, pAcNo, pSerialNo, pLEname, pBusiness,
                                                pInCorpDate, pRelationFlag, pCommunicationAddress, pCountryCode, pStateCode,
                                                pCity, pPincode, pTelephone, pFax, pMobile, pEmailID, pUIN, pRAddress, pRCountryCode,
                                                pRStateCode, pRCity, pRPincode, pRTelephone, pRFax, pConstType, pRegNo, pRegAuth,
                                                pRegPlace, pPanNo, pOCountryCode, pUser, pUploadingDate);

                                TotalLegalEntity = TotalLegalEntity + 1;

                                if (uploadresult == "Uploaded")
                                {
                                    UploadedLegalEntity = UploadedLegalEntity + 1;
                                }
                            }
                        }
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Data Uploaded.')", true);
                        list.InnerHtml = list.InnerHtml + "<br>" + "Legal Entity Master File Uploaded";
                        lblLegalFile.Text = UploadedLegalEntity + " Records uploaded of " + TotalLegalEntity;

                    }
                    else
                    {
                        list.InnerHtml = list.InnerHtml + "<br>" + "<font color='red'> Invalid File for Legal Entity Master.</font>";
                        lblLegalFile.Text = "";

                    }

                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    labelMessage.Text = errorMsg;
                }

                //if (File.Exists(tempFilePath))
                //    File.Delete(tempFilePath);//delete temp file

            }
            catch (System.Exception ex)
            {
                // uploadresult = "error";
                uploadresult = ex.Message;
                // oconn.Close();
                GC.Collect();
            }
            return uploadresult;
        }
        else
            lblLegalFile.Text = "";
            return uploadresult;

    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("CBWT_Main.aspx", true);
    }

}