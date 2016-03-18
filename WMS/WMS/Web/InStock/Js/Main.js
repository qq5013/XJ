//Ext.onReady(function () {
    var SearchPanel = Ext.create('Ext.form.Panel', {
        title: '按条件搜索',
        autoWidth: true,
        defaultType: 'textfield',
        frame: true,
        hidden: true,
        method: 'POST',
        collapsible: true, //可折叠
        bodyPadding: 5,
        layout: 'column',
        margin: '0 0 10 0',
        items: [{
            fieldLabel: '卷烟名称',
            labelWidth: 60,
            id: 'name'
        }, {
            xtype: 'button',
            text: '搜索',
            margin: '0 0 0 5',
            handler: function () {
                var name = Ext.getCmp('name').getValue(); //获取文本框值
                if (name != "") {
                    store.load({ params: { name: name} }); //传递参数  
                }
            }
        }],
        renderTo: Ext.getBody()
    });
    //3.创建grid
    var grid = Ext.create("Ext.grid.Panel", {
        xtype: "grid",
        store: store,
        id: "grid1",
        //                width: 500,
        height: document.documentElement.clientHeight / 2,
        //hetight: Ext.get("content").getHeight(), 
        //autoHeight: true,
        layout: 'fit',
        dock: 'full',
        autoWidth: true,
        frame: true,
        margin: 0,
        columnLines: true,
        renderTo: Ext.getBody(),
        selModel: {
            injectCheckbox: 0,
            mode: "SIMPLE",     //"SINGLE"/"SIMPLE"/"MULTI"
            checkOnly: true     //只能通过checkbox选择
        },
        selType: "checkboxmodel",
        columns: [
                { text: 'NO', xtype: 'rownumberer', width: 40, sortable: false },
                { text: '卷烟编号', dataIndex: 'CIGARETTECODE' },
                { text: '卷烟名称', dataIndex: 'CIGARETTENAME' },
                { text: '件烟条数', dataIndex: 'PALLETNUM', xtype: 'numbercolumn', format: '0',
                    editor: {
                        xtype: "numberfield",
                        decimalPrecision: 0,
                        selectOnFocus: true
                    }
                },
                { text: '件烟条码', dataIndex: 'BARCODE', editor: "textfield" },
                { text: '制造商', dataIndex: 'MAKERDESC', editor: "textfield" },
                { text: '省份', dataIndex: 'PROVINCE', editor: "textfield" },
                { text: '系列', dataIndex: 'VARIETYNAME', editor: "textfield" },
                { text: 'LED显示', dataIndex: 'SHOWNAME', editor: "textfield" }
            ],
        plugins: [
                Ext.create('Ext.grid.plugin.CellEditing', {
                    clicksToEdit: 1
                })
            ],

        dockedItems: [
                     {
                         xtype: 'pagingtoolbar',
                         store: store,    // same store GridPanel is using    
                         dock: 'bottom', //分页 位置  
                         emptyMsg: '没有数据',
                         displayInfo: true,
                         displayMsg: '当前显示{0}-{1}条记录 / 共{2}条记录 ',
                         beforePageText: '第', afterPageText: '页/共{0}页'
                     },
                    {
                        xtype: 'toolbar',
                        items: [
                        { iconCls: 'icon-search',
                            text: '查询',
                            itemId: 'search',
                            scope: this, //添加
                            handler: function () {
                                SearchPanel.show();
                            }
                        },
                        { iconCls: 'icon-add',
                            text: '新增',
                            itemId: 'add',
                            scope: this, //添加
                            handler: function () {
                                getSelectedTableName();
                                //AddPanel.show(); //显示
                                //win.show();
                                //MyformPanel.show();
                                syswin.show();
                            }
                        },
                        { iconCls: 'icon-edit',
                            text: '编辑',
                            itemId: 'edit',
                            scope: this, //添加
                            handler: function () {
                                getSelectedTableName();
                                //AddPanel.show(); //显示
                                //win.show();
                                //MyformPanel.show();
                                MyformPanel.show();
                            }
                        },
                        { iconCls: 'icon-remove',
                            text: '删除',
                            //disabled: true,     
                            itemId: 'delete',
                            //                        disabled: true,
                            scope: this,
                            handler: function () {
                                //var selModel = grid.getSelectionModel();
                                var selected = grid.getSelectionModel().getSelection();
                                var Ids = []; //要删除的id                        
                                Ext.each(selected, function (item) {
                                    Ids.push(item.data.id); //id 对应映射字段
                                })
                                //alert(Ids);
                            }
                        },
                        { iconCls: 'icon-print',
                            text: '打印',
                            itemId: 'print',
                            scope: this, //添加
                            handler: function () {
                                getSelectedTableName();
                                //AddPanel.show(); //显示
                                //win.show();
                                //MyformPanel.show();
                                syswin.show();
                            }
                        },
                        { iconCls: 'icon-exit',
                            text: '退出',
                            itemId: 'exit',
                            scope: this, //添加
                            handler: function () {
                                //var grid = Ext.getCmp("exit").up('#grid1');
                                var tab = top.window.parent.parent.Ext.getCmp("main");
                                var title = tab.getActiveTab().title;
//                                if (confirm("确定要弹出" + title + "吗？"))
//                                    tab.remove(tab.getActiveTab());
                                //tab.getActiveTab().id
                                Ext.Msg.confirm("提示", "确定要关闭" + title + "吗？", function (name) {
                                    if (name == 'yes')
                                        tab.remove(tab.getActiveTab());
                                });
                            }
                        }]
                    }],
        viewConfig: {
            stripeRows: true, //在表格中显示斑马线
            enableTextSelection: true, //可以复制单元格文字
            columnsText: '显示列', sortAscText: '升序', sortDescText: '降序'
        },
        listeners: {
            itemdblclick: function (me, record, item, index, e, eOpts) {
                //双击事件的操作
            }
        }
        //                bbar: { xtype: "pagingtoolbar", store: store, displayInfo: true }
    });

    //Grid toolbar根据权限禁用按钮
    Ext.getCmp("grid1").down('#delete').disabled = true;

    //3.创建grid
    var grid2 = Ext.create("Ext.grid.Panel", {
        xtype: "grid",
        store: store,
        id: "grid2",
        height: document.documentElement.clientHeight / 2,

        layout: 'fit',
        dock: 'full',
        autoWidth: true,
        frame: true,
        margin: 0,
        columnLines: true,
        renderTo: Ext.getBody(),
        selModel: {
            injectCheckbox: 0,
            mode: "SIMPLE",     //"SINGLE"/"SIMPLE"/"MULTI"
            checkOnly: true     //只能通过checkbox选择
        },
        selType: "checkboxmodel",
        columns: [
                            { text: 'NO', xtype: 'rownumberer', width: 40, sortable: false },
                            { text: '卷烟编号', dataIndex: 'CIGARETTECODE' },
                            { text: '卷烟名称', dataIndex: 'CIGARETTENAME' },
                            { text: '件烟条数', dataIndex: 'PALLETNUM', xtype: 'numbercolumn', format: '0',
                                editor: {
                                    xtype: "numberfield",
                                    decimalPrecision: 0,
                                    selectOnFocus: true
                                }
                            },
                            { text: '件烟条码', dataIndex: 'BARCODE', editor: "textfield" },
                            { text: '制造商', dataIndex: 'MAKERDESC', editor: "textfield" },
                            { text: '省份', dataIndex: 'PROVINCE', editor: "textfield" },
                            { text: '系列', dataIndex: 'VARIETYNAME', editor: "textfield" },
                            { text: 'LED显示', dataIndex: 'SHOWNAME', editor: "textfield" }
                        ],
        plugins: [
                            Ext.create('Ext.grid.plugin.CellEditing', {
                                clicksToEdit: 1
                            })
                        ],

        dockedItems: [
                     {
                         xtype: 'pagingtoolbar',
                         store: store,    // same store GridPanel is using    
                         dock: 'bottom', //分页 位置  
                         emptyMsg: '没有数据',
                         displayInfo: true,
                         displayMsg: '当前显示{0}-{1}条记录 / 共{2}条记录 ',
                         beforePageText: '第', afterPageText: '页/共{0}页'
                     }],
        viewConfig: {
            stripeRows: true, //在表格中显示斑马线
            enableTextSelection: true, //可以复制单元格文字
            columnsText: '显示列', sortAscText: '升序', sortDescText: '降序'
        },
        listeners: {
            itemdblclick: function (me, record, item, index, e, eOpts) {
                //双击事件的操作
            }
        }
        //                bbar: { xtype: "pagingtoolbar", store: store, displayInfo: true }
    });

    function getSelectedTableName() {
        var grid = Ext.getCmp('grid1');
        var rowSelectionModel = grid.getSelectionModel();
        if (rowSelectionModel.hasSelection()) {

            var record = rowSelectionModel.getSelected();

            for (var i = 0; i < record.length; i++) {
                alert(record.items[i].data.CIGARETTECODE);
            }
            var tableName = record.get('tableName');
            return tableName;
        } else {
            return '';
        }
    }
//});
