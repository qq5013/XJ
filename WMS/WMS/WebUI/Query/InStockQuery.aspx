<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InStockQuery.aspx.cs" Inherits="WMS.WebUI.Query.InStockQuery" %>
<%@ Register src="../../UserControl/Calendar.ascx" tagname="Calendar" tagprefix="uc1" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
        <link href="~/Css/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Css/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Css/Detail.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveClientUrl("~/JQuery/jquery-1.8.3.min.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/Common.js") %>'></script>
       <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/DataProcess.js") %>'></script>
       
       <script type="text/javascript">
           $(document).ready(function () {
               BindEvent();
           });
           function BindEvent() {

               $("#btnFact").bind("click", function () {
                   getMultiItems("CMD_Factory", "FactoryID", this, '#hdnFact');
                   return false;
               });
               $("#txtFact").bind("dblclick", function () {
                   GetOtherValueNullClear('CMD_Factory', 'txtFactID,txtFact', "FactoryID,FactoryName", "1=1");
                   return false;
               });

               $("#btnProduct").bind("click", function () {
                   getMultiItems("CMD_Product", "PRODUCT_CODE", this, '#HdnProduct');
                   return false;
               });
               $("#txtProductModule").bind("dblclick", function () {
                   GetOtherValueNullClear("CMD_Product", "txtProductModule,txtProductID", "PRODUCT_MODEL,PRODUCT_CODE", "1=1");
                   return false;
               });
               //规格
               $("#btnColor").bind("click", function () {
                   var ProductID = "";
                   if ($("#btnProduct").val() == "指定") {
                       if ($('#txtProductID').val() == "") {
                           alert("请先选择产品型号！");
                           return false;
                       }
                       ProductID = "'" + $('#txtProductID').val() + "'";
                   }
                   else {
                       ProductID = $('#HdnProduct').val();
                   }

                   getMultiItems("CMD_COLOR", "COLOR_CODE", this, '#hdnColor', "PRODUCT_CODE in (" + ProductID + ")");
                   return false;
               });
               $("#txtColor").bind("dblclick", function () {
                   var ProductID = "";
                   if ($("#btnProduct").val() == "指定") {
                       if ($('#txtProductID').val() == "") {
                           alert("请先选择产品型号！");
                           return false;
                       }
                       ProductID = "'" + $('#txtProductID').val() + "'";
                   }
                   else {
                       ProductID = $('#HdnProduct').val();
                   }
                   GetOtherValueNullClear('CMD_COLOR', "txtColorID,txtColor", "COLOR_CODE,COLOR_NAME", "PRODUCT_CODE in (" + ProductID + ")");
                   return false;
               });
           }

           function PrintClick() {
               $('#HdnWH').val(document.body.clientWidth + "#" + document.body.clientHeight);
               return true;

           }
        </script>
     
    </head>
