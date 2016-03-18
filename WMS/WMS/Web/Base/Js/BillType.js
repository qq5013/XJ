//Ext.onReady(function () {
var pageSize = 15

//3.创建grid
var grid = Ext.create("Ext.grid.Panel", {
    xtype: "grid",
    store: store,
    id: "grid1",
    height: document.documentElement.clientHeight,
    layout: 'fit',
    dock: 'full',
    autoWidth: true,
    frame: true,
    margin: 0,
    columnLines: true,
    renderTo: Ext.getBody(),
    selModel: {
        injectCheckbox: 0,
        mode: "SINGLE",     //"SINGLE"/"SIMPLE"/"MULTI"
        checkOnly: false     //只能通过checkbox选择
    },
    selType: "checkboxmodel",
    columns: [
                { text: 'NO', xtype: 'rownumberer', width: 40, sortable: false },
                { text: '类型编号', dataIndex: 'BillTypeCode', flex: true },
                { text: '入库类型', dataIndex: 'BillTypeName', flex: true },
                { text: '备注', dataIndex: 'Memo', flex: true }
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
                         items: ['每页显示', combo],
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
                                search_win.show();
                            }
                        },
                        { iconCls: 'icon-add',
                            text: '新增',
                            itemId: 'add',
                            scope: this, //添加
                            handler: function () {
                                showEditWindow('1');
                            }
                        },
                        { iconCls: 'icon-edit',
                            text: '编辑',
                            itemId: 'edit',
                            scope: this, //添加
                            handler: function () {
                                showEditWindow('2');
                            }
                        },
                        { iconCls: 'icon-remove',
                            text: '删除',
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
                                Ext.Msg.confirm("提示", "确定要关闭" + title + "吗？", function (name) {
                                    if (name == 'yes')
                                        tab.remove(tab.getActiveTab());
                                });
                            }
                        }]
                    }],
    viewConfig: {
        forceFit: true,
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

    function getItems(flag) {
        var allowblank = false;
        if (flag == "2")
            allowblank = true;
        var items = [{
            //输入文本框  
            xtype: 'textfield',
            name: 'BillTypeCode',
            fieldLabel: '类型编号',
            allowBlank: allowblank,
            emptyText: '',
            blankText: "请输入类型编号"
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'BillTypeName',
            fieldLabel: '入库类型',
            allowBlank: allowblank,
            emptyText: '',
            blankText: "请输入入库类型"
        }, {
            //输入文本框  
            xtype: 'textarea',
            name: 'Memo',
            fieldLabel: '备注',
            allowBlank: true,
            emptyText: '',
            blankText: "请输入备注"
        }];
        return items;
    }
    var edit_panel = Ext.create('Ext.form.Panel', {
        frame: true,   
        //title: 'Form Fields',  
        width: 340,
        bodyPadding: 5,
        fieldDefaults: {
            labelAlign: 'left',
            labelWidth: 90,
            anchor: '100%'
        }
    });
    edit_panel.add(getItems("1"));
    var search_panel = Ext.create('Ext.form.Panel', {
        frame: true,   //frame属性  
        //title: 'Form Fields',  
        width: 340,
        bodyPadding: 5,
        //renderTo:"panel21",  
        fieldDefaults: {
            labelAlign: 'left',
            labelWidth: 90,
            anchor: '100%'
        }
    });
    search_panel.add(getItems("2"));
    function showEditWindow(flag) {

        if (flag == "2") {
            var grid = Ext.getCmp('grid1');
            var record = grid.getSelectionModel().getSelected();

            var data = {
                BillTypeCode: record.items[0].data.BillTypeCode,
                BillTypeName: record.items[0].data.BillTypeName,
                Memo: record.items[0].data.Memo
            };
            edit_win.down('form').getForm().setValues(data);
        }
        else {
            edit_win.down('form').getForm().reset();
        }
        edit_win.show();          // 显示窗口
    }
    //创建window面板，表单面板是依托window面板显示的
    var edit_win = Ext.create('Ext.window.Window', {
        title: "入库类型新增",
        width: 350,
        iconCls: "addicon",
        resizable: false,
        // 是否可以拖动  
        // draggable:false,  
        collapsible: true,
        closeAction: 'close',
        closable: true,
        // 弹出模态窗体  
        modal: 'true',
        buttonAlign: "center",
        bodyStyle: "padding:0 0 0 0",
        items: [edit_panel],
        buttons: [{
            text: "保存",
            minWidth: 70,
            handler: function () {
                //debugger;
                if (edit_panel.getForm().isValid()) {
                    edit_panel.getForm().submit({
                        url: '../Base/CigaretteEdit.aspx',
                        //等待时显示 等待  
                        waitTitle: '请稍等...',
                        waitMsg: '正在提交信息...',
                        params: edit_panel.getValues(),
                        success: function (fp, o) {
                            debugger;
                            //alert(o.result.toString());
                            //alert(o);success函数，成功提交后，根据返回信息判断情况  
                            if (o.result) {
                                Ext.Msg.alert("信息提示", "保存成功!");
                                edit_win.close(); //关闭窗口  
                                store.reload();
                            } else {
                                Ext.Msg.alert('信息提示', '添加时出现异常！');
                            }
                        },
                        failure: function () {
                            Ext.Msg.alert('信息提示', '添加失败！');
                        }
                    });
                }
            }
        }, {
            text: "关闭",
            minWidth: 70,
            handler: function () {
                edit_win.close();
            }
        }]
    });

    //创建window面板，表单面板是依托window面板显示的
    var search_win = Ext.create('Ext.window.Window', {
        title: "入库类型查询",
        width: 350,
        //height : 120,  
        //plain : true,  
        iconCls: "searchicon",
        // 不可以随意改变大小  
        resizable: false,
        // 是否可以拖动  
        // draggable:false,  
        collapsible: true, // 允许缩放条  
        closeAction: 'close',
        closable: true,
        // 弹出模态窗体  
        modal: 'true',
        buttonAlign: "center",
        bodyStyle: "padding:0 0 0 0",
        items: [search_panel],
        buttons: [{
            text: "查询",
            type: 'submit',
            minWidth: 70,
            handler: function () {
                //debugger;
                store.load({ params: search_panel.getForm().getValues(), 'limit': pageSize });
                search_win.close();                
            }
        }, {
            text: "关闭",
            minWidth: 70,
            handler: function () {
                search_win.close();
            }
        }]
    });
//});
