using Common.Utils.ExcelReader;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Database
{
    public class OpenWorld
    {
        public static readonly IMongoCollection<OpenWorldScheme> collection = Global.db.GetCollection<OpenWorldScheme>("OpenWorlds");
        public static readonly uint[] ShowMapList = new uint[] { 1, 2, 301, 401, 601, 701, 801, 1001 };

        public static void InitData(uint uid)
        {
            collection.InsertMany(OpenWorldMap.GetInstance().All.Select(x => (uint)x.MapId).Select(mapId =>
            {
                return new OpenWorldScheme()
                {
                    MapId = mapId,
                    Cycle = OpenWorldCycleData.GetInstance().GetInitCycle(mapId),
                    OwnerUid = uid,
                    QuestLevel = 1,
                    HasTakeFinishRewardCycle = 0,
                    SpawnPoint = ""
                };
            }));
        }
        
        public static List<OpenWorldScheme> FromUid(uint uid)
        {
            List<OpenWorldScheme> Data = collection.AsQueryable().Where(x => x.OwnerUid == uid && ShowMapList.Contains(x.MapId)).ToList();
            if(Data.Count > 0)
                return Data;
            InitData(uid);
            return collection.AsQueryable().Where(x => x.OwnerUid == uid && ShowMapList.Contains(x.MapId)).ToList(); ;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class OpenWorldScheme
    {
        public ObjectId Id { get; set; }
        public uint OwnerUid { get; set; }
        public uint MapId { get; set; }
        public uint Cycle { get; set; }
        public uint QuestLevel { get; set; }
        public uint HasTakeFinishRewardCycle { get; set; }
        public string SpawnPoint { get; set; }

        public void Save()
        {
            OpenWorld.collection.ReplaceOne(Builders<OpenWorldScheme>.Filter.Eq(x => x.Id, Id), this);
        }
    }
}
