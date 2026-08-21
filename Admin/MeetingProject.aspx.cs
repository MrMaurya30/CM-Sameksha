using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MeetingProject : System.Web.UI.Page
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

                        if (Request.QueryString["NN"] != null)
                        {
                            Session["meetingID"] = Request.QueryString["NN"].ToString();
                            BindProject();
                            BindUploadmints();
                        }
                        else
                        {
                            Session.Clear();
                            Response.Redirect("Logout.aspx");
                        }
                    }
                    //else
                    //{
                    //    
                    //    Response.Redirect("../Default.aspx");
                    //}

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
    protected void BindProject()
    {
        try
        {
            if (Session["meetingID"] != null)
            {
                string sessionValue = Session["meetingID"].ToString();

                DateTime meetingDate;

                if (DateTime.TryParse(sessionValue, out meetingDate))
                {
                    string formattedDate = meetingDate.ToString("yyyy-MM-dd");
                    DataTable dt = Db.BindProjectdetails(formattedDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        grdproject.DataSource = dt;
                        grdproject.DataBind();
                    }
                    else
                    {
                        grdproject.DataSource = null;
                        grdproject.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void gvLetters_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "View")
        {
            int RowId = Convert.ToInt32(e.CommandArgument);
            int roleId = Convert.ToInt32(Session["RoleID"]);
            Session["RowId"] = RowId;
            Response.Redirect("View.aspx");
        }

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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string imagePath = null;

            if (FileUpload1 != null && FileUpload1.HasFile)
            {
                string extension = Path.GetExtension(FileUpload1.FileName);
                string[] allowedExt = { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".xls", ".xlsx" };

                if (allowedExt.Contains(extension.ToLower()))
                {
                    string fileName = Guid.NewGuid().ToString() + extension;
                    string folderPath = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fullPath = Path.Combine(folderPath, fileName);
                    FileUpload1.SaveAs(fullPath);
                    imagePath = "~/Uploads/" + fileName;
                }

                DataTable dt = Db.UploadMeetingMints(imagePath, Session["meetingID"].ToString());
                if (dt != null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Uploaded successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Not Uploaded!');", true);
                }

                BindUploadmints();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Failed";
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindUploadmints()
    {
        try
        {
            if (Session["meetingID"] != null)
            {
                string sessionValue = Session["meetingID"].ToString();
                DateTime meetingDate;

                if (DateTime.TryParse(sessionValue, out meetingDate))
                {
                    DataTable dt = Db.BIndUploadMeetingMints((Session["meetingID"].ToString()));

                    string filePath = (dt != null && dt.Rows.Count > 0)
                        ? dt.Rows[0]["MeetingmintUpload"].ToString()
                        : null;

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        // Already uploaded: show View link + "Uploaded" text, hide upload button
                        hlView.Visible = true;
                        hlView.NavigateUrl = ResolveUrl(filePath);

                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Uploaded!!!";

                        pnlUpload.Visible = false;
                    }
                    else
                    {
                        // Not uploaded: show only the Upload Document button
                        hlView.Visible = false;
                        lblMessage.Visible = false;

                        pnlUpload.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = ex.Message;
        }
    }
}
