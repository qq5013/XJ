//Ext.onReady(function () {
var pageSize = 15

function renderUrl(value) {
    return "<a href='../../WebUI/CMD/ColorView.aspx?ID=" + value + "'>" + value + "</a>";    
}

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
                { text: '卷烟编号', dataIndex: 'CIGARETTECODE', width: 70, renderer: renderUrl },
                { text: '卷烟名称', dataIndex: 'CIGARETTENAME', width: 100 },
                { text: '件烟条数', dataIndex: 'PALLETNUM', width: 70, xtype: 'numbercolumn', format: '0', editor: { xtype: "numberfield", decimalPrecision: 0, selectOnFocus: true} },
                { text: '件烟条码', dataIndex: 'BARCODE', width: 120, editor: "textfield" },
                { text: '制造商', dataIndex: 'MAKERDESC', width: 240, editor: "textfield" },
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
                                getSelectedTableName();
                                //AddPanel.show(); //显示
                                //win.show();
                                //MyformPanel.show();
                                edit_win.show();
                            }
                        },
                        { iconCls: 'icon-edit',
                            text: '编辑',
                            itemId: 'edit',
                            scope: this, //添加
                            handler: function () {
                                //getSelectedTableName();
                                var grid = Ext.getCmp('grid1');
                                var record = grid.getSelectionModel().getSelected();
                                debugger;
                                alert(record.items[0].data.CIGARETTENAME);

                                var data = {
                                    CigaretteCode: record.items[0].data.CIGARETTECODE,
                                    CigaretteName: record.items[0].data.CIGARETTENAME,
                                    ShortName: record.items[0].data.SHORTNAME,
                                    Barcode: record.items[0].data.BARCODE,
                                    Province: record.items[0].data.BARCODE
                                };
                                edit_win.down('form').getForm().setValues(data);
                                edit_win.show();
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
                                edit_win.show();
                            }
                        },
                        { iconCls: 'icon-exit',
                            text: '退出',
                            itemId: 'exit',
                            scope: this, //添加
                            handler: function () {
                                //var grid = Ext.getCmp("exit").up('#grid1');
                                debugger
                                var tab = window.parent.Ext.getCmp("main");
                                var title = tab.getActiveTab().title;
                                tab.items.hide(tab.getActiveTab());
                                //                                Ext.Msg.confirm("提示", "确定要关闭" + title + "吗？", function (name) {
                                //                                    if (name == 'yes') {
                                //tab.remove(tab.getActiveTab());
                                //                                    }
                                //                                });
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
    var edit_panel = Ext.create('Ext.form.Panel', {
        frame: true,   //frame属性  
        //title: 'Form Fields',  
        width: 340,
        bodyPadding: 5,
        //renderTo:"panel21",  
        fieldDefaults: {
            labelAlign: 'left',
            labelWidth: 90,
            anchor: '100%'
        },
        items: [{
            //输入文本框  
            xtype: 'textfield',
            name: 'CigaretteCode',
            fieldLabel: '卷烟编号',
            //value: '输入文本框',  
            allowBlank: false,
            emptyText: '0000',
            blankText: "请输入卷烟编号"
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'CigaretteName',
            fieldLabel: '卷烟名称',
            //value: '输入文本框',  
            allowBlank: false,
            emptyText: '',
            blankText: "请输入卷烟名称"
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'ShortName',
            fieldLabel: '卷烟简称',
            //value: '输入文本框',  
            allowBlank: false,
            emptyText: '',
            blankText: "请输入卷烟简称"
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'Barcode',
            fieldLabel: '卷烟条码',
            //value: '输入文本框',  
            allowBlank: false,
            emptyText: '',
            blankText: "请输入卷烟条码"
        }, {
            //下拉列表框  
            xtype: 'combobox', //9  
            name: 'Province',
            fieldLabel: '卷烟省份',
            valueField: 'id',
            displayField: 'name',
            editable: false,
            value: '---请选择---',
            store: Ext.create('Ext.data.Store', {
                fields: [{ type: 'string', name: 'id' },
                          { type: 'string', name: 'name' }
                          ],
                data: [
                          { "id": "1", "name": "云南省" },
                          { "id": "2", "name": "河南省" },
                          { "id": "3", "name": "福建省" },
                          { "id": "4", "name": "江西省" },
                          { "id": "5", "name": "山东省" }
                          ]
            }),
            queryMode: 'local',
            typeAhead: true
        }, {
            //只能输入数字的文本框  
            xtype: 'numberfield',
            name: 'PurchasePrice', //10  
            fieldLabel: '销售单价',
            value: 20,
            minValue: 0,
            maxValue: 100
        }, {
            //复选框  
            xtype: 'checkboxfield', //11  
            name: 'IsProvince',
            fieldLabel: '是否省内',
            boxLabel: '是否省内'
        }]
    });
    //创建window面板，表单面板是依托window面板显示的
    var edit_win = Ext.create('Ext.window.Window', {
        title: "卷烟品牌新增",
        width: 350,
        //height : 120,  
        //plain : true,  
        iconCls: "addicon",
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
        },
        items: [{
            //输入文本框  
            xtype: 'textfield',
            name: 'CigaretteCode',
            fieldLabel: '卷烟编号',
            //value: '输入文本框',  
            allowBlank: true,
            emptyText: "请输入卷烟编号"
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'CigaretteName',
            fieldLabel: '卷烟名称',
            //value: '输入文本框',  
            allowBlank: true,
            emptyText: "请输入卷烟名称"
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'ShortName',
            fieldLabel: '卷烟简称',
            //value: '输入文本框',  
            allowBlank: true,
            emptyText: '请输入卷烟简称'
        }, {
            //输入文本框  
            xtype: 'textfield',
            name: 'Barcode',
            fieldLabel: '卷烟条码',
            //value: '输入文本框',  
            allowBlank: true,
            emptyText: '请输入卷烟条码'
        }, {
            //下拉列表框  
            xtype: 'combobox', //9  
            name: 'Province',
            fieldLabel: '卷烟省份',
            valueField: 'id',
            displayField: 'name',
            editable: false,
            value: '---请选择---',
            store: Ext.create('Ext.data.Store', {
                fields: [{ type: 'string', name: 'id' },
                          { type: 'string', name: 'name' }
                          ],
                data: [
                          { "id": "1", "name": "云南省" },
                          { "id": "2", "name": "河南省" },
                          { "id": "3", "name": "福建省" },
                          { "id": "4", "name": "江西省" },
                          { "id": "5", "name": "山东省" }
                          ]
            }),
            queryMode: 'local',
            typeAhead: true
        }, {
            //只能输入数字的文本框  
            xtype: 'numberfield',
            name: 'PurchasePrice', //10  
            fieldLabel: '销售单价',
            value: 20,
            minValue: 0,
            maxValue: 100
        }, {
            //复选框  
            xtype: 'checkboxfield', //11  
            name: 'IsProvince',
            fieldLabel: '是否省内',
            boxLabel: '是否省内'
        }]
    });
    //创建window面板，表单面板是依托window面板显示的
    var search_win = Ext.create('Ext.window.Window', {
        title: "卷烟品牌查询",
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
