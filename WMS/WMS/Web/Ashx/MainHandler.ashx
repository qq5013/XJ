<%@ WebHandler Language="C#" Class="MainHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
using Util;

public class MainHandler : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        
        int page = int.Parse(context.Request["page"]);
        int pageSize = int.Parse(context.Request["limit"]);//每页显示数
        string formID = context.Request["formID"].ToString();
        string filter = getFilter(context);
        //context.Request["CigaretteCode"].ToString();
        //DataTable dt = getData(pageSize, filter);
        BLL.BLLBase bll = new BLL.BLLBase();

        int totalCount = 0;
        int pageCount;
        DataTable dt = bll.GetDataPage(formID, page, pageSize, out totalCount,out pageCount, new DataParameter[] { new DataParameter("{0}", filter) });

        string json = JsonHelper.Dtb2Json(dt);
        json = "{\"total\":\"" + totalCount + "\",\"items\":" + json + "}";
        context.Response.Write(json);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private DataTable getData(int pageSize, string filter)
    {
        SqlConnection cn = new SqlConnection("initial catalog=(local);database=SortSupplyDB;PASSWORD=a@1;USER ID=sa");
        string sql = string.Format("select top {0} * from AS_BI_CIGARETTE where {1}", pageSize,filter);
        SqlDataAdapter sda = new SqlDataAdapter(sql, cn);
        DataSet ds = new DataSet();
        sda.Fill(ds);

        return ds.Tables[0];
    }
    private string getFilter(HttpContext context)
    {
        string formID = context.Request["formID"].ToString();
        string filter = "1=1 ";
        if (formID == "Cigarette")
        {
            if (context.Request["CigaretteCode"] != null)
            {
                if (context.Request["CigaretteCode"].ToString().Trim().Length > 0)
                    filter += string.Format(" and CigaretteCode like '%{0}%'", context.Request["CigaretteCode"].ToString());
            }
            if (context.Request["CigaretteName"] != null)
            {
                if (context.Request["CigaretteName"].ToString().Trim().Length > 0)
                    filter += string.Format(" and CigaretteName like '%{0}%'", context.Request["CigaretteName"].ToString());
            }
            if (context.Request["Province"] != null)
            {
                if (context.Request["Province"].ToString().Trim().Length > 0 && context.Request["Province"].ToString().IndexOf("请选择") <= 0)
                    filter += string.Format(" and Province like '%{0}%'", context.Request["Province"].ToString());
            }
            if (context.Request["Barcode"] != null)
            {
                if (context.Request["Barcode"].ToString().Trim().Length > 0)
                    filter += string.Format(" and BARCODE like '%{0}%'", context.Request["Barcode"].ToString());
            }
            if (context.Request["PurchasePrice"] != null)
            {
                if (context.Request["PurchasePrice"].ToString().Trim().Length > 0)
                    filter += string.Format(" and PurchasePrice ={0}", context.Request["PurchasePrice"].ToString());
            }
        }

        return filter;
    }
}