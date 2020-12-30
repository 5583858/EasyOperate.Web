using DotNetty.Codecs.Http;
using EasyOperate.Web.Manager;
using EasyOperate.Web.Models.AccessControl;
using EasyOperate.Web.Models.AccessControlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.Handles
{
    public class EquipmentHandler
    {
        private string equipmentSerialNo = string.Empty;

        public EquipmentHandler(string serialNo)
        {
            equipmentSerialNo = serialNo;
        }

        public T Send<T>(string url, string json, HttpMethod httpMethod)
        {
            BasicResponse<T> response = null;

            IFullHttpRequest request = RequestDeviceManager.CreateRequestDevice(url, json, httpMethod);

            if (request != null)
            {
                response = HttpKeepAliveManager.SendRequest<T>(equipmentSerialNo, request);
            }

            return response.Response.Data;
        }
    }
}