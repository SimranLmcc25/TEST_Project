using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

using System.Configuration;

/// <summary>
/// Summary description for ActiveDirectoryHelper
/// </summary>
public class ActiveDirectoryHelper
{
    private DirectoryEntry _directoryEntry = null;
	public ActiveDirectoryHelper()
	{
		
	}

    public class ADUserDetail
    {
        private String _loginName;
        private String _Password;


        public String LoginName
        {
            get { return _loginName; }
        }

        public String Password
        {
            get { return _Password; }
        }
    
    }

    private String LDAPPath
    {
        get
        {
            return ConfigurationManager.AppSettings["LDAPPath"];
        }
    }


    private String LDAPUser
    {
        get
        {
            return ConfigurationManager.AppSettings["LDAPUser"];
        }
    }


    private String LDAPPassword
    {
        get
        {
            return ConfigurationManager.AppSettings["LDAPPassword"];
        }
    }






     private  DirectoryEntry SearchRoot
        {
            get
            {
                if (_directoryEntry == null)
                {
                    _directoryEntry = new DirectoryEntry(LDAPPath, LDAPUser, LDAPPassword, AuthenticationTypes.Secure);
                }
                return _directoryEntry;
            }
        }

        //Get User by Login Name
        //This function will return AD user. This takes Login name as input parameter.

       public  ADUserDetail GetUserByLoginName(String userName)
        {
            try
            {
                _directoryEntry = null;
                DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
                directorySearch.Filter = "(&(objectClass=user)(SAMAccountName=" + userName + "))";
                SearchResult results = directorySearch.FindOne();
 
                if (results != null)
                {
                    DirectoryEntry user = new DirectoryEntry(results.Path, LDAPUser, LDAPPassword);
                    //return ADUserDetail.GetUser(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


       public ADUserDetail GetUserByFullName(String userName)
       {
           try
           {
               _directoryEntry = null;
               DirectorySearcher directorySearch = new DirectorySearcher(SearchRoot);
               directorySearch.Filter = "(&(objectClass=user)(cn=" + userName + "))";
               SearchResult results = directorySearch.FindOne();

               if (results != null)
               {
                   DirectoryEntry user = new DirectoryEntry(results.Path, LDAPUser, LDAPPassword);
                   return null;
               }
               else
               {
                   return null;
               }
           }
           catch (Exception ex)
           {
               return null;
           }
       }

      //public static ADUserDetail GetUser(DirectoryEntry directoryUser)
      //  {
      //      //return new ADUserDetail(directoryUser);
      //  }
    

}