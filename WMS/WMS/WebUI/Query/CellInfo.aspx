<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CellInfo.aspx.cs" Inherits="WMS.WebUI.Query.CellInfo" %>

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
         <table class="maintable"  width="100%" align="center" cellspacing="0" cellpadding="0"  border="1">
                   <tr>
                      <td colspan="2" class="musttitle">
                        <b>产品信息</b>
                      </td>
                   </tr>
                   <tr>
                      <td  class="smalltitle" style="width:20%;">
                            &nbsp;品名:
                      </td>
                      <td>
                        &nbsp;<asp:Label ID="lblProductName" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td   class="smalltitle" style="width:20%;">
                             &nbsp;条码:
                      </td>
                      <td >
                        &nbsp;<asp:Label ID="lblBarcode" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td   class="smalltitle"  style="width:20%;">
                             &nbsp;托盘:
                      </td>
                      <td >
                          &nbsp;<asp:Label ID="lblPalletCode" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td   class="smalltitle"  style="width:20%;">
                             &nbsp;单据号:
                      </td>
                      <td >
                          &nbsp;<asp:Label ID="lblBillNo" runat="server"></asp:Label>
                      </td>
                   </tr>
                    <tr>
                      <td   class="smalltitle"  style="width:20%;">
                             &nbsp;入库时间:
                      </td>
                      <td >
                          &nbsp;<asp:Label ID="lblIndate" runat="server"></asp:Label>
                      </td>
                   </tr>
                    <tr>
                      <td  colspan="2" class="musttitle">
                        <b>货架信息</b>
                      </td>
                   </tr>
                    <tr>
                      <td class="smalltitle" style="width:20%;" >
                             &nbsp;库区名称:
                      </td>
                      <td>
                          &nbsp;<asp:Label ID="lblAreaName" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td   class="smalltitle"  style="width:20%;">
                             &nbsp;货架名称:
                      </td>
                      <td>
                          &nbsp;<asp:Label ID="lblShelfName" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td   class="smalltitle" style="width:20%;">
                             &nbsp;列:
                      </td>
                      <td>
                          &nbsp;<asp:Label ID="lblCellColumn" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                      <td  class="smalltitle"  style="width:20%;">
                             &nbsp;层:
                      </td>
                      <td>
                          &nbsp;<asp:Label ID="lblCellRow" runat="server"></asp:Label>
                      </td>
                   </tr>
                    <tr>
                      <td  class="smalltitle"  style="width:20%;">
                             &nbsp;状态:
                      </td>
                      <td>
                          &nbsp;<asp:Label ID="lblState" runat="server"></asp:Label>
                      </td>
                   </tr>
                   <tr>
                    <td align="center" valign="middle" colspan="2" style="height:40px" >
                       <input id="btnExit" value="退出" class="ButtonExit" onclick="Exit();" style=" width:40px; height:20px;" />
                    </td>
                   </tr>
                   
               </table>
    </div>
    </form>
</body>
</html>
