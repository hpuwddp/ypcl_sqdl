using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace sqdl
{
   public class OAuth_Token
    {
       //access_token	网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        //expires_in	access_token接口调用凭证超时时间，单位（秒）
        //refresh_token	用户刷新access_token
        //openid	用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        //scope	用户授权的作用域，使用逗号（,）分隔
        public string _access_token;
        public string _expires_in;
        public string _refresh_token;
        public string _openid;
        public string _scope;

       public string access_token
        {
            set { _access_token = value; }
            get { return _access_token; }
        }
        public string expires_in
        {
            set { _expires_in = value; }
            get { return _expires_in; }
        }

        public string refresh_token
        {
            set { _refresh_token = value; }
            get { return _refresh_token; }
        }
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        public string scope
        {
            set { _scope = value; }
            get { return _scope; }
        }
	    /// <summary>  
        /// 生成Json格式  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
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
        /// <summary>  
        /// json转实体  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="szJson"></param>  
        /// <returns></returns>  
        public OAuth_Token Get_token(string Code)
        {
            string appid = "wx22a95fa23348f66f";
            string appsecret = "ce14a10ba3b02428d68c06b7baf407c9";
            //获取微信回传的openid、access token
            string Str = GetJson("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + appsecret + "&code=" + Code + "&grant_type=authorization_code");
            //微信回传的数据为Json格式，将Json格式转化成对象
            OAuth_Token Oauth_Token_Model =JsonConvert.DeserializeObject<OAuth_Token>(Str);
            return Oauth_Token_Model;
        }
        /// <summary>  
        /// 通过access_token和openid获取用户信息  
        /// </summary>  
        /// <returns></returns>  
        public OAuthUser Get_UserInfo(string access_token, string openid)
        {
            //获取微信回传的openid、access token
            string Str = GetJson("https://api.weixin.qq.com/sns/userinfo?access_token="+ access_token + "&openid="+ openid + "&lang=zh_CN");
            //微信回传的数据为Json格式，将Json格式转化成对象
            OAuthUser userinfo = JsonConvert.DeserializeObject<OAuthUser>(Str);
            return userinfo;
        }
    }
}
