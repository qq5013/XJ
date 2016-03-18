有以下几种情况

1。初次配置参数

2。需要新加参数

3。需要改变原有的参数

4。需要删除已经有的参数




针对于第一个 初次配置参数  


     只要配置在baseParams里面就可以了

第二个   需要新加参数

     有两种方法 第一种是

                          var store = _grid.getStore();
                           store.setBaseParam("p1", "p11");
                           store.setBaseParam('limit', 50);

                           store.reload();




    第二种是    


                 var bp = store.baseParams;
                
                  Ext.apply(bp, {
                  'limit2' : 50,
                  'limit' : 50
                  });

                store.reload();




   注意：上面两种写法都只能新加参数，不能覆盖原有参数的值。


第三个    需要改变原有的参数

      var lastOptions = store.lastOptions;
                 Ext.apply(lastOptions.params, {
                     'limit2' : 50,
                     'limit' : 50
                 });
                 store.reload(lastOptions);

      就可以了




第四个    需要删除已经有的参数

             var lastOptions = store.lastOptions;
                 var p = lastOptions.params;
                 for ( var i in p) {
                     alert(i + "=" + p[i]);
                 }

         通过代码打印可以看到里面的参数的名称和值

        现在将不需要的参数进行删除




       delete lastOptions.params.limit;
     


         // lastOptions.params.limit = null;

                 Ext.apply(lastOptions.params, {
                     'limit2' : 100
                 });
                 store.reload(lastOptions);


          注意：在上述的两种删除变量的方法中，如果用delete删除，那么参数将彻底不存在，如果指定参数引用为null，则参数名称还存在，只不过传到后台是null；

                      建议使用delete进行删除。
