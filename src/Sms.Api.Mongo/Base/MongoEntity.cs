using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Api.Mongo.Base
{
    public abstract class MongoEntity
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}