<body  style="overflow:hidden;">
  <form id="form1" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>            
             <div id="progressBackgroundFilter" style="display:none"></div>
        <div id="processMessage"> Loading...<br /><br />
             <img alt="Loading" src="../../images/loading.gif" />
        </div>            
 
        </ProgressTemplate>
    </asp:UpdateProgress>  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">                
            <ContentTemplate>
            <table    style="width:100%;height:100%;" >
        <tr>
            <td align="left"  class="maintable" style="height:22px"  >
                列印
            </td>   
        </tr>
        <tr runat ="server" id = "rptform" valign="top">
            <td align="left"  style="width:100%; height:70px"  >
                <table style="width: 100%; height: 20px;" class="maintable"   width="100%" align="center" 	>
                    <tr>
                        <td  class="smalltitle" align="right" style="height: 24px; width:90px">
                           产品型号 <asp:textbox id="txtProductID"   runat="server" Width="0px" CssClass="TextBox" heigth="0px" style="display:none" ></asp:textbox>
                        </td>
                        <td align="left" style="height: 24px;width:90px ">
                          &nbsp;<asp:textbox id="txtProductModule" tabIndex="1" runat="server" Width="92%" CssClass="TextBox" heigth="16px"  AutoCompleteType="Disabled"></asp:textbox>
                        </td>
                        <td align="left" style="height: 24px;width:80px "  >
                               <asp:Button ID="btnProduct" runat="server" CssClass="but" Text="指定" Width="70px" Height="23px" />
                        </td>
                        <td class="smalltitle" align="right"  style="height: 24px; width:100px">
                           规格<asp:textbox id="txtColorID" tabIndex="1" runat="server" Width="0px" CssClass="TextBox" heigth="0px" style="display:none"></asp:textbox>
                        </td>
                        <td align="left" style="height: 24px;width:90px ">
                           &nbsp;<asp:textbox id="txtColor" tabIndex="1" runat="server" Width="92%" CssClass="TextBox" heigth="16px" AutoCompleteType="Disabled"></asp:textbox>
                        </td>
                        <td align="left" style="height: 24px;width:90px " >
                            <asp:Button ID="btnColor" runat="server" CssClass="but" Text="指定" Width="70px" Height="23px" />
                        </td>
                        <td class="smalltitle" align="right" style="height: 24px;width:90px "  >
                           供应商 <asp:textbox id="txtFactID" tabIndex="1" runat="server" Width="0px" CssClass="TextBox" heigth="0px" style="display:none"></asp:textbox>
                        </td>
                        <td align="left" style="height: 24px;width:175px " >
                          &nbsp;<asp:textbox id="txtFact" tabIndex="1" runat="server" Width="92%" CssClass="TextBox" heigth="16px" AutoCompleteType="Disabled"></asp:textbox>
                        </td>
                        <td align="left" style="height: 24px;width:75px " >
                            <asp:Button ID="btnFact" runat="server" CssClass="but" Text="指定"   Width="70px" Height="22px" />
                        </td>
                        <td class="smalltitle" align="right" style="height: 24px; width:70px">
                           状态
                        </td>
                        <td align="left" style="height: 24px; width:260px">
                           &nbsp; <asp:RadioButton ID="opt1" runat="server" Checked="True" GroupName="LabelSource" Text="全部" />&nbsp; 
                            <asp:RadioButton ID="opt2" runat="server" GroupName="LabelSource" Text="已审" />&nbsp;
                            <asp:RadioButton ID="opt3" runat="server" GroupName="LabelSource" Text="未审" />                           
                        </td>
                       
                    </tr>
                    <tr>
                         <td class="smalltitle" align="right" style="height: 24px; width:90px">
                            入库日期
                        </td>
                        <td align="left" style="height: 24px; width:100px ">
                            &nbsp;<uc1:Calendar ID="txtStartDate" runat="server" />
                        </td>
                        <td align="center" style="height: 24px;width:80px  " >
                         To 
                        </td>
                        <td  style="height: 24px;width:100px  ">
                        &nbsp;<uc1:Calendar ID="txtEndDate" runat="server" />
                        </td>
                          
                        <td class="smalltitle" align="right" colspan="" >
                            报表
                        </td>
                         <td colspan="4" >
                             <asp:RadioButton ID="rpt1" runat="server" Checked="True" GroupName="Rpt" Text="明细表" />&nbsp;
                             <asp:RadioButton ID="rbtTotal" runat="server" GroupName="Rpt" Text="汇总表" />&nbsp;
                                                 
                        </td>
                        <td  colspan="2" >
                           <asp:Button ID="btnSearch" runat="server" CssClass="ButtonQuery" 
                                 onclick="btnPreview_Click" tabIndex="2" Text="查询" Width="58px" 
                                 onclientclick="return PrintClick();" /> &nbsp;&nbsp;
                             <asp:Button ID="btnRefresh" runat="server" CssClass="ButtonRefresh" 
                                 OnClientClick="return Refresh()" tabIndex="2" 
                                 Text="刷新" Width="58px" />                     
                        </td>

                    </tr>
                 
                </table>
            </td>
        </tr>
        <tr>
            <td runat ="server" id = "rptview" valign="top" align="left">
    
                            <span style="font-size: 10pt; font-family: Tahoma"><strong>
                                    <span style="font-size: 4pt"> </span>
                                    <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" ButtonsPath="images\buttons1"
                                        Font-Bold="False" Height = "100%" OnStartReport="WebReport1_StartReport"
                                        ToolbarColor="Lavender" Width="100%" Zoom="1" />
                                </strong></span>     
               
            </td>
        </tr>
    </table>
         <input type="hidden" id="hdnFact" runat="server" />
                 <input type="hidden" id="HdnProduct" runat="server" />
                  <input type="hidden" id="hdnColor" runat="server" />
          <input id="HdnWH" type="hidden" runat="server" value="0#0" />
        </ContentTemplate>
     </asp:UpdatePanel> 
   </form>
</body>
</html>