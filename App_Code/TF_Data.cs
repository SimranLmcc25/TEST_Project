using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using NLog;
/// <summary>
/// Summary description for TF_DATA
/// </summary>
public class TF_DATA
{
	SqlConnection con;
	SqlDataAdapter da;
	public SqlCommand cmd;
	DataSet ds;
    string query;
    Encryption objEncryption = new Encryption();
    private static Logger logger = LogManager.GetCurrentClassLogger(); 
	

	public void connection()
	{
        //con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		if (con.State == ConnectionState.Closed)
		{
			con.Open();
		}
	}
	public TF_DATA()
	{
	
	}
	public DataTable getData(string query,params SqlParameter[] queryParameters)
	{
		DataTable dt = new DataTable();
		SqlDataAdapter da = new SqlDataAdapter();
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand(query, con);
		try
		{
			con.Open();
            
			com.CommandType = CommandType.StoredProcedure;
            com.CommandTimeout = 0;
			//com.Parameters.AddWithValue("@search", search);
			foreach (SqlParameter param in queryParameters)
				com.Parameters.Add(param);
			da.SelectCommand = com;
			da.Fill(dt);
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		catch (Exception ex)
		{
			dt.Rows.Clear();
            logger.Error("Error exception in the " + query + " The exception message is " + ex.Message);
            
           
		}
		finally
		{
            com.Parameters.Clear();
			da.Dispose();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return dt;
	}

	public string SaveDeleteData(string query,params SqlParameter[] queryParameters)
	  {
		string _result = "";
        //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand(query, con);
        com.CommandTimeout = 999999999;
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			foreach (SqlParameter param in queryParameters)
				com.Parameters.Add(param);
			_result = com.ExecuteScalar().ToString();
		}
		catch (Exception ex)
		{
			_result = ex.Message.Trim();
            logger.Error("Error exception in the " + query + " The exception message is " + ex.Message);
		}
		finally
		{
			com.Parameters.Clear();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return _result;
	}

	public string GetServerName()
	{
		string _result = "";
	//	SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_GetServerName", con);

		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			_result = com.ExecuteScalar().ToString();
		}
		catch (Exception ex)
		{
			_result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_GetServerName" + " The exception message is " + ex.Message);
		}
		finally
		{
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return _result;
	}
	public DataTable AlertUser(string _userName, string _password)
	{
		DataTable dt = new DataTable();
		SqlDataAdapter da = new SqlDataAdapter();
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_AlertUser", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			com.Parameters.AddWithValue("@password", _password);
			da.SelectCommand = com;
			da.Fill(dt);
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		catch (Exception ex)
		{
			dt.Rows.Clear();
            logger.Error("Error exception in the " + "TF_AlertUser" + " The exception message is " + ex.Message);
		}
		finally
		{
			da.Dispose();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return dt;
	}
	public DataTable authenticateUser(string _userName, string _password)
	{
		DataTable dt = new DataTable();
		SqlDataAdapter da = new SqlDataAdapter();
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_AuthenticateUser", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			com.Parameters.AddWithValue("@password", _password);
			da.SelectCommand = com;
			da.Fill(dt);
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		catch (Exception ex)
		{
			dt.Rows.Clear();
            logger.Error("Error exception in the " + "TF_AuthenticateUser" + " The exception message is " + ex.Message);
		}
		finally
		{
			da.Dispose();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return dt;
	}
	public DataTable CheckUser(string _userName, string _password)
	{
		DataTable dt = new DataTable();
		SqlDataAdapter da = new SqlDataAdapter();
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_CHECKUSEREXISTS", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			com.Parameters.AddWithValue("@password", _password);
			da.SelectCommand = com;
			da.Fill(dt);
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		catch (Exception ex)
		{
			dt.Rows.Clear();
            logger.Error("Error exception in the " + "TF_CHECKUSEREXISTS" + " The exception message is " + ex.Message);
		}
		finally
		{
			da.Dispose();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return dt;
	}

	public DataTable userList(string search, string username)
	{
		DataTable dt = new DataTable();
		SqlDataAdapter da = new SqlDataAdapter();
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_GetUserList", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@search", search);
			com.Parameters.AddWithValue("@username", username);
			da.SelectCommand = com;
			da.Fill(dt);
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		catch (Exception ex)
		{
			dt.Rows.Clear();
            logger.Error("Error exception in the " + "TF_GetUserList" + " The exception message is " + ex.Message);
		}
		finally
		{
			da.Dispose();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return dt;
	}

	public string deleteUserDetails(string _userName)
	{
		string _result = "";
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_DeleteUserDetails", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			com.ExecuteNonQuery();
			_result = "deleted";
		}
		catch (Exception ex)
		{
			_result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_GetUserList" + " The exception message is " + ex.Message);
		}
		finally
		{
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return _result;
	}

	public string UpdateUserPassword(string _userName, string _password)
	{
		string _result = "";
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_UpdateUserPassword", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			com.Parameters.AddWithValue("@password", _password);
			_result = com.ExecuteScalar().ToString();
		}
		catch (Exception ex)
		{
			_result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_UpdateUserPassword" + " The exception message is " + ex.Message);
		}
		finally
		{
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return _result;
	}

	public string UpdateUserExpiryDate(string _userName)
	{
		string _result = "";
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_UpdateUserExpiry", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			com.Parameters.AddWithValue("@passwordexpirydate", DateTime.Now.AddMonths(1));
			_result = com.ExecuteScalar().ToString();
		}
		catch (Exception ex)
		{
			_result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_UpdateUserExpiry" + " The exception message is " + ex.Message);
		}
		finally
		{
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return _result;
	}

    public string UserStatusUpdate(int id, string status)
    {
        string _result = "";
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
        SqlCommand com = new SqlCommand("TF_UpdateUserStatus", con);
        try
        {
            con.Open();
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            com.Parameters.AddWithValue("@UserUpdateStatus", status);
            _result = com.ExecuteNonQuery().ToString();
        }
        catch (Exception ex)
        {
            _result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_UpdateUserStatus" + " The exception message is " + ex.Message);
        }
        finally
        {
            com.Dispose();
            con.Close();
            con.Dispose();
        }
        return _result;
    }

	public string addUpdateUserDetails(string _mode, string _userName, string _password, string _role, string _active,string _adcode)
	{
		string _result = "";
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_UpdateUserDetails", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@mode", _mode);
			com.Parameters.AddWithValue("@username", _userName);
			com.Parameters.AddWithValue("@password", _password);
			com.Parameters.AddWithValue("@role", _role);
			com.Parameters.AddWithValue("@active", _active);
			com.Parameters.AddWithValue("@passwordexpirydate", System.DateTime.Now.AddMonths(1));
            com.Parameters.AddWithValue("@adcode", _adcode);
	   
			_result = com.ExecuteScalar().ToString();
		}
		catch (Exception ex)
		{
			_result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_UpdateUserDetails" + " The exception message is " + ex.Message);
		}
		finally
		{
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return _result;
	}
    public string addUpdateUserDetails(string _mode, string _userName, string _password, string _role, string _active, string _adcode, string _emailid)
    {
        string _result = "";
        //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
        SqlCommand com = new SqlCommand("TF_UpdateUserDetails", con);
        try
        {
            con.Open();
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@mode", _mode);
            com.Parameters.AddWithValue("@username", _userName);
            com.Parameters.AddWithValue("@password", _password);
            com.Parameters.AddWithValue("@role", _role);
            com.Parameters.AddWithValue("@active", _active);
            com.Parameters.AddWithValue("@passwordexpirydate", System.DateTime.Now.AddMonths(1));
            com.Parameters.AddWithValue("@adcode", _adcode);
            com.Parameters.AddWithValue("@emailid", _emailid);

            _result = com.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            _result = ex.Message.Trim();
            logger.Error("Error exception in the " + "TF_UpdateUserDetails" + " The exception message is " + ex.Message);
        }
        finally
        {
            com.Dispose();
            con.Close();
            con.Dispose();
        }
        return _result;
    }
	public DataTable getUserDetails(string _userName)
	{
		DataTable dt = new DataTable();
		SqlDataAdapter da = new SqlDataAdapter();
		//SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString);
        con = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString));
		SqlCommand com = new SqlCommand("TF_GetUserDetails", con);
		try
		{
			con.Open();
			com.CommandType = CommandType.StoredProcedure;
			com.Parameters.AddWithValue("@username", _userName);
			da.SelectCommand = com;
			da.Fill(dt);
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		catch (Exception ex)
		{
			dt.Rows.Clear();
            logger.Error("Error exception in the " + "TF_GetUserDetails" + " The exception message is " + ex.Message);
		}
		finally
		{
			da.Dispose();
			com.Dispose();
			con.Close();
			con.Dispose();
		}
		return dt;
	}


	public DataSet databind(string query)
	{
		try
		{
			ds = new DataSet();
			connection();
			// da = new SqlDataAdapter(cmd);
			da = new SqlDataAdapter(query, con);
			da.Fill(ds);
			return ds;

		}
		catch (Exception ex)
		{
			return ds;
			// throw;
		}
		finally
		{
			if (con.State == ConnectionState.Open)
			{
				con.Close();
				con.Dispose();

			}
		}
	}
	public DataSet databind1para(string query, string id)
	{
	   
		try
		{
			ds = new DataSet();
			connection();
			cmd = new SqlCommand(query, con);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@id", id);
			da = new SqlDataAdapter(cmd);
			da.Fill(ds);
			return ds;

		}
		catch (Exception)
		{

			return ds;
		}
		finally
		{
			if (con.State == ConnectionState.Open)
			{
				cmd.Dispose();
				con.Close();
				con.Dispose();
			}
		}
	}

    public string SaveDeleteData(SqlParameter Bank_Unique_TransactionId, SqlParameter IRM_issueDate, SqlParameter IRM_Number, SqlParameter IFSC_Code, SqlParameter Remittance_ADCode, SqlParameter Remittance_date, SqlParameter Remittance_FCC, SqlParameter Remittance_FCCAmount, SqlParameter INRCredit_Amount, SqlParameter Exchange_Rate, SqlParameter IEC_Code, SqlParameter Pan_Number, SqlParameter Remitter_Name, SqlParameter Remitter_country, SqlParameter Purpose_ofRemittance, SqlParameter Mode_ofPayment, SqlParameter Factoring_flag, SqlParameter Forfeiting_flag)
    {
        throw new NotImplementedException();
    }
}

public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
{

	// local variable for network credential.
	private string _UserName;
	private string _PassWord;
	private string _DomainName;
	public CustomReportCredentials(string UserName, string PassWord, string DomainName)
	{
		_UserName = UserName;
		_PassWord = PassWord;
		_DomainName = DomainName;
	}
	public System.Security.Principal.WindowsIdentity ImpersonationUser
	{
		get
		{
			return null;  // not use ImpersonationUser
		}
	}
	public System.Net.ICredentials NetworkCredentials
	{
		get
		{

			// use NetworkCredentials
			return new System.Net.NetworkCredential(_UserName, _PassWord, _DomainName);
		}
	}
	public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string user, out string password, out string authority)
	{

		// not use FormsCredentials unless you have implements a custom autentication.
		authCookie = null;
		user = password = authority = null;
		return false;
	}


}