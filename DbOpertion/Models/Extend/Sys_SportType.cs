using System;
using static DbOpertion.Models.Extend.AllModel;

namespace DbOpertion.Models
{

    public partial class Sys_SportType
    {

        public Sys_SportType(AddSportTypeReq req)
        {
            //if (req.Id != null)
            //    Id = Convert.ToInt32(req.Id);
            if (req.Name != null)
                Name = req.Name;
            if (req.Remark != null)
                Remark = req.Remark;
            NameE = req.NameE;
            RemarkE = req.RemarkE;
            //if (req.Enabled != null)
            //    Enabled = Convert.ToBoolean(req.Enabled);
        }

        public Sys_SportType(UpdateSportTypeReq req)
        {
            if (req.Id != null)
                Id = Convert.ToInt32(req.Id);
            if (req.Name != null)
                Name = req.Name;
            if (req.Remark != null)
                Remark = req.Remark;
            NameE = req.NameE;
            RemarkE = req.RemarkE;
            //if (req.Enabled != null)
            //    Enabled = Convert.ToBoolean(req.Enabled);
        }


    }
}
