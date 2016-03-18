using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;

namespace App.View.Task
{
    public partial class frmInStockTask : Form
    {
        BLL.BLLBase bll = new BLL.BLLBase();
        DataRow dr;
        string CraneNo = "01";
        public frmInStockTask()
        {
            InitializeComponent();
        }
        public frmInStockTask(DataRow dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        private void frmInStockTask_Load(object sender, EventArgs e)
        {
            this.txtTaskNo.Text = dr["TaskNo"].ToString();
            this.txtBillID.Text = dr["BillID"].ToString();
            this.txtProductCode.Text = dr["ProductCode"].ToString();
            this.txtProductName.Text = dr["ProductName"].ToString();
            this.txtAreaCode.Text = dr["AreaCode"].ToString();
            CraneNo = dr["CraneNo"].ToString();
            this.txtCraneNo.Text = CraneNo;

            DataTable dt = bll.FillDataTable("CMD.SelectCar", new DataParameter[] { new DataParameter("{0}", string.Format("CraneNo='{0}'", CraneNo)) });
            this.cmbCarNo.DataSource = dt.DefaultView;
            this.cmbCarNo.ValueMember = "CarNo";
            this.cmbCarNo.DisplayMember = "CarNo";
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.cbRow.Enabled = false;
                this.cbColumn.Enabled = false;
                this.cbHeight.Enabled = false;
            }
            else
            {
                this.cbRow.Enabled = true;
                this.cbColumn.Enabled = true;
                this.cbHeight.Enabled = true;
            }
        }

        private void cmbCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataParameter[] param = new DataParameter[] 
            { 
                new DataParameter("{0}", string.Format("CraneNo='{0}' and AreaCode='{1}'", CraneNo,this.txtAreaCode.Text))
            };
            DataTable dt = bll.FillDataTable("CMD.SelectCellShelf", param);
            this.cbRow.DataSource = dt.DefaultView;
            this.cbRow.ValueMember = "shelfcode";
            this.cbRow.DisplayMember = "shelfcode";
        }

