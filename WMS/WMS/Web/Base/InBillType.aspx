<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InBillType.aspx.cs" Inherits="WMS.Web.Base.InBillType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title></title>
    <link href="~/Css/Main.css" type="text/css" rel="stylesheet" /> 
    <link rel="stylesheet" type="text/css" href="../../Css/icon.css" />  
    <link rel="stylesheet" type="text/css" href="../../ext/packages/ext-theme-crisp/build/resources/ext-theme-crisp-all.css" />  
    <script type="text/javascript" src="../../JQuery/jquery-1.8.3.min.js"></script> 
    <script type="text/javascript" src="../../Ext/ext-all.js"></script>  
    <script type="text/javascript" src="../../Ext/packages/ext-theme-crisp/build/ext-theme-crisp.js"></script>
    <script type="text/javascript" src="../../JScript/gridPageSize.js"></script> 
    <script language="javascript" type="text/javascript">

        Ext.onReady(function () {
            $(window).resize(function () {
                grid.setHeight(document.documentElement.clientHeight);
            });
            $.getScript("Js/BillTypeModel.js").done(function () {
                $.getScript("Js/BillType.js");
            });
        }); 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
