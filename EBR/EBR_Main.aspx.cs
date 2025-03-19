using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.ServiceProcess;
using System.Web.Script.Serialization;


public partial class EBR_EBR_Main : System.Web.UI.Page
{
    StringBuilder str = new StringBuilder();
    Encryption objEncryption = new Encryption();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedUserId"] == null)
        {
            Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
            //Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "AO0gtPK5RIS5S1JzBJeCQ/" + objEncryption.URLIDEncription("yes") + "/" + "");
        }
        if (Session["userName"] == null)
        {

            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "6e3gDQCN6bWP1Pggg4KDsg/" + objEncryption.URLIDEncription("yes") + "/" + objEncryption.URLIDEncription(lbl.Value));

        }



        if (!IsPostBack)
        {


            int year = DateTime.Now.Year;
            for (int i = year - 10; i <= year + 10; i++)
            {
                ListItem li = new ListItem(i.ToString());
                ddlYear.Items.Add(li);
            }

            if (Session["Year"] != null)
            {
                ddlYear.Items.FindByText(Session["Year"].ToString()).Selected = true;
            }
            else
            {
                ddlYear.Items.FindByText(year.ToString()).Selected = true;
            }

            //string Year = System.DateTime.Now.ToString("yyyy");
            ddlYear.SelectedValue = System.DateTime.Now.ToString("yyyy");
            DDMonth.SelectedValue = DateTime.Now.ToString("MM");

            txtYearMonth.Text = ddlYear.SelectedValue;
            txtYearMonth.Focus();
            irmCount();
            ormCount();





        }
        if (Request.QueryString["year"] == "send")
        {
            if (Session["UserName"] == null)
            {
                //  Response.Redirect("TF_Login.aspx");
                Response.Redirect(ConfigurationManager.AppSettings["webpath"] + "PEMbASreFg4ScR4KSkCZjg", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    ddlYear.Text = Session["Year"].ToString();
                }
            }
        }


        string serviceName1 = "ANZ_DGFT_PUSH_IRM";
        bool isRunning1 = IsServiceRunning(serviceName1);

        if (isRunning1)
        {
            lblDGFTSERVICEStatus.Text = "Running";
            lblDGFTSERVICEStatus.ForeColor = System.Drawing.Color.Green;
            lblDGFTSERVICEStatus.Font.Bold = true;
        }
        else
        {
            lblDGFTSERVICEStatus.Text = "Stopped";
            lblDGFTSERVICEStatus.ForeColor = System.Drawing.Color.Red;
            lblDGFTSERVICEStatus.Font.Bold = true;
        }

        string serviceName2 = "ANZ_DGFT_GET_IRM";
        bool isRunning2 = IsServiceRunning(serviceName2);

        if (isRunning2)
        {
            lblGetIRMStatus_ServiceStatus.Text = "Running";
            lblGetIRMStatus_ServiceStatus.ForeColor = System.Drawing.Color.Green;
            lblGetIRMStatus_ServiceStatus.Font.Bold = true;
        }
        else
        {
            lblGetIRMStatus_ServiceStatus.Text = "Stopped";
            lblGetIRMStatus_ServiceStatus.ForeColor = System.Drawing.Color.Red;
            lblGetIRMStatus_ServiceStatus.Font.Bold = true;
        }

        string serviceName3 = "ANZ_DGFT_PUSH_ORM";
        bool isRunning3 = IsServiceRunning(serviceName3);

        if (isRunning3)
        {
            lblDGFTORMAPI_PUSHStatus.Text = "Running";
            lblDGFTORMAPI_PUSHStatus.ForeColor = System.Drawing.Color.Green;
            lblDGFTORMAPI_PUSHStatus.Font.Bold = true;
        }
        else
        {
            lblDGFTORMAPI_PUSHStatus.Text = "Stopped";
            lblDGFTORMAPI_PUSHStatus.ForeColor = System.Drawing.Color.Red;
            lblDGFTORMAPI_PUSHStatus.Font.Bold = true;
        }

        string serviceName4 = "ANZ_DGFT_GET_ORM";
        bool isRunning4 = IsServiceRunning(serviceName4);

        if (isRunning4)
        {
            lblDGETORMStatus_ServiceStatus.Text = "Running";
            lblDGETORMStatus_ServiceStatus.ForeColor = System.Drawing.Color.Green;
            lblDGETORMStatus_ServiceStatus.Font.Bold = true;
        }
        else
        {
            lblDGETORMStatus_ServiceStatus.Text = "Stopped";
            lblDGETORMStatus_ServiceStatus.ForeColor = System.Drawing.Color.Red;
            lblDGETORMStatus_ServiceStatus.Font.Bold = true;
        }


    }
    protected void txtYearMonth_TextChanged(object sender, EventArgs e)
    {

        irmCount();
        ormCount();

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        irmCount();
        ormCount();


    }
    protected void bthGo_Click(object sender, EventArgs e)
    {


    }
    protected void irmCount()
    {
        TF_DATA objdata = new TF_DATA();
        int Year = System.DateTime.Today.Year;

        SqlParameter p0 = new SqlParameter("@Year", ddlYear.SelectedValue);
        SqlParameter p1 = new SqlParameter("@Month", DDMonth.SelectedValue);
        string script = "IRM_Count";
        DataTable dt = objdata.getData(script, p0, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            lblIRMUpload.Text = dt.Rows[0]["IRMNumber"].ToString();
            lblIRMAprrove.Text = dt.Rows[0]["Approve"].ToString();
            lblIRMProcessed.Text = dt.Rows[0]["Processed"].ToString();
            if (lblIRMUpload.Text == "")
            {
                lblIRMUpload.Text = "0";
            }
            if (lblIRMAprrove.Text == "")
            {
                lblIRMAprrove.Text = "0";
            }
            if (lblIRMProcessed.Text == "")
            {
                lblIRMProcessed.Text = "0";
            }


        }
        if (dt.Rows.Count == 0)
        {
            lblIRMUpload.Text = "0";
            lblIRMAprrove.Text = "0";
            lblIRMProcessed.Text = "0";
        }


    }
    protected void ormCount()
    {
        TF_DATA objdata = new TF_DATA();
        int Year = System.DateTime.Today.Year;

        SqlParameter p0 = new SqlParameter("@Year", ddlYear.SelectedValue);
        SqlParameter p1 = new SqlParameter("@Month", DDMonth.SelectedValue);
        string script = "ORM_Count";
        DataTable dt = objdata.getData(script, p0, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            lblORMUploaded.Text = dt.Rows[0]["ORMNumber"].ToString();
            lblORMApproved.Text = dt.Rows[0]["Approve"].ToString();
            lblORMProcessed.Text = dt.Rows[0]["Processed"].ToString();


            if (lblORMUploaded.Text == "")
            {
                lblORMUploaded.Text = "0";
            }
            if (lblORMApproved.Text == "")
            {
                lblORMApproved.Text = "0";
            }
            if (lblORMProcessed.Text == "")
            {
                lblORMProcessed.Text = "0";
            }


        }
        if (dt.Rows.Count == 0)
        {
            lblORMUploaded.Text = "0";
            lblORMApproved.Text = "0";
            lblORMProcessed.Text = "0";
        }


    }
    protected void DDMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        irmCount();
        ormCount();
    }
    public bool IsServiceRunning(string serviceName)
    {
        //ServiceController sc = new ServiceController(serviceName, "server2");
        // lblhostname.Text = "LAPTOP-07S00PJ0";
        ServiceController sc = new ServiceController(serviceName, lblhostname.Text);
        try
        {
            // Check the status of the service.
            ServiceControllerStatus status = sc.Status;

            // If the service status is Running, return true.
            return (status == ServiceControllerStatus.Running);
        }
        catch (InvalidOperationException)
        {
            // The service doesn't exist or cannot be accessed.
            return false;
        }
    }

    public string GetChartData()
    {
        DataTable dt = GetRecordCounts();
        var chartData = new List<object>();

        bool allValuesAreZero = CheckAllValuesAreZero(dt);
        if (allValuesAreZero)
        {
          //If all values are zero, set a single dummy value to display all segments
            chartData.Add(new { name = "No Data", y = 0 });
        }
        else
        {
            DataRow row = dt.Rows.Count > 0 ? dt.Rows[0] : null;
         
            if (row != null)
            {
                chartData.Add(new { name = "IRM Fresh", y = Convert.ToInt32(row["IRM_Fresh"]) });
                chartData.Add(new { name = "IRM Amended", y = Convert.ToInt32(row["IRM_Amended"]) });
                chartData.Add(new { name = "IRM Cancelled", y = Convert.ToInt32(row["IRM_Cancelled"]) });
             
            }
           
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(chartData);
    }

    private DataTable GetRecordCounts()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p0 = new SqlParameter("@Year", ddlYear.SelectedValue);
        SqlParameter p1 = new SqlParameter("@Month", DDMonth.SelectedValue);
        DataTable dt = objData.getData("IRM_ORM_StatusCount", p0, p1);
        return dt;

    }

    private bool CheckAllValuesAreZero(DataTable dt1)
    {
        //ORM_StatusCount
        bool allValuesAreZero = true;

        if (dt1.Rows.Count > 0)
        {
            DataRow row = dt1.Rows[0];
            allValuesAreZero &= (Convert.ToInt32(row["IRM_Fresh"]) == 0) &&
                                (Convert.ToInt32(row["IRM_Amended"]) == 0) &&
                                (Convert.ToInt32(row["IRM_Cancelled"]) == 0);
        }

      return allValuesAreZero;
    }


}