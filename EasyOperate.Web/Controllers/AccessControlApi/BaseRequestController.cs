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
    public class BaseRequestController : ApiController
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
        public static string Get(string url)
        {

            string result = "";
            Stream stream=null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                //添加参数
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
            
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch
            {
                Com.log.Error("向设备发送数据时发生异常，设备可能不在线");
                return null;
            }
            finally
            {
                if(stream!=null)
                stream.Close();
            }  
            
        }
        ////设置请求接口
        //var request = (HttpWebRequest)WebRequest.Create("http://xxx.com/xxx");
        ////请求参数
        //var postData = string.Format("appId={0}&appScreat={1}&channel={2}", appId, appScreat, channel);
        //var data = Encoding.ASCII.GetBytes(postData);
        ////请求方式
        //request.Method = "POST";
        ////请求头参数设置
        //request.Headers.Add("sign", sign);
        //request.Headers.Add("timestamp", timestamp);
        //request.Headers.Add("token", token);
        //request.ContentType = "application/x-www-form-urlencoded";
        //request.ContentLength = data.Length;

        //using (var stream = request.GetRequestStream())
        //{
        //stream.Write(data, 0, data.Length);
        //}
        ////结果返回
        //var response = (HttpWebResponse)request.GetResponse();
        ////转字符串
        //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        ////转换为json对象
        //MXCZUserInfoResponse userInfoResponse = JsonConvert.DeserializeObject<MXCZUserInfoResponse>(responseString);
        //}
    }
}
