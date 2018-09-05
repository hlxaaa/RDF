using Common.Helper;
using DbOpertion.DBoperation;
using DbOpertion.Models.Extend;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WcfService.Oper.Function;
using WcfService.Sys;

namespace WcfService.Controllers
{
    public class TestController : ApiBaseController
    {
        string baseUrl = ConfigurationManager.AppSettings["HeadUrl"];

        [HttpPost]
        public object DeleteSportById(TestReq req)
        {
            var count = 0;
            var id = req.id;
            var ss = Sys_SportOper.Instance.GetById(id);
            var imgs = StringHelper.Instance.GetOldImgNames(ss.Content);
            foreach (var item in imgs)
            {
                var temp = baseUrl + "UpLoadFile\\SportInfoImg\\" + item;
                if (File.Exists(temp))
                {
                    File.Delete(temp);
                    count++;
                }
            }
            imgs = StringHelper.Instance.GetOldImgNames(ss.ContentE);
            foreach (var item in imgs)
            {
                var temp = baseUrl + "UpLoadFile\\SportInfoImg\\" + item;
                if (File.Exists(temp))
                {
                    File.Delete(temp);
                    count++;
                }
            }
            var t = baseUrl + ss.TitleUrl;
            if (File.Exists(t))
            {
                File.Delete(t);
                count++;
            }
            Sys_SportOper.Instance.Delete1(id);
            return count;
        }

        [HttpGet]
        public object GetGymData()
        {
            return CacheHelper.Instance.GetGymData();
        }

        [HttpGet]
        public object UpdateSportTitleUrl()
        {
            var list = SqlHelper.Instance.GetByCondition<DbOpertion.Models.Sys_Sport>(" id>=106 and id<=149 ");
            list = list.OrderBy(p => p.Id).ToList();
            var targets = list.Select(p => p.TitleUrl).ToList();


            list = SqlHelper.Instance.GetByCondition<DbOpertion.Models.Sys_Sport>(" id>=57 and id<=100 ");
            list = list.OrderBy(p => p.Id).ToList();
            var sources = list.Select(p => p.TitleUrl).ToList();

            for (int i = 0; i < sources.Count; i++)
            {
                File.Delete(baseUrl + targets[i]);
                File.Copy(baseUrl + sources[i], baseUrl + targets[i]);
            }

            //Sys_SportOper.Instance.UpdateList(list);

            return 1;
        }

        public string rp3(string str)
        {
            return str.Replace(".png", "E.png").Replace(".jpg", "E.jpg");
        }

        public string rp2(string str)
        {
            var np = @"/UpLoadFile/SportInfoImg/";
            return str.Replace(@"/ueditor/net/upload/image/", np);
        }

        public string Rp(string str)
        {
            return str.Replace("20180111" + "/", "").Replace("20180112" + "/", "").Replace("20180114" + "/", "").Replace("20180115" + "/", "").Replace("20180116" + "/", "").Replace("20180123" + "/", "").Replace("20180125" + "/", "").Replace("20180126" + "/", "").Replace("20180129" + "/", "").Replace("20180130" + "/", "").Replace("20180131" + "/", "").Replace("20180201" + "/", "").Replace("20180202" + "/", "");
        }

        [HttpPost]
        public object GetViewOwn()
        {
            return PayRecordOper.Instance.GetMoneyDetailListTest();
        }

        [HttpPost]
        public void SetActiveUser(TestReq req)
        {
            CacheHelper.Instance.SetActiveUser(req.id);
        }

        [HttpPost]
        public object GetActiveUser()
        {
            return CacheHelper.Instance.GetActiveUser();
        }

        [HttpGet]
        public object Testtt()
        {
            var str = @"http://39.107.231.159:8093//openapi/v2/data/batchdevdata?token=45255B6AB3C2117A7A1AE350F40D45F0B7AFFA05E557D5EE40930EE9AACF270D016C8643188F81C805064E132D628B18ACB92AB4889ADEA5F5B54EFE70543421FC4C5BFEB6173B9A7E625B1DA22637BCB67A7BFE59356C223488D04802DF24C3&deveuis=004a770442000000_004a770442000001_004a770442000002_004a770442000003_004a770442000004_004a770442000005_004a770442000006_004a770442000007_004a770442000008_004a770442000009&begintime=2018-08-27 00:00:00&endtime=2018-08-27 23:59:59&pageindex=1";
            return HttpHelper.Instance.HttpGet(str);
        }

