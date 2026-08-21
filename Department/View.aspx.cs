using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Department_View : System.Web.UI.Page
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
            if (Session["DeptName"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (Session["DeptName"].ToString() == null && objFunc.GetHashWIPAddress(Session["AuthToken"].ToString()) != Request.Cookies["AuthToken"].Value)
                {
                    Log_out();
                    Response.Redirect("~/ErrorPage.html");
                }
                else
                {

                    if (!IsPostBack)
                    {


                        if (Session["DeptName"].ToString() != null)
                        {
                            if (Session["RowId"] != null)
                            {
                                if (Session["RoleID"].ToString() == "1")
                                {

                                    bindText();
                                    btnmsg.Visible = false;

                                    //BindParentReplies();
                                    BindGrid();
                                }
                                else
                                {

                                    bindText();
                                    int RPID = Convert.ToInt32(Session["RowId"]);
                                    //BindParentReplies();
                                    BindGrid();


                                }
                            }
                        }
                        else
                        {
                            Response.Redirect("logout.aspx");
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
            DataTable dt = DB.GetLetterById(id); // Get data from database

            if (dt != null && dt.Rows.Count > 0)
            {
                // Level 1: Basic Info
                
                lblProjectID.Text = dt.Rows[0]["ID"].ToString().PadLeft(3, '0');

                DateTime createDate;
                if (DateTime.TryParse(dt.Rows[0]["CreateDate"].ToString(), out createDate))
                {
                    lblProjectDate.Text = createDate.ToString("dd/MMM/yyyy");
                }
               
                lblMeetingDate.Text = FormatDate(dt.Rows[0]["MeetingDate"].ToString());
                lblLetterDate.Text = FormatDate(dt.Rows[0]["LetterDate"].ToString());
                lblLetterNo.Text = dt.Rows[0]["LetterNo"].ToString();
                lblOfficer.Text = dt.Rows[0]["Officer"].ToString();

                // Level 2: Location & Costs
                lblLocation.Text = dt.Rows[0]["Location"].ToString();
                lblApprovedDate.Text = FormatDate(dt.Rows[0]["ApprovedDate"].ToString());
                lblProjectCost.Text = dt.Rows[0]["ProjectCost"].ToString();
                lblRevisedCost.Text = dt.Rows[0]["RevisedCost"].ToString();

                // Level 3: Completion & Progress
                lblScheduledCompletion.Text = FormatDate(dt.Rows[0]["ScheduledCompletion"].ToString());
                lblAnticipatedCompletion.Text = FormatDate(dt.Rows[0]["AnticipatedCompletion"].ToString());
                lblPhysicalProgress.Text = dt.Rows[0]["PhysicalProgress"].ToString();
                lblFinancialProgress.Text = dt.Rows[0]["FinancialProgress"].ToString();

                // Level 4: Department / District / Project
                lblDepartment.Text = dt.Rows[0]["deptIdreply"].ToString();
                lblDivision.Text = dt.Rows[0]["Division"].ToString();
                lblDistrict.Text = dt.Rows[0]["District"].ToString();
                lblProjectName.Text = dt.Rows[0]["ProjectName"].ToString();
                string projectDetails = dt.Rows[0]["LetterText"].ToString();
                lblProjectDetails.Text = projectDetails;

                const int charLimit = 200;
                if (!string.IsNullOrEmpty(projectDetails) && projectDetails.Length > charLimit)
                {
                    lblProjectDetails.CssClass = "details-text collapsed";
                    btnReadMore.Visible = true;
                }
                else
                {
                    lblProjectDetails.CssClass = "details-text";
                    btnReadMore.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }

    }
    private void BindGrid()
    {
        try
        {
            int RPID = Convert.ToInt32(Session["RowId"]);
            DataSet ds = DB.GetReplies(RPID);

            if (ds != null && ds.Tables.Count > 0)
            {
                pnlIssuesSection.Visible = ds.Tables[0].Rows.Count > 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvIssues.DataSource = ds.Tables[0];
                    gvIssues.DataBind();
                }
                else //if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    gvIssues.DataSource = null;
                    gvIssues.DataBind();
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void btnresponse_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int issueID = Convert.ToInt32(btn.CommandArgument);
            HiddenField hdnParentReplyId = (HiddenField)row.FindControl("hdnParentReplyId");
            TextBox txtresponse = (TextBox)row.FindControl("txtresponse");
            int roleID = Convert.ToInt32(Session["RoleID"]);
            string ip = Request.UserHostAddress;
            //FileUpload fileUpload = (FileUpload)row.FindControl("fileUpload2");
            //string imagePath = null;
            //if (fileUpload.HasFile)
            //{
            //    string extension = Path.GetExtension(fileUpload.FileName);

            //    // Optional: validate file type
            //    string[] allowedExt = { ".jpg", ".jpeg", ".png", ".gif" };
            //    if (allowedExt.Contains(extension.ToLower()))
            //    {
            //        string fileName = Guid.NewGuid().ToString() + extension;

            //        string folderPath = Server.MapPath("~/Uploads/");
            //        if (!Directory.Exists(folderPath))
            //        {
            //            Directory.CreateDirectory(folderPath);
            //        }

            //        string fullPath = Path.Combine(folderPath, fileName);

            //        fileUpload.SaveAs(fullPath);
            //        imagePath = "~/Uploads/" + fileName;
            //    }
            //}
            int? parentReplyId = null;

            if (hdnParentReplyId != null &&
                !string.IsNullOrEmpty(hdnParentReplyId.Value))
            {
                parentReplyId = Convert.ToInt32(hdnParentReplyId.Value);
            }
            else
            {
                parentReplyId = issueID;
            }
            int RPID = Convert.ToInt32(Session["RowId"]);

            if (roleID == 0)
            {
                DB.SaveReply(RPID, roleID, parentReplyId, ip, null, txtresponse.Text, Session["EmployeeName"].ToString());

            }
            else
            {
                DB.SaveReply(RPID, roleID, parentReplyId, ip, null, txtresponse.Text, Session["DeptName"].ToString());

            }

            BindGrid();
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        { }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int parentReplyId = Convert.ToInt32(HidenID.Value);
            int roleID = Convert.ToInt32(Session["RoleID"]);
            string ip = Request.UserHostAddress;
            //sint parentReplyId = 0;
            int RPID = Convert.ToInt32(Session["RowId"]);
            //FileUpload fileUpload = (FileUpload)FindControl("fileUpload2");
            string imagePath = null;
            string fullPath = null;

            if (fileUpload2.HasFile)
            {
                string extension = Path.GetExtension(fileUpload2.FileName);

                string[] allowedExt = { ".jpg", ".jpeg", ".png", ".gif",".pdf",".xls" };

                if (allowedExt.Contains(extension.ToLower()))
                {
                    string fileName = Guid.NewGuid().ToString() + extension;

                    string folderPath = Server.MapPath("~/Uploads/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    fullPath = Path.Combine(folderPath, fileName);

                    // Save image in folder
                    fileUpload2.SaveAs(fullPath);

                    imagePath = "~/Uploads/" + fileName;
                    //}
                }
                if (roleID == 0)
                {
                    DB.InsertImg(RPID, roleID, parentReplyId, Session["EmployeeName"].ToString(), imagePath);

                }
                else
                {
                    DB.InsertImg(RPID, roleID, parentReplyId, Session["DeptName"].ToString(), imagePath);

                }

                BindGrid();
                Response.Redirect(Request.RawUrl);
            }
        }
        catch (Exception ex)
        { }

    }

    //protected void tbnissuimg_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Button btn = (Button)sender;
    //        int parentReplyId = Convert.ToInt32(HidenID.Value);
    //        int roleID = Convert.ToInt32(Session["RoleID"]);
    //        string ip = Request.UserHostAddress;
    //        //sint parentReplyId = 0;
    //        int RPID = Convert.ToInt32(Session["RowId"]);
    //        //FileUpload fileUpload = (FileUpload)FindControl("fileUpload2");
    //        string imagePath = null;
    //        if (fileUpload3.HasFile)
    //        {
    //            string extension = Path.GetExtension(fileUpload3.FileName);

    //            // Optional: validate file type
    //            string[] allowedExt = { ".jpg", ".jpeg", ".png", ".gif" };
    //            if (allowedExt.Contains(extension.ToLower()))
    //            {
    //                string fileName = Guid.NewGuid().ToString() + extension;

    //                string folderPath = Server.MapPath("~/Uploads/");
    //                if (!Directory.Exists(folderPath))
    //                {
    //                    Directory.CreateDirectory(folderPath);
    //                }

    //                string fullPath = Path.Combine(folderPath, fileName);

    //                fileUpload3.SaveAs(fullPath);
    //                imagePath = "~/Uploads/" + fileName;
    //            }
    //            //}
    //            if (roleID == 0)
    //            {
    //                DB.InsertImg(RPID, roleID, parentReplyId, Session["EmployeeName"].ToString(), imagePath);

    //            }
    //            else
    //            {
    //                DB.InsertImg(RPID, roleID, parentReplyId, Session["DeptName"].ToString(), imagePath);

    //            }

    //            BindGrid();
    //        }
    //    }
    //    catch (Exception ex)
    //    { }
    //}
    protected void gvIssues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int issueId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));

                Repeater rpt = (Repeater)e.Row.FindControl("rptResponses");

                DataTable dt = DB.GetReplieschile(issueId);

                rpt.DataSource = dt;
                rpt.DataBind();
            }


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
            string _sessionValue = Convert.ToString(Session["DeptName"]) + "^" + DateTime.Now.Ticks + "^" + _browserInfo + "^" + System.Guid.NewGuid();

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
    protected void btnback_Click(object sender, EventArgs e)
    {
        try
        {
            //     Session.Clear();
            Response.Redirect("DepartmentView.aspx");

        }
        catch (Exception ex)
        { }

    }
}