using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqdl
{
    public partial class Form1 : Form
    {
        // 微信跳转的网址列表
        List<string> addressList = new List<string>();

        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            //微信每次跳转的页面放到list中，第一个是包含code的网址
            addressList.Add(url);
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (addressList != null && addressList.Count > 0)
            {
                string tempCode = addressList[0].ToString();
                //微信最终获得的code
                string code = "";
                if (tempCode.Contains("code"))
                {
                    int iStart = tempCode.IndexOf("=");
                    int iEnd = tempCode.IndexOf('&', iStart);
                    if (iEnd < 0)
                    {
                        iEnd = tempCode.Length - iStart;
                    }
                    else
                    {
                        iEnd -= iStart;
                    }
                    code = tempCode.Substring(iStart + 1, iEnd - 1);
                }
                else
                {
                    return;
                }

                if (string.IsNullOrEmpty(code))
                    return;
                OAuth_Token token = new OAuth_Token();
                OAuth_Token Model = token.Get_token(code);  //获取access_token
                OAuthUser OAuthUser_Model = token.Get_UserInfo(Model.access_token, Model.openid);//获取用户信息    
            }
           
        }
    }
}
