using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqdl
{
   public class QQUser
    {
        /// <summary>  
        /// 回调结果码（0成功，其他失败）  
        /// </summary>  
        public int ret { get; set; }
        public string msg { get; set; }
        public string is_lost { get; set; }
        /// <summary>  
        /// 用户昵称  
        /// </summary>  
        public string nickname { get; set; }
        /// <summary>  
        /// 性别  
        /// </summary>  
        public string gender { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string year { get; set; }
        /// <summary>  
        /// 用户头像  
        /// </summary>  
        public string figureurl { get; set; }

        public string figureurl_2 { get; set; }

        public string figureurl_qq_2 { get; set; } 
    }
}
