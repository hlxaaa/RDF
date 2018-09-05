using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using Tools;

namespace WcfService
{
    [ServiceContract]
    public interface IDataService
    {
        /// <summary>
        /// 播放回调地址
        /// </summary>
        /// <param name="action"></param>
        /// <param name="ip"></param>
        /// <param name="id"></param>
        /// <param name="app"></param>
        /// <param name="appname"></param>
        /// <param name="time"></param>
        /// <param name="usrargs"></param>
        /// <param name="node"></param>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void PlayCallBack(string nId, string time, int status, string cid);
        /// <summary>
        /// 根据手机号获取头像
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg GetHeadUrl(string number);
        /// <summary>
        /// 账号+密码登录
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg AccountLogin(string uId, string pwd);
        /// <summary>
        /// 手机号码+验证码登录
        /// </summary>
        /// <param name="number"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg NumberLogin(string number, int code);
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg GetCode(string number);
        /// <summary>
        /// 检查UId是否存在，存在则登录
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg CheckUId(string uId);

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="number">手机号码</param>
        /// <param name="uId">QQ或微信唯一标识</param>
        /// <param name="name">昵称</param>
        /// <param name="url">头像地址</param>
        /// <param name="sex">性别，true男，false女</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg BindUser(int code, string number, string uId, string name, string url, bool sex);
        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="toKen">登录身份标识</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg LoginOut(string toKen);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg TestHisPost();

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="toKen"></param>
        /// <param name="cls"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg Get(string toKen, string cls, string method, string param);

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="toKen"></param>
        /// <param name="cls"></param>
        /// <param name="method"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RdfMsg Post(string toKen, string cls, string method, string param);
    }
}
