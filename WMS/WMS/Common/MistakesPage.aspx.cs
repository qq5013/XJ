using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Util;

namespace WMS.Common
{
    public partial class MistakesPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    string strModuleName, strFunctionName, strExceptionalType, strExceptionalDescription;
                    strModuleName = Session["ModuleName"].ToString();
                    strFunctionName = Session["FunctionName"].ToString();
                    strExceptionalType = Session["ExceptionalType"].ToString();
                    strExceptionalDescription = Session["ExceptionalDescription"].ToString();

                    labModuleName.Text = strModuleName;
                    labFunctionName.Text = strFunctionName;
                    labExceptionalType.Text = strExceptionalType;
                    labExceptionalDescription.Text = strExceptionalDescription;
                    
                    BLL.BLLBase bll = new BLL.BLLBase();
                    bll.ExecNonQuery("Security.InsertExceptionalLog", new DataParameter[] { new DataParameter("@CatchTime", System.DateTime.Now), new DataParameter("@ModuleName", strModuleName),
                                                                      new DataParameter("@FunctionName", strFunctionName), new DataParameter("@ExceptionalType", strExceptionalType), new DataParameter("@ExceptionalDescription", strExceptionalDescription) });
                }     
              
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}