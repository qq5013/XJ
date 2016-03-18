using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WMS.Web.Base
{
    public partial class CigaretteEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string json = "{success:true,result:true}";

            Response.Write(json);
            Response.End(); 
        }
    }
}