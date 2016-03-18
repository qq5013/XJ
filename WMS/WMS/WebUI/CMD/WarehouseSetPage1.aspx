<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarehouseSetPage1.aspx.cs" Inherits="WMS.WebUI.CMD.WarehouseSetPage1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../Css/op.css" />  
    <link rel="stylesheet" type="text/css" href="../../Css/main.css" />  
    <link rel="stylesheet" type="text/css" href="../../ext/packages/ext-theme-crisp/build/resources/ext-theme-crisp-all.css" /> 
    <script type="text/javascript" src="../../JQuery/jquery-1.8.3.min.js"></script>  
    <script type="text/javascript" src= "../../JScript/Common.js"></script>
    <script type="text/javascript" src="../../Ext/ext-all.js"></script>  
    <script type="text/javascript" src="../../Ext/packages/ext-theme-crisp/build/ext-theme-crisp.js"></script>
    <script language="javascript" type="text/javascript">
        var tree;
        var buildTree = function (json) {
            return Ext.create('Ext.tree.Panel', {
                rootVisible: false,
                title: '仓库资料',
                //                collapsible: true,
                border: false,
                renderTo: "div_tree",
                bodyStyle: 'background:#bad5eb;',
                store: Ext.create('Ext.data.TreeStore', {
                    root: {
                        text: 'aaa',
                        expanded: true,
                        children: json.children
                    }
                }),
                listeners: {
//                    'afterrender': function () {
//                        debugger
//                        var root = tree.getRootNode();
//                        var record = this.getStore().getNodeById('select');
//                        this.getSelectionModel().select(record);
//                    },
                    'itemclick': function (view, record, item, index, e) {
                        var tNodeID = record.get('id');
                        var text = record.get('text');
                        var tNodeIDLen = tNodeID.length;
                        document.getElementById("lblCurrentNode").innerHTML = "当前选中的节点：" + text;
                        $("#hdnNodeID").val(tNodeID);
                        if (tNodeIDLen == 2) {
                            //仓库
                            $("#frame").attr("src", "WarehouseEditPage.aspx?WAREHOUSE_CODE=" + tNodeID);
                        }
                        if (tNodeIDLen == 3) {
                            //库区
                            $("#frame").attr("src", "WarehouseAreaEditPage.aspx?CMD_WH_AREA_ID=" + tNodeID);
                        }
                        if (tNodeIDLen == 6) {
                            //货架
                            $("#frame").attr("src", "WarehouseShelfEditPage.aspx?CMD_WH_SHELF_ID=" + tNodeID);
                        }
                        if (tNodeIDLen == 9) {
                            //货位
                            $("#frame").attr("src", "WarehouseCellEditPage.aspx?CMD_CELL_ID=" + tNodeID);
                        }
                    },
                    scope: this
                }
            });
        };
        Ext.onReady(function () {
            var blnReload = false;

            /** 
            * 加载菜单树 
            */
            Ext.Ajax.request({
                url: 'WareHouseTree.ashx',
                async: false,
                method: 'get',
                success: function (response) {
                    
                    var json = Ext.JSON.decode(response.responseText)
                    Ext.each(json, function (el) {
                        tree = buildTree(el);
                    });
                    if (!blnReload) {
                        var root = tree.getRootNode().firstChild;
                        document.getElementById("lblCurrentNode").innerHTML = "当前选中的节点：" + root.data.text;
                        $("#frame").attr("src", "WarehouseEditPage.aspx?WAREHOUSE_CODE=" + root.id);
                        $("#hdnNodeID").val(root.id);
                        blnReload = false;
                    }

                },
                failure: function (request) {
                    Ext.MessageBox.show({
                        title: '操作提示',
                        msg: "连接服务器失败",
                        buttons: Ext.MessageBox.OK,
                        icon: Ext.MessageBox.ERROR
                    });
                }

            });

            $('#btnUpdateSelected').bind('click', function () {
                return false;
            });
            $('#btnReload').bind('click', function () {
                blnReload = true;
                location.replace(location.href);
                return false;
            });

        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate> 
        <table style="width: 100%; height: 24px;" cellpadding="0" cellspacing="0">
           <tr  class="maintable" >
             <td  style="height:24px" colspan="2" align="right">
                    <asp:Button ID="btnNewWarehouse" Text="新增仓库" runat="server" 
                        CssClass="ButtonCreate"  OnClientClick="return OpenEditWarehouse()" 
                        Visible="False" />  
                    <asp:Button ID="btnNewArea" runat="server" Text="增加库区" CssClass="ButtonCreate" 
                        OnClientClick="return OpenEditArea()" Visible="False" Width="78px"/>
                    <asp:Button ID="btnNewShelf" runat="server" Text="增加货架" CssClass="ButtonCreate" 
                        OnClientClick="return OpenEditShelf()" Visible="False"/>
                    <asp:Button ID="btnNewCell" runat="server" Text="增加货位" CssClass="ButtonCreate" 
                        OnClientClick="return OpenEditCell()" Visible="False"/>

                     <asp:Button ID="btnCancel" Text="退出" runat="server" CssClass="ButtonExit" OnClientClick="Exit();"  />            
             </td>
           </tr>
           <tr>
              <td valign="top"  style="width:300px;" >
                <div id="div_tree" style="overflow:auto; width:300px; height:400px; border-right:2;">
		            
	            </div>
              </td>
          
               <td style="vertical-align:top; padding-left:10px; overflow:hidden"> <!--编辑区-->
                <div id="toptable" style="height:24px;vertical-align:middle; width:100% ">
                   <img alt="仓库" src="../../images/home.png" border="0" />
                   <asp:Label ID="lblCurrentNode" runat="server" ForeColor="#404040" 
                        Font-Names="微软雅黑"></asp:Label> 
                </div>
                <div style="width:100%; height:420px; overflow:hidden;">
                    
                   <iframe id="frame" runat="server" src="" style="width:100%; height:400px; overflow:hidden" bordercolor="blue" frameborder="0"></iframe>
                </div>
              </td>
            </tr>
        </table>
    

         <div>

            <asp:HiddenField ID="hidetree" runat="server" /><asp:HiddenField ID="hdnNodeID" runat="server" />
             <asp:Button ID="btnUpdateSelected" runat="server" style="display:none" Text=""/>  
            <asp:Button ID="btnReload" runat="server" style="display:none" Text=""/>  
         </div>  
  
  
     </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
