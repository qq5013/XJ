//1.定义Model
//Ext.onReady(function () {
    Ext.define("MyApp.model.Cigarette", {
        extend: "Ext.data.Model",
        fields: [
                { name: 'CIGARETTECODE', type: 'string' },
                { name: 'CIGARETTENAME', type: 'string' },
                { name: 'PALLETNUM', type: 'int' },
                { name: 'BARCODE', type: 'string' },
                { name: 'MAKERDESC', type: 'string' },
                { name: 'PROVINCE', type: 'string' },
                { name: 'VARIETYNAME', type: 'string' },
                { name: 'SHOWNAME', type: 'string' }
            ]
    });
    //2.创建store
    var store = Ext.create("Ext.data.Store", {
        model: "MyApp.model.Cigarette",
        autoLoad: true,
        pageSize: 10,
        proxy: {
            type: "ajax",
            url: "../Ashx/MainHandler.ashx",
            reader: {
                root: 'items', totalProperty: 'total'
            }
        },
        sorters: [{
            property: 'CIGARETTECODE', //排序字段       
            direction: 'asc'// 默认ASC     
        }]
    });

    //store 传参
    store.on('beforeload', function (store, options) {
        var new_params = { flag: "1", billId: "1234567890" };
        Ext.apply(store.proxy.extraParams, new_params);
        // alert('beforeload');  
    });
//});