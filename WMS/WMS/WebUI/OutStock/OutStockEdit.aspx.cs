using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Util;


namespace WMS.WebUI.OutStock
{
    public partial class OutStockEdit :App_Code.BasePage
    {
        protected string strID;
        BLL.BLLBase bll = new BLL.BLLBase();
        protected void Page_Load(object sender, EventArgs e)
        {
            strID = Request.QueryString["ID"] + "";
            this.dgViewSub1.PageSize = pageSubSize;
            if (!IsPostBack)
            {
                BindDropDownList();
                if (strID != "")
                {
                    DataTable dt = bll.FillDataTable("WMS.SelectBillMaster", new DataParameter[] { new DataParameter("{0}", string.Format("BillID='{0}'", strID)) });
                    BindData(dt);

                    SetTextReadOnly(this.txtID);
                }
                else
                {
                    BindDataSub();

                    txtBillDate.changed = "$('#txtID').val(autoCodeByTableName('OS', '1=1','WMS_BillMaster', 'txtBillDate'));";
                    this.txtBillDate.DateValue = DateTime.Now;
                    this.txtID.Text = bll.GetAutoCodeByTableName("OS", "WMS_BillMaster", DateTime.Now, "1=1");

                    this.txtCreator.Text = Session["EmployeeCode"].ToString();
                    this.txtUpdater.Text = Session["EmployeeCode"].ToString();
                    this.txtCreatDate.Text = ToYMD(DateTime.Now);
                    this.txtUpdateDate.Text = ToYMD(DateTime.Now);
                }
            }
          
            ScriptManager.RegisterStartupScript(this.updatePanel1, this.updatePanel1.GetType(), "Resize", "resize();BindEvent();", true);
            writeJsvar(FormID, SqlCmd, strID);
            SetTextReadOnly(this.txtCreator, this.txtCreatDate, this.txtUpdater, this.txtUpdateDate);


        }

        private void BindDropDownList()
        {
            DataTable dtArea = bll.FillDataTable("Cmd.SelectArea");
            this.ddlAreaCode.DataValueField = "AreaCode";
            this.ddlAreaCode.DataTextField = "AreaName";
            this.ddlAreaCode.DataSource = dtArea;
            this.ddlAreaCode.DataBind();

            DataTable ProductType = bll.FillDataTable("Cmd.SelectProductType", new DataParameter[] { new DataParameter("{0}", "cmd.AreaCode='001' and ProductTypeCode<>'0001'") });
            this.ddlTrainTypeCode.DataValueField = "ProductTypeCode";
            this.ddlTrainTypeCode.DataTextField = "ProductTypeName";
            this.ddlTrainTypeCode.DataSource = ProductType;
            this.ddlTrainTypeCode.DataBind();

            DataTable dtBillType = bll.FillDataTable("Cmd.SelectBillType",new DataParameter[]{new DataParameter("{0}","Flag=2")});
            this.ddlBillTypeCode.DataValueField = "BillTypeCode";
            this.ddlBillTypeCode.DataTextField = "BillTypeName";
            this.ddlBillTypeCode.DataSource = dtBillType;
            this.ddlBillTypeCode.DataBind();

        }


