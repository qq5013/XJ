
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
    },{
        //输入文本框  
        xtype: 'textfield', 
        name: 'Barcode',
        fieldLabel: '卷烟条码',
        //value: '输入文本框',  
        allowBlank: false,
        emptyText: '',
        blankText: "请输入卷烟条码"
    },{
        //下拉列表框  
        xtype: 'combobox', //9  
        name: 'Province',
        fieldLabel: '卷烟省份',
        valueField:  'id',
        displayField: 'name',
        editable: false,
        value:'---请选择---',
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
var syswin = Ext.create('Ext.window.Window', {
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
    items: [add_winForm],
    buttons: [{
        text: "保存",
        minWidth: 70,
        handler: function () {
            //debugger;
            if (add_winForm.getForm().isValid()) {
                add_winForm.getForm().submit({
                    url: '../Home.aspx',
                    //等待时显示 等待  
                    waitTitle: '请稍等...',
                    waitMsg: '正在提交信息...',
                    params: add_winForm.getValues(),
                    success: function (fp, o) {
                        debugger;
                        //alert(o.result.toString());
                        //alert(o);success函数，成功提交后，根据返回信息判断情况  
                        if (o.result) {
                            Ext.Msg.alert("信息提示", "保存成功!");
                            syswin.close(); //关闭窗口  
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
            syswin.close();
        }
    }]
});
    
