using EasyNetQ;
using EasyNetQ.Scheduling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Sms.Api.Model;
using Sms.Api.Models;
using Sms.Api.Service;
using Sms.Api.Service.Models;
using Sms.Api.Service.Sms;
using Sms.Api.ToolKits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sms.Api.Controllers
{
   
    [Route("api/sms")]
    [ApiController]
    [OpenApiTag("短信接口", Description = "短信接口")]
    [Authorize]
    public class SmsController : ControllerBase
    {
        private readonly SmsService _smsService;
        private readonly SmsFactory _smsFactory;
        private readonly IBus _bus;

        public SmsController(SmsService smsService, SmsFactory smsFactory, IBus bus)
        {
            _smsService = smsService;
            _smsFactory = smsFactory;
            _bus = bus;
        }

        #region Private

        /// <summary>
        /// 及时发送
        /// </summary>
        private void ImmediatelyPublish(List<PostModel> postModels)
        {
            postModels.Where(a => a.TimeSendDateTime == null).ToList().MapTo<List<PostModel>, List<SmsQueueModel>>()
                .ForEach(
                    item =>
                    {
                        _bus.Publish(item, SmsQueueModelKey.Topic);
                    });
        }

        /// <summary>
        /// 定时发送
        /// </summary>
        private void TimingPublish(List<PostModel> postModels)
        {
            postModels.Where(a => a.TimeSendDateTime != null).ToList()
                .ForEach(
                    item =>
                    {
                        _bus.FuturePublish(item.TimeSendDateTime.Value.ToUniversalTime(), item.MapTo<PostModel, SmsQueueModel>(),
                            SmsQueueModelKey.Topic);
                    });
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
        /// 获取短信记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<SmsModel> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();
            return _smsService.Get(id);
        }

        /// <summary>
        /// 添加短信记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("msg")]
        public ActionResult Post([FromBody] List<PostModel> model)
        {
            var pageRes = new List<PostModel>();

            foreach (var sms in model)
            {
                var maxCount = _smsFactory.Create(sms.Type).MaxCount;
                sms.Mobiles = sms.Mobiles.Distinct().ToList();
                var page = GetPageCount(sms.Mobiles.Count, maxCount);
                var index = 0;
                do
                {
                    var toBeSendPhones = sms.Mobiles.Skip(index * maxCount).Take(maxCount).ToList();
                    pageRes.Add(new PostModel
                    {
                        Content = sms.Content,
                        Mobiles = toBeSendPhones,
                        TimeSendDateTime = sms.TimeSendDateTime,
                        Type = sms.Type
                    });
                    index++;
                } while (index < page);
            }

            ImmediatelyPublish(pageRes);

            TimingPublish(pageRes);

            return Ok();
        }

        /// <summary>
        /// 查询短信记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public ActionResult<List<SmsModel>> Post([FromBody] SearchModel model)
        {
            var res=_smsService.Search(model.MapTo<SearchModel, SearchSmsModel>());

            return res;
        }
    }
}
