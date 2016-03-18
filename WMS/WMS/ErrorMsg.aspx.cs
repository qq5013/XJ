using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WMS
{
    public partial class ErrorMsg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblMsg.Text = "<br>程序发生意外，请重试或与开发商联系！" + Environment.NewLine + Server.GetLastError().Message;
                Server.ClearError();
            }
        }
    }
}