        private void BindData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.txtID.Text = dt.Rows[0]["BillID"].ToString();
                this.txtBillDate.DateValue = dt.Rows[0]["BillDate"];
                this.ddlAreaCode.SelectedValue = dt.Rows[0]["AreaCode"].ToString();
                this.ddlBillTypeCode.SelectedValue = dt.Rows[0]["BillTypeCode"].ToString();
                this.ddlTrainTypeCode.SelectedValue = dt.Rows[0]["TrainTypeCode"].ToString();
                this.txtTrainNo.Text = dt.Rows[0]["TrainNo"].ToString();
                this.txtAxieLocation.Text = dt.Rows[0]["AxieLocation"].ToString();
                this.txtXc.Text = dt.Rows[0]["XC"].ToString();
                this.txtCcnz.Text = dt.Rows[0]["Ccnz"].ToString();
                this.txtCcwz.Text = dt.Rows[0]["Ccwz"].ToString();
                this.txtFccnz.Text = dt.Rows[0]["Fccnz"].ToString();
                this.txtFccwz.Text = dt.Rows[0]["Fccwz"].ToString();
                this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
                this.txtCreator.Text = dt.Rows[0]["Creator"].ToString();
                this.txtCreatDate.Text = ToYMD(dt.Rows[0]["CreateDate"]);
                this.txtUpdater.Text = dt.Rows[0]["Updater"].ToString();
                this.txtUpdateDate.Text = ToYMD(dt.Rows[0]["UpdateDate"]);
            }
            BindDataSub();
        }

        private void BindDataSub()
        {
            DataTable dt = bll.FillDataTable("WMS.SelectBillDetail", new DataParameter[] { new DataParameter("{0}",string.Format("BillID='{0}'", this.txtID.Text)) });
            Session[FormID + "_Edit_dgViewSub1"] = dt;
            this.dgViewSub1.DataSource = dt;
            this.dgViewSub1.DataBind();
            MovePage("Edit", this.dgViewSub1, 0, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);

        }

        protected void dgViewSub1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                SetTextReadOnly((TextBox)e.Row.FindControl("ProductName"));
                ((Label)e.Row.FindControl("RowID")).Text = drv.Row.ItemArray[drv.DataView.Table.Columns.IndexOf("RowID")].ToString();
                ((TextBox)e.Row.FindControl("ProductCode")).Text = drv.Row.ItemArray[drv.DataView.Table.Columns.IndexOf("ProductCode")].ToString();
                ((TextBox)e.Row.FindControl("ProductName")).Text = drv.Row.ItemArray[drv.DataView.Table.Columns.IndexOf("ProductName")].ToString();
                ((TextBox)e.Row.FindControl("Quantity")).Text = drv.Row.ItemArray[drv.DataView.Table.Columns.IndexOf("Quantity")].ToString();
                ((TextBox)e.Row.FindControl("SubMemo")).Text = drv.Row.ItemArray[drv.DataView.Table.Columns.IndexOf("Memo")].ToString();
            }
        }

        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            UpdateTempSub(this.dgViewSub1);
            DataTable dt = (DataTable)Session[FormID + "_Edit_dgViewSub1"];
            DataTable dt1 = Util.JsonHelper.Json2Dtb(hdnMulSelect.Value);

            DataView dv = dt.DefaultView;
            dv.Sort = "RowID";
            dt = dv.ToTable();

            DataRow dr;
            int cur = dt.Rows.Count;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                dr = dt.NewRow();
                dt.Rows.InsertAt(dr, cur + i);


                dr["RowID"] = cur + i + 1;

                dr["BillID"] = this.txtID.Text.Trim();
                dr["ProductCode"] = dt1.Rows[i]["ProductCode"];
                dr["ProductName"] = dt1.Rows[i]["ProductName"];
                dr["Quantity"] = 1;

            }

            this.dgViewSub1.DataSource = dt;
            this.dgViewSub1.DataBind();
            Session[FormID + "_Edit_dgViewSub1"] = dt;
            MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageIndex, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);




            //int pagecount = this.dgViewSub1.PageCount;

            //DataTable dt = (DataTable)Session[FormID + "_Edit_dgViewSub1"];
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[dt.Rows.Count - 1]["ProductCode"].ToString() == "")
            //    {
            //        return;
            //    }
            //}
            //DataRow dr;
            //dr = dt.NewRow();
            //dr["RowID"] = dt.Rows.Count + 1;
            //dr["Quantity"] = 1;

            //dt.Rows.Add(dr);
            //this.dgViewSub1.DataSource = dt;
            //this.dgViewSub1.DataBind();
            //Session[FormID + "_Edit_dgViewSub1"] = dt;
            //MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageCount, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }
        protected void btnDelDetail_Click(object sender, EventArgs e)
        {
            UpdateTempSub(this.dgViewSub1);
            DataTable dt = (DataTable)Session[FormID + "_Edit_" + dgViewSub1.ID];
            int RowID = 0;
            for (int i = 0; i < this.dgViewSub1.Rows.Count; i++)
            {
                CheckBox cb = (CheckBox)(this.dgViewSub1.Rows[i].FindControl("cbSelect"));
                if (cb != null && cb.Checked && cb.Enabled)
                {
                    Label hk = (Label)(this.dgViewSub1.Rows[i].Cells[1].FindControl("RowID"));
                    RowID = int.Parse(hk.Text);
                    DataRow[] drs = dt.Select(string.Format("RowID ={0}", hk.Text));
                    for (int j = 0; j < drs.Length; j++)
                        dt.Rows.Remove(drs[j]);

                }
            }
            this.dgViewSub1.DataSource = dt;
            this.dgViewSub1.DataBind();
            Session[FormID + "_Edit_" + dgViewSub1.ID] = dt;
            MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageIndex, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);

        }

       
        
       

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            UpdateTempSub(this.dgViewSub1);
            DataTable dt = (DataTable)Session[FormID + "_Edit_dgViewSub1"];
            DataTable dt1 = Util.JsonHelper.Json2Dtb(hdnMulSelect.Value);
            int cur = int.Parse(((Button)sender).ClientID.Split('_')[1].Replace("ctl", "")) - 2 + this.dgViewSub1.PageSize * dgViewSub1.PageIndex;
            if (dt1.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select("RowID>" + (cur + 1));
                for (int j = 0; j < drs.Length; j++)
                {
                    drs[j].BeginEdit();
                    drs[j]["RowID"] = cur + j + 1 + dt1.Rows.Count;
                    drs[j].EndEdit();
                }
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "RowID";
            dt = dv.ToTable();

            DataRow dr;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (i == 0)
                {
                    dr = dt.Rows[cur];
                }
                else
                {
                    dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, cur + i);
                   
                }
                dr["RowID"] = i + cur + 1;
               
                dr["BillID"] = this.txtID.Text.Trim();
                dr["ProductCode"] = dt1.Rows[i]["ProductCode"];
                dr["ProductName"] = dt1.Rows[i]["ProductName"];
                dr["Quantity"] = 1;
                 
            }
           
            this.dgViewSub1.DataSource = dt;
            this.dgViewSub1.DataBind();
            Session[FormID + "_Edit_dgViewSub1"] = dt;
            MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageIndex, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }



        public override void UpdateTempSub(GridView dgv)
        {
            DataTable dt1 = (DataTable)Session[FormID + "_Edit_" + dgv.ID];
            if (dt1.Rows.Count == 0)
            {
                this.ddlAreaCode.Enabled = true;
                this.ddlTrainTypeCode.Enabled = true;
                return;
            }
            DataRow dr;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dr = dt1.Rows[i + dgv.PageSize * dgv.PageIndex];
                dr.BeginEdit();
                
                dr["BillID"] = this.txtID.Text.Trim();
                dr["RowID"] = ((Label)dgv.Rows[i].FindControl("RowID")).Text;
                dr["ProductCode"] = ((TextBox)dgv.Rows[i].FindControl("ProductCode")).Text;
                dr["ProductName"] = ((TextBox)dgv.Rows[i].FindControl("ProductName")).Text;
                dr["Quantity"] = ((TextBox)dgv.Rows[i].FindControl("Quantity")).Text;
                dr["Memo"] = ((TextBox)dgv.Rows[i].FindControl("SubMemo")).Text;
                dr.EndEdit();
            }
            dt1.AcceptChanges();
            if (dt1.Rows.Count > 0)
            {
                this.ddlAreaCode.Enabled = false;
                this.ddlTrainTypeCode.Enabled = false;
            }

            object o = dt1.Compute("SUM(Quantity)", "");
            this.txtTotalQty.Text = o.ToString();
            Session[FormID + "_Edit_" + dgv.ID] = dt1;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UpdateTempSub(this.dgViewSub1);
            string[] Commands = new string[3];
            DataParameter[] para;

            //判断库存
            DataTable dt = (DataTable)Session[FormID + "_Edit_dgViewSub1"];

            DataTable dtProduct = dt.DefaultView.ToTable("Product", true, new string[] { "ProductCode", "ProductName" });

            for (int i = 0; i < dtProduct.Rows.Count; i++)
            {
                object o = dt.Compute("Sum(Quantity)", string.Format("ProductCode='{0}'", dtProduct.Rows[i]["ProductCode"]));
                if (o != null)
                {
                    int Qty = int.Parse(o.ToString());

                    DataTable dtProductQty = bll.FillDataTable("WMS.SelectProductQty", new DataParameter[] { new DataParameter("@BillID", this.txtID.Text), new DataParameter("@ProductCode", dtProduct.Rows[i]["ProductCode"].ToString()) });
                    int StockQty = 0;
                    bool blnvalue = false;
                    if (dtProductQty.Rows.Count == 0)
                    {
                        blnvalue = true;
                    }
                    else
                    {
                        StockQty = int.Parse(dtProductQty.Rows[0]["StockQty"].ToString());
                        if (Qty > StockQty)
                            blnvalue = true;
                    }
                    if (blnvalue)
                    {

                        WMS.App_Code.JScript.Instance.ShowMessage(this.updatePanel1, dtProduct.Rows[i]["ProductName"].ToString() + "现有库存数量为：" + StockQty.ToString() + ", 库存不足，请修改出库数量。");
                        return;

                    }
                }

            }
            if (strID == "") //新增
            {
                int Count = bll.GetRowCount("WMS_BillMaster", string.Format("BillID='{0}'", this.txtID.Text.Trim()));
                if (Count > 0)
                {
                    WMS.App_Code.JScript.Instance.ShowMessage(this.updatePanel1, "该出库单已经存在！");
                    return;
                }
                para = new DataParameter[] { 
                                             new DataParameter("@BillID", this.txtID.Text.Trim()),
                                             new DataParameter("@BillDate", this.txtBillDate.DateValue),
                                             new DataParameter("@BillTypeCode",this.ddlBillTypeCode.SelectedValue),
                                             new DataParameter("@AreaCode",this.ddlAreaCode.SelectedValue),
                                             new DataParameter("@TrainTypeCode",this.ddlTrainTypeCode.SelectedValue),
                                             new DataParameter("@TrainNo",this.txtTrainNo.Text),
                                             new DataParameter("@AxieLocation",this.txtAxieLocation.Text),
                                             new DataParameter("@Xc",this.txtXc.Text),
                                             new DataParameter("@Ccnz",this.txtCcnz.Text),
                                             new DataParameter("@Ccwz",this.txtCcwz.Text),
                                             new DataParameter("@Fccnz",this.txtFccnz.Text),
                                             new DataParameter("@Fccwz",this.txtFccwz.Text),
                                             new DataParameter("@Memo", this.txtMemo.Text.Trim()),
                                             new DataParameter("@Creator", Session["EmployeeCode"].ToString()),
                                             new DataParameter("@Updater", Session["EmployeeCode"].ToString())
                                             
                                              };
                Commands[0] = "WMS.InsertOutStockBill";

            }
            else //修改
            {
                para = new DataParameter[] { 
                                             new DataParameter("@BillDate", this.txtBillDate.DateValue),
                                              new DataParameter("@BillDate", this.txtBillDate.DateValue),
                                             new DataParameter("@BillTypeCode",this.ddlBillTypeCode.SelectedValue),
                                             new DataParameter("@AreaCode",this.ddlAreaCode.SelectedValue),
                                             new DataParameter("@TrainTypeCode",this.ddlTrainTypeCode.SelectedValue),
                                             new DataParameter("@TrainNo",this.txtTrainNo.Text),
                                             new DataParameter("@AxieLocation",this.txtAxieLocation.Text),
                                             new DataParameter("@Xc",this.txtXc.Text),
                                             new DataParameter("@Ccnz",this.txtCcnz.Text),
                                             new DataParameter("@Ccwz",this.txtCcwz.Text),
                                             new DataParameter("@Fccnz",this.txtFccnz.Text),
                                             new DataParameter("@Fccwz",this.txtFccwz.Text),
                                             new DataParameter("@Memo", this.txtMemo.Text.Trim()),
                                             new DataParameter("@Updater", Session["EmployeeCode"].ToString()),
                                             new DataParameter("{0}",string.Format("BillID='{0}'", this.txtID.Text.Trim())) };
                Commands[0] = "WMS.UpdateOutStock";
            }
            try
            {
                Commands[1] = "WMS.DeleteBillDetail";
                Commands[2] = "WMS.InsertInStockDetail";
                bll.ExecTran(Commands, para, "BillID", new DataTable[] { dt });
            }
            catch (Exception ex)
            {
                WMS.App_Code.JScript.Instance.ShowMessage(this.updatePanel1, ex.Message);
                return;
            }

            Response.Redirect(FormID + "View.aspx?SubModuleCode=" + SubModuleCode + "&FormID=" + Server.UrlEncode(FormID) + "&SqlCmd=" + SqlCmd + "&ID=" + Server.UrlEncode(this.txtID.Text));
        }
      
        #region 子表绑定

        protected void btnFirstSub1_Click(object sender, EventArgs e)
        {
            MovePage("Edit", this.dgViewSub1, 0, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }

        protected void btnPreSub1_Click(object sender, EventArgs e)
        {
            MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageIndex - 1, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }

        protected void btnNextSub1_Click(object sender, EventArgs e)
        {
            MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageIndex + 1, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }

        protected void btnLastSub1_Click(object sender, EventArgs e)
        {
            MovePage("Edit", this.dgViewSub1, this.dgViewSub1.PageCount - 1, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }

        protected void btnToPageSub1_Click(object sender, EventArgs e)
        {
            MovePage("Edit", this.dgViewSub1, int.Parse(this.txtPageNoSub1.Text) - 1, btnFirstSub1, btnPreSub1, btnNextSub1, btnLastSub1, btnToPageSub1, lblCurrentPageSub1);
        }

      
       
        #endregion

        protected void ddlAreaCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string AreaCode = ddlAreaCode.SelectedValue;
            DataTable ProductType = bll.FillDataTable("Cmd.SelectProductType", new DataParameter[] { new DataParameter("{0}", "cmd.AreaCode='" + AreaCode + "' and ProductTypeCode<>'0001'") });
            this.ddlTrainTypeCode.DataValueField = "ProductTypeCode";
            this.ddlTrainTypeCode.DataTextField = "ProductTypeName";
            this.ddlTrainTypeCode.DataSource = ProductType;
            this.ddlTrainTypeCode.DataBind();
            SetGridViewEmptyRow(this.dgViewSub1, "Edit");
        }

    }
}