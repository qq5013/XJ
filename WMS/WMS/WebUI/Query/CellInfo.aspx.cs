using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Util;


namespace WMS.WebUI.Query
{
    public partial class CellInfo : App_Code.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strID = Request.QueryString["Where"] + "";
            BLL.BLLBase bll=new BLL.BLLBase();
            DataTable dtCell = bll.FillDataTable("CMD.SelectWareHouseCellInfoByCell", new DataParameter[] { new DataParameter("@CellCode", strID) });
            if (dtCell.Rows.Count > 0)
            {
                DataRow dr=dtCell.Rows[0];
                lblProductName.Text =dr["ProductName"].ToString();
                lblBarcode.Text = dr["PalletBarcode"].ToString();
                lblPalletCode.Text = dr["PalletCode"].ToString();
                lblBillNo.Text = dr["BillNo"].ToString();
                lblIndate.Text = ToYMDHM(dr["InDate"]);

                lblAreaName.Text = dr["AreaName"].ToString();
                lblShelfName.Text = dr["ShelfName"].ToString();
                lblCellColumn.Text = dr["CellColumn"].ToString();
                lblCellRow.Text = dr["CellRow"].ToString();


                if (dr["IsLock"].ToString() == "0")
                    lblState.Text = "正常";
                else
                    lblState.Text = "锁定";
                if (dr["ErrorFlag"].ToString() == "1")
                    lblState.Text = "异常";

            }




        }
    }
}