        [HttpPost]
        public void CompleteUserAuth(TestReq req)
        {
            var str = "select DISTINCT id from Sys_User where type=0 and isEnglish!=1 ";
            var list = SqlHelper.Instance.ExecuteGetDt<Sys_User>(str, new Dictionary<string, string>());
            //var r = new List<Sys_UserAuth>();
            var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 99 };
            foreach (var item in list)
            {
                foreach (var item2 in arr)
                {
                    var temp = new DbOpertion.Models.Auth { userId = item.Id, menuId = item2 };
                    AuthOper.Instance.Add(temp);
                }
            }
        }

        [HttpPost]
        public object DeleteVideoTest(TestReq req)
        {
            var id = req.id;
            var pv = Sys_PlayVideoOper.Instance.GetById(id);
            Sys_PlayVideoOper.Instance.DeleteVideoById(id);
            AllFunc.Instance.DeleteVideo(pv.VieldId);
            Sys_VideoInfoOper.Instance.RemoveVideoIdAfterDelVideo(id);
            return null;
        }

        [HttpGet]
        public void CompletedImg()
        {
            var users = Sys_UserOper.Instance.GetAllList();
            var temp = users.Select(p => p.Url).ToList();
            temp.AddRange(users.Select(p => p.coachImg).ToList());
            temp.AddRange(users.Select(p => p.idCardFront).ToList());
            temp.AddRange(users.Select(p => p.idCardBack).ToList());
            var lives = Sys_VideoInfoOper.Instance.GetAllList();
            temp.AddRange(lives.Select(p => p.Url).ToList());
            var temps = Sys_SportOper.Instance.GetAllList();
            temp.AddRange(temps.Select(p => p.TitleUrl).ToList());
            var videos = Sys_PlayVideoOper.Instance.GetAllList();
            temp.AddRange(videos.Select(p => p.TitleUrl).ToList());
            var ats = Sys_AudioTypeOper.Instance.GetAllList();
            temp.AddRange(ats.Select(p => p.TitleUrl).ToList());
            temp = temp.Distinct().ToList();
            temp = GetUploadFileUrl(temp);


            var copySource = baseUrl + "UpLoadFile\\AudioTypeImage\\1.jpg";
            foreach (var item in temp)
            {
                if (!File.Exists(baseUrl + item))
                {
                    File.Copy(copySource, baseUrl + item);
                }
            }

        }

        public List<string> GetUploadFileUrl(List<string> strs)
        {
            var r = new List<string>();
            foreach (var item in strs)
            {
                if (item != null)
                {
                    if (item.Contains("UpLoadFile"))
                    {
                        r.Add(RemoveTimestamp(item));
                    }
                }
            }
            return r;
        }

        public string RemoveTimestamp(string url)
        {
            var i = url.LastIndexOf('?');
            if (i < 0)
                return url;
            return url.Substring(0, i);
        }


        [HttpGet]
        public object Get()
        {
            return 11;
        }

        public object TestPost()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.20.218:8045/DataService.svc/LoadPlayVideo&cls=Sys.PlayVideo&method=LoadPlayVideo");
            request.ContentType = "application/json";
            request.Method = "POST";
            byte[] data = Encoding.GetEncoding("UTF-8").GetBytes("{\"toKen\":\"18857120152\"}");
            request.ContentLength = data.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = sr.ReadToEnd().Trim();
            return result;
            sr.Close();
            response.Close();
            request.Abort();
        }

        //[HttpPost]
        //public object Post(TestReq req)
        //{
        //    return req.str;
        //}

        //[HttpGet]
        //public object GetDict()
        //{
        //    return DataService.UserDic;
        //}

        //public void Set()
        //{
        //    Dictionary<string, Sys_User> dict = new Dictionary<string, Sys_User>();
        //    var user = new Sys_User();
        //    user.Id = 999;
        //    dict.Add("12321", user);
        //    CacheHelper.Instance.SetUserToken(dict);
        //}

        //public object Get2()
        //{
        //    return CacheHelper.Instance.GetUserToken();
        //}

        [HttpGet]
        public object GetToken()
        {
            return CacheHelper.Instance.GetUserToken();
        }
        [HttpPost]
        public void ClearCache(TestReq req)
        {
            CacheHelper.Instance.ClearCache(req.str);
        }

        public void CreatePayRecord()
        {

        }

        //[HttpGet]
        //public void SetToken()
        //{
        //    var token = "111";
        //    var user = new Sys_User();
        //    user.Id = 110;
        //    var dict = new Dictionary<string, Sys_User>();
        //    dict.Add(token, user);
        //    CacheHelper.Instance.SetUserToken(dict);
        //}
    }
}
