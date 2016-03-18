//Ext.onReady(function () {
    //3.创建grid
    var grid3 = Ext.create("Ext.grid.Panel", {
        xtype: "grid",
        store: store,
        id: "grid3",
        height: document.documentElement.clientHeight / 2,

        layout: 'fit',
        dock: 'full',
        autoWidth: true,
        frame: true,
        margin: 0,
        columnLines: true,
        //renderTo: Ext.getBody(),
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
    var add_winForm_detail = Ext.create('Ext.form.Panel', {
        frame: true,   //frame属性  
        width: 600,
        bodyPadding: 5,
        fieldDefaults: {
            labelAlign: 'left',
            labelWidth: 90,
            anchor: '100%'
        },
        items: [{
            autoHeight: true,
            layout: 'column',
            border: false,
            labelSeparator: ':',
            defaults: { layout: 'form', border: false, columnWidth: .5 },
            items: [{
                columnWidth: .5,
                xtype: 'fieldset',
                //title: '第一列信息',
                layout: 'form',
                defaults: { anchor: '95%' },
                style: 'margin-left: 5px;padding-left: 5px;',
                items: [{ xtype: 'textfield', name: 'textfield1', fieldLabel: '单号', allowBlank: false, emptyText: 'OS20150728001', blankText: "提示" },
                            { xtype: 'datefield', name: 'date1', fieldLabel: '单据日期', value: new Date() },
                            { xtype: 'combobox', fieldLabel: 'Combobox', displayField: 'name', store: Ext.create('Ext.data.Store', {
                                fields: [{ type: 'string', name: 'name'}],
                                data: [{ "name": "Alabama" }, { "name": "Alaska" }, { "name": "Arizona" }, { "name": "Arkansas" }, { "name": "California"}]
                            }),
                                queryMode: 'local',
                                typeAhead: true
                            }]
            },
                            {
                                columnWidth: .5,
                                xtype: 'fieldset',
                                //title: '第二列信息',
                                layout: 'form',
                                defaults: { anchor: '95%' },
                                style: 'margin-left: 5px;padding-left: 5px;',
                                items: [{ xtype: 'textfield', name: 'textfield1', fieldLabel: '单号', allowBlank: false, emptyText: 'OS20150728001', blankText: "提示" },
                                        { xtype: 'datefield', name: 'date1', fieldLabel: 'Date Field', value: new Date() },
                                        { xtype: 'combobox', fieldLabel: 'Combobox', displayField: 'name', store: Ext.create('Ext.data.Store', {
                                            fields: [{ type: 'string', name: 'name'}],
                                            data: [{ "name": "Alabama" }, { "name": "Alaska" }, { "name": "Arizona" }, { "name": "Arkansas" }, { "name": "California"}]
                                        }),
                                            queryMode: 'local',
                                            typeAhead: true
                                        }]
                            }]
        }
                ]

    });
    //创建window面板，表单面板是依托window面板显示的
    var syswin = Ext.create('Ext.window.Window', {
        title: "入库单新增",
        width: 600,
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
        items: [add_winForm_detail, grid3],
        buttons: [{
            text: "保存",
            minWidth: 70,
            handler: function () {
                //debugger;
                if (add_winForm_detail.getForm().isValid()) {
                    add_winForm_detail.getForm().submit({
                        url: 'Home.aspx',
                        //等待时显示 等待  
                        waitTitle: '请稍等...',
                        waitMsg: '正在提交信息...',
                        params: {
                            t: "add"
                        },
                        success: function (fp, o) {
                            //debugger;
                            //alert(o);success函数，成功提交后，根据返回信息判断情况  
                            if (o.result == true) {
                                Ext.Msg.alert("信息提示", "保存成功!");
                                syswin.close(); //关闭窗口  
                                // Store1.reload();  
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
                syswin.close();
            }
        }]
    });
    var add_winForm = Ext.create('Ext.form.Panel', {
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
            //隐藏的文本框  
            xtype: 'hiddenfield', //1  
            name: 'hiddenfield1',
            value: '隐藏的文本框'
        }, {
            //显示文本框，相当于label  
            xtype: 'displayfield', //2  
            name: 'displayfield1',
            fieldLabel: 'Display field',
            value: '显示文本框'

        }, {
            //输入文本框  
            xtype: 'textfield', //3  
            name: 'textfield1',
            fieldLabel: 'Text field',
            //value: '输入文本框',  
            allowBlank: false,
            emptyText: '陈建强',
            blankText: "提示"
        }, {
            //输入密码的文本框，输入的字符都会展现为.  
            xtype: 'textfield', //4  
            name: 'password1',
            inputType: 'password',
            fieldLabel: 'Password field'
        }, {
            //多行文本输入框  
            xtype: 'textareafield', //5  
            name: 'textarea1',
            fieldLabel: 'TextArea',
            id: "areaid",
            value: '啦啦啦，我是卖报的小行家'
        }, {
            //上传文件文本框  
            xtype: 'filefield', //6  
            name: 'file1',
            fieldLabel: 'File upload'
        }, {
            //时间文本框  
            xtype: 'timefield', //7  
            name: 'time1',
            fieldLabel: 'Time Field',
            minValue: '8:00 AM',
            maxValue: '5:00 PM',
            increment: 30
        }, {
            //日期文本框  
            xtype: 'datefield', //8  
            name: 'date1',
            fieldLabel: 'Date Field',
            value: new Date()
        }, {
            //下拉列表框  
            xtype: 'combobox', //9  
            fieldLabel: 'Combobox',
            displayField: 'name',
            store: Ext.create('Ext.data.Store', {
                fields: [
                          { type: 'string', name: 'name' }
                          ],
                data: [
                          { "name": "Alabama" },
                          { "name": "Alaska" },
                          { "name": "Arizona" },
                          { "name": "Arkansas" },
                          { "name": "California" }
                          ]
            }),
            queryMode: 'local',
            typeAhead: true
        }, {
            //只能输入数字的文本框  
            xtype: 'numberfield',
            name: 'numberfield1', //10  
            fieldLabel: 'Number field',
            value: 20,
            minValue: 0,
            maxValue: 50
        }, {
            //复选框  
            xtype: 'checkboxfield', //11  
            name: 'checkbox1',
            fieldLabel: 'Checkbox',
            boxLabel: '复选框'
        }, {
            //单选框，注意name和下面的单选框相同  
            xtype: 'radiofield', //12  
            name: 'radio1',
            value: 'radiovalue1',
            fieldLabel: 'Radio buttons',
            boxLabel: 'radio 1'
        }, {
            //单选框，注意name和上面的单选框相同  
            xtype: 'radiofield', //13  
            name: 'radio1',
            value: 'radiovalue2',
            fieldLabel: '',
            labelSeparator: '',
            hideEmptyLabel: false,
            boxLabel: 'radio 2'
        }, {
            //拖动组件  
            xtype: 'multislider', //14  
            fieldLabel: 'Multi Slider',
            values: [25, 50, 75],
            increment: 5,
            minValue: 0,
            maxValue: 100
        }, {
            //拖动组件  
            xtype: 'sliderfield', //15  
            fieldLabel: 'Single Slider',
            value: 50,
            increment: 10,
            minValue: 0,
            maxValue: 100
        }]
    });
    var AddPanel = Ext.create('Ext.form.Panel', {
        title: '表单',
        width: 300,
        frame: true,
        bodyPadding: 5,                //closable:true,//是否可关闭    
        hidden: true, //隐藏              
        margin: '10 0 0 0',
        defaultType: 'textfield', //name对应grid列中的dataIndex      
        items: [{ fieldLabel: 'name', name: 'name' }, { fieldLabel: 'time', name: 'time' }, { fieldLabel: 'phone', name: 'phone'}],
        renderTo: Ext.getBody(),
        buttons: [{
            text: "提交",
            handler: function () {
                AddPanel.form.submit({
                    waitTitle: "请稍候",
                    waitMsg: "正在提交表单数据，请稍候"
                });
            }
        }, {
            text: "取消",
            handler: function () {
                AddPanel.form.reset();
            }
        }]

    });



    var win = new Ext.Window({
        title: '选择商品',
        width: 901,
        height: 391,
        modal: true,
        hidden: true,
        layout: 'border',
        items: [
            { xtype: 'treepanel',
                title: '商品分类',
                region: 'west',
                width: 218,
                height: 332,
                root: { text: 'Tree Node' },
                loader: {}
            },
            {
                xtype: 'grid',
                title: '商品信息',
                region: 'center',
                columns: [
                       { xtype: 'gridcolumn',
                           dataIndex: 'string',
                           header: 'Column',
                           sortable: true,
                           width: 100
                       },
                       { xtype: 'numbercolumn',
                           dataIndex: 'number',
                           header: 'Column',
                           sortable: true,
                           width: 100,
                           align: 'right'
                       },
                       { xtype: 'datecolumn',
                           dataIndex: 'date',
                           header: 'Column',
                           sortable: true,
                           width: 100
                       },
                         {
                             xtype: 'booleancolumn',
                             dataIndex: 'bool',
                             header: 'Column',
                             sortable: true,
                             width: 100
                         }
                         ],
                bbar: {
                    items: [
               {
                   xtype: 'button',
                   text: '确定',
                   width: 50
               },

                { xtype: 'tbseparator' },
                { xtype: 'button', text: '取消', width: 50 }
                ]
                }
            }]
    });
    //创建表单面板  
    var MyformPanel = Ext.create('Ext.form.Panel', {
        frame: true,
        title: 'FormFields Validation',
        width: 340,
        hidden: true, //隐藏    
        bodyPadding: 5,
        renderTo: "form1",    //渲染到页面的form中去  
        fieldDefaults: {
            labelAlign: 'left',
            labelWidth: 90,
            anchor: '100%',
            //错误提示显示在下方，还可以配置为side、title、none  
            msgTarget: 'under'
        },
        items: [{
            xtype: 'fieldset',
            title: '用户信息',   //外框的title  
            collapsible: true,
            autoHeight: true,
            autoWidth: true,
            defaults: { width: 150, allowBlank: false, xtype: 'textfield' }, //提取共同属性  
            items: [{
                xtype: 'textfield',
                name: 'textfield1',
                fieldLabel: '必须输入',
                //不允许为空验证  
                allowBlank: false //1  
            }, {
                xtype: 'textfield',
                name: 'textfield2',
                fieldLabel: '最多两个字符',
                //输入的字符长度验证（至少输入2个字符）  
                minLength: 2 //2  
            }, {
                xtype: 'textfield',
                name: 'textfield3',
                fieldLabel: '最长5个字符',
                //输入的字符长度验证（最多输入2个字符）  
                maxLength: 5 //3  
            }, {
                xtype: 'textfield',
                name: 'textfield7',
                fieldLabel: '正则表达式验证电话号码',
                //通过正则表达式验证  
                regex: /^\d{3}-\d{3}-\d{4}$/, //4  
                regexText: 'Must be in the format xxx-xxx-xxxx'
            }, {
                xtype: 'textfield',
                name: 'textfield4',
                fieldLabel: '验证用户输入的是否为email',
                //已经定义好的验证，请通过文档查看vtype  
                vtype: 'email' //5  
            }, {
                xtype: 'textfield',
                name: 'textfield6',
                fieldLabel: '验证用户输入的是否是URL',
                vtype: 'url' //8  
            }]
        }],
        buttons: [{ text: "确定", handler: function () {
            //获取按钮的父表单  
            var form = this.up("form").getForm();
            //alert(form);  
            if (form.isValid())  //判断是否通过验证  
            {
                //获取页面的表单转化为dom对象后提交  
                Ext.get("form1").dom.submit();
                //获取页面的表单元素后提交  
            };
        }
        }, { text: "取消", handler: reset}],
        buttonAlign: 'center'
    });

    function reset() {
        MyformPanel.form.reset();
    }
//});