using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Sessionmanager
/// </summary>
public class Sessionmanager
{
    public Sessionmanager()
    {

    }

    public static bool IsSessionActive(string userId, string sessionId)
    {
        Encryption objEncryption = new Encryption();
        using (SqlConnection conn = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString)))
        {
            string query = "SELECT COUNT(*) FROM session_Stored WHERE UserID = @UserID AND SessionID = @SessionID AND LastActivityTime > @ExpirationTime";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserID", userId);
            command.Parameters.AddWithValue("@SessionID", sessionId);
            command.Parameters.AddWithValue("@ExpirationTime", System.DateTime.Now.AddMinutes(-20)); // Assuming session timeout is 20 minutes

            conn.Open();
            int sessionCount = (int)command.ExecuteScalar();

            return sessionCount > 0;
        }
    }

    public static void UpdateSessionActivity(string userId, string sessionId)
    {
        Encryption objEncryption = new Encryption();
        using (SqlConnection conn = new SqlConnection(objEncryption.decrypttext(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString)))
        {
            string query = "UPDATE session_Stored SET LastActivityTime = @LastActivityTime WHERE UserID = @UserID AND SessionID = @SessionID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserID", userId);
            command.Parameters.AddWithValue("@SessionID", sessionId);
            command.Parameters.AddWithValue("@LastActivityTime", System.DateTime.Now);

            conn.Open();
            command.ExecuteNonQuery();
        }
    }

 

 }