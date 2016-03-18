using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Util;


namespace WMS.WebUI.CMD
{
    public partial class ProductChangeNo : App_Code.BasePage
    {
        protected string strID;
        BLL.BLLBase bll = new BLL.BLLBase();
        protected void Page_Load(object sender, EventArgs e)
        {
            strID = Request.QueryString["ID"] + "";
            
            if (!IsPostBack)
            {
                BindDropDownList();
            }
            SetTextReadOnly(this.txtProductCode, this.txtProductName);

        }
        private void BindDropDownList()
        {

            this.txtProductCode.Text = strID;
            this.txtProductName.Text = bll.GetFieldValue("CMD_Product", "ProductName", "ProductCode='" + strID + "'");
            this.hdnArea.Value = bll.GetFieldValue("CMD_Product", "AreaCode", "ProductCode='" + strID + "'");
            

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                DataParameter[] para = new DataParameter[] { 
                                             new DataParameter("@ProductCode",strID),
                                             new DataParameter("@ProductNewCode",this.txtProductNewCode.Text),
                                             new DataParameter("@UserName", Session["EmployeeCode"].ToString()),
                                             };

                bll.ExecNonQueryTran("WMS.spBatchChangeProductNo", para);
                App_Code.JScript.Instance.ShowMessage(this, "编号修改成功！");
                


                Response.Redirect(FormID + "View.aspx?SubModuleCode=" + SubModuleCode + "&FormID=" + Server.UrlEncode(FormID) + "&SqlCmd=" + SqlCmd + "&ID=" + Server.UrlEncode(this.txtProductNewCode.Text));

            }
            catch (Exception ex)
            {
                WMS.App_Code.JScript.Instance.ShowMessage(this, ex.Message);
                return;
            }
        }
    }
}