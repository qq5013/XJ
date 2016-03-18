
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
