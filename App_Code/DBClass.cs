

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;

    public class DBClass
    {
        DAL obj = new DAL();
    private object GetDateOrNull(string dateText)
        {
            return string.IsNullOrWhiteSpace(dateText)
                ? (object)DBNull.Value
                : Convert.ToDateTime(dateText);
        }
    public DataTable InsertLetterDetails(string LetterDate,string LetterNo,string District,string Officer,string Meta,
    string LetterText,string Location,string ProjectCost,string RevisedCost,string PhysicalProgress,string ScheduledCompletion,string ApprovedDate,string FinancialProgress,string AnticipatedCompletion,string department,string projectname,string projectdetails,string meeting,string Role
)
        {
            Hashtable param = new Hashtable();

            param["@LetterDate"] = GetDateOrNull(LetterDate);
            param["@LetterNo"] = LetterNo;
            param["@District"] = District;
            param["@Officer"] = Officer;
            param["@Meta"] = Meta;
            param["@LetterText"] = LetterText;
           // param["@Headline"] = Headline;

            // New columns
            param["@Location"] = Location;
            param["@ProjectCost"] = ProjectCost;
            param["@RevisedCost"] = RevisedCost;
            param["@PhysicalProgress"] = PhysicalProgress;
            param["@ScheduledCompletion"] = GetDateOrNull(ScheduledCompletion);
            param["@ApprovedDate"] = GetDateOrNull(ApprovedDate);
            param["@FinancialProgress"] = FinancialProgress;
            param["@AnticipatedCompletion"] = GetDateOrNull(AnticipatedCompletion);
            param["@DepartmentName"] = department;
            param["@ProjectName"] =projectname ;
            param["@Projectdetail"] = projectdetails;
            param["@MeetingDate"] = GetDateOrNull(meeting);
            param["@Role"] = Role;

            // Execute stored procedure
            DataTable dt = obj.ExecuteSQL("Proc_NewsLetter", param, true);
            return dt;
        }
        public DataTable UpdateProject(
         string LetterDate,
         string LetterNo,
         string District,
         string Officer,
         string LetterText,
         string Location,
         string ProjectCost,
         string RevisedCost,
         string PhysicalProgress,
         string ScheduledCompletion,
         string ApprovedDate,
         string FinancialProgress,
         string AnticipatedCompletion, string department, string projectname, string meeting, string Role,string ProjectID
)
        {
            Hashtable param = new Hashtable();

            param["@LetterDate"] = GetDateOrNull(LetterDate);
            param["@LetterNo"] = LetterNo;
            param["@District"] = District;
            param["@Officer"] = Officer;
          //  param["@Meta"] = Meta;
            param["@LetterText"] = LetterText;
            // param["@Headline"] = Headline;

            // New columns
            param["@Location"] = Location;
            param["@ProjectCost"] = ProjectCost;
            param["@RevisedCost"] = RevisedCost;
            param["@PhysicalProgress"] = PhysicalProgress;
            param["@ScheduledCompletion"] = GetDateOrNull(ScheduledCompletion);
            param["@ApprovedDate"] = GetDateOrNull(ApprovedDate);
            param["@FinancialProgress"] = FinancialProgress;
            param["@AnticipatedCompletion"] =GetDateOrNull(AnticipatedCompletion);
            param["@DepartmentName"] = department;
            param["@ProjectName"] = projectname;
        //    param["@Projectdetail"] = projectdetails;
            param["@MeetingDate"] = GetDateOrNull(meeting);
            param["@Role"] = Role;
            param["@ProjectID"] = ProjectID;
            DataTable dt = obj.ExecuteSQL("Proc_UpdateProject", param, true);
            return dt;
        }
        public DataTable BindView()
        {
            Hashtable param = new Hashtable();
           // param["@ID"] = id;
            DataTable dt = obj.ExecuteSQL("Proc_AdminInfoBind", param, true);
            return dt;
        }
        public DataTable BindDeptView( string UserID)
        {
            Hashtable param = new Hashtable();
             param["@ID"] = UserID;
            DataTable dt = obj.ExecuteSQL("Proc_DeptInfoBind", param, true);
            return dt;
        }
        public DataTable BindDiscticList(string UserID)
        {
            Hashtable param = new Hashtable();
            param["@ID"] = UserID;
            DataTable dt = obj.ExecuteSQL("Proc_DistictInfoBind", param, true);
            return dt;
        }
        public DataTable BindDivisionList(string divCode)
        {
            Hashtable param = new Hashtable();
            param["@DivCode"] = divCode;

            DataTable dt = obj.ExecuteSQL("Proc_DivisionInfoBind", param, true);
            return dt;
        }
    public DataTable GetLetterById(int ID)
        {
            Hashtable param = new Hashtable();
            param["@ID"] = ID;
            DataTable dt = obj.ExecuteSQL("Proc_BindModelView", param, true);
            return dt;
        }
        public DataTable BindViewUpdate(int ID)
        {
            Hashtable param = new Hashtable();
            param["@ID"] = ID;
            DataTable dt = obj.ExecuteSQL("Proc_BindUpdate", param, true);
            return dt;
        }        
        public DataSet SaveReply(int issueId,int Roleid, int? parentReplyId, string IPAddress,string Issue,string response,string deptIdreply)
        { 
            Hashtable param = new Hashtable();
            param["@RowID"] = issueId;
           // param["@ReplyText"] = reply;          
            param["@ReplyBy_ID"] = Roleid;
            param["@deptIdreply"] = deptIdreply;
            if (parentReplyId == null)
             param["@parentReplyId"] = DBNull.Value;
            else
            param["@parentReplyId"] = parentReplyId; 
            param["@IPAddress"] = IPAddress;
            
            if (Issue == null)
                param["@Issuetext"] = DBNull.Value;
            else
                param["@Issuetext"] = Issue;
           // param["@img"] = img;
            if (response == null)
                param["@RespronsText"] = DBNull.Value;
            else
                param["@RespronsText"] = response;
           // param["@ID"] = btnhiden;
            DataSet ds = obj.ExecuteSQL_returnDataset("sp_InsertProjectReply", param, true);
            return ds;
        }
        public DataTable SaveDepartment(string projectName)
        {
            Hashtable param = new Hashtable();
            param["@DeptName"] = projectName;       
            DataTable ds = obj.ExecuteSQL("Proc_AddDepartment", param, true);
            return ds;
        }
        public DataSet GetReplies(int RPID)
        {
            Hashtable param = new Hashtable();
            param["@RPID"] = RPID;
            //param["@roleId"] = roleId;

            //if (parentReplyId == null)
            //    param["@parentReplyId"] =DBNull.Value;
            //else
            //    param["@parentReplyId"] = parentReplyId;

            DataSet dt = obj.ExecuteSQL_returnDataset("Pro_BindReplyparent", param, true);
            return dt;
        }
        public DataTable GetReplieschile(int parentID)
        {
            Hashtable param = new Hashtable();
           // param["@RPID"] = ID;
            //param["@roleId"] = roleId;
            param["@ParentID"] = parentID;
            DataTable dt = obj.ExecuteSQL("sp_GetChildReplies", param, true);
            return dt;
        }
        public DataTable GetReplieschileIMg(int parentID)
        {
            Hashtable param = new Hashtable();
            // param["@RPID"] = ID;
            //param["@roleId"] = roleId;
            param["@ParentID"] = parentID;
            DataTable dt = obj.ExecuteSQL("sp_GetAllReplyImages", param, true);
            return dt;
        }
        //sp_GetAllReplyImages
        public DataTable loginUser(string username, string password, string ip)
        {
            Hashtable param = new Hashtable();
            param["@UserName"] = username;
            param["@PassWord"] = password;
            param["@IPAddress"] = ip;
            //param["@Role"] = Role;
            DataTable dt=obj.ExecuteSQL("Proc_login", param, true);
            return dt;
        }
        public DataTable loginUserdept(string username, string password, string ip)
        {
            Hashtable param = new Hashtable();
            param["@UserName"] = username;
            param["@PassWord"] = password;
            param["@IPAddress"] = ip;
            //param["@Role"] = Role;
            DataTable dt = obj.ExecuteSQL("Proc_logindept", param, true);
            return dt;
        }
        public DataTable LoginDistrict(string username, string password,string ip)
        {
            Hashtable param = new Hashtable();
            param["@UserName"] = username;
            param["@PassWord"] = password;
            param["@IPAddress"] = ip;
            //param["@Role"] = Role;
            DataTable dt = obj.ExecuteSQL("Proc_loginDistricts", param, true);
            return dt;
        }
    public DataTable LoginDivision(string username, string password, string ip)
    {
        Hashtable param = new Hashtable();

        param["@UserName"] = username;
        param["@PassWord"] = password;
        param["@IPAddress"] = ip;

        DataTable dt = obj.ExecuteSQL("Proc_loginDivision", param, true);
        return dt;
    }

    //loginUser
    public DataTable BindDistricts()
            {
            Hashtable param = new Hashtable();
            DataTable dt = obj.ExecuteSQL("Proc_BindDistricts", param, true);
            return dt;
        }
        public DataTable BindDistrictsByDivision(string divCode)
        {
            Hashtable param = new Hashtable();
            param["@DivCode"] = divCode;

            DataTable dt = obj.ExecuteSQL("Proc_BindDistrictsByDivision", param, true);
            return dt;
        }
    public DataTable BindDivision()
        {
            Hashtable param = new Hashtable();
            DataTable dt = obj.ExecuteSQL("Proc_BindDivision", param, true);
            return dt;
        }
    public DataTable BindDepartment()
        {
            Hashtable param = new Hashtable();
            DataTable dt = obj.ExecuteSQL("Proc_BindDepartment", param, true);
            return dt;
        }
        public DataTable BindMeetingdateHeld()
        {
            Hashtable param = new Hashtable();
            DataTable dt = obj.ExecuteSQL("Proc_BindMeetingDate", param, true);
            return dt;
        }
        public DataTable BindProjectdetails(string meetingdate)
        {
            Hashtable param = new Hashtable();
            param["@MeetingDate"] = meetingdate;
            DataTable dt = obj.ExecuteSQL("proc_bindProjectOrdetails", param, true);
            return dt;
        }
        public DataTable BindDepartmentAll()
        {
            Hashtable param = new Hashtable();
           
            DataTable dt = obj.ExecuteSQL("Proc_BindDeptAll", param, true);
            return dt;
        }
        public DataTable Logindeptprofil(string name, string mobile,string emailID,string deptid)
        {
            Hashtable param = new Hashtable();
            param["@name"] = name;
            param["@mobileNo"] = mobile;
            param["@EmailID"] = emailID;
            param["@dept"] = deptid;
            DataTable dt = obj.ExecuteSQL("proc_profile", param, true);
            return dt;
        }
        public DataTable Logindistictprofil(string name, string mobile, string emailID,string UserName)
        {
            Hashtable param = new Hashtable();
            param["@name"] = name;
            param["@mobileNo"] = mobile;
            param["@EmailID"] = emailID;
            param["@dept"] = UserName;
            DataTable dt = obj.ExecuteSQL("proc_InsertfileDistrict", param, true);
            return dt;
        }
        public DataTable Binddisctricprofile(string UserName)
        {
            Hashtable param = new Hashtable();
           
            param["@dept"] = UserName;
            DataTable dt = obj.ExecuteSQL("proc_select_fileDistrict", param, true);
            return dt;
        }
    public DataTable BindDivisionProfile(string UserName)
    {
        Hashtable param = new Hashtable();

        param["@dept"] = UserName;

        DataTable dt = obj.ExecuteSQL("proc_select_Division_profile", param, true);

        return dt;
    }

    public DataTable UpdateDivisionProfile(
        string name,
        string mobile,
        string emailID,
        string UserName)
    {
        Hashtable param = new Hashtable();

        param["@name"] = name;
        param["@mobileNo"] = mobile;
        param["@EmailID"] = emailID;
        param["@dept"] = UserName;

        DataTable dt = obj.ExecuteSQL(
            "Proc_UpdateDivisionProfile",
            param,
            true);

        return dt;
    }
    public DataTable BindDeptFprofile(string deptid)
        {
            Hashtable param = new Hashtable();

            param["@dept"] = deptid;
            DataTable dt = obj.ExecuteSQL("proc_select_Dept_profile", param, true);
            return dt;
        }
        public DataTable changpassDEPT(string password, string confirm,string UserName)
        {
            Hashtable param = new Hashtable();
            param["@password"] = password;
            param["@confirmpass"] = confirm;
          
            param["@Dept"] = UserName;
            DataTable dt = obj.ExecuteSQL("proc_ChangPass_DEPT", param, true);
            return dt;
        }
        public DataTable changpassDIST(string password, string confirm, string UserName)
        {
            Hashtable param = new Hashtable();
            param["@password"] = password;
            param["@confirmpass"] = confirm;
            param["@dept"] = UserName;
            DataTable dt = obj.ExecuteSQL("proc_ChangPass_Disrict", param, true);
            return dt;
        }
        public DataTable changpassDisct(string password, string confirm, string dept)
        {
            Hashtable param = new Hashtable();
            param["@password"] = password;
            param["@confirmpass"] = confirm;
            param["@Dept"] = dept;
            DataTable dt = obj.ExecuteSQL("proc_ChangPass_Disrict", param, true);
            return dt;
        }
    public DataTable changpassDivision(string password, string confirm, string UserName)
    {
        Hashtable param = new Hashtable();
        param["@password"] = password;
        param["@confirmpass"] = confirm;
        param["@Dept"] = UserName;

        DataTable dt = obj.ExecuteSQL(
            "proc_ChangPass_Division",
            param,
            true);

        return dt;
    }
    public DataTable InsertImg(int RowID,int ReplyBy,int parentReplyId, string deptIdreply, string img)
        {
            Hashtable param = new Hashtable();
            param["@RowID"] = RowID;                    
            param["@ReplyBy_ID"] = ReplyBy;
            param["@deptIdreply"] = deptIdreply;
            //if (parentReplyId == null)
            //    param["@parentReplyId"] = DBNull.Value;
            //else
             param["@parentReplyId"] = parentReplyId;
            param["@img"] = img;
            DataTable dt = obj.ExecuteSQL("Pro_Insertimg", param, true);
            return dt;
        }       
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
        public DataTable UploadMeetingMints(string uploadmeeting,string Uploaddate)
        {

            Hashtable param = new Hashtable();
            param["@MeetingmintUpload"] = uploadmeeting;
            param["@Uploaddate"] = Uploaddate;
            DataTable dt = obj.ExecuteSQL("Proc_Uploadmint", param, true);
            return dt;
        }
        public DataTable BIndUploadMeetingMints(string Uploadmeeting)
        {

            Hashtable param = new Hashtable( );
            param["@Uploaddate"] = Uploadmeeting;
            DataTable dt = obj.ExecuteSQL("Proc_Bind_Uploadmint", param, true);
            return dt;
        }

        
    }

