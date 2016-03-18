using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strUserName;
        if (Session["G_user"] != null)
        {
            strUserName = Session["G_user"].ToString();
        }
        else
        {
            strUserName = "";
        }

        //入库单分配权限控制【一次只允许一个用户进行分配】

        if (Application["MNU_M00B_00D"] != null && Application["MNU_M00B_00D"].ToString() == Session["G_user"].ToString())
        {
            Application["MNU_M00B_00D"] = null;
        }
        //出库单分配权限控制【一次只允许一个用户进行分配】

        if (Application["MNU_M00E_00D"] != null && Application["MNU_M00E_00D"].ToString() == Session["G_user"].ToString())
        {
            Application["MNU_M00E_00D"] = null;
        }
        //移位单生成权限控制【一次只允许一个用户进行生成移位单】

        if (Application["MNU_M00D_00G"] != null && Application["MNU_M00D_00G"].ToString() == Session["G_user"].ToString())
        {
            Application["MNU_M00D_00G"] = null;
        }
        //OperatorLog bll = new OperatorLog();
        //bll.InsertOperationLog(DateTime.Now, LoginUserInfo.UserName, "退出系统", "用户退出");

        HttpContext.Current.Cache.Remove(strUserName);
        Session.Abandon();
        GC.Collect();
        string strScript = "<script language='javascript'>window.opener=null; window.open('','_self','');window.close();</script>";
        Page.RegisterStartupScript("a", strScript);
    }
}
