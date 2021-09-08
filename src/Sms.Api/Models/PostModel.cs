using Sms.Api.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api.Models
{
    /// <summary>
    /// 短信发送
    /// </summary>
    public class PostModel
    {
        [Required, Display(Name = "短信内容")]
        public string Content { get; set; }
        /// <summary>
        /// 运营商
        /// </summary>
        public SmsEnums.SmsType Type { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public List<string> Mobiles { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? TimeSendDateTime { get; set; }
    }
}
