using MongoDB.Bson.Serialization.Attributes;
using Sms.Api.Mongo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Model
{
    [Mongo(MongoKey.SmsDataBase, MongoKey.SmsCollection)]
    public class SmsModel : MongoEntity
    {
        public string Content { get; set; }
        public SmsEnums.SmsType Type { get; set; }
        public SmsEnums.SmsStatus Status { get; set; }
        public List<string> Mobiles { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? TimeSendDateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDateTime { get; set; }
        public int SendCount { get; set; }
    }
}