        private void cbRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbRow.Text == "System.Data.DataRowView")
                return;

            DataParameter[] param = new DataParameter[] 
            { 
                new DataParameter("{0}", string.Format("ShelfCode='{0}' and AreaCode='{1}'",this.cbRow.Text,this.txtAreaCode.Text))
            };
            DataTable dt = bll.FillDataTable("CMD.SelectColumn", param);

            this.cbColumn.DataSource = dt.DefaultView;
            this.cbColumn.ValueMember = "CellColumn";
            this.cbColumn.DisplayMember = "CellColumn";
        }

        private void cbColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbRow.Text == "System.Data.DataRowView")
                return;
            if (this.cbColumn.Text == "System.Data.DataRowView")
                return;

            DataParameter[] param = new DataParameter[] 
            { 
                new DataParameter("{0}", string.Format("ShelfCode='{0}' and CellColumn={1} and AreaCode='{2}'",this.cbRow.Text,this.cbColumn.Text,this.txtAreaCode.Text))
            };
            DataTable dt = bll.FillDataTable("CMD.SelectCell", param);
            DataView dv = dt.DefaultView;
            dv.Sort = "CellRow";
            this.cbHeight.DataSource = dv;
            this.cbHeight.ValueMember = "CellRow";
            this.cbHeight.DisplayMember = "CellRow";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataParameter[] param;

            param = new DataParameter[] 
            { 
                new DataParameter("@CraneNo", this.txtCraneNo.Text), 
                new DataParameter("@CarNo", this.cmbCarNo.Text),
                new DataParameter("@AreaCode", this.txtAreaCode.Text) 
            };
            string ProductCode = "00" + this.txtCraneNo.Text;

            if (this.radioButton4.Checked)
            {
                if (this.radioButton1.Checked)
                {
                    dt = bll.FillDataTable("WCS.sp_GetCell", param);
                    if (dt.Rows.Count > 0)
                        this.txtCellCode.Text = dt.Rows[0][0].ToString();
                    else
                        this.txtCellCode.Text = "";
                }
                else
                {
                    this.txtCellCode.Text = this.cbRow.Text.Substring(3, 3) + (1000 + int.Parse(this.cbColumn.Text)).ToString().Substring(1, 3) + (1000 + int.Parse(this.cbHeight.Text)).ToString().Substring(1, 3);
                }
                

                //判断货位是否空闲，且只有空托盘
                param = new DataParameter[] 
                { 
                    new DataParameter("{0}", string.Format("CellCode='{0}' and ProductCode='{1}' and IsActive='1' and IsLock='0' and AreaCode='{2}'", this.txtCellCode.Text,ProductCode,this.txtAreaCode.Text))
                };
                dt = bll.FillDataTable("CMD.SelectCell", param);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("自动获取的货位或指定的货位无空托盘或非指定库区的货位,请确认！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //锁定货位
                param = new DataParameter[] 
                { 
                    new DataParameter("@CarNo", this.cmbCarNo.Text),     
                    new DataParameter("@CraneNo", this.txtCraneNo.Text),                
                    new DataParameter("@CellCode", this.txtCellCode.Text),
                    new DataParameter("@BillID", this.txtBillID.Text),
                    new DataParameter("@TaskNo", this.txtTaskNo.Text),
                    new DataParameter("@ProductCode", this.txtProductCode.Text),
                };
                bll.ExecNonQueryTran("WCS.Sp_CreatePalletOutTask", param);
            }
            else if(this.radioButton3.Checked)
            {
                //自带托盘的处理
                dt = bll.FillDataTable("WCS.sp_GetEmptyCellWithPallet", param);
                if (dt.Rows.Count > 0)
                    this.txtCellCode.Text = dt.Rows[0][0].ToString();
                else
                    this.txtCellCode.Text = "";

                //判断货位是否空闲，且只有空托盘
                param = new DataParameter[] 
                { 
                    new DataParameter("{0}", string.Format("CellCode='{0}' and ProductCode='' and IsActive='1' and IsLock='0'", this.txtCellCode.Text))
                };
                dt = bll.FillDataTable("CMD.SelectCell", param);
                if (dt.Rows.Count <= 0) 
                {
                    MessageBox.Show("自动获取的货位或指定的货位非空货位,请确认！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //锁定货位
                    param = new DataParameter[] 
                    { 
                        new DataParameter("@TaskNo", this.txtTaskNo.Text),
                        new DataParameter("@CarNo", this.cmbCarNo.Text),
                        new DataParameter("@CellCode", this.txtCellCode.Text)                        
                    };
                    bll.ExecNonQueryTran("WCS.Sp_CreateProductInTask", param);
                }
            }
            else if (this.radioButton5.Checked)
            {
                //用出库的空托盘位
                dt = bll.FillDataTable("WCS.sp_GetOutStockCell", param);
                if (dt.Rows.Count > 0)
                    this.txtCellCode.Text = dt.Rows[0][0].ToString();
                else
                    this.txtCellCode.Text = "";

                //判断货位是否空闲，且只有空托盘
                param = new DataParameter[] 
                { 
                    new DataParameter("{0}", string.Format("CellCode='{0}' and ProductCode='{1}' and IsActive='1' and IsLock='1'", this.txtCellCode.Text,ProductCode))
                };
                dt = bll.FillDataTable("CMD.SelectCell", param);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("获取出库的空托盘货位异常,请确认！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //锁定货位
                    param = new DataParameter[] 
                    { 
                        new DataParameter("@TaskNo", this.txtTaskNo.Text),
                        new DataParameter("@CarNo", this.cmbCarNo.Text),
                        new DataParameter("@CellCode", this.txtCellCode.Text)                        
                    };
                    bll.ExecNonQueryTran("WCS.Sp_CreateProductInTask", param);
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
