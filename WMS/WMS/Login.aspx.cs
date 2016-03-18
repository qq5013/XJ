using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using BLL.Security;
using Util;

namespace WMS
{
    public partial class Login : System.Web.UI.Page
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (Page.Request.Url.Query != "")
        //    {
        //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Resize", "alert(\"对不起,操作时限已过,请重新登入！\");window.top.location =\"Login.aspx\";", true);
        //    }
             
        //}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //测试
            Request.Cookies.Clear();
            if (txtUserName.Text.Trim() != "")
            {
                try
                {
                    string key = txtUserName.Text.ToLower();
                    string UserCache = Convert.ToString(Cache[key]);


                    UserBll userBll = new UserBll();

                    DataTable dtUserList = userBll.GetUserInfo(txtUserName.Text.Trim());
                    if (dtUserList != null && dtUserList.Rows.Count > 0)
                    {
                        if (dtUserList.Rows[0]["UserPassword"].ToString().Trim() == txtPassWord.Text.Trim())
                        {
                            FormsAuthentication.SetAuthCookie(this.txtUserName.Text, false);


                            Session["UserID"] = dtUserList.Rows[0]["UserID"].ToString();
                            Session["GroupID"] = dtUserList.Rows[0]["GroupID"].ToString();
                            Session["G_user"] = dtUserList.Rows[0]["UserName"].ToString();
                            
                            string EmployeeCode = dtUserList.Rows[0]["EmployeeCode"].ToString();
                            App_Code.LoginUserInfo.UserName = dtUserList.Rows[0]["UserName"].ToString();
                            App_Code.LoginUserInfo.UserCode = EmployeeCode;

                            Session["EmployeeCode"] = dtUserList.Rows[0]["EmployeeCode"].ToString();
                             
                            //Session["sys_PageCount"] = 15;


                            //Session["grid_ColumnTitleFont"] = "楷体_GB2312,Coral,10,加粗";
                            //Session["grid_ContentFont"] = "宋体,Black,10,正常";

                            //Session["grid_ColumnTextAlign"] = "1";
                            //Session["grid_ContentTextAlign"] = "1";
                            //Session["grid_NumberColumnAlign"] = "1";
                            //Session["grid_MoneyColumnAlign"] = "1";
                            //Session["grid_SelectMode"] = "0";
                            //Session["grid_IsRefreshBeforeAdd"] = "1";
                            //Session["grid_IsRefreshBeforeUpdate"] = "1";
                            //Session["grid_IsRefreshBeforeDelete"] = "1";

                            //Session["grid_OddRowColor"] = "White";
                            //Session["grid_EvenRowColor"] = "AliceBlue";




                            //Session.Timeout = int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"]);
                            #region 添加登录日志

                            BLL.BLLBase bll = new BLL.BLLBase();
                            bll.ExecNonQuery("Security.InsertOperatorLog", new DataParameter[]{new DataParameter("@LoginUser",App_Code.LoginUserInfo.UserName),new DataParameter("@LoginTime",DateTime.Now),
                                                         new DataParameter("@LoginModule","登录系统"),new DataParameter("@ExecuteOperator","用户登录")});

                            #endregion
                            TimeSpan stLogin = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);
                            HttpContext.Current.Cache.Insert(key, Page.Request.UserHostAddress, null, DateTime.MaxValue, stLogin, System.Web.Caching.CacheItemPriority.NotRemovable, null);

                            Response.Redirect("Default.aspx", false);
                        }
                        else
                        {
                            BLL.BLLBase bll = new BLL.BLLBase();
                            bll.ExecNonQuery("Security.InsertOperatorLog", new DataParameter[]{new DataParameter("@LoginUser",this.txtUserName.Text.Trim()),new DataParameter("@LoginTime",DateTime.Now),
                                                         new DataParameter("@LoginModule","登录页面"),new DataParameter("@ExecuteOperator","登录(用户密码有误)")});
                            ltlMessage.Text = "对不起,您输入的密码有误!";
                        }
                    }
                    else
                    {
                        ltlMessage.Text = "对不起,您输入的用户名不存在!";
                    }

                }
                catch (Exception exp)
                {
                    System.Diagnostics.StackFrame frame = new System.Diagnostics.StackFrame(0);
                    Session["ModuleName"] = this.Page.Title;
                    Session["FunctionName"] = frame.GetMethod().Name;
                    Session["ExceptionalType"] = exp.GetType().FullName;
                    Session["ExceptionalDescription"] = exp.Message;
                    Response.Redirect("Common/MistakesPage.aspx", false);
                }
            }
            else
            {
                ltlMessage.Text = "请输入用户名!";
            }
        }
    }
}