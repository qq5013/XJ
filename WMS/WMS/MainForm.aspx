<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="WMS.MainForm" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>自动化仓储管理系统</title>
<script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/Common.js") %>'></script>
<script type="text/javascript" language="javascript">
    window.moveTo(0, 0);
    window.resizeTo(screen.availWidth, screen.availHeight);
    function GotoReset() {
        //alert("start");
        window.open("Reset.aspx", '_self');
        //window.opener = null;
        //window.open('', '_self', ''); 
        //window.close();
    }
    function avoid(obj) {
        alert(obj.src);
    }
    function hrefTab(url, titleName, id) {
        mainFrame.addTab(url, titleName, id);
    }
    function delTab() {
        mainFrame.delTab();
    }

</script>
</head>
<frameset id="frameMain"  target="_parent" rows="71,*" cols="*" frameborder="no" border="0" framespacing="0">
  <frame src="Index/Top.aspx" name="topFrame" scrolling="No" noresize="noresize" frameborder="0" id="topFrame" title="topFrame" />
  <frameset id="main" rows="*" cols="170,8,100%" framespacing="0" frameborder="no" border="0">
    <frame src="Index/Left.aspx" name="leftFrame" scrolling="auto" noresize="noresize" frameborder="0" id="leftFrame" title="leftFrame"/>
    <frame id="Spliter" name="handle" src="Index/Spliter.aspx" frameborder=0 scrolling="no" noresize="noresize"/>
    <frameset rows="8,*" cols="*" frameborder="no" border="0" framespacing="0">
        <frame id="topButton" name="handle" src="Index/topButton.aspx" frameborder=0 scrolling="no" noresize="noresize"/>
        <frame name="mainFrame" id="mainFrame" frameborder="0" scrolling="no" title="mainFrame" src="Index/Default.aspx"/>
    </frameset>
  </frameset>
</frameset>
</html>