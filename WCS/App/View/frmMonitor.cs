using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util;
using DataGridViewAutoFilter;
using MCP;

namespace App.View
{
    public partial class frmMonitor : BaseForm
    {
        private Point InitialP1;
        private Point InitialP2;
        private Point InitialP3;
        int columnDis1 = 88;
        int columnDis2 = 90;
        int columnDis3 = 54;
        private System.Timers.Timer tmWorkTimer = new System.Timers.Timer();
        private System.Timers.Timer tmCrane1 = new System.Timers.Timer();
        private System.Timers.Timer tmCrane2 = new System.Timers.Timer();
        private System.Timers.Timer tmCar = new System.Timers.Timer();
        BLL.BLLBase bll = new BLL.BLLBase();
        Dictionary<int, string> dicCraneFork = new Dictionary<int, string>();
        Dictionary<int, string> dicCraneTaskType = new Dictionary<int, string>();
        Dictionary<int, string> dicCraneAction = new Dictionary<int, string>();
        Dictionary<int, string> dicCraneError = new Dictionary<int, string>();
        Dictionary<int, string> dicCraneOver = new Dictionary<int, string>();

        Dictionary<int, string> dicCarAction = new Dictionary<int, string>();
        Dictionary<int, string> dicCarLoad = new Dictionary<int, string>();
        Dictionary<int, string> dicCarTaskType = new Dictionary<int, string>();
        Dictionary<int, string> dicCarError = new Dictionary<int, string>();


        public frmMonitor()
        {
            InitializeComponent();
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            Point P1 = picCrane11.Location;
            P1.X = P1.X - 88;

            this.picCrane11.Location = P1;

            Point P2 = picCrane12.Location;
            P2.X = P2.X - 90;

            this.picCrane12.Location = P2;

            Point P3 = picCrane2.Location;
            P3.X = P3.X - 54;

            this.picCrane2.Location = P3;
        }

        //private void btnBack_Click(object sender, EventArgs e)
        //{
        //    this.picCrane11.Location = InitialP1;
        //    this.picCrane2.Location = InitialP2;
        //    this.picCrane12.Location = InitialP3;
        //}

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            MainData.OnTask += new TaskEventHandler(Data_OnTask);
            Cranes.OnCrane += new CraneEventHandler(Monitor_OnCrane);
            Cars.OnCar += new CarEventHandler(Monitor_OnCar);
            AddDicKeyValue();

            InitialP1 = picCrane11.Location;
            InitialP2 = picCrane2.Location;
            InitialP3 = picCrane12.Location;
            picCrane11.Parent = pictureBox1;
            picCrane11.BackColor = Color.Transparent;
            picCrane12.Parent = pictureBox1;
            picCrane12.BackColor = Color.Transparent;
            picCrane2.Parent = pictureBox1;
            picCrane2.BackColor = Color.Transparent;

            this.BindData();
            for (int i = 0; i < this.dgvMain.Columns.Count - 1; i++)
                ((DataGridViewAutoFilterTextBoxColumn)this.dgvMain.Columns[i]).FilteringEnabled = true;

            tmWorkTimer.Interval = 3000;
            tmWorkTimer.Elapsed += new System.Timers.ElapsedEventHandler(tmWorker);
            tmWorkTimer.Start();

            tmCrane1.Interval = 3000;
            tmCrane1.Elapsed += new System.Timers.ElapsedEventHandler(tmCraneWorker1);
            tmCrane1.Start();

            tmCrane2.Interval = 3000;
            tmCrane2.Elapsed += new System.Timers.ElapsedEventHandler(tmCraneWorker2);
            tmCrane2.Start();

