using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DivisionLogin : System.Web.UI.Page
{
    DBClass DB = new DBClass();
    TABP_Functions objFunc = new TABP_Functions();
    public string Hasvalue = "";
    private Random rand = new Random();
    string IPAddress = string.Empty;
    private List<int> used = new List<int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string loginType = Request.QueryString["type"];
            if (!string.IsNullOrEmpty(loginType))
            {
                Session["type"] = loginType.ToString();
                string captcha = GenerateCaptcha();

                Session["Captcha"] = captcha;
                lblCaptcha.Text = captcha;
            }
            BindDivision();

        }
    }
    protected void BindDivision()
    {
        try
        {
            DataTable dt = DB.BindDivision();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataTextField = "Div_name";
                ddlDivision.DataValueField = "Div_code";
                ddlDivision.DataBind();
            }
            else
            {
                ddlDivision.DataSource = null;
                ddlDivision.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCaptcha.Text.Trim() != (Session["Captcha"] == null ? "" : Session["Captcha"].ToString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Captcha ❌');", true);

                string captcha = DB.GenerateCaptcha();
                Session["Captcha"] = captcha;
                lblCaptcha.Text = captcha;

                return;
            }
            string ip = Request.UserHostAddress;
            DataTable dt = DB.LoginDivision(ddlDivision.SelectedValue.ToString(), txtPassword.Text.Trim(), ip);
            if (dt == null || dt.Rows.Count == 0 || dt.Columns.Contains("Message"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Invalid Username or Password ❌');", true);
                return;
            }
            String DeptName = ddlDivision.SelectedItem.Text.ToString();// dt.Rows[0]["DeptName"].ToString();
            string UserName = dt.Rows[0]["UserName"].ToString();
            string usertype = dt.Rows[0]["UserType"].ToString();

            Session["Username"] = UserName;
            Session["RoleID"] = "3";
            Session["DeptName"] = DeptName;
            if (usertype == "Division")
            {
                RegenerateId();
                //ISSUE/CREATE AUTHENTICATION TICKET
                FormsAuthentication.SetAuthCookie(UserName, false);
                FormsAuthenticationTicket EntReg = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddMinutes(30), false, UserName, FormsAuthentication.FormsCookiePath);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(EntReg));
                Response.Cookies.Add(cookie);

                //CREATE AUTHORIZATION COOKIE AND SESSION (SAFE-GUARDING ROLE INFRINGEMENT)
                HttpCookie UsrCookie = new HttpCookie("UsrCookie");
                UsrCookie.Value = objFunc.RandomPassword();
                UsrCookie.Name = "UsrCookie";
                Response.Cookies.Add(UsrCookie);
                Session["UsrCookie"] = UsrCookie.Value;

                Session["ChangePassword"] = "";
                Session["Entry"] = "";
                Session.Timeout = 60;
                genrate_Token_Cookies();
                //IPAddress = objLoggedUserRecord.GetIPAddress();
                //Todaydatetime = DateTime.Now.ToString();
                //objLoggedUserRecord.Logged_History(str_DistName, Todaydatetime.ToDate(), "Successful Login", IPAddress, "Log-in", Todaydatetime.ToDate(), "District User");
                Response.Redirect("Division/Default.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    /**************To Get Has value start*************/
    public void check_Values()
    {
        string str_RandomNO = Convert.ToString(randomNonrepeating());
        Hasvalue = GetHashWIPAddress(str_RandomNO);
        Session["hasvalue"] = Hasvalue.ToString();
    }
    public string GetHashWIPAddress_Withbroser(string s)
    {
        string strHostName = Dns.GetHostName();
        IPHostEntry ipaddress = Dns.GetHostEntry(strHostName);
        string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
        string enc = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((ipaddress + s + _browserInfo), "MD5");
        return enc;
    }
    public void genrate_Token_Cookies()
    {
        string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
        string _sessionValue = Convert.ToString(Session["dist_code"]) + "^" + _browserInfo;

        byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
        string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
        Session["encryptedSession"] = _encryptedString;

        string Tokenid = Convert.ToString(randomNonrepeating());
        Session["AuthToken"] = Tokenid;
        HttpCookie cook = new HttpCookie("AuthToken", GetHashWIPAddress_Withbroser(Tokenid));
        Response.Cookies.Add(cook);

    }
    public void RegenerateId()
    {
        var manager = new SessionIDManager();
        string oldId = manager.GetSessionID(Context);
        string newId = manager.CreateSessionID(Context);
        bool isAdd, isRedir;
        manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
        var ctx = HttpContext.Current.ApplicationInstance;
        HttpModuleCollection mods = ctx.Modules;
        var ssm = (SessionStateModule)mods.Get("Session");
        var fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        SessionStateStoreProviderBase store = null;
        FieldInfo rqIdField = null;
        FieldInfo rqLockIdField = null;
        FieldInfo rqStateNotFoundField = null;
        foreach (var field in fields)
        {
            if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
            if (field.Name.Equals("_rqId")) rqIdField = field;
            if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
            if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
        }
        object lockId = rqLockIdField.GetValue(ssm);
        if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
        rqStateNotFoundField.SetValue(ssm, true);
        rqIdField.SetValue(ssm, newId);
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
    /**************To Get Has value End*************/
    public string GenerateCaptcha()
    {
        string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        string captcha = "";
        Random rnd = new Random();

        for (int i = 0; i < 5; i++)
        {
            captcha += chars[rnd.Next(chars.Length)];
        }

        return captcha;
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            string captcha = GenerateCaptcha();

            Session["Captcha"] = captcha;
            lblCaptcha.Text = captcha;
        }
        catch (Exception ex)
        { }
    }
}