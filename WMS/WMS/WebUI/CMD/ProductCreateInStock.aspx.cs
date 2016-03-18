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
    public partial class ProductCreateInStock : App_Code.BasePage
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

        }
        private void BindDropDownList()
        {
            

            DataTable dtFactory = bll.FillDataTable("Cmd.SelectFactory");
            DataRow dr = dtFactory.NewRow();
            dr["FactoryID"] = "";
            dr["FactoryName"] = "请选择";
            dtFactory.Rows.InsertAt(dr, 0);

            this.ddlFactoryID.DataValueField = "FactoryID";
            this.ddlFactoryID.DataTextField = "FactoryName";
            this.ddlFactoryID.DataSource = dtFactory;
            this.ddlFactoryID.DataBind();

            DataTable dtBillType = bll.FillDataTable("Cmd.SelectBillType", new DataParameter[] { new DataParameter("{0}", "Flag=1") });
            this.ddlBillTypeCode.DataValueField = "BillTypeCode";
            this.ddlBillTypeCode.DataTextField = "BillTypeName";
            this.ddlBillTypeCode.DataSource = dtBillType;
            this.ddlBillTypeCode.DataBind();

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                DataParameter[] para = new DataParameter[] { 
                                             new DataParameter("@ProductCode",strID),
                                             new DataParameter("@BillTypeCode",this.ddlBillTypeCode.SelectedValue),
                                             new DataParameter("@FactoryID",this.ddlFactoryID.SelectedValue),
                                             new DataParameter("@UserName", Session["EmployeeCode"].ToString()),
                                            };

                bll.ExecNonQueryTran("WMS.spCreateInStockByProduct", para);
                ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "myscript", "<script type='text/javascript'>alert('入库单新增成功!'); Exit();</script>");
            }
            catch (Exception ex)
            {
                WMS.App_Code.JScript.Instance.ShowMessage(this, ex.Message);
                return;
            }
        }
    }
}