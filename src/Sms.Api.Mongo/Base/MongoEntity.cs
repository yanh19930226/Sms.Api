using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Mongo.Base
{
    public abstract class MongoEntity
    {
        [BsonId(IdGenerator = typeof(YandeIdGenerator))]
        [BsonElement("_id")]
        public string Id { get; set; }
    }

    /// <summary>
    /// 自定义Id生成器
    /// </summary>
    public class YandeIdGenerator : IIdGenerator
    {

        public object GenerateId(object container, object document)
        {

            return "yande_" + Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }
}
