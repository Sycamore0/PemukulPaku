using Common.Resources.Proto;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Database
{
    public class AutoIncrement
    {
        public static readonly IMongoCollection<AI> collection = Global.db.GetCollection<AI>("AutoIncrements");

        public static int GetNextNumber(string name, int starting = 100)
        {
            AI AutoIncrement = collection.FindOneAndUpdate(Builders<AI>.Filter.Eq("Name", name), Builders<AI>.Update.SetOnInsert("Count", starting), new FindOneAndUpdateOptions<AI> { IsUpsert = true, ReturnDocument = ReturnDocument.After });
            collection.UpdateOne(Builders<AI>.Filter.Eq("Name", AutoIncrement.Name), Builders<AI>.Update.Inc("Count", 1));
            return AutoIncrement.Count + 1;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public class AI
        {
            public ObjectId Id { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
