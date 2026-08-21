using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Districts_DistrictsList : System.Web.UI.Page
{
    DBClass Db = new DBClass();
    TABP_Functions objFunc = new TABP_Functions();
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer == null)
            {
                Log_out();
                Response.Redirect("~/ErrorPage.html");
                return;
            }
            if (Session["Username"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (Session["Username"].ToString() == null && objFunc.GetHashWIPAddress(Session["AuthToken"].ToString()) != Request.Cookies["AuthToken"].Value)
                {
                    Log_out();
                    Response.Redirect("~/ErrorPage.html");
                }
                else
                {

                    if (!IsPostBack)
                    {
                        if (Session["Username"] != null)
                        {
                            string UserID = Session["Username"].ToString();
                            BindDistrict();
                        }
                        else
                        {
                            Session.Clear();
                            Response.Redirect("../Default.aspx");
                        }
                    }



                }
                genrate_Token_Cookies();
            }
            else
            {
                Log_out();
                Response.Redirect("~/ErrorPage.html");
            }
        }
        catch (Exception ex)
        { }

    }
    protected void BindDistrict()
    {
        try
        {
            string UserID = Session["Username"].ToString();
            DataTable dt = Db.BindDiscticList(UserID);
            if (dt != null)
            {
                gvLetters.DataSource = dt;
                gvLetters.DataBind();
            }
            else
            {
                gvLetters.DataSource = null;
                gvLetters.DataBind();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void gvLetters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                int RowId = Convert.ToInt32(e.CommandArgument);
                int roleId = Convert.ToInt32(Session["RoleID"]);


                Session["RowId"] = RowId;

                Response.Redirect("/Districts/View.aspx");
            }
        }
        catch (Exception ex)
        { }
    }
    public void genrate_Token_Cookies()
    {
        try
        {
            string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
            string _sessionValue = Convert.ToString(Session["Username"]) + "^" + DateTime.Now.Ticks + "^" + _browserInfo + "^" + System.Guid.NewGuid();

            byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
            string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
            Session["encryptedSession"] = _encryptedString;

            string Tokenid = Convert.ToString(objFunc.randomNonrepeating());
            Session["AuthToken"] = objFunc.GetHashWIPAddress(Tokenid);
            HttpCookie cook = new HttpCookie("AuthToken", objFunc.GetHashWIPAddress(Tokenid));
            cook.Value = Tokenid;
            cook.HttpOnly = true;
            cook.Secure = true;
            cook.Path = "~/SDG/StrCookie";
            cook.Domain = "http://epariyojana.up.gov.in";
            Response.Cookies.Add(cook);
        }
        catch (Exception ex)
        {
            HttpContext.Current.Server.Transfer("~/ErrorPage.html", false);
        }

    }
    public void Log_out()
    {
        // System.Web.Security.FormsAuthentication.SignOut();
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Session.Clear();
        Session.Contents.RemoveAll();

        HttpCookie cookies = Context.Request.Cookies[FormsAuthentication.FormsCookieName];//Or Response
        if (cookies != null)
        {
            cookies.Expires = DateTime.Now.AddDays(-1);
            Context.Response.Cookies.Add(cookies);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["d"] != null)
            {
                Response.Cookies["d"].Value = string.Empty;
                Response.Cookies["d"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
        else
        {
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["d"] != null)
            {
                Response.Cookies["d"].Value = string.Empty;
                Response.Cookies["d"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
        FormsAuthentication.SignOut();

    }
    /****************************Genrate Token End***********************************/
}