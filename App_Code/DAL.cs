using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    string SqlCon = System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

    public DAL()
    {
    }
    public string GetTextFromHtml(string htmlContent)
    {
        // Remove HTML tags using a regular expression
        string text = Regex.Replace(htmlContent, "<.*?>", string.Empty);

        return text;
    }
    public DataTable ExecuteSQL(string cmd, bool IsProcedure)
    {
        using (SqlDataAdapter da = new SqlDataAdapter(cmd, SqlCon))
        {
            if (IsProcedure) da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public DataTable ExecuteSQL(string cmd, Hashtable Param, bool IsProcedure)
    {
        using (SqlDataAdapter da = new SqlDataAdapter(cmd, SqlCon))
        {
            if (IsProcedure) da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Param != null)
                foreach (DictionaryEntry de in Param)
                    da.SelectCommand.Parameters.AddWithValue(de.Key.ToString(), de.Value);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    public DataSet ExecuteSQL_returnDataset(string cmd, Hashtable Param, bool IsProcedure)
    {
        using (SqlDataAdapter da = new SqlDataAdapter(cmd, SqlCon))
        {
            if (IsProcedure) da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Param != null)
                foreach (DictionaryEntry de in Param)
                    da.SelectCommand.Parameters.AddWithValue(de.Key.ToString(), de.Value);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }
    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    public static string SHA512(string input)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        using (var hash = System.Security.Cryptography.SHA512.Create())
        {
            var hashedInputBytes = hash.ComputeHash(bytes);

            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new System.Text.StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }

    public int ExecuteNonQuery(string cmdText, Hashtable parameters, bool isProcedure)
    {
        using (SqlConnection con = new SqlConnection(SqlCon))
        using (SqlCommand cmd = new SqlCommand(cmdText, con))
        {
            cmd.CommandType = isProcedure ? CommandType.StoredProcedure : CommandType.Text;

            if (parameters != null)
            {
                foreach (DictionaryEntry param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key.ToString(), param.Value);
                }
            }

            con.Open();
            return cmd.ExecuteNonQuery();
        }
    }

}