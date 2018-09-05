using Common.Attribute;
using Common.Result;
using DbOpertion.Models.Extend;
using System.Web.Http;

using WcfService.Oper.Function;

namespace WcfService.Controllers
{
    [ValidateModel]
    public class CoachController : ApiBaseController
    {
        [HttpPost]
        public ResultJson<GetVideoListRes> LoadPlayVideo(LoadPlayVideoReq req)
        {
            return AllFunc.Instance.LoadPlayVideo(req);
        }

        [HttpPost]
        public ResultJson<CheckLiveRes> CheckLive(IdReq req)
        {
            return AllFunc.Instance.CheckLive(req);
        }

        [HttpPost]
        public ResultJson<LiveFlag> CheckLiveTest(IdReq req)
        {
            return AllFunc.Instance.CheckLiveTest(req);
        }

        [HttpPost]
        public ResultJson PayLiveTest(PayLiveTestReq req)
        {
            return AllFunc.Instance.PayLiveTest(req);
        }

        [HttpPost]
        public ResultJson PayVideoTest(PayVideoTestReq req)
        {
            return AllFunc.Instance.PayVideoTest(req);
        }

        [HttpPost]
        public ResultJson<VideoFlag> CheckVideoTest(IdReq req)
        {
            return AllFunc.Instance.CheckVideoTest(req);
        }

        [HttpPost]
        public ResultJson<TakeCashRes> TakeCash(TakeCashReq req)
        {
            return AllFunc.Instance.TakeCash(req);
        }

        [HttpPost]
        public ResultJson<GetInComeListRes> GetInComeRecord(GetInComeRecordReq req)
        {
            return AllFunc.Instance.GetInComeRecord(req);
        }

        [HttpPost]
        public ResultJson CloseLive(IdReq req)
        {
            return AllFunc.Instance.CloseLive(req);
        }

        [HttpPost]
        public ResultJson DeleteVideoById(IdReq req)
        {
            return AllFunc.Instance.DeleteVideoById(req);
        }

        [HttpPost]
        public ResultJson<PlugFlowUrlRes> GetPlugFlowUrl(IdReq req)
        {
            return AllFunc.Instance.GetPlugFlowUrl(req);
        }

        //[HttpPost]
        //public void DeleteVideo(TestReq req)
        //{
        //    AllFunc.Instance.DeleteVideo(req.str);
        //}

        [HttpPost]
        public ResultJson<AppUploadAuthRes> GetAuth()
        {
            return AllFunc.Instance.GetAuth();
        }

        [HttpPost]
        public ResultJson<GetLiveListRes> GetLiveList(PageIndexReq req)
        {
            return AllFunc.Instance.GetLiveList(req);
        }

        [HttpPost]
        public ResultJson<GetVideoListRes> GetVideoList(PageIndexReq req)
        {
            return AllFunc.Instance.GetVideoList(req);
        }

        public ResultJson<AddCourseRes> AddCourse(AddCourseReq req)
        {
            return AllFunc.Instance.AddCourse(req);
        }

        public ResultJson ApplyToCoach(ApplyToCoachReq req)
        {
            return AllFunc.Instance.ApplyToCoach(req);
        }

        public ResultJson CreateLive(CreateLiveReq req)
        {
            return AllFunc.Instance.CreateLive(req);
        }
    }
}
