//2.创建store
var store = Ext.create("Ext.data.Store", {
    model: "MyApp.model.Cigarette",
    autoLoad: true,
    pageSize: 10,
    proxy: {
        type: "ajax",
        url: "GridHandler.ashx",
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