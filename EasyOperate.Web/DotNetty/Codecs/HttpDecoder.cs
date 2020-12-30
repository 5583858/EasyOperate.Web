using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyOperate.Web.DotNetty.Codecs
{
    public class HttpDecoder : HttpObjectDecoder
    {
        private bool isDecodingRequest = true;

        private readonly HttpResponseStatus UNKOWN_STATUS = new HttpResponseStatus(999, new AsciiString("Unkown"));

        protected override IHttpMessage CreateInvalidMessage()
        {
            if (isDecodingRequest)
            {
                return new DefaultFullHttpRequest(HttpVersion.Http10, HttpMethod.Get, "/bad-request", ValidateHeaders);
            }
            else
            {
                return new DefaultFullHttpResponse(HttpVersion.Http10, UNKOWN_STATUS, ValidateHeaders);
            }
        }

        protected override IHttpMessage CreateMessage(AsciiString[] initialLine)
        {
            if (initialLine[0].Contains(new AsciiString("HTTP")))
            {
                isDecodingRequest = false;
            }
            else if (initialLine[2].Contains(new AsciiString("HTTP")))
            {
                isDecodingRequest = true;
            }
            if (isDecodingRequest)
            {
                return new DefaultHttpRequest(new HttpVersion(initialLine[2].ToString(), true), new HttpMethod(initialLine[0].ToString()), initialLine[1].ToString(), ValidateHeaders);
            }
            else
            {
                return new DefaultHttpResponse(new HttpVersion(initialLine[0].ToString(), true), new HttpResponseStatus(Convert.ToInt32(initialLine[1].ToString()), initialLine[2]), ValidateHeaders);
            }
        }

        protected override bool IsDecodingRequest()
        {
            return isDecodingRequest;
        }
    }
}