using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;

namespace WcfService.Sys
{
    public class SendSMS
    {
        public static string Key { get; set; }
        public static string Secret { get; set; }
        public static string Sign { get; set; }
        public static string Temp { get; set; }
        /// <summary>
        /// 发生短信验证码
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public RdfMsg SendCode(string number, string code)
        {
            if (string.IsNullOrWhiteSpace(Key))
            {
                string live = System.Configuration.ConfigurationManager.AppSettings["AliParam"];
                if (string.IsNullOrWhiteSpace(live))
                    throw new Exception("未配置阿里云参数!");
                string[] array = live.Split(',');
                if (array.Length != 4)
                    throw new Exception("阿里云参数配置错误!");
                Key = array[0];
                Secret = array[1];
                Sign = array[2];
                Temp = array[3];
            }
            return Send(number, Sign, Temp, "{\"code\":\"" + code + "\"}");
        }
        /// <summary>
        /// 阿里云发送短信
        /// </summary>
        /// <param name="number">手机号码</param>
        /// <param name="signName">签名</param>
        /// <param name="tempCode">模板</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        private RdfMsg Send(string number, string signName, string tempCode, string param)
        {
            try
            {
                IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", Key, Secret);
                DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", "Dysmsapi", "dysmsapi.aliyuncs.com");
                IAcsClient acsClient = new DefaultAcsClient(profile);
                SendSmsRequest request = new SendSmsRequest();
                request.PhoneNumbers = number;
                request.SignName = signName;
                request.TemplateCode = tempCode;
                request.TemplateParam = param;
                request.OutId = "";//回调参数
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                return GetMessage(sendSmsResponse);
            }
            catch (Exception e)
            {
                return new RdfMsg(false, e.Message);
            }
        }
        private RdfMsg GetMessage(SendSmsResponse response)
        {
            RdfMsg msg = new RdfMsg(false);
            if (response.Message == "OK")
            {
                msg.Success = true;
                return msg;
            }
            switch (response.Code)
            {
                case "isv.OUT_OF_SERVICE":
                    msg.Error = "业务停机";
                    break;
                case "isv.PRODUCT_UN_SUBSCRIPT":
                    msg.Error = "未开通云通信产品的阿里云客户";
                    break;
                case "isv.PRODUCT_UNSUBSCRIBE":
                    msg.Error = "产品未开通";
                    break;
                case "isv.ACCOUNT_NOT_EXISTS":
                    msg.Error = "账户不存在";
                    break;
                case "isv.ACCOUNT_ABNORMAL":
                    msg.Error = "账户异常";
                    break;
                case "isv.SMS_TEMPLATE_ILLEGAL":
                    msg.Error = "短信模板不合法";
                    break;
                case "isv.SMS_SIGNATURE_ILLEGAL":
                    msg.Error = "短信签名不合法";
                    break;
                case "isv.INVALID_PARAMETERS":
                    msg.Error = "参数异常";
                    break;
                case "isv.MOBILE_NUMBER_ILLEGAL":
                    msg.Error = "非法手机号";
                    break;
                case "isv.MOBILE_COUNT_OVER_LIMIT":
                    msg.Error = "手机号码数量超过限制";
                    break;
                case "isv.TEMPLATE_MISSING_PARAMETERS":
                    msg.Error = "模板缺少变量";
                    break;
                case "isv.BUSINESS_LIMIT_CONTROL":
                    msg.Error = "业务限流";
                    break;
                case "isv.INVALID_JSON_PARAM":
                    msg.Error = "JSON参数不合法，只接受字符串值";
                    break;
                case "isv.BLACK_KEY_CONTROL_LIMIT":
                    msg.Error = "黑名单管控";
                    break;
                case "isv.PARAM_LENGTH_LIMIT":
                    msg.Error = "参数超出长度限制";
                    break;
                case "isv.PARAM_NOT_SUPPORT_URL":
                    msg.Error = "不支持URL";
                    break;
                case "isv.AMOUNT_NOT_ENOUGH":
                    msg.Error = "账户余额不足";
                    break;
                case "isv.TEMPLATE_PARAMS_ILLEGAL":
                    msg.Error = "模板变量里包含非法关键字";
                    break;
                default:
                    msg.Error = "错误编号:" + response.Code + ",错误消息:" + response.Message;
                    break;
            }
            return msg;
        }
    }
}