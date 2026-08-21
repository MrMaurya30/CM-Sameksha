
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Default : System.Web.UI.Page
{
    DBClass DB = new DBClass();
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
                        BindMeetingdate();
                    }
                    else
                    {
                        Session.Clear();
                        Response.Redirect("../Default.aspx");
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
    protected void BindMeetingdate()
    {
        try
        {
            DataTable dt = DB.BindMeetingdateHeld();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("ProjectCount"))
                    dt.Columns.Add("ProjectCount", typeof(int));

                foreach (DataRow row in dt.Rows)
                {
                    string meetingDate = Convert.ToDateTime(row["MeetingDate"])
                                            .ToString("yyyy-MM-dd");

                    DataTable projectDt = DB.BindProjectdetails(meetingDate);

                    row["ProjectCount"] = projectDt.Rows.Count;
                }
                //  Latest meeting first
                DataView dv = dt.DefaultView;
                dv.Sort = "MeetingDate DESC";

                rptDates.DataSource = dt;
                rptDates.DataBind();
            }
        }
        catch (Exception ex)
        { }
    }
    /*******************************Genrate Token Start********************************/
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