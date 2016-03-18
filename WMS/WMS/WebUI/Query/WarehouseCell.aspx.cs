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

    public partial class WarehouseCell :App_Code.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string WareHouse = Request.QueryString["WareHouse"].ToString();
            string ShelfCode = Request.QueryString["ShelfCode"].ToString();
            string AreaCode = Request.QueryString["AreaCode"].ToString();
            BLL.BLLBase bll = new BLL.BLLBase();

            DataTable tableCell;
            if (WareHouse != "" &&  AreaCode=="")
            {
                tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByWareHouse", new DataParameter[] { new DataParameter("@WareHouse", WareHouse) });
                ShowWareHouseChart(tableCell);
            }
            else  if(AreaCode!="" && ShelfCode=="")
            {
                tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByArea", new DataParameter[] { new DataParameter("@AreaCode", AreaCode) });
                ShowCellChart(tableCell);
            }
            else
            {
                tableCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByShelf", new DataParameter[] { new DataParameter("{0}", string.Format("ShelfCode='{0}' and AreaCode='{1}'", ShelfCode, AreaCode)) });
                ShowCellChart(tableCell);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Resize", "resize();", true);
        }


        #region 显示仓库图标
        private void ShowWareHouseChart(DataTable tableCell)
        {
            this.pnlCell.Controls.Clear();
            if (tableCell.Rows.Count == 0)
                return;

            DataTable dtShelf = tableCell.DefaultView.ToTable(true, "ShelfCode");
            for (int i = 0; i < dtShelf.Rows.Count; i++)
            {

                Table shelfchar = CreateShelfChart("", dtShelf.Rows[i]["ShelfCode"].ToString());
                this.pnlCell.Controls.Add(shelfchar);
            }
        }
        #endregion


        #region 显示库区图标
        #endregion


        #region 显示货架图标
        #endregion

        #region 显示货位图表
        protected void ShowCellChart(DataTable tableCell)
        {
            this.pnlCell.Controls.Clear();
            if (tableCell.Rows.Count == 0)
                return;
            DataTable dtShelf = tableCell.DefaultView.ToTable(true, "AreaCode", "ShelfCode");
            for (int i = 0; i < dtShelf.Rows.Count; i++)
            {

                Table shelfchar = CreateShelfChart(dtShelf.Rows[i]["AreaCode"].ToString(), dtShelf.Rows[i]["ShelfCode"].ToString());
                this.pnlCell.Controls.Add(shelfchar);


            }
        }

        //货架显示图；
        protected Table CreateShelfChart(string AreaCode, string shelfCode)
        {
            BLL.BLLBase bll = new BLL.BLLBase();
           
            string strWhere = "";
            if (AreaCode == "")
                strWhere = string.Format("ShelfCode='{0}'", shelfCode);
            else
                strWhere = string.Format("ShelfCode='{0}' and AreaCode='{1}'", shelfCode, AreaCode);

            DataTable ShelfCell = bll.FillDataTable("CMD.SelectWareHouseCellQueryByShelf", new DataParameter[] { new DataParameter("{0}", strWhere) });

            int Rows = int.Parse(ShelfCell.Rows[0]["Rows"].ToString());
            int Columns = int.Parse(ShelfCell.Rows[0]["Columns"].ToString());
            string Width = "15%";
            if (Columns > 6)
                Width = "8%";
               


            Table tb = new Table();
            string tbstyle = "width:100%";
            tb.Attributes.Add("style", tbstyle);
            //tb.Attributes.Add("display", "table-cell");
            for (int i = Rows; i >= 1; i--)
            {
                TableRow row = new TableRow();
                for (int j = Columns; j >= 1; j--)
                {
                    int k = j;
                    if (shelfCode == "001002" || shelfCode == "001004")  //特殊处理
                    {
                        k = j + 1;
                    }
                    if (shelfCode == "001005")
                    {
                        k = 12 - j;
                    }
                    if (shelfCode == "001006")
                    {
                        k = 13 - j;
                    }

                    if (AreaCode == "")
                        strWhere = string.Format("CellRow={0} and CellColumn={1}", i, k);
                    else
                        strWhere = string.Format("CellRow={0} and CellColumn={1} and AreaCode='{2}'", i, k, AreaCode);


                    DataRow[] drs = ShelfCell.Select(strWhere, "");
                    if (drs.Length > 0)
                    {
                        TableCell cell = new TableCell();
                        cell.ID = drs[0]["CellCode"].ToString();

                        string style = "height:25px;width:" + Width + ";border:2px solid #008B8B;";
                        string backColor = ReturnColorFlag(drs[0]["ProductCode"].ToString(), drs[0]["IsActive"].ToString(), drs[0]["IsLock"].ToString(), drs[0]["ErrorFlag"].ToString(), ToYMD(drs[0]["InDate"]));
                        if (drs[0]["ProductCode"].ToString() != "")
                        {
                            style += "background-color:" + backColor + ";";
                        }

                        cell.Attributes.Add("style", style);
                        cell.Attributes.Add("onclick", "ShowCellInfo('" + cell.ID + "');");
                        row.Cells.Add(cell);
                    }
                    else
                    {
                        TableCell cell = new TableCell();
                        string style = "height:25px;width:" + Width + ";border:0px solid #008B8B";
                     
                        cell.Attributes.Add("style", style);
                      
                        row.Cells.Add(cell);
                    }
                    if (j == 1)
                    {
                        if (shelfCode == "001002" || shelfCode == "001005" || shelfCode == "001004")
                        {
                            TableCell cellAdd = new TableCell();
                            cellAdd.Attributes.Add("style", "height:25px;width:" + Width + ";border:0px solid #008B8B");
                            row.Cells.Add(cellAdd);
                        }
                        TableCell cellTag = new TableCell();
                        cellTag.Attributes.Add("style", "height:25px;border:0px solid #008B8B");
                        cellTag.Attributes.Add("align", "right");
                        cellTag.Text = "<font color=\"#008B8B\"> 第" + int.Parse(shelfCode.Substring(3, 3)).ToString() + "排第" + i.ToString() + "层</font>";
                        row.Cells.Add(cellTag);
                    }


                }
                tb.Rows.Add(row);

                if (i == 1)
                {
                    TableRow rowNum = new TableRow();
                    for (int j = Columns; j >= 1; j--)
                    {
                       
                        int k = j;
                        if (shelfCode == "001002" || shelfCode == "001004")  //特殊处理
                        {
                            k = j + 1;
                        }
                        if (shelfCode == "001005")
                        {
                            k = 12 - j;
                        }
                        if (shelfCode == "001006")
                        {
                            k = 13 - j;
                        }
                        TableCell cellNum = new TableCell();
                        cellNum.Attributes.Add("style", "height:40px;width:" + Width.ToString() + "px;border:0px solid #008B8B");
                        cellNum.Attributes.Add("align", "center");
                        cellNum.Attributes.Add("Valign", "top");
                        cellNum.Text = "<font color=\"#008B8B\">"+k.ToString()+"</font>";

                        rowNum.Cells.Add(cellNum);
                        
                    }
                    tb.Rows.Add(rowNum);

                }

            }
            return tb;

        }
        private string  ReturnColorFlag(string ProductCode, string IsActive, string IsLock, string ErrFlag,string Indate)
        {
            string Flag = "White";
            if (ProductCode == "" || Indate == "") //空货位锁定
            {
                if (IsLock == "1")
                {
                    Flag = "Yellow";
                }

            }
            else
            {

                if (IsLock == "1")
                {
                    if (ProductCode == "0001" || ProductCode == "0002")
                    {
                        Flag = "Gold";
                    }
                    else
                    {
                        Flag = "Green";
                    }
                }
                else
                {
                    if (ProductCode == "0001" || ProductCode == "0002")
                    {
                        Flag = "Orange";
                    }
                    else
                    {
                        Flag = "Blue";
                    }
                }



            }
            if (IsActive == "0")
                Flag = "Gray";
            if (ErrFlag == "1")
                Flag = "Red";
            return Flag;
        }
        #endregion
    }
}



 