            tmCar.Interval = 3000;
            tmCar.Elapsed += new System.Timers.ElapsedEventHandler(tmCarWorker);
            tmCar.Start();
        }
        private void AddDicKeyValue()
        {
            dicCraneFork.Add(0, "叉非原位和极限");
            dicCraneFork.Add(1, "叉原位时叉上无货");
            dicCraneFork.Add(2, "叉原位时叉上有货");
            dicCraneFork.Add(3, "叉左极限时叉上无货");
            dicCraneFork.Add(4, "叉左极限时叉上有货");
            dicCraneFork.Add(5, "叉右极限时叉上无货");
            dicCraneFork.Add(6, "叉右极限时叉上有货");

            dicCraneTaskType.Add(0, "待机");
            dicCraneTaskType.Add(1, "入库");
            dicCraneTaskType.Add(2, "出库");
            dicCraneTaskType.Add(3, "移库");
            dicCraneTaskType.Add(4, "转库");
            dicCraneTaskType.Add(5, "双重入库");
            dicCraneTaskType.Add(6, "回库台位");
            dicCraneTaskType.Add(7, "召回");
            dicCraneTaskType.Add(8, "急停");

            dicCraneAction.Add(0, "维修");
            dicCraneAction.Add(1, "手动(脱机)");
            dicCraneAction.Add(2, "单机自动");
            dicCraneAction.Add(3, "联机自动");

            dicCraneError.Add(0, "");
            dicCraneError.Add(1, "货物左超限");
            dicCraneError.Add(2, "货物右超限");
            dicCraneError.Add(3, "左侧外形超限");
            dicCraneError.Add(4, "右侧外形超限");
            dicCraneError.Add(5, "满货位入库");
            dicCraneError.Add(6, "空货位出库");
            dicCraneError.Add(7, "运行变频器报警");
            dicCraneError.Add(8, "起升变频器报警");
            dicCraneError.Add(9, "传送地址错");
            dicCraneError.Add(10, "通讯故障");
            dicCraneError.Add(11, "运行超时");
            dicCraneError.Add(12, "起升超时");
            dicCraneError.Add(13, "货叉超时");
            dicCraneError.Add(14, "超高1");
            dicCraneError.Add(15, "超高2");
            dicCraneError.Add(16, "超高3");
            dicCraneError.Add(17, "水平激光数据错误");
            dicCraneError.Add(18, "起升激光数据错误");
            dicCraneError.Add(19, "货物前超限");
            dicCraneError.Add(20, "货物后超限");

            dicCarAction.Add(0, "脱机");
            dicCarAction.Add(1, "手动");
            dicCarAction.Add(2, "自动");

            dicCarLoad.Add(0, "无货");
            dicCarLoad.Add(1, "有货");

            dicCarTaskType.Add(0, "待机");
            dicCarTaskType.Add(1, "入库");
            dicCarTaskType.Add(2, "出库");

            dicCarError.Add(0, "");
            dicCarError.Add(1, "运行超时故障");
            dicCarError.Add(2, "变频器故障");
            dicCarError.Add(3, "超时故障+变频器故障");
        }
        void Data_OnTask(TaskEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new TaskEventHandler(Data_OnTask), args);
            }
            else
            {
                lock (this.dgvMain)
                {
                    DataTable dt = args.datatTable;
                    this.bsMain.DataSource = dt;
                    for (int i = 0; i < this.dgvMain.Rows.Count; i++)
                    {
                        if (dgvMain.Rows[i].Cells["colErrCode"].Value.ToString() != "0")
                            this.dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        else
                        {
                            if(i%2==0)
                                this.dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            else
                                this.dgvMain.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192);

                        }
                    }
                }
            }
        }
        void Monitor_OnCrane(CraneEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CraneEventHandler(Monitor_OnCrane), args);
            }
            else
            {
                Crane crane = args.crane;
                TextBox txt = GetTextBox("txtTaskNo", crane.CraneNo);
                if (txt != null)
                    txt.Text = crane.TaskNo;

                txt = GetTextBox("txtTaskType", crane.CraneNo);
                if (txt != null && dicCraneTaskType.ContainsKey(crane.TaskType))
                    txt.Text = dicCraneTaskType[crane.TaskType];

                txt = GetTextBox("txtCraneAction", crane.CraneNo);
                if (txt != null && dicCraneAction.ContainsKey(crane.Action))
                    txt.Text = dicCraneAction[crane.Action];

                txt = GetTextBox("txtColumn", crane.CraneNo);
                if (txt != null)
                    txt.Text = crane.Column.ToString();

                if (crane.WalkCode > 0)
                {
                    txt = GetTextBox("txtBpWalkCode", crane.CraneNo);
                    if (txt != null)
                    {
                        txt.Text = crane.WalkCode.ToString();
                        txt.BackColor = Color.Red;
                    }
                }
                else
                {
                    txt = GetTextBox("txtBpWalkCode", crane.CraneNo);
                    if (txt != null)
                    {
                        txt.Text = "";
                        txt.BackColor = Control.DefaultBackColor;
                    }
                }

                if (crane.UpDownCode > 0)
                {
                    txt = GetTextBox("txtBpUpDownCode", crane.CraneNo);
                    if (txt != null)
                    {
                        txt.Text = crane.UpDownCode.ToString();
                        txt.BackColor = Color.Red;
                    }
                }
                else
                {
                    txt = GetTextBox("txtBpUpDownCode", crane.CraneNo);
                    if (txt != null)
                    {
                        txt.Text = "";
                        txt.BackColor = Control.DefaultBackColor;
                    }
                }

                //堆垛机位置
                if (crane.CraneNo == 1)
                {
                    if (crane.Column < 7)
                    {
                        this.picCrane11.Visible = true;
                        this.picCrane12.Visible = false;
                        Point P1 = InitialP1;
                        P1.X = P1.X - (crane.Column - 1) * columnDis1;
                        this.picCrane11.Location = P1;
                    }
                    else
                    {
                        this.picCrane11.Visible = false;
                        this.picCrane12.Visible = true;
                        Point P3 = InitialP3;
                        P3.X = P3.X - (12 - crane.Column) * columnDis2;
                        this.picCrane12.Location = P3;
                    }
                }
                else
                {
                    Point P2 = InitialP2;
                    P2.X = P2.X - (crane.Column - 1) * columnDis3;
                    this.picCrane2.Location = P2;
                }

                txt = GetTextBox("txtHeight", crane.CraneNo);
                if (txt != null)
                    txt.Text = crane.Height.ToString();

                txt = GetTextBox("txtForkStatus", crane.CraneNo);
                if (txt != null && dicCraneFork.ContainsKey(crane.ForkStatus))
                    txt.Text = dicCraneFork[crane.ForkStatus];
                txt = GetTextBox("txtErrorNo", crane.CraneNo);
                if (txt != null)
                    txt.Text = crane.ErrCode.ToString();

                txt = GetTextBox("txtErrorDesc", crane.CraneNo);
                if (txt != null && dicCraneError.ContainsKey(crane.ErrCode))
                    txt.Text = dicCraneError[crane.ErrCode];

                //更新错误代码、错误描述
                //更新任务状态为执行中
                bll.ExecNonQuery("WCS.UpdateTaskError", new DataParameter[] { new DataParameter("@CraneErrCode", crane.ErrCode.ToString()), new DataParameter("@CraneErrDesc", dicCraneError[crane.ErrCode]), new DataParameter("@TaskNo", crane.TaskNo) });
                if(crane.ErrCode>0)
                    Logger.Error(crane.CraneNo.ToString() + "堆垛机执行时出现错误,代码:"+ crane.ErrCode.ToString() + ",描述:" + dicCraneError[crane.ErrCode]);
            }
        }
        TextBox GetTextBox(string name, int CraneNo)
        {
            Control[] ctl = this.Controls.Find(name + CraneNo.ToString(), true);
            if (ctl.Length > 0)
                return (TextBox)ctl[0];
            else
                return null;
        }
        void Monitor_OnCar(CarEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new CarEventHandler(Monitor_OnCar), args);
            }
            else
            {
                Car car = args.car;
                TextBox txt = GetTextBox("txtCarAction", car.CarNo);
                if (txt != null && dicCarAction.ContainsKey(car.Status))
                    txt.Text = dicCarAction[car.Status];

                txt = GetTextBox("txtCarTaskNo", car.CarNo);
                if (txt != null)
                    txt.Text = car.TaskNo;

                txt = GetTextBox("txtCarTaskType", car.CarNo);
                if (txt != null && dicCraneTaskType.ContainsKey(car.TaskType))
                    txt.Text = dicCraneTaskType[car.TaskType];

                txt = GetTextBox("txtCarLoad", car.CarNo);
                if (txt != null && dicCarLoad.ContainsKey(car.Load))
                    txt.Text = dicCarLoad[car.Load];

                txt = GetTextBox("txtCarErrorDesc", car.CarNo);
                if (txt != null && dicCarError.ContainsKey(car.ErrCode))
                {
                    txt.Text = dicCarError[car.ErrCode];
                    if (car.Action == 2)
                    {
                        if (txt.Text.Length > 0)
                            txt.Text += "+急停";
                        else
                            txt.Text += "急停";
                    }
                    txt.ForeColor = Color.Black;
                    if (txt.Text.Length > 0)
                        txt.BackColor = Color.Red;
                    else
                        txt.BackColor = Control.DefaultBackColor;
                    //txt.Font.
                }
            }
        }
        private void tmWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmWorkTimer.Stop();
                DataTable dt = GetMonitorData();
                MainData.TaskInfo(dt);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                tmWorkTimer.Start();
            }
        }
        private void tmCraneWorker1(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmCrane1.Stop();

                int[] location = new int[2];
                string serviceName = "CranePLC1";
                object[] obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneLocation"));
                for (int j = 0; j < obj.Length; j++)
                    location[j] = Convert.ToInt16(obj[j]) - 48;

                int[] craneInfo = new int[6];
                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneInfo"));
                for (int j = 0; j < obj.Length; j++)
                    craneInfo[j] = Convert.ToInt16(obj[j]) - 48;

                string palletCode = Util.ConvertStringChar.BytesToString(ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CranePalletCode")));
                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneTaskNo"));
                string plcTaskNo = Util.ConvertStringChar.BytesToString(obj);

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneSpeed"));

                Crane crane = new Crane();
                crane.CraneNo = 1;
                crane.Column = location[0];
                crane.Height = location[1];
                crane.ForkStatus = craneInfo[1];
                crane.Action = craneInfo[2];
                crane.TaskType = craneInfo[3];
                crane.ErrCode = craneInfo[4];
                crane.PalletCode = palletCode;
                crane.TaskNo = plcTaskNo;
                crane.WalkCode = int.Parse(obj[3].ToString());
                crane.UpDownCode = int.Parse(obj[4].ToString());
                Cranes.CraneInfo(crane);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                tmCrane1.Start();
            }
        }
        private void tmCraneWorker2(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmCrane2.Stop();

                int[] location = new int[2];
                string serviceName = "CranePLC2";
                object[] obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneLocation"));
                for (int j = 0; j < obj.Length; j++)
                    location[j] = Convert.ToInt16(obj[j]) - 48;

                int[] craneInfo = new int[6];
                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneInfo"));
                for (int j = 0; j < obj.Length; j++)
                    craneInfo[j] = Convert.ToInt16(obj[j]) - 48;

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneTaskNo"));
                string plcTaskNo = Util.ConvertStringChar.BytesToString(obj);

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService(serviceName, "CraneSpeed"));

                Crane crane = new Crane();
                crane.CraneNo = 2;
                crane.Column = location[0];
                crane.Height = location[1];
                crane.ForkStatus = craneInfo[1];
                crane.Action = craneInfo[2];
                crane.TaskType = craneInfo[3];
                crane.ErrCode = craneInfo[4];
                crane.TaskNo = plcTaskNo;
                crane.WalkCode = int.Parse(obj[3].ToString());
                crane.UpDownCode = int.Parse(obj[4].ToString());

                Cranes.CraneInfo(crane);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                tmCrane2.Start();
            }
        }
        private void tmCarWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmCar.Stop();


                int[] carInfo = new int[6];
                object[] obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("CarPLC", "01_CarInfo"));
                for (int j = 0; j < 6; j++)
                    carInfo[j] = Convert.ToInt16(obj[j]) - 48;

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("CarPLC", "01_CarTaskInfo"));
                string TaskNo = Util.ConvertStringChar.BytesToString(obj);

                Car car1 = new Car();
                car1.CarNo = 1;
                car1.Load = carInfo[0];

                car1.Status = carInfo[2];
                car1.TaskType = carInfo[3];
                car1.ErrCode = carInfo[4];
                car1.Action = carInfo[5];
                car1.TaskNo = TaskNo;

                Cars.CarInfo(car1);

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("CarPLC", "02_CarInfo"));
                for (int j = 0; j < 6; j++)
                    carInfo[j] = Convert.ToInt16(obj[j]) - 48;

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("CarPLC", "02_CarTaskInfo"));
                TaskNo = Util.ConvertStringChar.BytesToString(obj);

                Car car2 = new Car();
                car2.CarNo = 2;
                car2.Load = carInfo[0];

                car2.Status = carInfo[2];
                car2.TaskType = carInfo[3];
                car2.ErrCode = carInfo[4];
                car2.Action = carInfo[5];
                car2.TaskNo = TaskNo;

                Cars.CarInfo(car2);

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("CarPLC", "03_CarInfo"));
                for (int j = 0; j < 6; j++)
                    carInfo[j] = Convert.ToInt16(obj[j]) - 48;

                obj = ObjectUtil.GetObjects(Context.ProcessDispatcher.WriteToService("CarPLC", "03_CarTaskInfo"));
                TaskNo = Util.ConvertStringChar.BytesToString(obj);

                Car car3 = new Car();
                car3.CarNo = 3;
                car3.Load = carInfo[0];

                car3.Status = carInfo[2];
                car3.TaskType = carInfo[3];
                car3.ErrCode = carInfo[4];
                car3.Action = carInfo[5];
                car3.TaskNo = TaskNo;

                Cars.CarInfo(car3);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                tmCar.Start();
            }
        }
        private void BindData()
        {
            bsMain.DataSource = GetMonitorData();
        }
        private DataTable GetMonitorData()
        {
            DataTable dt = bll.FillDataTable("WCS.SelectTask", new DataParameter[] { new DataParameter("{0}", "(WCS_TASK.TaskType='11' and WCS_TASK.State in('1','2','3')) OR (WCS_TASK.TaskType in('12','13') and WCS_TASK.State in('0','2','3')) OR (WCS_TASK.TaskType in('14') and WCS_TASK.State in('0','2','3','4','5','6'))") });
            return dt;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.btnBack1.Text == "启动")
            {
                Context.ProcessDispatcher.WriteToProcess("CraneProcess", "Run", 1);
                this.btnBack1.Text = "停止";
            }
            else
            {
                Context.ProcessDispatcher.WriteToProcess("CraneProcess", "Run", 0);
                this.btnBack1.Text = "启动";
            }
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要召回1号堆垛机到初始位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PutCommand("1", 7);
                Logger.Info("1号堆垛机下发召回命令");
            }
        }
        private void PutCommand(string craneNo, byte TaskType)
        {
            byte[] cellAddr = new byte[8];
            cellAddr[0] = TaskType;
            cellAddr[1] = 0;

            for (int i = 0; i < cellAddr.Length; i++)
                cellAddr[i] += 48;

            string serviceName = "CranePLC" + craneNo;
            Context.ProcessDispatcher.WriteToService(serviceName, "TaskAddress", cellAddr);

            Context.ProcessDispatcher.WriteToService(serviceName, "WriteFinished", 49);
        }

        private void btnStop1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要急停1号堆垛机?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PutCommand("1", 8);
                Logger.Info("1号堆垛机下发急停命令");
            }
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要召回2号堆垛机到初始位置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PutCommand("2", 7);
                Logger.Info("2号堆垛机下发召回命令");
            }
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要急停2号堆垛机?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PutCommand("2", 8);
                Logger.Info("2号堆垛机下发急停命令");
            }
        }


        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dgvMain.Rows[e.RowIndex].Selected == false)
                    {
                        dgvMain.ClearSelection();
                        dgvMain.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dgvMain.SelectedRows.Count == 1)
                    {
                        dgvMain.CurrentCell = dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    string TaskType = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells["colTaskType"].Value.ToString();
                    if (TaskType == "11")
                    {
                        this.ToolStripMenuItem11.Visible = true;
                        this.ToolStripMenuItem12.Visible = true;
                        this.ToolStripMenuItem13.Visible = true;
                        this.ToolStripMenuItem14.Visible = false;
                        this.ToolStripMenuItem15.Visible = false;
                        this.ToolStripMenuItem16.Visible = false;
                    }
                    else if (TaskType == "12" || TaskType == "13")
                    {
                        this.ToolStripMenuItem11.Visible = false;
                        this.ToolStripMenuItem12.Visible = false;
                        this.ToolStripMenuItem13.Visible = true;
                        this.ToolStripMenuItem14.Visible = false;
                        this.ToolStripMenuItem15.Visible = false;
                        this.ToolStripMenuItem16.Visible = false;
                    }
                    else if (TaskType == "14")
                    {
                        this.ToolStripMenuItem10.Visible = true;
                        this.ToolStripMenuItem11.Visible = false;
                        this.ToolStripMenuItem12.Visible = false;
                        this.ToolStripMenuItem13.Visible = true;
                        this.ToolStripMenuItem14.Visible = true;
                        this.ToolStripMenuItem15.Visible = true;
                        this.ToolStripMenuItem16.Visible = true;
                    }
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void ToolStripMenuItemCellCode_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                BLL.BLLBase bll = new BLL.BLLBase();
                string TaskNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[0].Value.ToString();
                string TaskType = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells["colTaskType"].Value.ToString();
                string ErrCode = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells["colErrCode"].ToString();

                if (TaskType=="11")
                {
                    DataGridViewSelectedRowCollection rowColl = dgvMain.SelectedRows;
                    if (rowColl == null)
                        return;
                    DataRow dr = (rowColl[0].DataBoundItem as DataRowView).Row;
                    frmReassignEmptyCell f = new frmReassignEmptyCell(dr);
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.BindData(); 
                }
                else if (TaskType == "12" || TaskType == "14")
                {
                    DataGridViewSelectedRowCollection rowColl = dgvMain.SelectedRows;
                    if (rowColl == null)
                        return;
                    DataRow dr = (rowColl[0].DataBoundItem as DataRowView).Row;
                    frmReassign f = new frmReassign(dr);
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        this.BindData(); 
                }
                else if (TaskType == "13")
                {
                    DataGridViewSelectedRowCollection rowColl = dgvMain.SelectedRows;
                    if (rowColl == null)
                        return;
                    DataRow dr = (rowColl[0].DataBoundItem as DataRowView).Row;

                    frmReassignOption fo = new frmReassignOption();
                    if (fo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (fo.option == 0)
                        {
                            frmReassign f = new frmReassign(dr);
                            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                this.BindData(); 
                        }
                        else
                        {
                            frmReassignEmptyCell fe = new frmReassignEmptyCell(dr);
                            if (fe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                this.BindData(); 
                        }
                    }                    
                }
            }
        }

        private void ToolStripMenuItemReassign_Click(object sender, EventArgs e)
        {
            if (this.dgvMain.CurrentCell != null)
            {
                BLL.BLLBase bll = new BLL.BLLBase();
                string TaskNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[0].Value.ToString();
                string TaskType = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells["colTaskType"].Value.ToString();

                if (TaskType == "11")
                    bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", 2), new DataParameter("@TaskNo", TaskNo) });
                else if (TaskType == "12" || TaskType == "13")
                    bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", 0), new DataParameter("@TaskNo", TaskNo) });
                else if (TaskType == "14")
                {
                    frmTaskOption f = new frmTaskOption();
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if(f.option==0)
                            bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", 2), new DataParameter("@TaskNo", TaskNo) });
                        else
                            bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", 5), new DataParameter("@TaskNo", TaskNo) });

                    }
                }
                this.BindData();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ItemName = ((ToolStripMenuItem)sender).Name;
            string State = ItemName.Substring(ItemName.Length-1, 1);

            if (this.dgvMain.CurrentCell != null)
            {
                BLL.BLLBase bll = new BLL.BLLBase();
                string TaskNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[0].Value.ToString();

                DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo), new DataParameter("@State", State) };
                bll.ExecNonQueryTran("WCS.Sp_UpdateTaskState", param);
                
                //bll.ExecNonQuery("WCS.UpdateTaskStateByTaskNo", new DataParameter[] { new DataParameter("@State", State), new DataParameter("@TaskNo", TaskNo) });

                ////堆垛机完成执行
                //if (State == "7")
                //{
                //    DataParameter[] param = new DataParameter[] { new DataParameter("@TaskNo", TaskNo) };
                //    bll.ExecNonQueryTran("WCS.Sp_TaskProcess", param);
                //}
                BindData();
            }
        }

        private void ToolStripMenuItemChangeCarNo_Click(object sender, EventArgs e)
        {
            string ItemName = ((ToolStripMenuItem)sender).Name;
            string State = ItemName.Substring(ItemName.Length - 1, 1);

            if (this.dgvMain.CurrentCell != null)
            {
                BLL.BLLBase bll = new BLL.BLLBase();
                string TaskNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells[0].Value.ToString();
                string CraneNo = this.dgvMain.Rows[this.dgvMain.CurrentCell.RowIndex].Cells["colCraneNo"].Value.ToString();
                frmTaskCarNo f = new frmTaskCarNo(CraneNo);

                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string CarNo = f.carNo;
                    bll.ExecNonQuery("WCS.UpdateTaskCarNoByTaskNo", new DataParameter[] { new DataParameter("@CarNo", CarNo), new DataParameter("@TaskNo", TaskNo) });
                }
                
                BindData();
            }
        }
    }
}
