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
        public void AddPeopleInfo()
        {
            PersonInfo personInfo = new PersonInfo();
            personInfo.PersonID = 1;
            personInfo.LastChange = 1564022548;
            personInfo.PersonCode = "1001";
            personInfo.PersonName = "uniview";
            personInfo.Remarks = "remarks";
            personInfo.TimeTemplateNum = 0;
            personInfo.TimeTemplateList = null;
            personInfo.IdentificationNum = 0;
            personInfo.IdentificationList = null;
            personInfo.ImageNum = 1;
            personInfo.ImageList = new List<FaceImage>();

            FaceImage faceImage = new FaceImage();
            faceImage.FaceID = 1;
            faceImage.Name = "20201217210022.jpg";

            Image image = Image.FromFile(@"E:\我的项目\ACS门禁\images\20201217210022.jpg");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();

                faceImage.Size = Convert.ToUInt64(imageBytes.Length);
                faceImage.Data = Convert.ToBase64String(imageBytes);
                faceImage.Size = Convert.ToUInt64(faceImage.Data.Length);
            }

            personInfo.ImageList.Add(faceImage);

            PersonRequestModel personRequestModel = new PersonRequestModel();
            personRequestModel.AddPersonInfo(personInfo);

            string url = BasicRequestUrl.GetPeopleInfoProcessingUrl(4);
            string json = JsonConvert.SerializeObject(personRequestModel);

            IFullHttpRequest request = RequestDeviceManager.CreateRequestDevice(url, json, HttpMethod.Post);

            BasicResponse<PersonResponseData> result = HttpKeepAliveManager.SendRequest<PersonResponseData>("210235C3R0320B001510", request);
        }
    }
}