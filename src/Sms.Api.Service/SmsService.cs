using Sikiro.Nosql.Mongo;
using Sms.Api.Model;
using Sms.Api.Service.Models;
using Sms.Api.ToolKits;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Service
{
    public class SmsService
    {
        private readonly MongoRepository _mongoProxy;
        public SmsService()
        {

        }

        #region Private

        /// <summary>
        /// 发送成功
        /// </summary>
        /// <param name="model"></param>
        private void Success(SmsModel model)
        {
            model.Status = SmsEnums.SmsStatus.成功;
            model.CreateDateTime = DateTime.Now;
            _mongoProxy.Add(MongoKey.SmsDataBase, MongoKey.SmsCollection + "_" + DateTime.Now.ToString("yyyyMM"), model);
        }

        /// <summary>
        /// 发送失败
        /// </summary>
        /// <param name="model"></param>
        private void Fail(SmsModel model)
        {
            model.Status = SmsEnums.SmsStatus.失败;
            model.CreateDateTime = DateTime.Now;
            _mongoProxy.Add(MongoKey.SmsDataBase, MongoKey.SmsCollection + "_" + DateTime.Now.ToString("yyyyMM"), model);
        }

        /// <summary>
        /// 获取页数
        /// </summary>
        /// <param name="phoneCount"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        private int GetPageCount(int phoneCount, int maxCount)
        {
            return (int)Math.Ceiling(phoneCount / (double)maxCount);
        } 

        #endregion

        /// <summary>
        ///Id获取短信记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SmsModel Get(string id)
        {
            return _mongoProxy.Get<SmsModel>(a => a.Id.ToString() == id);
        }

        /// <summary>
        /// 条件搜索短信记录
        /// </summary>
        /// <param name="searchSmsModel"></param>
        /// <returns></returns>
        public List<SmsModel> Search(SearchSmsModel searchSmsModel)
        {
            var builder = ExpressionBuilder.Init<SmsModel>();
            if (searchSmsModel != null)
            {
                if (searchSmsModel.Status.HasValue)
                    builder = builder.And(a => a.Status == searchSmsModel.Status.Value);

                if (searchSmsModel.Type.HasValue)
                    builder = builder.And(a => a.Type == searchSmsModel.Type.Value);

                if (searchSmsModel.BeganCreateDateTime.HasValue)
                    builder = builder.And(a => a.CreateDateTime >= searchSmsModel.BeganCreateDateTime.Value);

                if (searchSmsModel.EndCreateDateTime.HasValue)
                    builder = builder.And(a => a.CreateDateTime <= searchSmsModel.EndCreateDateTime.Value);

                if (searchSmsModel.BeganTimeSendDateTime.HasValue)
                    builder = builder.And(a => a.TimeSendDateTime >= searchSmsModel.BeganTimeSendDateTime.Value);

                if (searchSmsModel.EndTimeSendDateTime.HasValue)
                    builder = builder.And(a => a.TimeSendDateTime <= searchSmsModel.EndTimeSendDateTime.Value);

                if (!string.IsNullOrEmpty(searchSmsModel.Mobile))
                    builder = builder.And(a => a.Mobiles.Contains(searchSmsModel.Mobile));

                if (!string.IsNullOrEmpty(searchSmsModel.Content))
                    builder = builder.And(a => a.Content.Contains(searchSmsModel.Content));
            }

            return _mongoProxy.ToList(builder);
        }
       
    }
}
