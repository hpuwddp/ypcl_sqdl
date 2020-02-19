using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using System;
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
    public partial class SmsSend : Form
    {
        public SmsSend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", "LTAI4Fvz7m6iggRvBuu6fdqG", "WzfhsSfgisHar3lUtIXxN0SkW6oyOu");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", "13917571446");
            request.AddQueryParameters("SignName", "极风文字语音转换");
            request.AddQueryParameters("TemplateCode", "SMS_183855040");
            request.AddQueryParameters("TemplateParam", "{\"code\":\"nhpl\"}");
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                Console.WriteLine(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
            }
            catch (ServerException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ClientException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
