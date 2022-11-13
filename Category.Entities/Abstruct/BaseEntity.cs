using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Category.Entities.Abstruct
{
    public abstract class BaseEntity : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ActiveFlag { get; set; }
    }
}
