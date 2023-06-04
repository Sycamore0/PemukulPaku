using Common.Resources.Proto;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Database
{
    public class ClientData
    {
        public static readonly IMongoCollection<ClientDataScheme> collection = Global.db.GetCollection<ClientDataScheme>("ClientDatas");

        public static ClientDataScheme? GetClientData(uint uid, ClientDataType type, uint id)
        {
            return collection.AsQueryable().Where(c => c.OwnerUid == uid && c.Type == type && c.ClientDataId == id).FirstOrDefault();
        }

        public static void SetClientData(uint uid, ClientDataType type, uint id, byte[] data)
        {
            collection.UpdateOne(Builders<ClientDataScheme>.Filter.Where(c => c.OwnerUid == uid && c.Type == type && c.ClientDataId == id), Builders<ClientDataScheme>.Update.Set("Type", type).Set("ClientDataId", id).Set("Data", data).SetOnInsert("OwnerUid", uid), new UpdateOptions() { IsUpsert = true });
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ClientDataScheme
    {
        public ObjectId Id { get; set; }
        public uint OwnerUid { get; set; }
        public ClientDataType Type { get; set; }
        public uint ClientDataId { get; set; }
        public byte[] Data { get; set; }
    }
}
