using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Project : System.Web.UI.Page
{
    //  DBmanager DB = new DBmanager();
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
                        if (Session["UserName"].ToString() != null)
                        {
                            BindDivision();
                            BindDistrict();
                            BindDepastment();
                        }
                        else
                        {
                            Response.Redirect("Logout.aspx");
                        }
                    }
                    //else
                    //{
                    //    Session.Clear();
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
    protected void btnsaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            string Role = Session["RoleID"].ToString();
            string shordorder = "";
            string shortordername = "";

            foreach (ListItem lstshort in chkDistricts.Items)
            {
                if (lstshort.Selected)
                {
                    shordorder += lstshort.Value + ",";

                    string[] order = lstshort.Text.Split(new[] { "-" }, StringSplitOptions.None);
                    if (order.Length > 1)
                    {
                        shortordername += order[1] + ",";
                    }
                }
            }

            string departmentIds = "";

            foreach (ListItem item in chkDepartment.Items)
            {
                if (item.Selected)
                {
                    departmentIds += "," + item.Value + ",";
                }
            }

            // departmentIds = departmentIds.TrimEnd(',');
            string districtNames = "";
            foreach (ListItem item in chkDistricts.Items)
            {
                if (item.Selected)
                {
                    districtNames += "," + item.Value + ",";
                }
            }
            // districtNames = districtNames.TrimEnd(',');
            string ckEditorText = CKEditorControl1.Text;
          
            DataTable dt = DB.InsertLetterDetails(txtleterdate.Text.ToString(), txtletterno.Text.ToString(), districtNames, txtofficer.Text.ToString(), txtmeta.Text.ToString(), ckEditorText,
             txtLocation.Text.ToString(), txtProjectCost.Text.ToString(), txtRevisedCost.Text.ToString(), txtPhysicalProgress.Text.ToString(), txtScheduledCompletion.Text.ToString(), txtApprovedDate.Text.ToString(), txtFinancialProgress.Text.ToString(),
             txtAnticipatedCompletion.Text.ToString(), departmentIds, txtProjectName.Text.ToString(), txtprojectdetails.Text.ToString(), txtmeetingdate.Text.ToString(), Role);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "Inserted")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Submitted successfully!');", true);
            }
            clearselection();
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "alert",
                "alert('Error occurred');",
                true
            );
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void BindDistrict()
    {
        try
        {
            DataTable dt = DB.BindDistricts();
            DataView dv = dt.DefaultView;
            dv.Sort = "Name ASC";
            dt = dv.ToTable();

            if (dt != null && dt.Rows.Count > 0)
            {
                chkDistricts.DataSource = dt;
                chkDistricts.DataTextField = "Name";
                chkDistricts.DataValueField = "id";
                chkDistricts.DataBind();
            }
            else
            {
                chkDistricts.DataSource = null;
                chkDistricts.DataBind();
            }
            //  chkDistricts.Items.Insert(0, new ListItem("--- Select District ---", "0"));
        }
        catch (Exception ex)
        { }
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

                ddlDivision.Items.Insert(0, new ListItem("-- Select Division --", "0"));
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void BindDepastment()
    {
        try
        {
            DataTable dt = DB.BindDepartment();
            DataView dv = dt.DefaultView;
            dv.Sort = "Name ASC";
            dt = dv.ToTable();


            if (dt != null && dt.Rows.Count > 0)
            {
                chkDepartment.DataSource = dt;
                chkDepartment.DataTextField = "Name";
                chkDepartment.DataValueField = "Id";
                chkDepartment.DataBind();
            }
            else
            {
                chkDepartment.DataSource = null;
                chkDepartment.DataBind();
            }
            //  chkDepartment.Items.Insert(0, new ListItem("--- Select Department ---", "0"));
        }
        catch (Exception ex)
        { }
    }
    private void clearselection()
    {
        txtleterdate.Text = ""; txtletterno.Text = ""; chkDistricts.ClearSelection();
     //   txtofficer.Text = ""; txtmeta.Text = ""; CKEditorControl1.Text = "";
        txtLocation.Text = ""; txtProjectCost.Text = ""; txtRevisedCost.Text = ""; txtPhysicalProgress.Text = "";
        txtScheduledCompletion.Text = ""; txtApprovedDate.Text = ""; txtFinancialProgress.Text = "";
        txtAnticipatedCompletion.Text = "";
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

    protected void btnback_Click(object sender, EventArgs e)
    {
        try
        {
            //     Session.Clear();
            Response.Redirect("Default.aspx");

        }
        catch (Exception ex)
        { }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = DB.BindDistrictsByDivision(ddlDivision.SelectedValue);

            if (dt != null && dt.Rows.Count > 0)
            {
                chkDistricts.DataSource = dt;
                chkDistricts.DataTextField = "Name";
                chkDistricts.DataValueField = "Id";
                chkDistricts.DataBind();
                chkDistricts.Enabled = true;
                
            }
            else
            {
                chkDistricts.DataSource = null;
                chkDistricts.DataBind();
                chkDistricts.Enabled = false;
                
            }
        }
        catch (Exception ex)
        {

        }
    }
    /****************************Genrate Token End***********************************/


}