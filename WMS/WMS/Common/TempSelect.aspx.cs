using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WMS.Common
{
    public partial class TempSelect : System.Web.UI.Page
    {
        
        protected string Option;
        
        protected string strWhere;
        protected string TableName;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Option = Request.Params["option"] + "";
            if (Request.Params["where"] != null)
                strWhere = Server.UrlEncode(Request.Params["where"] + "");
            else
                strWhere = "1=1";

            TableName = Request.QueryString["TableName"];
        }
    }
}