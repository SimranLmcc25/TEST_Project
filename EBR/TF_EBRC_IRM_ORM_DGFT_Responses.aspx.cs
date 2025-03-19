using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;

public partial class EBR_TF_EBRC_IRM_ORM_DGFT_Responses : System.Web.UI.Page
{
    TF_DATA objdata = new TF_DATA();
    Encryption objEnc = new Encryption();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            //Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg==/" + objEnc.URLIDEncription("yes") + "/" + objEnc.URLIDEncription(lbl.Value));
        }
        if (!IsPostBack)
        {
            PageAccess();
            ddlrecordperpage.SelectedValue = "20";
            ddlirmormstatus.SelectedValue = "1";
            txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddltype.Focus();
            rowPager.Visible = false;
            fillGrid();
        }

      
    }
    //protected void btngetStatus_Click(object sender, EventArgs e)
    //{
    //    string res = "";

    //    if (ddltype.SelectedIndex== 0)
    //    {

    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.')", true);
    //    }
    //    if (txtnumber.Text == "") 
    //    {

    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter BankUniqueTxtID')", true);
    //    }
    //    else if (ddltype.SelectedIndex == 1)
    //    {
    //        rdb_Errored.Visible = true;
    //        rdb_Processed.Visible = true;
    //        rdb_All.Visible = true;

         
    //          SqlParameter a1 = new SqlParameter("@uniquetxid", txtnumber.Text);
    //          DataTable dt2 = objdata.getData("TF_EBRC_IRMORM_JsonOutput_List", a1);//get irm status list from jsonoutput table
    //              if (dt2.Rows.Count > 0)
    //              {
    //                  for (int j = 0; j < dt2.Rows.Count; j++)
    //                  {

    //                      string json = dt2.Rows[j].ItemArray.GetValue(0).ToString();
    //                      //string json = dt2.Rows[i]["IRMStatusList"].ToString();
    //                      //Creates DataTable 
    //                      DataTable dt11 = new DataTable();
    //                      dt11.Columns.Add("irmNumber", typeof(string));
    //                      dt11.Columns.Add("irmIssueDate", typeof(string));
    //                      dt11.Columns.Add("ackStatus", typeof(string));
    //                      dt11.Columns.Add("errorCode", typeof(string));
    //                      dt11.Columns.Add("errorDetails", typeof(string));

    //                      List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json);

    //                      JArray jsonArray = JArray.Parse(json);

    //                      foreach (var item in jsonArray)
    //                      {
    //                          JObject jsonObject = (JObject)item;

    //                          string irmNumber = jsonObject["irmNumber"].ToString();
    //                          string irmIssueDate = jsonObject["irmIssueDate"].ToString();
    //                          string ackStatus = jsonObject["ackStatus"].ToString();
    //                          string errorCode = "";
    //                          string errorDetails = "";
    //                          JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

    //                          foreach (var errorDetailsItem in errorDetailsArray)
    //                          {
    //                              JObject errorDetailsObject = (JObject)errorDetailsItem;

    //                              errorCode = errorDetailsObject["errorCode"].ToString();
    //                              errorDetails = errorDetailsObject["errorDetails"].ToString();

    //                              dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails);

    //                          }

    //                         //dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails);
    //                      }
    //                      #region not use
    //                      //foreach (var item in jsonDataList)
    //                      //{
    //                      //    DataRow row = dt11.NewRow();
    //                      //    row["irmNumber"] = item["irmNumber"].ToString();
    //                      //    row["irmIssueDate"] = item["irmIssueDate"].ToString();
    //                      //    row["ackStatus"] = item["ackStatus"].ToString();

    //                      //    // Handle errorDetails as needed (e.g., convert it to a string)
    //                      //    row["errorDetails"] = item["errorDetails"].ToString();
    //                      //    string errordetails = item["errorDetails"].ToString();
    //                      //    List<JObject> jsonDataList2 = JsonConvert.DeserializeObject<List<JObject>>(errordetails);//errordetaillist
    //                      //    foreach (var item2 in jsonDataList2)
    //                      //    {
    //                      //        row["errorCode"] =  item2["errorCode"].ToString();
    //                      //        row["errorDetails"] = item2["errorDetails"].ToString();

    //                      //    }
    //                      //      dt11.Rows.Add(row);
    //                      //}

    //                      #endregion

    //                     // insert dgft response data(eg.error details,error code ,ack status) into table
    //                      string result = ""; string uniquetxid = txtnumber.Text;
    //                      string irmno = "", irmissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
    //                      if (dt11.Rows.Count > 0)
    //                      {
    //                          for (int k = 0; k < dt11.Rows.Count; k++)
    //                          {
    //                              irmno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
    //                              irmissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
    //                              ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
    //                              errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
    //                              errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
    //                              string irmunique = irmno + uniquetxid + errorcode;
    //                              SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                              SqlParameter i2 = new SqlParameter("@irmno", irmno);
    //                              SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate);
    //                              SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
    //                              SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
    //                              SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
    //                              SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
    //                              result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);
    //                          }
    //                      }
    //                  }

    //             }
              
    //      SqlParameter p10 = new SqlParameter("@uniquetxid",txtnumber.Text);
    //      DataTable dt3 = objdata.getData("TF_EBRC_IRMORM_JsonOutput_ErrordeatilsList", p10);//IRMORMErrorDetailsList
    //      if (dt3.Rows.Count > 0)
    //      {
    //          for (int p = 0; p < dt3.Rows.Count; p++)
    //          {
    //              string json1 = dt3.Rows[p].ItemArray.GetValue(0).ToString();
    //              //Creates DataTable 
    //              DataTable dt12 = new DataTable();
    //              dt12.Columns.Add("errorCode", typeof(string));
    //              dt12.Columns.Add("errorDetails", typeof(string));
    //              List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json1);

    //              JArray jsonArray1 = JArray.Parse(json1);

    //              foreach (var item in jsonArray1)
    //              {

    //                  JObject jsonObject1 = (JObject)item;

    //                  string errorCode = jsonObject1["errorCode"].ToString();
    //                  string errorDetails = jsonObject1["errorDetails"].ToString();

    //                  dt12.Rows.Add(errorCode, errorDetails);
    //              }

    //              //insert datatatble into table
    //              //get irm number for this errordetails list
    //              DataTable dt4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_IRMNo", p10);
    //              string irmno_ = "";
    //              string irmissuedate_ = "";
    //              if (dt4.Rows.Count > 0)
    //              {
    //                  for (int q = 0; q < dt4.Rows.Count; q++)
    //                  {
    //                      irmno_ = dt4.Rows[q]["IRMNumber"].ToString();
    //                      irmissuedate_ = dt4.Rows[q]["IRMissueDate"].ToString();
    //                  }
    //              }
    //              string result = "";
    //              string irmno = "", irmissuedate = "", errorcode = "", errordetails_ = "";
    //              if (dt12.Rows.Count > 0)
    //              {
    //                  for (int s = 0; s < dt12.Rows.Count; s++)
    //                  {
    //                      irmno = irmno_;
    //                      irmissuedate = irmissuedate_;
    //                      errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
    //                      errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
    //                      string uniquetxid = txtnumber.Text;
    //                      string irmunique = irmno + uniquetxid + errorcode;
    //                      SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                      SqlParameter i2 = new SqlParameter("@irmno", irmno);
    //                      SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate);
    //                      SqlParameter i4 = new SqlParameter("@ackstatus", "");
    //                      SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
    //                      SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
    //                      SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
    //                      result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);
    //                  }
    //              }
    //          }

    //      }
    //      fillGrid();
         
    //     //----------------------------------------------
    //    }
    //    else if (ddltype.SelectedIndex == 2)
    //    {
    //        rdb_Errored.Visible = true;
    //        rdb_Processed.Visible = true;
    //        rdb_All.Visible = true;
         

    //        SqlParameter a1 = new SqlParameter("@uniquetxid", txtnumber.Text);
    //        DataTable dt2 = objdata.getData("TF_EBRC_IRMORM_JsonOutput_List", a1);//get irm status list from jsonoutput table
    //        if (dt2.Rows.Count > 0)
    //        {
    //            for (int j = 0; j < dt2.Rows.Count; j++)
    //            {

    //                string json = dt2.Rows[j].ItemArray.GetValue(0).ToString();
    //                //string json = dt2.Rows[i]["IRMStatusList"].ToString();
    //                //Creates DataTable 
    //                DataTable dt11 = new DataTable();
    //                dt11.Columns.Add("ormNumber", typeof(string));
    //                dt11.Columns.Add("ormIssueDate", typeof(string));
    //                dt11.Columns.Add("ackStatus", typeof(string));
    //                dt11.Columns.Add("errorCode", typeof(string));
    //                dt11.Columns.Add("errorDetails", typeof(string));


    //                List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json);

    //                JArray jsonArray = JArray.Parse(json);

    //                foreach (var item in jsonArray)
    //                {
    //                    JObject jsonObject = (JObject)item;

    //                    string ormNumber = jsonObject["ormNumber"].ToString();
    //                    string ormIssueDate = jsonObject["ormIssueDate"].ToString();
    //                    string ackStatus = jsonObject["ackStatus"].ToString();
    //                    string errorCode = "";
    //                    string errorDetails = "";
    //                    JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

    //                    foreach (var errorDetailsItem in errorDetailsArray)
    //                    {
    //                        JObject errorDetailsObject = (JObject)errorDetailsItem;

    //                        errorCode = errorDetailsObject["errorCode"].ToString();
    //                        errorDetails = errorDetailsObject["errorDetails"].ToString();

    //                        dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);

    //                    }

    //                    //dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails);
    //                }
    //                #region not use
    //                //foreach (var item in jsonDataList)
    //                //{
    //                //    DataRow row = dt11.NewRow();
    //                //    row["irmNumber"] = item["irmNumber"].ToString();
    //                //    row["irmIssueDate"] = item["irmIssueDate"].ToString();
    //                //    row["ackStatus"] = item["ackStatus"].ToString();

    //                //    // Handle errorDetails as needed (e.g., convert it to a string)
    //                //    row["errorDetails"] = item["errorDetails"].ToString();
    //                //    string errordetails = item["errorDetails"].ToString();
    //                //    List<JObject> jsonDataList2 = JsonConvert.DeserializeObject<List<JObject>>(errordetails);//errordetaillist
    //                //    foreach (var item2 in jsonDataList2)
    //                //    {
    //                //        row["errorCode"] =  item2["errorCode"].ToString();
    //                //        row["errorDetails"] = item2["errorDetails"].ToString();

    //                //    }
    //                //      dt11.Rows.Add(row);
    //                //}

    //                #endregion

    //                // insert dgft response data(eg.error details,error code ,ack status) into table
    //                string result = ""; string uniquetxid = txtnumber.Text;
    //                string irmno = "", irmissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
    //                if (dt11.Rows.Count > 0)
    //                {
    //                    for (int k = 0; k < dt11.Rows.Count; k++)
    //                    {
    //                        irmno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
    //                        irmissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
    //                        ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
    //                        errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
    //                        errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
    //                        string irmunique = irmno + uniquetxid + errorcode;
    //                        SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                        SqlParameter i2 = new SqlParameter("@irmno", irmno);
    //                        SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate);
    //                        SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
    //                        SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
    //                        SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
    //                        SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
    //                        result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);
    //                    }
    //                }
    //            }

    //        }

    //        SqlParameter p10 = new SqlParameter("@uniquetxid", txtnumber.Text);
    //        DataTable dt3 = objdata.getData("TF_EBRC_IRMORM_JsonOutput_ErrordeatilsList", p10);//IRMORMErrorDetailsList
    //        if (dt3.Rows.Count > 0)
    //        {
    //            for (int p = 0; p < dt3.Rows.Count; p++)
    //            {
    //                string json1 = dt3.Rows[p].ItemArray.GetValue(0).ToString();
    //                //Creates DataTable 
    //                DataTable dt12 = new DataTable();
    //                dt12.Columns.Add("errorCode", typeof(string));
    //                dt12.Columns.Add("errorDetails", typeof(string));
    //                List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json1);

    //                JArray jsonArray1 = JArray.Parse(json1);

    //                foreach (var item in jsonArray1)
    //                {

    //                    JObject jsonObject1 = (JObject)item;

    //                    string errorCode = jsonObject1["errorCode"].ToString();
    //                    string errorDetails = jsonObject1["errorDetails"].ToString();

    //                    dt12.Rows.Add(errorCode, errorDetails);
    //                }

    //                //insert datatatble into table
    //                //get irm number for this errordetails list
    //                DataTable dt4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_ORMNo", p10);
    //                string irmno_ = "";
    //                string irmissuedate_ = "";
    //                if (dt4.Rows.Count > 0)
    //                {
    //                    for (int q = 0; q < dt4.Rows.Count; q++)
    //                    {
    //                        irmno_ = dt4.Rows[q]["ORMNumber"].ToString();
    //                        irmissuedate_ = dt4.Rows[q]["ORMIssueDate"].ToString();
    //                    }
    //                }
    //                string result = "";
    //                string irmno = "", irmissuedate = "", errorcode = "", errordetails_ = "";
    //                if (dt12.Rows.Count > 0)
    //                {
    //                    for (int s = 0; s < dt12.Rows.Count; s++)
    //                    {
    //                        irmno = irmno_;
    //                        irmissuedate = irmissuedate_;
    //                        errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
    //                        errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
    //                        string uniquetxid = txtnumber.Text;
    //                        string irmunique = irmno + uniquetxid + errorcode;
    //                        SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
    //                        SqlParameter i2 = new SqlParameter("@irmno", irmno);
    //                        SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate);
    //                        SqlParameter i4 = new SqlParameter("@ackstatus", "");
    //                        SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
    //                        SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
    //                        SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
    //                        result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);
    //                    }
    //                }
    //            }

    //        }
    //        fillGrid();
         
    //    }

        
      
    //}
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddltype.SelectedIndex == 1)
        //{
        //    txtnumber.Text = "";
        //}
        //else if (ddltype.SelectedIndex == 2)
        //{
        //    txtnumber.Text = "";
        //}
        //else
        //{
        //    txtnumber.Text = "";
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.')", true);
        //}
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewReturnData.PageCount != GridViewReturnData.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewReturnData.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewReturnData.PageIndex + 1) + " of " + GridViewReturnData.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewReturnData.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewReturnData.PageIndex != (GridViewReturnData.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private  void  navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewReturnData.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewReturnData.PageIndex > 0)
        {
            GridViewReturnData.PageIndex = GridViewReturnData.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewReturnData.PageIndex != GridViewReturnData.PageCount - 1)
        {
            GridViewReturnData.PageIndex = GridViewReturnData.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewReturnData.PageIndex = GridViewReturnData.PageCount - 1;
        fillGrid();
    }
    protected void fillGrid()
    {
       
      
        string status = "";
        if (rdb_Errored.Checked == true)
        {
            status = "Errored";
        }
        else if (rdb_Processed.Checked == true)
        {
            status = "Processed";
        }
        else
        {
            status = "All";
        }
        rowPager.Visible = true;
        string mode = "";
        if (ddltype.SelectedValue == "1")
        {
            DGFTStatusListIRM();
            mode = "IRM";
           
        }
        else if (ddltype.SelectedValue == "2")
        {
            DGFTStatusListORM();
            mode = "ORM";
        }
        else
        {
            mode = "";
        }
        string irmormstauts = "";
        if (ddlirmormstatus.SelectedValue == "1")
        {
            irmormstauts = "F";
        }
        else if (ddlirmormstatus.SelectedValue == "2")
        {
            irmormstauts = "A";
        }
        else if (ddlirmormstatus.SelectedValue == "3")
        {
            irmormstauts = "C";
        }
        else
        {
            irmormstauts = "";
        }

        if (irmormstauts == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select irm status')", true);
            ddlirmormstatus.Focus();
        }
        else
        {
            SqlParameter p1 = new SqlParameter("@status", status);
            SqlParameter p2 = new SqlParameter("@mode", mode);
            SqlParameter p3 = new SqlParameter("@approvalDate", txtfromDate.Text.ToString());
            SqlParameter p4 = new SqlParameter("@irmormstatus", irmormstauts);
            DataTable dt = objdata.getData("TF_EBRC_IRM_ORM_GetStatus_Grid", p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                int _records = dt.Rows.Count;
                int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
                GridViewReturnData.PageSize = _pageSize;
                GridViewReturnData.DataSource = dt.DefaultView;
                GridViewReturnData.DataBind();
                GridViewReturnData.Visible = true;
                rowGrid.Visible = true;
                rowPager.Visible = true;
                labelMessage.Visible = false;
                pagination(_records, _pageSize);
                if (ddltype.SelectedIndex == 1)
                {
                    GridViewReturnData.HeaderRow.Cells[1].Text = "IRM Number";
                    GridViewReturnData.HeaderRow.Cells[2].Text = "IRM Issue Date";
                    GridViewReturnData.HeaderRow.Cells[3].Text = "IRM Status";
                }

                if (ddltype.SelectedIndex == 2)
                {
                    GridViewReturnData.HeaderRow.Cells[1].Text = "ORM Number";
                    GridViewReturnData.HeaderRow.Cells[2].Text = "ORM Issue Date";
                    GridViewReturnData.HeaderRow.Cells[3].Text = "ORM Status";
                }
            }
            else
            {
                GridViewReturnData.Visible = false;
                rowGrid.Visible = false;
                rowPager.Visible = false;
                labelMessage.Text = "No record(s) found.";
                labelMessage.Visible = true;
            }
        }
       
    }
    protected void ddlirmormstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlirmormstatus.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select IRM/ORM status')", true);
            ddlirmormstatus.Focus();
        }
        if (ddlirmormstatus.SelectedValue == "1")
        {
            fillGrid();
        }
        else if (ddlirmormstatus.SelectedValue == "2")
        {
            fillGrid();
        }
        else if (ddlirmormstatus.SelectedValue == "3")
        {
            fillGrid();
        }
    }
    protected void rdb_Processed_CheckedChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.')", true);
            ddltype.Focus();
        }
        fillGrid();
    }
    protected void rdb_Errored_CheckedChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.')", true);
            ddltype.Focus();
        }
        fillGrid();
    }
    protected void rdb_All_CheckedChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type.')", true);
            ddltype.Focus();
        }
        fillGrid();
        GridViewReturnData.Columns[6].Visible = true;
        GridViewReturnData.Columns[7].Visible = false;
    }
    protected void txtfromDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    public void DGFTStatusListIRM()
    {
        
            TF_DATA objdata = new TF_DATA();
            SqlParameter s1 = new SqlParameter("@fromdate", txtfromDate.Text);
            SqlParameter s2 = new SqlParameter("@todate", txtfromDate.Text);
            DataTable dt1 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_PushValidate", s1, s2);

            if (dt1.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt1.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt1.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Validated";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt2 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_PushFailed", s1, s2);
            if (dt2.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt2.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt2.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt3 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_GetValidated", s1, s2);
            if (dt3.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt3.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt3.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "ValidatedGet";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt4 = objdata.getData("TF_EBRC_IRM_uniqueTxIdList_GetProcessedorFailed", s1, s2);
            if (dt4.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt4.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt4.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Processed/failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTStatus_update", a1, a2, a3);
                }
            }

            SqlParameter b1 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter b2 = new SqlParameter("@type", "IRM");
            DataTable dt_2 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_list", b1, b2);// list of IRMORMStatusList_Get from TF_EBRC_IRM_ORM_JSONOutput where  UniqueTxId=a1 and RMORMStatusList_Get is not null and IRMORMStatus_Get!='Validated'
            if (dt_2.Rows.Count > 0)
            {
                for (int j = 0; j < dt_2.Rows.Count; j++)
                {
                    string json = dt_2.Rows[j].ItemArray.GetValue(0).ToString(); // It retrieves a value from the first column of the current row (at index j) in the dt2 DataTable and converts it to a string. This value appears to be in JSON format.
                    string uniquetxid = dt_2.Rows[j].ItemArray.GetValue(1).ToString();
                    //Creates DataTable 
                    DataTable dt11 = new DataTable(); // creates a new DataTable named as 'dt11'  with columns "irmNumber," "irmIssueDate," "ackStatus," "errorCode," and "errorDetails". This DataTable is intended to store structured data from the JSON content.
                    dt11.Columns.Add("irmNumber", typeof(string));
                    dt11.Columns.Add("irmIssueDate", typeof(string));
                    dt11.Columns.Add("ackStatus", typeof(string));
                    dt11.Columns.Add("errorCode", typeof(string));
                    dt11.Columns.Add("errorDetails", typeof(string));


                    JArray jsonArray = JArray.Parse(json); // It parses the JSON string json using the Newtonsoft.Json library (specifically JArray.Parse) into a JArray named jsonArray.

                    foreach (var item in jsonArray) // it extracts values from the JSON objects (elements) in jsonArray to populate variables like irmNumber, irmIssueDate, ackStatus, and extracts an array of error details.
                    {

                        JObject jsonObject = (JObject)item;

                        string irmNumber = jsonObject["irmNumber"].ToString();
                        string irmIssueDate = jsonObject["irmIssueDate"].ToString();
                        string ackStatus = jsonObject["ackStatus"].ToString();
                        string errorCode = "";
                        string errorDetails = "";
                        JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

                        foreach (var errorDetailsItem in errorDetailsArray) // It iterates through the error details array and extracts errorCode and errorDetails for each item.
                        {
                            JObject errorDetailsObject = (JObject)errorDetailsItem;

                            errorCode = errorDetailsObject["errorCode"].ToString();
                            errorDetails = errorDetailsObject["errorDetails"].ToString();
                            dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails); // The extracted values are added as a new row to the dt11 DataTable for each iteration.

                        }
                        dt11.Rows.Add(irmNumber, irmIssueDate, ackStatus, errorCode, errorDetails); // The extracted values are added as a new row to the dt11 DataTable for each iteration.

                    }


                    //insert datatatble into table
                    string result = "";
                    string irmno = "", irmissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
                    if (dt11.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt11.Rows.Count; k++)
                        {
                            irmno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
                            irmissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
                            ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
                            errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
                            errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
                            string irmunique = irmno + uniquetxid + errorcode;
                            SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                            SqlParameter i2 = new SqlParameter("@irmno", irmno);
                            SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate.Replace("/", ""));
                            SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
                            SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                            SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                            SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                            result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7); // If no matching record is found in TF_EBRC_IRMORM_DGFT_List table where irmormNumber+uniquetxid+errorCode=@irmunique, a new record is inserted into the [TF_EBRC_IRMORM_DGFT_List] table. 

                            result = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTUpdateMainTable", i2, i1, i4, i5, i6); // If no matching record is found in TF_EBRC_IRMORM_DGFT_List table where irmormNumber+uniquetxid+errorCode=@irmunique, a new record is inserted into the [TF_EBRC_IRMORM_DGFT_List] table. 

                            if (ackstatus == "Errored")
                            {
                                result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus", i2, i1);
                                result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                            }
                        }
                    }
                }
            }

            DataTable dt_3 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_ErrorDetaillist", b1, b2);//IRMORMErrorDetailsList
            if (dt_3.Rows.Count > 0)
            {
                for (int p = 0; p < dt_3.Rows.Count; p++)
                {
                    string json1 = dt_3.Rows[p].ItemArray.GetValue(0).ToString();
                    string uniquetxid = dt_3.Rows[p].ItemArray.GetValue(1).ToString();
                    //Creates DataTable 
                    DataTable dt12 = new DataTable();
                    dt12.Columns.Add("errorCode", typeof(string));
                    dt12.Columns.Add("errorDetails", typeof(string));
                    List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json1);

                    JArray jsonArray1 = JArray.Parse(json1);

                    foreach (var item in jsonArray1)
                    {

                        JObject jsonObject1 = (JObject)item;

                        string errorCode = jsonObject1["errorCode"].ToString();
                        string errorDetails = jsonObject1["errorDetails"].ToString();

                        dt12.Rows.Add(errorCode, errorDetails);
                    }

                    //insert datatatble into table
                    //get irm number for this errordetails list
                    SqlParameter z1 = new SqlParameter("@uniquetxid", uniquetxid);
                    DataTable dt_4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_IRMNo", z1);
                    string irmno_ = "";
                    string irmissuedate_ = "";
                    if (dt_4.Rows.Count > 0)
                    {
                        for (int q = 0; q < dt_4.Rows.Count; q++)
                        {
                            irmno_ = dt_4.Rows[q]["IRMNumber"].ToString();
                            irmissuedate_ = dt_4.Rows[q]["IRMIssueDate"].ToString();


                            string result = "";
                            string irmno = "", irmissuedate = "", errorcode = "", errordetails_ = "";
                            if (dt12.Rows.Count > 0)
                            {
                                for (int s = 0; s < dt12.Rows.Count; s++)
                                {
                                    irmno = irmno_;
                                    irmissuedate = irmissuedate_;
                                    errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
                                    errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
                                    string irmunique = irmno + uniquetxid + errorcode;
                                    SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                                    SqlParameter i2 = new SqlParameter("@irmno", irmno);
                                    SqlParameter i3 = new SqlParameter("@irmissuedate", irmissuedate.Replace("/", ""));
                                    SqlParameter i4 = new SqlParameter("@ackstatus", "");
                                    SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                                    SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                                    SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_DGFTUpdateMainTable", i2, i1, i4, i5, i6);

                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus", i2, i1);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                                }
                            }
                        }
                    }
                }
            }
        }
    public void DGFTStatusListORM()
    {
        
            TF_DATA objdata = new TF_DATA();
            SqlParameter s1 = new SqlParameter("@fromdate", txtfromDate.Text);
            SqlParameter s2 = new SqlParameter("@todate", txtfromDate.Text);
            DataTable dt1 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_PushValidate", s1, s2);

            if (dt1.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt1.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt1.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Validated";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt2 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_PushFailed", s1, s2);
            if (dt2.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt2.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt2.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt3 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_GetValidated", s1, s2);
            if (dt3.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt3.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt3.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "ValidatedGet";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            DataTable dt4 = objdata.getData("TF_EBRC_ORM_uniqueTxIdList_GetProcessedorFailed", s1, s2);
            if (dt4.Rows.Count > 0) // checks whether the number of rows in a DataTable (dt1) is greater than 0. if greater than 0 then it goes to the next line
            {
                for (int i = 0; i < dt4.Rows.Count; i++) // This loop iterates through the rows in the DataTable. It continues to execute the loop and increments i after each iteration.
                {
                    string uniquetxid = dt4.Rows[i]["BankUniqueTransactionId"].ToString();
                    string status = "Processed/failed";
                    SqlParameter a1 = new SqlParameter("@Status", status);
                    SqlParameter a2 = new SqlParameter("@uniquetxid", uniquetxid);
                    SqlParameter a3 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
                    string result_ = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTStatus_update", a1, a2, a3);
                }
            }

            SqlParameter b1 = new SqlParameter("@date", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter b2 = new SqlParameter("@type", "ORM");
            DataTable dt_2 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_list", b1, b2);// list of IRMORMStatusList_Get from TF_EBRC_IRM_ORM_JSONOutput where  UniqueTxId=a1 and RMORMStatusList_Get is not null and IRMORMStatus_Get!='Validated'
            if (dt_2.Rows.Count > 0)
            {
                for (int j = 0; j < dt_2.Rows.Count; j++)
                {
                    string json = dt_2.Rows[j].ItemArray.GetValue(0).ToString(); // It retrieves a value from the first column of the current row (at index j) in the dt2 DataTable and converts it to a string. This value appears to be in JSON format.
                    string uniquetxid = dt_2.Rows[j].ItemArray.GetValue(1).ToString();
                    //Creates DataTable 
                    DataTable dt11 = new DataTable();
                    dt11.Columns.Add("ormNumber", typeof(string));
                    dt11.Columns.Add("ormIssueDate", typeof(string));
                    dt11.Columns.Add("ackStatus", typeof(string));
                    dt11.Columns.Add("errorCode", typeof(string));
                    dt11.Columns.Add("errorDetails", typeof(string));


                    JArray jsonArray = JArray.Parse(json);

                    foreach (var item in jsonArray)
                    {

                        JObject jsonObject = (JObject)item;

                        string ormNumber = jsonObject["ormNumber"].ToString();
                        string ormIssueDate = jsonObject["ormIssueDate"].ToString();
                        string ackStatus = jsonObject["ackStatus"].ToString();
                        string errorCode = "";
                        string errorDetails = "";
                        JArray errorDetailsArray = (JArray)jsonObject["errorDetails"];

                        foreach (var errorDetailsItem in errorDetailsArray)
                        {
                            JObject errorDetailsObject = (JObject)errorDetailsItem;

                            errorCode = errorDetailsObject["errorCode"].ToString();
                            errorDetails = errorDetailsObject["errorDetails"].ToString();
                            dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);

                        }
                        dt11.Rows.Add(ormNumber, ormIssueDate, ackStatus, errorCode, errorDetails);
                    }


                    //insert datatatble into table
                    string result = "";
                    string ormno = "", ormissuedate = "", ackstatus = "", errorcode = "", errordetails_ = "";
                    if (dt11.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt11.Rows.Count; k++)
                        {
                            ormno = dt11.Rows[k].ItemArray.GetValue(0).ToString();
                            ormissuedate = dt11.Rows[k].ItemArray.GetValue(1).ToString();
                            ackstatus = dt11.Rows[k].ItemArray.GetValue(2).ToString();
                            errorcode = dt11.Rows[k].ItemArray.GetValue(3).ToString();
                            errordetails_ = dt11.Rows[k].ItemArray.GetValue(4).ToString();
                            string irmunique = ormno + uniquetxid + errorcode;
                            SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                            SqlParameter i2 = new SqlParameter("@irmno", ormno);
                            SqlParameter i3 = new SqlParameter("@irmissuedate", ormissuedate.Replace("/", ""));
                            SqlParameter i4 = new SqlParameter("@ackstatus", ackstatus);
                            SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                            SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                            SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                            result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

                            result = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTUpdateMainTable", i2, i1, i4, i5, i6); // If no matching record is found in TF_EBRC_IRMORM_DGFT_List table where irmormNumber+uniquetxid+errorCode=@irmunique, a new record is inserted into the [TF_EBRC_IRMORM_DGFT_List] table. 

                            if (ackstatus == "Errored")
                            {
                                result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus", i2, i1);
                                result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                            }
                        }
                    }

                }
            }

            DataTable dt_3 = objdata.getData("TF_EBRC_IRMORM_DGFT_Status_ErrorDetaillist", b1, b2);//IRMORMErrorDetailsList
            if (dt_3.Rows.Count > 0)
            {
                for (int p = 0; p < dt_3.Rows.Count; p++)
                {
                    string json1 = dt_3.Rows[p].ItemArray.GetValue(0).ToString();
                    string uniquetxid = dt_3.Rows[p].ItemArray.GetValue(1).ToString();
                    //Creates DataTable 
                    DataTable dt12 = new DataTable();
                    dt12.Columns.Add("errorCode", typeof(string));
                    dt12.Columns.Add("errorDetails", typeof(string));
                    List<JObject> jsonDataList = JsonConvert.DeserializeObject<List<JObject>>(json1);

                    JArray jsonArray1 = JArray.Parse(json1);

                    foreach (var item in jsonArray1)
                    {

                        JObject jsonObject1 = (JObject)item;

                        string errorCode = jsonObject1["errorCode"].ToString();
                        string errorDetails = jsonObject1["errorDetails"].ToString();

                        dt12.Rows.Add(errorCode, errorDetails);
                    }

                    //insert datatatble into table
                    //get irm number for this errordetails list
                    SqlParameter z1 = new SqlParameter("@uniquetxid", uniquetxid);
                    DataTable dt_4 = objdata.getData("TF_EBRC_IRMORM_ErrorDetaillist_Get_ORMNo", z1);
                    string ormno_ = "";
                    string ormissuedate_ = "";
                    if (dt_4.Rows.Count > 0)
                    {
                        for (int q = 0; q < dt_4.Rows.Count; q++)
                        {
                            ormno_ = dt_4.Rows[q]["ORMNumber"].ToString();
                            ormissuedate_ = dt_4.Rows[q]["ORMIssueDate"].ToString();

                            string result = "";
                            string ormno = "", ormissuedate = "", errorcode = "", errordetails_ = "";
                            if (dt12.Rows.Count > 0)
                            {
                                for (int s = 0; s < dt12.Rows.Count; s++)
                                {
                                    ormno = ormno_;
                                    ormissuedate = ormissuedate_;
                                    errorcode = dt12.Rows[s].ItemArray.GetValue(0).ToString();
                                    errordetails_ = dt12.Rows[s].ItemArray.GetValue(1).ToString();
                                    string irmunique = ormno + uniquetxid + errorcode;
                                    SqlParameter i1 = new SqlParameter("@uniquetxid", uniquetxid);
                                    SqlParameter i2 = new SqlParameter("@irmno", ormno);
                                    SqlParameter i3 = new SqlParameter("@irmissuedate", ormissuedate.Replace("/", ""));
                                    SqlParameter i4 = new SqlParameter("@ackstatus", "");
                                    SqlParameter i5 = new SqlParameter("@errorcode", errorcode);
                                    SqlParameter i6 = new SqlParameter("@errordetails", errordetails_.Replace("[]", ""));
                                    SqlParameter i7 = new SqlParameter("@irmunique", irmunique);
                                    result = objdata.SaveDeleteData("TF_EBRC_IRM_ORM_DGFTList", i1, i2, i3, i4, i5, i6, i7);

                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_DGFTUpdateMainTable", i2, i1, i4, i5, i6);

                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus", i2, i1);
                                    result = objdata.SaveDeleteData("TF_EBRC_ORM_UpdateDGFTFailedStatus_Injsoncreated", i2, i1);
                                }
                            }
                        }
                    }
                }
            }
        }
    public void PageAccess()
    {
        int C = 0;
        TF_DATA objData = new TF_DATA();
        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = Session["userName"].ToString();
        SqlParameter menuName = new SqlParameter("@menuName", SqlDbType.VarChar);
        menuName.Value = "EBRC IRM/ORM DGFT Responses";
        DataTable dt = objData.getData("TF_GetAccessed_Pages", pUserName, menuName);
        if (dt.Rows.Count > 0)
        {
            string menu_Name = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menu_Name = dt.Rows[i]["MenuName"].ToString();
                if (menu_Name == "EBRC IRM/ORM DGFT Responses")
                {
                    C = 1;
                }
            }
        }
        if (C != 1)
        {
            string result = "Access denied";

            //string redirectUrl = ResolveUrl("~/TF_Logout.aspx");
            string redirectUrl = ResolveUrl(ConfigurationManager.AppSettings["webpath"] + "0rJaTMnF39W4f93iMtXSg/");
            string script = "alert('" + result + "');";
            script += "window.location.href = '" + redirectUrl + "';";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "RedirectScript", script, true);

        }
    }
 
  
}