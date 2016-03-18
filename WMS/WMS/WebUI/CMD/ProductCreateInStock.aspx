<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCreateInStock.aspx.cs" Inherits="WMS.WebUI.CMD.ProductCreateInStock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Css/Main.css" type="text/css" rel="stylesheet" /> 
    <link href="~/Css/op.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src="../../JQuery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src= "../../JScript/Common.js"></script>
    <script type="text/javascript">
        function Exit() {
            window.opener = null;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table class="maintable"  style="width:100%; height:100%;"  align="center" cellspacing="0" cellpadding="0"  border="1">
            <tr>
                <td class="musttitle" align="center" style="width:30%">
                     入库类型
                </td>
                <td>
                    <asp:DropDownList ID="ddlBillTypeCode" runat="server" Width="90%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
               <td class="musttitle" align="center" style="width:30%" >
                工厂
               </td>
               <td>
                  <asp:DropDownList ID="ddlFactoryID" runat="server" Width="90%">
                       </asp:DropDownList>
               </td>
            
            </tr>
            <tr>
                <td colspan="2" align="center" style="height:50px;">
                     <asp:Button ID="btnCreate" runat="server"  Text="新增入库单" CssClass="ButtonCreate" 
                         onclick="btnCreate_Click"/>&nbsp;
                     <input id="btnExit" value="退出" class="ButtonExit" onclick="Exit();" style=" width:40px; height:20px;" />
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
