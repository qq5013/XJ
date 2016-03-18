<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topButton.aspx.cs" Inherits="WMS.Index.topButton" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
  <meta   content="Microsoft   Visual   Studio   .NET   7.0"   name="GENERATOR"/>   
  <meta   content="Visual   Basic   7.0"   name="CODE_LANGUAGE"/>   
  <meta   content="JavaScript"   name="vs_defaultClientScript"/>   
  <meta   content="http://schemas.microsoft.com/intellisense/ie5"   name="vs_targetSchema"/> 
  <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/Common.js") %>'></script>  
  <script type="text/javascript"   language="javascript"   id="clientEventHandlersJS">  
   
      function adjust2() {
          if (window.parent.frameMain.rows == "0,100%") {
              window.IMG1.src = "../images/leftmenu/toptop.jpg";
              window.parent.frameMain.rows = "71,100%";
              window.IMG1.alt = "隐藏导航栏";
              window.parent.mainFrame.setTabsHeight(false);

          }
          else {
              window.IMG1.src = "../images/leftmenu/topdown.jpg";
              window.parent.frameMain.rows = "0,100%";
              window.IMG1.alt = "显示导航栏";
              window.parent.mainFrame.setTabsHeight(true);
          }
      }    
    
  </script>   
  </head>  
  <body style="left:0px;top:0px; margin:0 0 0 0;" >  
      <table height="8px" width="100%" border="0" cellpadding="0" cellspacing="0" style="background-image:url(../images/leftmenu/tops_bg.jpg);">        
          <tr>
             <td height="8px"  style="width: 100%;text-align:center;"><a href="#"><img id="IMG1" alt="隐藏头部信息" onclick="return adjust2()" src="../images/leftmenu/toptop.jpg" border="0" name="IMG1"/></a></td>
          </tr>
      </table>
  </body> 
</html>

