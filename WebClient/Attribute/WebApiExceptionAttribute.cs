using Common.Helper;
using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ServerEnd.Attribute
{
    public class WebApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var r = new ResultJson();
            r.HttpCode = 500;
            var test = actionExecutedContext.Exception.Message;
            r.Message = test;
            //if (isServer == "0")
            //    r.message = test;
            //else
            //    r.message = "网络不稳定";
            LogHelper.WriteLog(GetType(), test);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, r);
            base.OnException(actionExecutedContext);
        }
    }
}