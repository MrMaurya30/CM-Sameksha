using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBmanager
/// </summary>
public class DBmanager
{
    SqlConnection con;
    public SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;
    DataSet ds;
    private List<SqlParameter> Parameters = new List<SqlParameter>();
    public string MyCmdText { get; set; }
    public SqlCommand sp_cmd { get; set; }
    public CommandType CommandType { get; set; }
    public DBmanager()
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        cmd = new SqlCommand();
        da = new SqlDataAdapter(); 
         dt = new DataTable();
        sp_cmd = new SqlCommand();
        cmd.Connection = con;
        sp_cmd.Connection = con;
    }

    public void OpenDB()
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
    }
    public void CloseDB()
    {
        if (con.State == ConnectionState.Open)
            con.Close();
    }

    public DataSet FN_ExecuteQuery(string proc)
    {

        cmd = new SqlCommand();
        da = new SqlDataAdapter();
        ds = new DataSet();
        try
        {
            OpenDB();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "execute " + proc;
            cmd.CommandTimeout = 2000000;
            da.SelectCommand = cmd;
            da.Fill(ds);
            CloseDB();
            return ds;
        }
        catch (Exception ex)
        {
            CloseDB();
            return ds;
        }
    }
    void FN_OpenConnection()
    {
        // Get connection string from config
        string str = System.Configuration.ConfigurationSettings.AppSettings.Get("connection");
        if (string.IsNullOrEmpty(str))
        {
            throw new Exception("Connection string is missing in configuration.");
        }

        // Initialize connection if null
        if (con == null)
        {
            con = new SqlConnection();
        }

        // Parse password (if needed)
        string strPasswd = string.Empty;
        string[] strSplit = str.Split(';');
        if (strSplit.Length > 4)
        {
            strPasswd = strSplit[4].Replace("Password=", "");
            // TODO: decrypt password if needed, then rebuild connection string
        }

        // Set connection string
        con.ConnectionString = str;

        // Open connection safely
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }

        con.Open();
    }

    //void FN_OpenConnection()
    //{
    //    if (con.State == ConnectionState.Open)
    //    {
    //        con.Close();
    //    }

    //    //change by vinit 
    //    string strPasswd = string.Empty;
    //    string str = System.Configuration.ConfigurationSettings.AppSettings.Get("connection");
    //    string[] strSplit = str.Split(';');
    //    if (strSplit.Length > 3)
    //    {
    //        strPasswd = strSplit[4].ToString();
    //        strPasswd = strSplit[4].ToString().Replace("Password=", "");
    //    }
    //   // string strPasswd1 = Decrypt(strPasswd.Replace(" ", "+"));
    //  //  con.ConnectionString = str.Replace(strPasswd, strPasswd1);

    //   // string testdata = Encrypt("@#Mnbvcxz@987!");

    //    //  / change by vinit 
    //    con.Open();
    //}
    void FN_CloseConnection()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }


    //public bool ExecuteStoredProcedure()
    //{
    //    try
    //    {
    //        sp_cmd.CommandType = CommandType.StoredProcedure;
    //        OpenDB();
    //        sp_cmd.ExecuteNonQuery();
    //        CloseDB();
    //        sp_cmd.Dispose();
    //        return true;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //}

    public DataTable ReadDataFromStoredProc()
    {
        try
        {
            sp_cmd.CommandType = CommandType.StoredProcedure;
            OpenDB();

            DataTable dt = new DataTable();
            dt.Load(sp_cmd.ExecuteReader());

            CloseDB();
            sp_cmd.Dispose();
            return dt;
        }
        catch (Exception ex)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Error");
            DataRow dr = dtNew.NewRow();
            dr["Error"] = ex.Message.ToString();
            dtNew.Rows.Add(dr);
            return dtNew;
        }
    }

    public DataSet ReadDataSetFromStoredProc()
    {
        try
        {
            sp_cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sp_cmd);

            OpenDB();

            DataSet ds = new DataSet();
            da.Fill(ds);

            CloseDB();
            sp_cmd.Dispose();
            return ds;
        }
        catch (Exception ex)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Error");
            DataRow dr = dtNew.NewRow();
            dr["Error"] = ex.Message.ToString();
            dt.Rows.Add(dr);
            DataSet ds = new DataSet();
            ds.Tables.Add(dtNew);
            return ds;
        }
    }

    public bool ExecuteInsertUpdateOrDelete()
    {
        try
        {
            cmd.CommandText = MyCmdText;
            OpenDB();

            int n = cmd.ExecuteNonQuery();
            CloseDB();
            cmd.Dispose();
            return n > 0 ? true : false;
        }
        catch (Exception ex)
        {           
           return false;
        }

    }

    public object ReadSingleValue()
    {
        try
        {
            cmd.CommandText = MyCmdText;
            OpenDB();

            object ob = cmd.ExecuteScalar();
            CloseDB();
            cmd.Dispose();
            return ob;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public DataTable ReadBulkRecord()
    {
        try
        {
            dt = new DataTable();
            cmd.CommandText = MyCmdText;
            da.SelectCommand = cmd;
            da.Fill(dt);

            cmd.Dispose();
            da.Dispose();

            return dt;
        }
        catch (Exception ex)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Error");
            DataRow dr = dtNew.NewRow();
            dr["Error"] = ex.Message.ToString();
            dtNew.Rows.Add(dr);
            return dtNew;
        }
    }

    public DataTable ExecuteSelect(string Query)
    {
        try
        {
            cmd.CommandText = Query;
            dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Error");
            DataRow dr = dtNew.NewRow();
            dr["Error"] = ex.Message.ToString();
            dtNew.Rows.Add(dr);
            return dtNew;
        }
    }

    public DataTable ExecProcDataTable(string ProName, SqlParameter[] Param)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(ProName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter prm in Param)
            {
                cmd.Parameters.Add(prm);
            }
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
        return dt;
    }
    public void AddParameter(string name, object value)
    {
        SqlParameter param = new SqlParameter(name, value);
        cmd.Parameters.Add(param);
    }

   

    public void ClearParameters()
    {
        Parameters.Clear();
    }

    public bool ExecuteStoredProcedure()
    {
        try
        {
            if (string.IsNullOrEmpty(MyCmdText))
                throw new Exception("CommandText");

          //  sp_cmd.Parameters.Clear(); 
            sp_cmd.CommandText = MyCmdText; 
            sp_cmd.CommandType = CommandType.StoredProcedure;

         
            foreach (SqlParameter param in Parameters)
            {
                sp_cmd.Parameters.Add(param);
            }

            OpenDB();
            sp_cmd.ExecuteNonQuery();
            CloseDB();
            return true;
        }
        catch (Exception ex)
        {
            // Optional: log the exception
            return false;
        }
    }
    }