using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewUpdate : System.Web.UI.Page
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

                        if (Session["RowId"] != null)
                        {
                            if (Session["RoleID"].ToString() == "0")
                            {
                                BindDivision();
                                BindDepastment();
                                BindDistrict();
                                bindText();
                            }

                            else
                            {
                                Session.Clear();
                                Response.Redirect("Logout.aspx");
                            }
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
    protected void bindText()
    {
        try
        {
            int id = Convert.ToInt32(Session["RowId"]);
            DataTable dt = Db.BindViewUpdate(id); // Get data from database

            if (dt != null && dt.Rows.Count > 0)
            {
                // Level 1: Basic Info
                // txtLetterText.Text = dt.Rows[0]["LetterText"].ToString();
                lblProjectID.Text = dt.Rows[0]["ID"].ToString().PadLeft(3, '0');

                lblProjectDate.Text =
                    Convert.ToDateTime(dt.Rows[0]["MeetingDate"])
                    .ToString("dd/MMM/yyyy");
                txtMeetingDate.Text = FormatDate(dt.Rows[0]["MeetingDate"].ToString());
                txtLetterDate.Text = FormatDate(dt.Rows[0]["LetterDate"].ToString());
                txtLetterNo.Text = dt.Rows[0]["LetterNo"].ToString();
                txtOfficer.Text = dt.Rows[0]["Officer"].ToString();

                // Level 2: Location & Costs
                txtLocation.Text = dt.Rows[0]["Location"].ToString();
                txtApprovedDate.Text = FormatDate(dt.Rows[0]["ApprovedDate"].ToString());
                txtProjectCost.Text = dt.Rows[0]["ProjectCost"].ToString();
                txtRevisedCost.Text = dt.Rows[0]["RevisedCost"].ToString();

                // Level 3: Completion & Progress
                txtScheduledCompletion.Text = FormatDate(dt.Rows[0]["ScheduledCompletion"].ToString());
                txtAnticipatedCompletion.Text = FormatDate(dt.Rows[0]["AnticipatedCompletion"].ToString());
                txtPhysicalProgress.Text = dt.Rows[0]["PhysicalProgress"].ToString();
                txtFinancialProgress.Text = dt.Rows[0]["FinancialProgress"].ToString();

                string deptValue = dt.Rows[0]["deptIdreply"].ToString();

                if (!string.IsNullOrEmpty(deptValue))
                {
                    //string[] deptArray = deptValue
                    //    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] deptArray = deptValue
                     .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(x => x.Trim())
                     .ToArray();
                    foreach (ListItem item in chkDepartment.Items)
                    {
                        var itemText = item.Text.Trim().ToLower();

                        if (deptArray.Any(x => x.Trim().ToLower() == itemText))
                        {
                            item.Selected = true;
                        }
                    }
                }
                string districtValue = dt.Rows[0]["District"].ToString();

                if (!string.IsNullOrEmpty(districtValue))
                {
                    //string[] deptArray = districtValue
                    //  .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] deptArray1 = districtValue
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();

                    foreach (ListItem item in chkDistricts.Items)
                    {
                        if (deptArray1.Contains(item.Text))
                        {
                            item.Selected = true;
                        }
                    }
                }
                txtProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
                CKEditorControl1.Text = dt.Rows[0]["LetterText"].ToString();
            }

        }
        catch (Exception ex)
        { }
    }
    protected void TxtEdit_Click(object sender, EventArgs e)
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
            DataTable dt = Db.UpdateProject(
                (txtLetterDate.Text),
                txtLetterNo.Text,
                districtNames,
                txtOfficer.Text,
                ckEditorText,
                txtLocation.Text,
                txtProjectCost.Text,
                txtRevisedCost.Text,
                txtPhysicalProgress.Text,
                txtScheduledCompletion.Text,
                txtApprovedDate.Text,
                txtFinancialProgress.Text,
                txtAnticipatedCompletion.Text,
                departmentIds,
                txtProjectName.Text,
                txtMeetingDate.Text,
                Role,
               Session["RowId"].ToString()
            );

            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "Update")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Upadetd successfully!');", true);
            }
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error occurred');", true);
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void BindDivision()
    {
        try
        {
            DataTable dt = Db.BindDivision();

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
    protected void BindDistrict()
    {
        try
        {
            DataTable dt = Db.BindDistricts();

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
    protected void BindDepastment()
    {
        try
        {
            DataTable dt = Db.BindDepartment();

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
    private string FormatDate(object dateValue)
    {
        if (dateValue == null) return "";

        DateTime dt;
        if (DateTime.TryParse(dateValue.ToString(), out dt))
        {
            if (dt == new DateTime(1900, 1, 1))
            {
                return ""; // empty dikhao
            }
            return dt.ToString("yyyy/MM/dd"); // format
        }

        return "";
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
    /****************************Genrate Token End***********************************/
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Db.BindDistrictsByDivision(ddlDivision.SelectedValue);

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
}
