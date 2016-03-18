<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WarehouseSetPage.aspx.cs" Inherits="WMS.WebUI.CMD.WarehouseSetPage" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>仓库资料设置</title>
    <script type="text/javascript" src="../../JScript/Check.js?t=00"></script>
    <script type="text/javascript" src="../../JScript/Common.js"></script>
    
    <link href="../../css/op.css" rel="Stylesheet" type="text/css" /> 
    <link href="~/Css/Main.css" type="text/css" rel="stylesheet" /> 
   
    <link href="../../css/zTreeStyle/zTreeStyle.css" type="text/css" rel="stylesheet"/>

    <script type="text/javascript" src='<%=ResolveClientUrl("~/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/JQuery/jquery.ztree.core-3.5.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>
    
    <script type="text/javascript">
        var setting = {
            view: {
                showIcon: showIconForTree
            },
            data: {
                simpleData: {
                    enable: true,
                    IdKey: "id",
                    pIdKey: "pid"
                }
            },
            callback: {
                onClick: onClick
            }
        };
        var blnReload = false;
        function onClick(event, treeId, treeNode, clickFlag) {
            var tNodeID = treeNode.id;
            var tNodeIDLen = tNodeID.length;
            document.getElementById("lblCurrentNode").innerHTML = treeNode.name;
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
           
        };
        function showIconForTree(treeId, treeNode) {
            return !treeNode.isParent;
        };
        $(document).ready(function () {
            zNodes = eval($('#hidetree').val()); //.replace('[','').replace(']','')
            $.fn.zTree.init($("#treeDemo"), setting, zNodes);
            var zTree = $.fn.zTree.getZTreeObj("treeDemo");
            var tNodeID = zTree.getNodes(0)[0];
            tNodeID.iconSkin = "../../images/leftmenu/in_warehouse.gif";
            zTree.updateNode(tNodeID);
            zTree.expandNode(tNodeID, true, null, null, false);
            if (!blnReload) {
                document.getElementById("lblCurrentNode").innerHTML = tNodeID.name;
                $("#frame").attr("src", "WarehouseEditPage.aspx?WAREHOUSE_CODE=" + tNodeID.id);
                $("#hdnNodeID").val(tNodeID.id);
            }
            blnReload = false;
           
            $('#btnUpdateSelected').bind('click', function () {
                return false;
            });
            $('#btnReload').bind('click', function () {
                blnReload = true;
                location.replace(location.href);
                return false;
            });

        });

        function OpenEditWarehouse() {
            var date = new Date();
            var time = date.getMilliseconds() + date.getSeconds();
            document.getElementById('frame').src = "WarehouseEditPage.aspx?time=" + time;
            return false;

        }

        function OpenEditArea() {

            var WHCODE = $("#hdnNodeID").val();
            if (WHCODE != '01') {
                alert("请选择仓库！")
                return false;
            }
            document.getElementById('frame').src = "WarehouseAreaEditPage.aspx?WHCODE=" + WHCODE;
            return false;
        }

        function OpenEditShelf() {
            var AREACODE = $("#hdnNodeID").val();
            if (AREACODE.length != 3) {
                alert("请选择库区！")
                return false;
            }
            document.getElementById('frame').src = "WarehouseShelfEditPage.aspx?AREACODE=" + AREACODE;
            return false;

        }

        function OpenEditCell() {
            var SHELFCODE = $("#hdnNodeID").val();
            if (SHELFCODE.length != 6) {
                alert("请选择货架！")
                return false;
            }

            document.getElementById('frame').src = "WarehouseCellEditPage.aspx?SHELFCODE=" + SHELFCODE;

            return false;
        }

    </script>
    <style type="text/css">
        .SideBar
        {
           background-image: url(../../images/bar_bg.gif);
           background-position:right;
           background-repeat:no-repeat;
        
           padding-top:5px;
           vertical-align:top; 
           width:214px; 
           padding-right:4px;
        }
        .topic
        {
           padding-top:10px;
        }
        .topic2
        {
           text-align:center; 
           padding-top:3px;
           height:25px; 
           width:72px; 
           background-image:url(../../images/topic.gif);
           background-repeat:no-repeat;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate> 
        <table  class="OperationBar" cellpadding="0" cellspacing="0">
           <tr>
             <td   colspan="2" align="right">
                    <asp:Button ID="btnNewWarehouse" Text="新增仓库" runat="server" 
                        CssClass="ButtonCreate"  OnClientClick="return OpenEditWarehouse()" 
                        Visible="False" />  
                    <asp:Button ID="btnNewArea" runat="server" Text="增加库区" CssClass="ButtonCreate" 
                        OnClientClick="return OpenEditArea()" Visible="False"/>
                    <asp:Button ID="btnNewShelf" runat="server" Text="增加货架" CssClass="ButtonCreate" 
                        OnClientClick="return OpenEditShelf()" Visible="False"/>
                    <asp:Button ID="btnNewCell" runat="server" Text="增加货位" CssClass="ButtonCreate" 
                        OnClientClick="return OpenEditCell()" Visible="False"/>

                     <asp:Button ID="btnCancel" Text="退出" runat="server" CssClass="ButtonExit" OnClientClick="Exit();"  />            
             </td>
           </tr>
           <tr>
              <td class="SideBar">
                <div id="div_tree" style="overflow:auto; width:400px; height:500px;">
		            <ul id="treeDemo" class="ztree"></ul>
	            </div>

              </td>
          
               <td style="vertical-align:top; padding-left:10px;"> <!--编辑区-->
                <div id="toptable" style="height:24px;vertical-align:middle; width:100% ">
                   <img alt="仓库" src="../../images/home.png" border="0"  />当前选中的节点：
                   <asp:Label ID="lblCurrentNode" runat="server" ForeColor="#404040"></asp:Label> 
                </div>
                <div style="width:100%; height:473px; overflow:hidden;">
                   <iframe id="frame" runat="server" src="" style="width:100%; height:440px;" bordercolor="white" frameborder="0"></iframe>
                </div>
              </td>
            </tr>
        </table>
    

     <div>
        <asp:HiddenField ID="hidetree" runat="server" />
         <asp:HiddenField ID="hdnNodeID" runat="server" />
         <asp:Button ID="btnUpdateSelected" runat="server" CssClass="HiddenControl" Text=""/>  
          <asp:Button ID="btnReload" runat="server" CssClass="HiddenControl" Text=""/>  
     </div>  
  
  
     </ContentTemplate>
        </asp:UpdatePanel>


    </form>  
</body>
</html>