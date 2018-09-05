using System.Text;
using Common.Helper;
using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using DbOpertion.Models;
using System.Data.SqlClient;
using static DbOpertion.Models.Extend.AllModel;
using Common.Extend;

namespace DbOpertion.DBoperation
{
    public partial class Sys_PlayAudioOper : SingleTon<Sys_PlayAudioOper>
    {
        public AudioView GetViewById(int id)
        {
            return SqlHelper.Instance.GetById<AudioView>(id);
        }
    }
}
