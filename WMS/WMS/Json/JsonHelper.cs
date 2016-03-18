using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;


public abstract class JsonHelper
{
    /// <summary> 
    /// 对象转JSON 
    /// </summary> 
    /// <param name="obj">对象</param> 
    /// <returns>JSON格式的字符串</returns> 
    public static string ObjectToJSON(object obj)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            return jss.Serialize(obj);
        }
        catch (Exception ex)
        {
            throw new Exception("JSONHelper.ObjectToJSON(): " + ex.Message);
        }
    }
    /// <summary> 
    /// 数据表转键值对集合 
    /// 把DataTable转成 List集合, 存每一行 
    /// 集合中放的是键值对字典,存每一列 
    /// </summary> 
    /// <param name="dt">数据表</param> 
    /// <returns>哈希表数组</returns> 
    public static List<Dictionary<string, object>> DataTableToList(DataTable dt)
    {
        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        foreach (DataRow dr in dt.Rows)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DataColumn dc in dt.Columns)
            {
                dic.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            list.Add(dic);
        }
        return list;
    }
    /// <summary> 
    /// 数据集转键值对数组字典 
    /// </summary> 
    /// <param name="dataSet">数据集</param> 
    /// <returns>键值对数组字典</returns> 
    public static Dictionary<string, List<Dictionary<string, object>>> DataSetToDic(DataSet ds)
    {
        Dictionary<string, List<Dictionary<string, object>>> result = new Dictionary<string, List<Dictionary<string, object>>>();
        foreach (DataTable dt in ds.Tables)
            result.Add(dt.TableName, DataTableToList(dt));
        return result;
    }
    /// <summary> 
    /// 数据表转JSON 
    /// </summary> 
    /// <param name="dataTable">数据表</param> 
    /// <returns>JSON字符串</returns> 
    public static string DataTableToJSON(DataTable dt)
    {
        return ObjectToJSON(DataTableToList(dt));
    }
    /// <summary> 
    /// JSON文本转对象,泛型方法 
    /// </summary> 
    /// <typeparam name="T">类型</typeparam> 
    /// <param name="jsonText">JSON文本</param> 
    /// <returns>指定类型的对象</returns> 
    public static T JSONToObject<T>(string jsonText)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            return jss.Deserialize<T>(jsonText);
        }
        catch (Exception ex)
        {
            throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
        }
    }
    /// <summary> 
    /// 将JSON文本转换为数据表数据 
    /// </summary> 
    /// <param name="jsonText">JSON文本</param> 
    /// <returns>数据表字典</returns> 
    public static Dictionary<string, List<Dictionary<string, object>>> TablesDataFromJSON(string jsonText)
    {
        return JSONToObject<Dictionary<string, List<Dictionary<string, object>>>>(jsonText);
    }
    /// <summary> 
    /// 将JSON文本转换成数据行 
    /// </summary> 
    /// <param name="jsonText">JSON文本</param> 
    /// <returns>数据行的字典</returns> 
    public static Dictionary<string, object> DataRowFromJSON(string jsonText)
    {
        return JSONToObject<Dictionary<string, object>>(jsonText);
    }

    public static string Dtb2Json(DataTable dtb)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ArrayList dic = new ArrayList();
        foreach (DataRow row in dtb.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            foreach (DataColumn col in dtb.Columns)
            {
                if (row[col.ColumnName].GetType() == typeof(DateTime))
                {
                    drow.Add(col.ColumnName, ToYMDHM(row[col.ColumnName]));

                }
                else
                    drow.Add(col.ColumnName, row[col.ColumnName]);
            }
            dic.Add(drow);
        }
        return jss.Serialize(dic);
    }

    public static string ToYMDHM(object dateobj)
    {
        return DateFormat(dateobj, "yyyy/MM/dd HH:mm:ss");
    }
    private static string DateFormat(object dateobj, string strFormat)
    {
        if (dateobj == null || dateobj.ToString().Trim().Length == 0) return "";
        return ((DateTime)dateobj).ToString(strFormat).Replace("-", "/");   //"yyyy/MM/dd HH:mm"     
    }

    public static DataTable Json2Dtb(string json)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ArrayList dic = jss.Deserialize<ArrayList>(json);
        DataTable dtb = new DataTable();
        if (dic.Count > 0)
        {
            foreach (Dictionary<string, object> drow in dic)
            {
                if (dtb.Columns.Count == 0)
                {
                    foreach (string key in drow.Keys)
                    {
                        if (key.ToLower().IndexOf("date") >= 0)
                            dtb.Columns.Add(key, System.Type.GetType("System.DateTime"));
                        else
                            dtb.Columns.Add(key, drow[key].GetType());

                    }
                }
                DataRow row = dtb.NewRow();
                foreach (string key in drow.Keys)
                {
                    row[key] = Microsoft.JScript.GlobalObject.unescape(drow[key]);
                }
                dtb.Rows.Add(row);
            }
        }
        return dtb;
    }
    //类似 前台jQuery.parseJSON（dt）函数
    public static Dictionary<string, object> DatToJson(DataTable table)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Add(table.TableName, RowsToDictionary(table));
        return d;
    }
    public static List<Dictionary<string, object>> RowsToDictionary(DataTable table)
    {
        List<Dictionary<string, object>> objs = new List<Dictionary<string, object>>();
        foreach (DataRow dr in table.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                drow.Add(table.Columns[i].ColumnName, dr[i]);
            }
            objs.Add(drow);
        }
        return objs;
    }
}


