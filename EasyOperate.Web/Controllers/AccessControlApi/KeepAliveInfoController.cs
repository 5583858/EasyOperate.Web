using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.IO;
using EasyOperate.Web.Controllers.AccessControlApi;
using EasyOperate.Web.Models.AccessControlRequest;

namespace EasyOperate.Web.Controllers.AccessControlApi
{
    public class KeepAliveInfoController : ApiController
    {
        //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        //req.Method = "GET";
        //req.Headers["Accept-Language"] = "zh-CN,zh;q=0.8";
        //req.Referer = "https://www.baidu.com/";
        //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        //Stream stream = resp.GetResponseStream();
        //string result = "";
        ////注意，此处使用的编码是：gb2312
        ////using (StreamReader reader = new StreamReader(stream, Encoding.Default))
        //using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("gb2312")))
        //{
        //result = reader.ReadToEnd();
        //}
    //public static string Get(string url)
    //    {
    //        string result = "";
    //        //StringBuilder builder = new StringBuilder();
    //        //builder.Append(url);
    //        //if (dic.Count > 0)
    //        //{
    //        //    builder.Append("?");
    //        //    int i = 0;
    //        //    foreach (var item in dic)
    //        //    {
    //        //        if (i > 0)
    //        //            builder.Append("&");
    //        //        builder.AppendFormat("{0}={1}", item.Key, item.Value);
    //        //        i++;
    //        //    }
    //        //}
            
    //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
    //        //添加参数
    //        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
    //        Stream stream = resp.GetResponseStream();
    //        try
    //        {
    //            //获取内容
    //            using (StreamReader reader = new StreamReader(stream))
    //            {
    //                result = reader.ReadToEnd();
    //            }
    //        }
    //        finally
    //        {
    //            stream.Close();
    //        }
    //        return result;
    //    }
        
    }
}
