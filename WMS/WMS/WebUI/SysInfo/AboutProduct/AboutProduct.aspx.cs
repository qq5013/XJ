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

namespace WMS.WebUI.SysInfo.AboutProduct
{
    public partial class AboutProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                BLL.BLLBase bll = new BLL.BLLBase();

                dt = bll.FillDataTable("Security.SelectSoftWareInfo", null);
            }
            catch (Exception exp)
            {
                Session["ModuleName"] = "关于模块";
                Session["FunctionName"] = "Page_Load";
                Session["ExceptionalType"] = exp.GetType().FullName;
                Session["ExceptionalDescription"] = exp.Message;
                Response.Redirect("MistakesPage.aspx");
            }

            LabVersion.Font.Size = FontUnit.Smaller;
            labCompany.Font.Size = FontUnit.Smaller;
            labCopyrigth.Font.Size = FontUnit.Smaller;
            labCompanyTelephone.Font.Size = FontUnit.Smaller;
            labCompanyFax.Font.Size = FontUnit.Smaller;
            labCompanyAddress.Font.Size = FontUnit.Smaller;
            
            labCompanyWeb.Font.Size = FontUnit.Smaller;
            lbtnCompanyWeb.Font.Size = FontUnit.Smaller;
            lbtnQuit.Font.Size = FontUnit.Smaller;

            if (dt.Rows.Count > 0)
            {
                // LabSoftWareName.Text = dt.Rows[0][0].ToString();
                //LabSoftWareName.Font.Size = FontUnit.Smaller;
                LabVersion.Text = "软件版本:" + dt.Rows[0]["Version"].ToString();
                LabVersion.Font.Size = FontUnit.Smaller;
                this.lblSoftwareName.Text = "软件名称:" + dt.Rows[0]["SoftwareName"].ToString();
                this.lblSoftwareName.Font.Size = FontUnit.Smaller;

                labCompany.Text = "公司名称:" + dt.Rows[0]["Company"].ToString();
                labCompany.Font.Size = FontUnit.Smaller;
                labCopyrigth.Text = dt.Rows[0]["Copyrigth"].ToString();
                labCopyrigth.Font.Size = FontUnit.Smaller;
                labCompanyTelephone.Text = "公司电话:" + dt.Rows[0]["CompanyTelephone"].ToString();
                labCompanyTelephone.Font.Size = FontUnit.Smaller;
                labCompanyFax.Text = "公司传真:" + dt.Rows[0]["CompanyFax"].ToString();
                labCompanyFax.Font.Size = FontUnit.Smaller;
                labCompanyAddress.Text = "公司地址:" + dt.Rows[0]["CompanyAddress"].ToString();
                labCompanyAddress.Font.Size = FontUnit.Smaller;
                
                labCompanyWeb.Text = "公司网址:";
                labCompanyWeb.Font.Size = FontUnit.Smaller;
                lbtnCompanyWeb.Text = dt.Rows[0]["CompanyWeb"].ToString();
                lbtnCompanyWeb.Font.Size = FontUnit.Smaller;

            }
        }



        protected void lbtnCompanyWeb_Click(object sender, EventArgs e)
        {
            string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.open ('http://" + lbtnCompanyWeb.Text + "','_blank')" + "</SCRIPT>";
            ClientScript.RegisterStartupScript(this.GetType(), "", strScript);
             
        }
        //protected void lbtnCompanyEmail_Click(object sender, EventArgs e)
        //{
        //    string strScript = @"<SCRIPT LANGUAGE='javascript'> " + "window.open ('mailto:" + lbtnCompanyEmail.Text + "','newwindow')" + "</SCRIPT>";
        //    ClientScript.RegisterStartupScript(this.GetType(), "", strScript);
        //}
        protected void lbtnSoftWareName_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnVersion_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnQuit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../../MainPage.aspx");
        }
    }
}