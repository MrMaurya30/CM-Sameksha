using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Net;


public class TABP_Functions
   
{
    
    DAL objDAL = new DAL();
    DataSet ds;
    DataTable dt;
    string str;
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter da;
    private Random rand = new Random();
    private List<int> used = new List<int>();
	public TABP_Functions()
	{
		
	}
    public string RandomNo()
    {
        char[] arrPossibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        int intPasswordLength = 16;
        string stringPassword = null;
        System.Random rand = new Random();
        int i = 0;
        for (i = 0; (i <= intPasswordLength); i++)
        {
            int intRandom = rand.Next(arrPossibleChars.Length);
            stringPassword = (stringPassword + arrPossibleChars[intRandom].ToString());
        }
        return stringPassword;
    }
    public string RandomPassword()
    {
        char[] arrPossibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        int intPasswordLength = 16;
        string stringPassword = null;
        System.Random rand = new Random();
        int i = 0;
        for (i = 0; (i <= intPasswordLength); i++)
        {
            int intRandom = rand.Next(arrPossibleChars.Length);
            stringPassword = (stringPassword + arrPossibleChars[intRandom].ToString());
        }
        return stringPassword;
    }
    public string GetIPAddress()
    {
        //System.Web.HttpContext context = System.Web.HttpContext.Current;
        //string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //if (!string.IsNullOrEmpty(ipAddress))
        //{
        //    string[] addresses = ipAddress.Split(',');
        //    if (addresses.Length != 0)
        //    {
        //        return addresses[0];
        //    }
        //}
        //return context.Request.ServerVariables["REMOTE_ADDR"];

        string ip = "";
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addr = ipEntry.AddressList;
        ip = addr[1].ToString();
        return ip;
    }
    public int randomNonrepeating()
    {
        int i = rand.Next();
        while (used.Contains(i))
            i = rand.Next();
        used.Add(i);
        return i;
    }
    public string GetHashWIPAddress(string s)
    {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipaddress = Dns.GetHostEntry(strHostName);
        string enc = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((ipaddress + s), "MD5");
        return enc;
    }
    public string DecodeBase64(string encodedString)
    {
        byte[] data = Convert.FromBase64String(encodedString);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return decodedString;
    }

    public string Base64Encode(string text)
    {
        var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
        return System.Convert.ToBase64String(textBytes);
    }
}