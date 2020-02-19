using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqdl
{
    public partial class QQLogin : Form
    {
        List<string> addressList = new List<string>();
        private string app_id = "";
        public QQLogin()
        {
            InitializeComponent();
        }

        private void webBrowser_QQ_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string url = e.Url.ToString();
            addressList.Add(url);
        }

        //Step2：一般情况是通过Authorization Code获取Access Token，这里直接从网址获取Access Token  
        protected string Get_AccessToken()
        {
            string strTempToken = "";
            // 获取含有Access_token的网址  
            foreach (string url in addressList)
            {
                if (url.Contains("access_token"))
                {
                    strTempToken = url;
                    break;
                }
            }
            if (string.IsNullOrEmpty(strTempToken))
                return null;
            //qq最终获得的Access_token  
            string AccessToken = "";
            int iStart = strTempToken.IndexOf("=");
            int iEnd = strTempToken.LastIndexOf('&');
            if (iStart < iEnd)
            {
                int codeLength = iEnd - iStart - 1;
                AccessToken = strTempToken.Substring(iStart + 1, codeLength);
            }
            return AccessToken;
        }

        //Step3：使用Access Token来获取用户的OpenID  
        protected string Get_OpenID(string Access_token)
        {
            string callback = GetJson("https://graph.qq.com/oauth2.0/me?access_token=" + Access_token);
            int ibegin = callback.IndexOf("{");
            int iEnd = callback.IndexOf("}");
            int ilength = 0;
            if (ibegin < iEnd)
            {
                ilength = iEnd - ibegin;
            }
            string strOpenId = callback.Substring(ibegin, ilength + 1);
            QQCallBack qqOpenId = JsonConvert.DeserializeObject<QQCallBack>(strOpenId);
            string openId = qqOpenId.openid;
            return openId;
        }

        protected QQUser Get_qqUserInfo(string Access_token, string openId)
        {
            string userInfostr = GetJson("https://graph.qq.com/user/get_user_info?access_token=" + Access_token + "&oauth_consumer_key=" + app_id + "&openid=" + openId);
            QQUser qqUser = JsonConvert.DeserializeObject<QQUser>(userInfostr);
            return qqUser;
        }
        public string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
            }
            return returnText;
        }
        private void webBrowser_QQ_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string access_token = Get_AccessToken();
            if (string.IsNullOrEmpty(access_token))
                return;
            string openid = Get_OpenID(access_token);
            if (string.IsNullOrEmpty(openid))
            {
                return;
            }
            QQUser qqUser = Get_qqUserInfo(access_token, openid);
        }
    }
}
