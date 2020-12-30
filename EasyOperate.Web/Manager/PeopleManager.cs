using DotNetty.Codecs.Http;
using EasyOperate.Common;
using EasyOperate.Web.Handles;
using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;
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
        private EquipmentHandler equipmentHandler = null;

        public PeopleManager(AccessControlEquipmentModel equipmentModel)
        {
            equipmentHandler = new EquipmentHandler(equipmentModel.Serialno);
        }

        public void AddPeopleInfo(BaseUserModel baseUserModel, UserPhotoModel userPhotoModel)
        {
            PersonInfo personInfo = new PersonInfo();
            personInfo.PersonID = (ulong)baseUserModel.ID;
            personInfo.LastChange = (ulong)CommonFunctions.ConvertDateTimeToInt10(baseUserModel.UpdateTime.Value);
            personInfo.PersonCode = baseUserModel.ID.ToString();
            personInfo.PersonName = baseUserModel.RealName;
            personInfo.Remarks = string.Empty;
            personInfo.TimeTemplateNum = 0;
            personInfo.TimeTemplateList = null;
            personInfo.IdentificationNum = 0;
            personInfo.IdentificationList = null;
            personInfo.ImageNum = 1;
            personInfo.ImageList = new List<FaceImage>();

            FaceImage faceImage = new FaceImage();
            faceImage.FaceID = Convert.ToUInt64(userPhotoModel.BaseUserId);
            faceImage.Name = userPhotoModel.Path;

            Image image = Image.FromFile(userPhotoModel.Path);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                
                faceImage.Data = Convert.ToBase64String(imageBytes);
                faceImage.Size = Convert.ToUInt64(faceImage.Data.Length);
            }

            personInfo.ImageList.Add(faceImage);

            PersonRequestModel personRequestModel = new PersonRequestModel();
            personRequestModel.AddPersonInfo(personInfo);

            string url = BasicRequestUrl.GetPeopleInfoProcessingUrl(4);
            string json = JsonConvert.SerializeObject(personRequestModel);

            PersonResponseData personResponseData = equipmentHandler.Send<PersonResponseData>(url, json, HttpMethod.Post);
        }

        //private void AddPeopleInfo(List<PersonInfo> personInfos)
        //{
        //    PersonRequestModel personRequestModel = new PersonRequestModel();

        //    personInfos.ForEach(persionInfo => {
        //        personRequestModel.AddPersonInfo(persionInfo);
        //    });

        //    IFullHttpRequest request = RequestDeviceManager.CreateRequestDevice(BasicRequestUrl.GetPeopleInfoProcessingUrl(4), JsonConvert.SerializeObject(personRequestModel), HttpMethod.Post);

        //    BasicResponse<PersonResponseData> result = HttpKeepAliveManager.SendRequest<PersonResponseData>("210235C3R0320B001510", request);
        //}
    }
}