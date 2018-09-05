using Tools;
using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WcfService.Sys
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseOpertion
    {
        public User() : base(1) { }

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetUserInfo(dynamic param)
        {
            if (!param.Exists("number"))
                return new RdfMsg(false, "参数number不存在!");
            string number = param.number;
            if (string.IsNullOrWhiteSpace(number))
                return new RdfMsg(false, "手机号码不能为空!");
            if (!Tools.RdfRegex.MobilePhone(number))
                return new RdfMsg(false, "手机号码格式错误!");
            Sys_User user = new Sys_User();
            if (!user.GetEntity<Sys_User>(item => item.Phone == number))
                return new RdfMsg(false, "手机号码无效!");
            Sys_Data sd = new RdfSqlQuery<Sys_Data>().Where(t1 => t1.UserId == user.Id).OrderByDesc(t1 => t1.CreateTime).Take(1).ToEntity();
            if (sd != null)
                user.TotalKM = sd.TotalKM;
            return new RdfMsg(true, user.ToJson());
        }
        public RdfMsg GetUserInfoByLogin(dynamic param)
        {
            Sys_User user = new Sys_User();
            if (param.Exists("userId"))
            {
                user.Id = Convert.ToInt32(param.userId.ToString());
            }
            else
            {
                user.Id = UserInfo.Id;
            }
            if (!user.GetEntity())
                return new RdfMsg(false, "用户不存在!");
            return new RdfMsg(true, user.ToJson(), DateTime.Now.Year - user.Birthday.Year);
        }
        /// <summary>
        /// 根据Id取用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg GetUserInfoById(dynamic param)
        {
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            Sys_User user = new Sys_User() { Id = param.Id };
            if (!user.GetEntity())
                return new RdfMsg(false, "用户不存在!");
            if (user.UId != "admin")
            {
                List<Sys_UserAuth> auths = new RdfSqlQuery<Sys_UserAuth>()
                         .JoinTable<Sys_Menu>((t1, t2) => t1.MenuId == t2.Id)
                         .Select<Sys_Menu>((t1, t2) => new
                         {
                             UserId = t1.UserId,
                             MenuId = t1.MenuId,
                             MenuName = t2.MenuName
                         })
                         .Where(t1 => t1.UserId == user.Id)
                         .ToList();
                user.Menus = string.Join(",", auths.ConvertAll(item => item.MenuName));
            }
            return new RdfMsg(true, user.ToJson(), DateTime.Now.Year - user.Birthday.Year);
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg AddUserInfo(dynamic param)
        {
            Sys_User user = new Sys_User();
            if (param.Exists("Phone"))
            {
                if (string.IsNullOrWhiteSpace(param.Phone))
                    return new RdfMsg(false, "手机号码不能为空!");
                if (!RdfRegex.MobilePhone(param.Phone))
                    return new RdfMsg(false, "手机号码格式错误!");
            }
            if (param.Exists("Birthday") && !RdfRegex.Date(param.Birthday))
                return new RdfMsg(false, "生日格式错误!");
            user.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "Url" && param.Exists(field.ColumnAs))
                {
                    object value = param[field.ColumnAs];
                    user.SetValue(value, field);
                }
            });
            if (string.IsNullOrWhiteSpace(user.UserPwd))
                return new RdfMsg(false, "密码不能为空!");
            if (string.IsNullOrWhiteSpace(user.UId))
                return new RdfMsg(false, "账号不能为空!");
            user.UserPwd = RdfConvert.Md5_32(user.UserPwd);
            user.LoginTime = DateTime.Now;
            user.RegisterTime = DateTime.Now;
            user.Enabled = true;
            user.Url = "Images\\default.png";
            user.UserExplain = "";
            RdfMsg msg = user.Insert(true);
            if (msg.Success)
            {
                UserAuth(param, user);
                if (param.Exists("UrlUpLoad"))
                {
                    user.Url = "UpLoadFile\\AppHeadImage\\" + user.Id.ToString() + ".jpg";
                    user.Edit();
                }
                msg.Value = DateTime.Now.Year - user.Birthday.Year;
                msg.Result = user.ToJson();
            }
            return msg;
        }
        public void UserAuth(dynamic param, Sys_User user)
        {
            if (user.Type != 0)
                return;
            if (!param.Exists("MenuIds"))
                return;
            string menuIds = param.MenuIds;
            if (string.IsNullOrWhiteSpace(menuIds))
                return;
            List<string> list = menuIds.Split(',').ToList();
            List<Sys_Menu> menulist = new RdfSqlQuery<Sys_Menu>().ToList();
            StringBuilder sb = new StringBuilder();
            list.ForEach(id =>
            {
                if (RdfRegex.Int(id))
                {
                    Sys_Menu sm = menulist.FirstOrDefault(menu => menu.Id == Convert.ToInt32(id));
                    if (sm != null)
                    {
                        if (user.Menus == null)
                            user.Menus = "";
                        if (user.Menus != "")
                            user.Menus += ",";
                        user.Menus += sm.MenuName;
                        sb.AppendLine(new Sys_UserAuth { UserId = user.Id, MenuId = sm.Id }.GetInsertSql());
                    }
                }
            });
            if (!string.IsNullOrWhiteSpace(sb.ToString()))
            {
                RdfExecuteSql.ExecuteNonQuery("delete Sys_UserAuth where UserId=" + user.Id + ";" + sb.ToString(), false);
                RdfCache.RemoveCache("Sys_UserAuth");
                RdfCache.RemoveCache("Sys_Menu");
            }
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg EditUserInfo(dynamic param)
        {
            Sys_User user = new Sys_User();
            if (!param.Exists("Id"))
                return new RdfMsg(false, "参数Id不存在!");
            user.Id = Convert.ToInt32(param.Id);
            if (!user.GetEntity())
                return new RdfMsg(false, "获取用户信息失败!");

            //int isEnglish = 0;
            //if (param.Exists("isEnglish"))
            //{
            //    var temp = Convert.ToInt32(param.isEnglish);
            //    isEnglish = temp == 1 ? 1 : 0;
            //}

            user.Cfg.FieldList.ForEach(field =>
            {
                if (field.ColumnAs != "Url" && field.ColumnAs != "Id" && param.Exists(field.ColumnAs))
                {
                    if (field.ColumnAs == "UserPwd" && user.Type != 2 && param.UserPwd != user.UserPwd)
                    {
                        param.UserPwd = RdfConvert.Md5_32(param.UserPwd);
                    }
                    object value = param[field.ColumnAs];
                    user.SetValue(value, field);
                }
            });
            if (param.Exists("Phone"))
            {
                if (string.IsNullOrWhiteSpace(param.Phone))
                    return new RdfMsg(false, "手机号码不能为空!");
                if (!RdfRegex.MobilePhone(param.Phone))
                    return new RdfMsg(false, "手机号码格式错误!");
            }
            if (param.Exists("Birthday") && !RdfRegex.Date(param.Birthday))
                return new RdfMsg(false, "生日格式错误!");
            if (param.Exists("Url"))
            {
                string url = "UpLoadFile\\AppHeadImage\\" + user.Id.ToString() + ".jpg";
                string baseUrl = System.Configuration.ConfigurationManager.AppSettings["HeadUrl"];
                if (string.IsNullOrWhiteSpace(baseUrl))
                    return new RdfMsg(false, "HeadUrl配置错误!");
                byte[] bytes = Convert.FromBase64String(param.Url);
                RdfConvert.BytesToFile(bytes, baseUrl + url);

                user.Url = UrlSetTimeStamp(url);
            }
            else if (param.Exists("UrlUpLoad"))
            {
                user.Url = "UpLoadFile\\AppHeadImage\\" + user.Id.ToString() + ".jpg";
            }
            UserAuth(param, user);
            RdfMsg msg = user.Edit();
            if (OnlineUser._dic != null)
            {
                foreach (var item in OnlineUser._dic)
                {
                    foreach (var item2 in OnlineUser.GetUserList(item.Key))
                    {
                        if (item2.Id == user.Id)
                        {
                            var info = item2;
                            var userHere = DbOpertion.DBoperation.Sys_UserOper.Instance.GetById(item2.Id);
                            info.Name = userHere.UserName;
                            OnlineUser.AddOnlineUser(item.Key, info);
                            break;
                        }
                    }
                }
            }

            if (msg.Success)
            {
                msg.Value = DateTime.Now.Year - user.Birthday.Year;
                msg.Result = user.ToJson();
            }
            return msg;
        }

        public string UrlSetTimeStamp(string url)
        {
            var i = url.LastIndexOf('?');
            if (i < 0)
            {
                return url + "?" + GetTimeStamp();
            }
            else
            {
                return url.Substring(0, i) + "?" + GetTimeStamp();
            }
        }

        public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public RdfMsg UpdatePwd(dynamic param)
        {
            if (!param.Exists("pwd_new"))
                return new RdfMsg(false, "参数新密码不存在!");
            if (!param.Exists("pwd_old"))
                return new RdfMsg(false, "参数原密码不存在!");
            if (string.IsNullOrWhiteSpace(param.pwd_new))
                return new RdfMsg(false, "新密码不能为空!");
            if (string.IsNullOrWhiteSpace(param.pwd_old))
                return new RdfMsg(false, "原密码不能为空!");
            Sys_User user = new Sys_User() { Id = UserInfo.Id };
            if (!user.GetEntity())
                return new RdfMsg(false, "获取用户信息失败!");
            if (user.UserPwd != RdfConvert.Md5_32(param.pwd_old))
                return new RdfMsg(false, "原密码错误!");
            user.UserPwd = RdfConvert.Md5_32(param.pwd_new);
            return user.Edit();
        }
        public RdfMsg LoadUser(dynamic param)
        {
            if (!param.Exists("type"))
                return new RdfMsg(false, "参数type不存在!");
            if (!param.Exists("pageSize"))
                return new RdfMsg(false, "参数pageSize不存在!");
            if (!param.Exists("pageIndex"))
                return new RdfMsg(false, "参数pageIndex不存在!");
            int type = Convert.ToInt32(param.type);
            int size = Convert.ToInt32(param.pageSize);
            int index = Convert.ToInt32(param.pageIndex);
            var query = new RdfSqlQuery<Sys_User>().Where(t1 => t1.Type == type);
            if (param.Exists("search"))
            {
                string search = param.search;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (search == "男")
                    {
                        query = query.Where(t1 => t1.Sex == true);
                    }
                    else if (search == "女")
                    {
                        query = query.Where(t1 => t1.Sex == false);
                    }
                    else
                    {
                        query = query.Where(t1 => t1.UId.Contains(search) || t1.UserName.Contains(search) || t1.Phone.Contains(search) || t1.UserExplain.Contains(search));
                    }
                }
            }
            int sum = (int)query.Count(t1 => new { cnt = t1.Id }).ToObject();
            int pageCount = 1;
            if (sum % size == 0)
                pageCount = sum / size;
            else
                pageCount = (sum / size) + 1;
            List<Sys_User> list = query.Select("*", true).OrderBy(t1 => t1.Id).Take(size).PageIndex(index).ToList();
            List<int> uIds = list.ConvertAll<int>(item => item.Id);
            if (type == 0)
            {
                List<Sys_UserAuth> auths = new RdfSqlQuery<Sys_UserAuth>()
                    .JoinTable<Sys_Menu>((t1, t2) => t1.MenuId == t2.Id)
                    .Select<Sys_Menu>((t1, t2) => new
                    {
                        UserId = t1.UserId,
                        MenuId = t1.MenuId,
                        MenuName = t2.MenuName
                    })
                    .Where(t1 => uIds.Contains(t1.UserId))
                    .ToList();
                list.ForEach(user =>
                {
                    user.Menus = string.Join(",", auths.FindAll(auth => auth.UserId == user.Id).ConvertAll(item => item.MenuName));
                });
            }
            return new RdfMsg(true, RdfSerializer.ObjToJson(list), pageCount);
        }
    }
}
