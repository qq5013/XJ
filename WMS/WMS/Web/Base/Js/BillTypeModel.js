//1.定义Model
//Ext.onReady(function () {
    Ext.define("MyApp.model.BillType", {
        extend: "Ext.data.Model",
        fields: [
                { name: 'Flag', type: 'string' },
                { name: 'BillTypeCode', type: 'string' },
                { name: 'BillTypeName', type: 'string' },
                { name: 'Memo', type: 'string' }
            ]
    });
    //2.创建store
    var store = Ext.create("Ext.data.Store", {
        model: "MyApp.model.BillType",
        autoLoad: true,
        pageSize: 15,
        proxy: {
            type: "ajax",
            url: "../Ashx/MainHandler.ashx",
            reader: {
                root: 'items', totalProperty: 'total'
            }
        },
        sorters: [{
            property: 'BillTypeCode', //排序字段       
            direction: 'asc'// 默认ASC
        }]
    });

    //store 传参
    store.on('beforeload', function (store, options) {
        var new_params = { formID: "CMD.SelectInBillType", flag: "1", billId: "" };
        Ext.apply(store.proxy.extraParams, new_params);
        // alert('beforeload');  
    });
//});