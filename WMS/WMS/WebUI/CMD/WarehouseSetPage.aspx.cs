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
using Util;
 
namespace WMS.WebUI.CMD
{
    public partial class WarehouseSetPage :WMS.App_Code.BasePage
    {
         
        DataTable dtHouse;
        
        BLL.BLLBase bll = new BLL.BLLBase();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dt = bll.FillDataTable("Cmd.selectWareHouseTree", null);
                DataTable dtnew = dt.Clone();

                this.hidetree.Value = Util.JsonHelper.DataTableToJSON(dt);
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.UpdatePanel1.GetType(), "Resize", "treeview_resize();", true);  

                //LoadHouseTree();
                //LoadTreeView();
                //if (this.TreeView1.Nodes.Count > 0)
                //{
                //    this.hdnWarehouseCode.Value = TreeView1.Nodes[0].Value;
                //    this.lblCurrentNode.Text = TreeView1.Nodes[0].Text;
                //    this.TreeView1.Nodes[0].Selected = true;

                //    HtmlGenericControl frame = (HtmlGenericControl)form1.FindControl("frame");
                //    frame.Attributes.Add("src", "WarehouseEditPage.aspx?WAREHOUSE_CODE=" + TreeView1.SelectedNode.Value + "&time=" + DateTime.Now.ToString());
                //}
            }
            else
            {

            }
        }


        #region 加载仓库树结构

        //protected void LoadHouseTree()
        //{
            
        //    this.tvWarehouse.Nodes.Clear();
        //    dtHouse = bll.FillDataTable("Cmd.SelectWarehouse");
            
        //    //仓库
        //    foreach (DataRow row in dtHouse.Rows)
        //    {
        //        TreeNode node = new TreeNode(row["WAREHOUSE_NAME"].ToString(), row["WAREHOUSE_CODE"].ToString());
        //        node.Target = "frame";

        //        node.ImageUrl = "../../images/leftmenu/in_warehouse.gif";
        //        tvWarehouse.Nodes.Add(node);
        //    }
        //    //库区
        //    DataTable dtTemp = bll.FillDataTable("Cmd.SelectArea");
        //    if (dtTemp.Rows.Count > 0)
        //    {
        //        foreach (DataRow r in dtTemp.Rows)
        //        {
        //            TreeNode nodeHouse = tvWarehouse.FindNode(r["WAREHOUSE_CODE"].ToString());

        //            if (nodeHouse != null)
        //            {
        //                nodeHouse.ExpandAll();
        //                TreeNode nodeArea = new TreeNode("库区：" + r["AREA_NAME"].ToString(), r["AREA_CODE"].ToString());
        //                nodeArea.ToolTip = r["CMD_WH_AREA_ID"].ToString();
        //                nodeArea.Target = "frame";
        //                nodeHouse.ChildNodes.Add(nodeArea);
        //            }
        //        }
        //    }
        //    //货架
        //    dtTemp = bll.FillDataTable("Cmd.SelectShelf");
        //    foreach (DataRow r2 in dtTemp.Rows)
        //    {
        //        TreeNode nodeArea = tvWarehouse.FindNode(r2["WAREHOUSE_CODE"].ToString() + "/" + r2["AREA_CODE"].ToString());
        //        if (nodeArea != null)
        //        {
        //            TreeNode nodeShelf = new TreeNode("货架：" + r2["SHELF_NAME"].ToString(), r2["SHELF_CODE"].ToString());
        //            nodeShelf.ToolTip = r2["CMD_WH_SHELF_ID"].ToString();
        //            nodeArea.ChildNodes.Add(nodeShelf);
        //        }

        //    }
        //    //货位
        //    dtTemp = bll.FillDataTable("Cmd.SelectCell");
        //    foreach (DataRow r3 in dtTemp.Rows)
        //    {
        //        TreeNode nodeShelf = tvWarehouse.FindNode(r3["WAREHOUSE_CODE"].ToString() + "/" + r3["AREA_CODE"].ToString() + "/" + r3["SHELF_CODE"].ToString());
        //        if (nodeShelf != null)
        //        {
        //            if (!IsPostBack)
        //            {
        //                nodeShelf.CollapseAll();
        //            }
        //            TreeNode nodeCell = new TreeNode("货位：" + r3["CELL_NAME"].ToString(), r3["CELL_CODE"].ToString());
        //            nodeCell.Text = string.Format("货位：<font color='#1E7ACE'>{0}</font>", r3["CELL_NAME"].ToString());

        //            nodeCell.ToolTip = r3["CMD_CELL_ID"].ToString();
        //            nodeShelf.ChildNodes.Add(nodeCell);
        //        }
        //    }
        //}

        #endregion

        #region 选中节点事件
        //protected void tvWarehouse_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    Change();
        //}

        protected void Change()
        {
            //DataTable dtTemp = bll.FillDataTable("Cmd.SelectArea");
            //int i = 0;
            //string areatype;
            //this.lblCurrentNode.Text = tvWarehouse.SelectedNode.Text;
            //HtmlGenericControl frame = (HtmlGenericControl)form1.FindControl("frame");
            //if (TreeView1.SelectedNode.Depth == 0)
            //{
            //    this.hdnWarehouseCode.Value = TreeView1.SelectedNode.Value;
            //    this.hdnAreaCode.Value = "";
            //    this.hdnShelfCode.Value = "";


            //    frame.Attributes.Add("src", "WarehouseEditPage.aspx?WAREHOUSE_CODE=" + TreeView1.SelectedNode.Value + "&time=" + DateTime.Now.ToString());

            //}
            //if (TreeView1.SelectedNode.Depth == 1)
            //{
            //    //i = int.Parse(objArea.QueryAreaByCode(tvWarehouse.SelectedNode.Value).Tables[0].Rows[0]["AREATYPE"].ToString());
            //    //areatype = dsTemp.Tables[0].Rows[i]["AREATYPE"].ToString();
            //    this.hdnWarehouseCode.Value = TreeView1.SelectedNode.Parent.Value;
            //    this.hdnAreaCode.Value = TreeView1.SelectedNode.Value;
            //    // this.hdnAreaType.Value = areatype;
            //    this.hdnShelfCode.Value = "";

            //    frame.Attributes.Add("src", "WarehouseAreaEditPage.aspx?CMD_WH_AREA_ID=" + TreeView1.SelectedNode.ToolTip + "&time=" + DateTime.Now.ToString());
            //}
            //if (TreeView1.SelectedNode.Depth == 2)
            //{
            //    //i = int.Parse(objArea.QueryAreaByCode(tvWarehouse.SelectedNode.Parent.Value).Tables[0].Rows[0]["AREATYPE"].ToString());
            //    //areatype = dsTemp.Tables[0].Rows[i]["AREATYPE"].ToString();
            //    this.hdnWarehouseCode.Value = TreeView1.SelectedNode.Parent.Parent.Value;
            //    this.hdnAreaCode.Value = TreeView1.SelectedNode.Parent.Value;
            //    this.hdnShelfCode.Value = TreeView1.SelectedNode.Value;
            //    //this.hdnAreaType.Value = areatype;

            //    frame.Attributes.Add("src", "WarehouseShelfEditPage.aspx?CMD_WH_SHELF_ID=" + TreeView1.SelectedNode.ToolTip + "&time=" + DateTime.Now.ToString());
            //}
            //else if (TreeView1.SelectedNode.Depth == 3)
            //{
            //    //i = int.Parse(objArea.QueryAreaByCode(tvWarehouse.SelectedNode.Parent.Parent.Value).Tables[0].Rows[0]["AREATYPE"].ToString());
            //    //areatype = dsTemp.Tables[0].Rows[i]["AREATYPE"].ToString();
            //    this.hdnWarehouseCode.Value = TreeView1.SelectedNode.Parent.Parent.Parent.Value;
            //    this.hdnAreaCode.Value = TreeView1.SelectedNode.Parent.Parent.Value;
            //    this.hdnShelfCode.Value = TreeView1.SelectedNode.Parent.Value;
            //    //this.hdnAreaType.Value = areatype;

            //    frame.Attributes.Add("src", "WarehouseCellEditPage.aspx?CMD_CELL_ID=" + TreeView1.SelectedNode.ToolTip + "&time=" + DateTime.Now.ToString());
            //}
        }
        #endregion

        private void LoadTreeView()
        {
            //TreeView1.Visible = false;
            //TreeView1.Nodes.Clear();
           
            //dtHouse = bll.FillDataTable("Cmd.SelectWarehouse");

            ////仓库
            //foreach (DataRow row in dtHouse.Rows)
            //{
            //    TreeNode node = new TreeNode(row["WAREHOUSE_NAME"].ToString(), row["WAREHOUSE_CODE"].ToString());
            //    node.Target = "frame";

            //    node.ImageUrl = "../../images/leftmenu/in_warehouse.gif";
            //    TreeView1.Nodes.Add(node);
            //}

            //DataTable dtTemp = bll.FillDataTable("Cmd.SelectArea");
            //if (dtTemp.Rows.Count > 0)
            //{
            //    foreach (DataRow r in dtTemp.Rows)
            //    {
            //        TreeNode nodeHouse = TreeView1.FindNode(r["WAREHOUSE_CODE"].ToString());

            //        if (nodeHouse != null)
            //        {
            //            nodeHouse.ExpandAll();
            //            TreeNode nodeArea = new TreeNode("库区：" + r["AREA_NAME"].ToString(), r["AREA_CODE"].ToString());
            //            nodeArea.ToolTip = r["CMD_WH_AREA_ID"].ToString();
            //            nodeArea.Target = "frame";
            //            nodeHouse.ChildNodes.Add(nodeArea);
            //        }
            //    }
            //}
            ////货架
            //dtTemp = bll.FillDataTable("Cmd.SelectShelf");
            //foreach (DataRow r2 in dtTemp.Rows)
            //{
            //    TreeNode nodeArea = TreeView1.FindNode(r2["WAREHOUSE_CODE"].ToString() + "/" + r2["AREA_CODE"].ToString());
            //    if (nodeArea != null)
            //    {
            //        TreeNode nodeShelf = new TreeNode("货架：" + r2["SHELF_NAME"].ToString(), r2["SHELF_CODE"].ToString());
            //        nodeShelf.ToolTip = r2["CMD_WH_SHELF_ID"].ToString();
            //        nodeArea.ChildNodes.Add(nodeShelf);
            //    }

            //}
            ////货位
            //dtTemp = bll.FillDataTable("Cmd.SelectCell");
            //foreach (DataRow r3 in dtTemp.Rows)
            //{
            //    TreeNode nodeShelf = TreeView1.FindNode(r3["WAREHOUSE_CODE"].ToString() + "/" + r3["AREA_CODE"].ToString() + "/" + r3["SHELF_CODE"].ToString());
            //    if (nodeShelf != null)
            //    {
            //        if (!IsPostBack)
            //        {
            //            nodeShelf.CollapseAll();
            //        }
            //        TreeNode nodeCell = new TreeNode("货位：" + r3["CELL_NAME"].ToString(), r3["CELL_CODE"].ToString());
            //        nodeCell.Text = string.Format("货位：<font color='#1E7ACE'>{0}</font>", r3["CELL_NAME"].ToString());

            //        nodeCell.ToolTip = r3["CMD_CELL_ID"].ToString();
            //        nodeShelf.ChildNodes.Add(nodeCell);
            //    }
            //}
            //TreeView1.Visible = true;

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

            Change();
 
        }
        protected void btnUpdateSelected_Click(object sender, EventArgs e)
        {
        //    DataTable dttmp;

        //    if (this.TreeView1.SelectedNode != null)
        //    {
        //        if (TreeView1.SelectedNode.Depth == 0)
        //        {

        //            dttmp = bll.FillDataTable("Cmd.SelectWarehouse", new DataParameter[] { new DataParameter("{0}", "WAREHOUSE_CODE='" + TreeView1.SelectedNode.Value + "'") });
        //            if (dtHouse.Rows.Count == 1)
        //            {
        //                TreeView1.SelectedNode.Text = dttmp.Rows[0]["WAREHOUSE_NAME"].ToString();
        //            }
        //        }
        //        else if (TreeView1.SelectedNode.Depth == 1)
        //        {

        //            dttmp = bll.FillDataTable("Cmd.SelectArea", new DataParameter[] { new DataParameter("{0}", "AREA_CODE='" + TreeView1.SelectedNode.Value + "'") });
        //            if (dttmp.Rows.Count == 1)
        //            {
        //                TreeView1.SelectedNode.Text = "库区：" + dttmp.Rows[0]["AREA_NAME"].ToString();
        //            }
        //        }
        //        else if (TreeView1.SelectedNode.Depth == 2)
        //        {

        //            dttmp = bll.FillDataTable("Cmd.SelectShelf", new DataParameter[] { new DataParameter("{0}", "CMD_WH_SHELF_ID='" + TreeView1.SelectedNode.Value + "'") });
        //            if (dttmp.Rows.Count == 1)
        //            {
        //                TreeView1.SelectedNode.Text = "货架：" + dttmp.Rows[0]["SHELF_NAME"].ToString();
        //            }
        //        }
        //        else if (TreeView1.SelectedNode.Depth == 3)
        //        {
        //            dttmp = bll.FillDataTable("Cmd.SelectCell", new DataParameter[] { new DataParameter("{0}", "CELL_CODE='" + TreeView1.SelectedNode.Value + "'") });
 
        //            if (dttmp.Rows.Count == 1)
        //            {
        //                TreeView1.SelectedNode.Text = string.Format("货位：{0}", dttmp.Rows[0]["CELL_NAME"].ToString());
        //            }
        //        }
        //    }
        }
 
        
        
    }
}