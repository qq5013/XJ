<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="WMS.Index.Left" %>
<%@ Register TagPrefix="UC" TagName="LeftMenu" Src="~/UserControl/LeftMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../css/leftmenu.css?t=009" type="text/css" rel="Stylesheet"/>
    <script type="text/javascript" src='<%=ResolveUrl("~/JQuery/jquery-1.8.3.min.js") %>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/Common.js") %>'></script>
<script type="text/javascript">
    // 
    $(document).ready(function () {
        $(".main").toggle(function () {
            $(this).next(".sub").animate({ height: 'toggle', opacity: 'toggle' }, "slow");
        }, function () {
            $(this).next(".sub").animate({ height: 'toggle', opacity: 'toggle' }, "slow");
        });

    });
    var timerRunning = false;

    function stopclock() {

        if (timerRunning)

            clearTimeout(timerID);

        timerRunning = false;

    }

    function showtime() {

        var now = new Date();

        var year = now.getYear();

        var month = now.getMonth();

        var day = now.getDate();

        var hours = now.getHours();

        var minutes = now.getMinutes();

        var seconds = now.getSeconds()

        var timeValue = " 当前登录用户: <%= PersonName%>      ";

        timeValue += "当前日期:" + year + "年" + (month + 1) + "月" + day + "日";

        timeValue += "     " + ((hours > 12) ? hours - 12 : hours);

        timeValue += ((minutes < 10) ? ":0" : ":") + minutes;

        timeValue += ((seconds < 10) ? ":0" : ":") + seconds;

        timeValue += (hours >= 12) ? " PM" : " AM";

        window.status = timeValue;

        timerID = setTimeout("showtime()", 1000);

        timerRunning = true;

    }

    function startclock() {

        stopclock();

        showtime();

    };
</script>
</head>
<body style=" margin:0 0 0 0;" onload="startclock();" bgcolor="#F8FCFF">
    <form id="form1" runat="server">
    <div style="background-image:url(../images/leftmenu/left-bg.jpg);">
    <UC:LeftMenu runat="server" ID="leftmenu" />
    </div>
    </form>
    <script type="text/javascript">
        function Logout() {
            window.opener = null;
            window.parent.close();
            window.open('../Login.aspx?Logout=true', '_Parent', 'height=690, width=1014, top=0, left=0, toolbar=yes, menubar=yes, scrollbars=yes, resizable=yes,location=yes, status=yes');
        }

        function Exit() {
            window.parent.close();
        }
    </script>
</body>
</html>