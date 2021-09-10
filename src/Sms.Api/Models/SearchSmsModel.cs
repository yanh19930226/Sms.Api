using Sms.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api.Models
{
    /// <summary>
    /// SearchModel
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 运营商
        /// </summary>
        public SmsEnums.SmsType? Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SmsEnums.SmsStatus? Status { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        #region 数据创建时间段
        public DateTime? BeganCreateDateTime { get; set; }

        public DateTime? EndCreateDateTime { get; set; }
        #endregion

        #region 数据发送时间段
        public DateTime? BeganTimeSendDateTime { get; set; }

        public DateTime? EndTimeSendDateTime { get; set; } 
        #endregion
    }
}
