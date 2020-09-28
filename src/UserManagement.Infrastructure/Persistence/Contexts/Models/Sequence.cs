using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserManagement.Infrastructure.Persistence.Contexts.Models
{
   public  class Sequence
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        public string SequenceName { get; set; }

        public long SequenceValue { get; set; }
    }
}
