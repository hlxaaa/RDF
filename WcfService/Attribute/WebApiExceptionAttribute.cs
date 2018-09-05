using Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WcfService.Attribute
{
    public class WebApiExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var r = new ResultJson();

            if (actionExecutedContext.Exception.Data["code"] != null)
                r.HttpCode = 400;
            else
                r.HttpCode = 500;
            var test = actionExecutedContext.Exception.Message;
            r.Message = test;
            //if (isServer == "0")
            //    r.message = test;
            //else
            //    r.message = "网络不稳定";
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, r);
            base.OnException(actionExecutedContext);
        }
    }
}