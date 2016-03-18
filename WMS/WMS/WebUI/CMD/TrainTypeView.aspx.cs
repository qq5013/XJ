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
    public partial class TrainTypeView :App_Code.BasePage
    {
        private string strID;
        private string TableName = "CMD_TrainType";
        private string PrimaryKey = "TypeCode";
        BLL.BLLBase bll = new BLL.BLLBase();
        protected void Page_Load(object sender, EventArgs e)
        {
            strID = Request.QueryString["ID"] + "";
            if (!IsPostBack)
            {
                BindDropDownList();
                DataTable dt = bll.FillDataTable("Cmd.SelectTrainType", new DataParameter[] { new DataParameter("{0}", string.Format("TypeCode='{0}'", strID)) });
                BindData(dt);
                writeJsvar(FormID, SqlCmd, strID);
            }
        }

        #region 绑定方法
        private void BindDropDownList()
        {
             

        }

        private void BindData(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                this.txtID.Text = dt.Rows[0]["TypeCode"].ToString();
                this.txtTypeName.Text = dt.Rows[0]["TypeName"].ToString();

                this.txtMemo.Text = dt.Rows[0]["Memo"].ToString();
                this.txtCreator.Text = dt.Rows[0]["Creator"].ToString();
                this.txtCreatDate.Text = ToYMD(dt.Rows[0]["CreateDate"]);
                this.txtUpdater.Text = dt.Rows[0]["Updater"].ToString();
                this.txtUpdateDate.Text = ToYMD(dt.Rows[0]["UpdateDate"]);

            }
            
        }
        private void BindDataNull()
        {
            this.txtID.Text = "";
            this.txtTypeName.Text = "";
            this.txtMemo.Text = "";
            this.txtCreator.Text = "";
            this.txtCreatDate.Text = "";
            this.txtUpdater.Text = "";
            this.txtUpdateDate.Text = "";
        }
        #endregion
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strID = this.txtID.Text;
            int Count = bll.GetRowCount("VUsed_CMD_TrainType", string.Format("TypeCode='{0}'", this.txtID.Text.Trim()));
            if (Count > 0)
            {
                WMS.App_Code.JScript.Instance.ShowMessage(this.updatePanel, "该车型编码已被其它单据使用，请调整后再删除！");
                return;
            }
            bll.ExecNonQuery("Cmd.DeleteTrainType", new DataParameter[] { new DataParameter("{0}", "'" + strID + "'") });
            AddOperateLog("车型资料", "删除单号：" + strID);

            btnNext_Click(sender, e);
            if (this.txtID.Text == strID)
            {
                btnPre_Click(sender, e);
                if (this.txtID.Text == strID)
                {
                    BindDataNull();
                }
            }

        }

        #region 上下笔事件
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            BindData(bll.GetRecord("F", TableName, "1=1", PrimaryKey, this.txtID.Text));
        }
        protected void btnPre_Click(object sender, EventArgs e)
        {
            BindData(bll.GetRecord("P", TableName, "1=1", PrimaryKey, this.txtID.Text));
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            BindData(bll.GetRecord("N", TableName, "1=1", PrimaryKey, this.txtID.Text));
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            BindData(bll.GetRecord("L", TableName, "1=1", PrimaryKey, this.txtID.Text));
        }
        #endregion
        
    }
}