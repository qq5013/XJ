<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="WMS.Start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function opennew() {
            //var udswin = window.open("Index.aspx", "", "toolbar=no,status=yes,resizable=no");
            var udswin = window.open("MainForm.aspx");
            udswin.moveTo(0, 0);
            udswin.resizeTo(window.screen.availWidth, window.screen.availHeight);
            window.opener = null;
            //window.close();
            closeWindow();
        }
        function closeWindow() {
            window.open('', '_self', '');
            window.close();
        }
		</script>
</head>
<body onload="opennew()">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>