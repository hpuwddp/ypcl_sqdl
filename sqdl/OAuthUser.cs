using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqdl
{
    public class OAuthUser
    {
        #region 数据库字段
        public string openid{get;set;}
        public string nickname { get; set; }
        public string sex { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        /// <summary>
        /// 图像路径地址
        /// </summary>
        public string headimgurl { get; set; }
         
        #endregion
         
    }
}
