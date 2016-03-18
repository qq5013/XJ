<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WMS.Index.Default" %>
<%@ Register TagPrefix="UC" TagName="LeftMenu" Src="~/UserControl/LeftMenu.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>    
    <script src="../JScript/Check.js?time=2008" type="text/javascript"></script>
    <script type="text/javascript" src="../JQuery/jquery-1.8.3.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../ext-3.3.1/resources/css/ext-all.css" />
 	<script type="text/javascript" src="../ext-3.3.1/ext-base.js"></script>
    <script type="text/javascript" src="../ext-3.3.1/ext-all.js"></script>
    <script type="text/javascript" src="../ext-3.3.1/ux/TabCloseMenu.js"></script>
       <script type="text/javascript" src='<%=ResolveClientUrl("~/JScript/Common.js") %>'></script>
    <script type="text/javascript">
        var tabs = null;
        var tabHeight;
        var tabMaxHeight;
        var resize = false;
        Ext.onReady(function () {
            tabs = new Ext.TabPanel({
                renderTo: 'tabs',
                resizeTabs: true, // turn on tab resizing
                minTabWidth: 115,
                tabWidth: 135,
                enableTabScroll: true,
                layoutOnTabChange: true,
                width: '100%',

                //                height: Ext.get("tabs").getComputedHeight(),
                height: document.body.offsetHeight - 5,
                defaults: { autoScroll: true, layout: 'fit' },
                plugins: new Ext.ux.TabCloseMenu()
            });

            addTab("main.aspx", "首页", "default");
            $(window).resize(setTabsHeight);
            setTabsHeight();

        });


        function addTab(url, name, idName) {
//            if (tabs.findById(idName) != null) {
//                tabs.setActiveTab(idName);
//                //tabs.getUpdater().refresh();
//                var updater = tabs.getUpdater();
//                //updater.update({ html: "<iframe src='" + url + "' width='100%' height='100%' frameborder='0' style='overflow:hidden'></iframe>" });
//                window.frames["if_" + idName].location.reload();
//                //if (updater) {
//                //    updater.loadScripts = true;
//                //    updater.defaultUrl = url;
//                //    updater.refresh();
//                //}
//                return;
//            }
            var closeable = true;
            if (name == "首页")
                closeable = false;
            tabs.add({
                title: name,
                //id: idName,
                layout: 'fit',
                iconCls: 'tabs',
                html: "<iframe id='if_" + idName + "' src='" + url + "' width='100%' height='100%' frameborder='1' style='overflow:hidden' scrolling='no'></iframe>",
                closable: closeable
            }).show();
        }
        function delTab() {
            //tabs.remove(id);
            tabs.remove(tabs.getActiveTab().id)
        }
        function setTabsHeight(top) {
            if (resize) {
                resize = false;
                return;
            }

            resize = true;
            if (top) {

                if (tabMaxHeight != null) {

                    tabs.setSize(document.body.offsetWidth, tabMaxHeight);
                    resize = false;
                    return;
                }
                tabMaxHeight = document.body.clientHeight - 16
                tabs.setSize(document.body.offsetWidth, tabMaxHeight);
            }
            else {
                if (tabHeight != null) {
                    tabs.setSize(document.body.offsetWidth, tabHeight);
                    resize = false;
                    return;
                }
                tabHeight = Ext.get("tabs").getComputedHeight() - 5;
                tabMaxHeight = tabHeight + 70;
                tabs.setSize(document.body.offsetWidth, tabHeight);
            }
            resize = false;
        }
            
    </script>
</head>

 
<body  bgcolor="#F8FCFF" style="width:100%;Height:100%;">
    <div id="tabs" style="width:100%;Height:100%;margin-left:0px;margin-top:0px;margin-right:0;"></div> 
</body> 
<script type="text/javascript">
</script>
</html> 
