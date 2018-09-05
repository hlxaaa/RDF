using System;
using static DbOpertion.Models.Extend.AllModel;

namespace DbOpertion.Models
{

    public partial class DragModel
    {
        public DragModel(int id)
        {
            this.id = id;
        }

        public DragModel(AddModelReq req, string str)
        {
            //if (req.id != null)
            //    id = Convert.ToInt32(req.id);
            if (req.modelType != null)
                modelType = Convert.ToInt32(req.modelType);

            if (req.modelName != null)
                modelName = req.modelName;
            content = str;
            //if (req.createTime != null)
            //    createTime = Convert.ToDateTime(req.createTime);
            //if (req.isDeleted != null)
            //    isDeleted = Convert.ToBoolean(req.isDeleted);
        }


        public DragModel(UpdateModelReq req, string str)
        {
            if (req.id != null)
                id = Convert.ToInt32(req.id);
            if (req.modelType != null)
                modelType = Convert.ToInt32(req.modelType);
            //if (req.m != null)
            //    m = Convert.ToInt32(req.m);
            //if (req.n != null)
            //    n = Convert.ToInt32(req.n);
            content = str;
            if (req.modelName != null)
                modelName = req.modelName;
            //if (req.createTime != null)
            //    createTime = Convert.ToDateTime(req.createTime);
            //if (req.isDeleted != null)
            //    isDeleted = Convert.ToBoolean(req.isDeleted);
        }


        //public DragModel(Req req){
        //if(req.id != null)
        //id = Convert.ToInt32(req.id);
        //if(req.modelType != null)
        //modelType = Convert.ToInt32(req.modelType);
        //if(req.m != null)
        //m = Convert.ToInt32(req.m);
        //if(req.n != null)
        //n = Convert.ToInt32(req.n);
        //if(req.modelName != null)
        //modelName = req.modelName;
        //if(req.createTime != null)
        //createTime = Convert.ToDateTime(req.createTime);
        //if(req.isDeleted != null)
        //isDeleted = Convert.ToBoolean(req.isDeleted);
        //}


    }
}
