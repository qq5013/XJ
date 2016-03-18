<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutProduct.aspx.cs" Inherits="WMS.WebUI.SysInfo.AboutProduct.AboutProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<style> 
html { overflow-y:auto; }  

    .style2
    {
        height: 19px;
        width: 2147px;
    }
    .style3
    {
        width: 2147px;
    }

</style>
    <title>无标题页</title>
    <script type="text/javascript" src="../../../JScript/Common.js"></script>
</head>
<body  bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" style="background-color:#f8fcff;">
    <form id="form1" runat="server">
    <div>
        <table id="aboutthisprocduct"   align="center" cellpadding="1" 
            cellspacing="0" style="top: 0px;position: absolute; left: 0px; height: 460px;" >
            <tr>
                <td style="" colspan="4">
           
                   <%-- <asp:Image ID="imgSoftName" runat="server" ImageUrl="~/images/Version.jpg" Visible="False"/>--%><br />
                    
                     
                </td>
            </tr>
            <tr>
                <td style="width: 374px;">
                </td>
                <td style="font-size: 12pt;" class="style3">
                    <asp:Label ID="LabVersion" runat="server" Text="软件版本号" Font-Names="宋体" Width="100%" Font-Bold="True"></asp:Label></td>
                 <td align="center" rowspan="7" style="width: 729px">
                                    <asp:Image ID="imgLogo" runat="server" ImageAlign="Middle" ImageUrl="../../../images/LOGO.jpg" /></td>
                <td align="center" rowspan="7" style="width: 540px">
                </td>
            </tr>
            <tr>
                <td style="width: 374px">
                    &nbsp;</td>
                <td class="style3">
                    <asp:Label ID="lblSoftwareName" runat="server" Font-Bold="True" Font-Names="宋体" Text="软件名称"
                        Width="100%"></asp:Label></td>
                 
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="font-size: 12pt;" class="style3">
                    <asp:Label ID="labCompany" runat="server" Text="公司名" Font-Names="宋体" Width="100%" Font-Bold="true"></asp:Label></td>
               
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="font-size: 12pt;" class="style3">
                    <asp:Label ID="labCompanyAddress" runat="server" Text="公司地址" Font-Names="宋体" Width="100%" Font-Bold="true"></asp:Label></td>
                      
            </tr>
            <tr>
                <td style="width: 374px">
                </td>
                <td style="font-size: 12pt;" class="style3">
                    <asp:Label ID="labCompanyTelephone" runat="server" Text="公司电话" Font-Names="宋体" Width="100%" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 374px; height: 19px">
                </td>
                <td style="font-size: 12pt;" class="style2">
                    <asp:Label ID="labCompanyFax" runat="server" Text="公司传真" Font-Names="宋体" Width="100%" Font-Bold="true"></asp:Label></td>
            </tr>
            
            <tr>
                <td style="width: 374px">
                </td>
                <td style="font-size: 12pt;" class="style3">
                    <asp:Label ID="labCompanyWeb" runat="server" Text="公司网页" Font-Names="宋体" Font-Bold="true"></asp:Label>
                    <asp:LinkButton ID="lbtnCompanyWeb" runat="server" OnClick="lbtnCompanyWeb_Click">www.jingjing-auto.com</asp:LinkButton></td>
            </tr>
             <tr>
                <td style="" colspan="4">
                <br /> 
                </td>
            </tr>
            
            <tr>
                <td align="center" colspan="1" style="height: 19px; width: 374px;">
                </td>
                <td align="center" colspan="2" style="height: 19px;font-size: 12pt;">
                    <asp:Label ID="labCopyrigth" runat="server" Font-Names="宋体" Width="100%" Font-Bold="True"></asp:Label></td>
                <td align="center" colspan="1" style="height: 19px;width: 540px; font-size: 12pt;">
                    <asp:LinkButton ID="lbtnQuit" runat="server" OnClientClick="Exit();" ForeColor="#404040">返回主界面</asp:LinkButton>
                    
                </td>
            </tr>
            
        </table>
        </div>
    </form>
</body>
</html>