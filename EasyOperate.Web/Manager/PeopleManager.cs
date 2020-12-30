using DotNetty.Codecs.Http;
using EasyOperate.Web.Models.AccessControlModel;
using EasyOperate.Web.Models.AccessControlRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Manager
{
    public class PeopleManager
    {
        public void GetBasicInfo()
        {
            ////string url = $"http://192.168.1.13{BasicRequestUrl.PeopleLibrariesoUrl}";

            //string url = "/LAPI/V1.0/System/DeviceBasicInfo";

            //IFullHttpRequest request = RequestDeviceManager.CreateRequestDevice(url, string.Empty, HttpMethod.Get);

            //HttpKeepAliveManager.SendRequest("210235C3R0320B001510", request);
        }

        public void AddPeopleInfo()
        {
            //PersonInfoList personInfo = new PersonInfoList();
            //personInfo.PersonID = 1;
            //personInfo.LastChange = 1564022548;
            //personInfo.PersonCode = "1001";
            //personInfo.PersonName = "uniview";
            //personInfo.Remarks = "remarks";
            //personInfo.TimeTemplateNum = 0;
            //personInfo.TimeTemplateList = null;
            //personInfo.IdentificationNum = 0;
            //personInfo.IdentificationList = null;
            //personInfo.ImageNum = 1;
            //personInfo.ImageList = new List<ImageList>();

            //ImageList imageList = new ImageList();
            //imageList.FaceID = 1;
            //imageList.Name = "20201217210022.jpg";

            //Image image = Image.FromFile(@"E:\我的项目\ACS门禁\images\20201217210022.jpg");
            ////ImageFormat format = image.RawFormat;
            ////using (MemoryStream ms = new MemoryStream())
            ////{
            ////    image.Save(ms, ImageFormat.Jpeg);
            ////    byte[] buffer = new byte[ms.Length];
            ////    ms.Seek(0, SeekOrigin.Begin);
            ////    ms.Read(buffer, 0, buffer.Length);

            ////    imageList.Size = buffer.Length;
            ////    imageList.Data = buffer.ToString();
            ////}

            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    image.Save(memoryStream, image.RawFormat);
            //    byte[] imageBytes = memoryStream.ToArray();

            //    imageList.Size = imageBytes.Length;
            //    imageList.Data = Convert.ToBase64String(imageBytes);
            //}

            //personInfo.ImageList.Add(imageList);

            //string url = BasicRequestUrl.GetPeopleInfoProcessingUrl(4);
            //string json = JsonConvert.SerializeObject(personInfo);

            //IFullHttpRequest request = RequestDeviceManager.CreateRequestDevice(url, json, HttpMethod.Post);

            //HttpKeepAliveManager.SendRequest("210235C3R0320B001510", request);
        }
    }
}