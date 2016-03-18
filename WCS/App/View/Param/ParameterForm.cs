using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MCP.Config;
using Util;


namespace App.View.Param
{
    public partial class ParameterForm : View.BaseForm
    {
        private App.View.Param.Parameter parameter = new App.View.Param.Parameter();
        private DBConfig config = new DBConfig();


        MCP.Service.TCP.Config.Configuration TcpConfg = new MCP.Service.TCP.Config.Configuration("Crane.xml");

        MCP.Service.Siemens.Config.Configuration PLC1 = new MCP.Service.Siemens.Config.Configuration("CranePLC1.xml");
        MCP.Service.Siemens.Config.Configuration PLC2 = new MCP.Service.Siemens.Config.Configuration("CranePLC2.xml");
        MCP.Service.Siemens.Config.Configuration PLC3 = new MCP.Service.Siemens.Config.Configuration("CarPLC.xml");
       
        private Dictionary<string, string> attributes = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //本机数据库连接参数
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DBUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["pwd"].ToString();


            //扫描枪--由于使用USB接口，而屏蔽
            //ConfigUtil configUtil = new ConfigUtil();
            //attributes = configUtil.GetAttribute();          

            //PLC1
            parameter.PLC1ServerName = PLC1.ProgID;
            parameter.PLC1ServerIP = PLC1.ServerName;
            parameter.PLC1GroupString = PLC1.GroupString;
            parameter.PLC1UpdateRate = PLC1.UpdateRate;
            
            //PLC2
            parameter.PLC2ServerName = PLC2.ProgID;
            parameter.PLC2ServerIP = PLC2.ServerName;
            parameter.PLC2GroupString = PLC2.GroupString;
            parameter.PLC2UpdateRate = PLC2.UpdateRate;

            //PLC3
            parameter.PLC3ServerName = PLC3.ProgID;
            parameter.PLC3ServerIP = PLC3.ServerName;
            parameter.PLC3GroupString = PLC3.GroupString;
            parameter.PLC3UpdateRate = PLC3.UpdateRate;

            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存本机数据库连接参数
                config.Parameters["server"] = parameter.ServerName;
                config.Parameters["uid"] = parameter.DBUser;
                config.Parameters["pwd"] = parameter.Password;
                config.Save();


                //由于扫码枪使用USB接口，而屏蔽。
                ////保存Context参数
                //attributes["ScanPortName"] = parameter.ScanPortName;
                //attributes["ScanBaudRate"] = parameter.ScanBaudRate;

                //attributes["IsOutOrder"] = parameter.IsOutOrder;
                //attributes["MesWebServiceUrl"] = parameter.MesWebServiceUrl;
                //ConfigUtil configUtil = new ConfigUtil();
                //configUtil.Save(attributes);

                //PLC1
                PLC1.GroupString = parameter.PLC1GroupString;
                PLC1.ProgID = parameter.PLC1ServerName;
                PLC1.UpdateRate = parameter.PLC1UpdateRate;
                PLC1.ServerName = parameter.PLC1ServerIP;
                PLC1.Save();
                //PLC2
                PLC2.GroupString = parameter.PLC2GroupString;
                PLC2.ProgID = parameter.PLC2ServerName;
                PLC2.UpdateRate = parameter.PLC2UpdateRate;
                PLC2.ServerName = parameter.PLC2ServerIP;
                PLC2.Save();
                //PLC3
                PLC3.GroupString = parameter.PLC2GroupString;
                PLC3.ProgID = parameter.PLC2ServerName;
                PLC3.UpdateRate = parameter.PLC2UpdateRate;
                PLC3.ServerName = parameter.PLC2ServerIP;
                PLC3.Save();  

                MessageBox.Show("系统参数保存成功，请重新启动本系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("保存系统参数过程中出现异常，原因：" + exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

