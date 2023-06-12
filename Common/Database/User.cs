using MongoDB.Bson;
using Common.Resources.Proto;
using MongoDB.Driver;
using Common.Utils.ExcelReader;

namespace Common.Database
{
    public class User
    {
        public static readonly IMongoCollection<UserScheme> collection = Global.db.GetCollection<UserScheme>("Users");
        
        public static UserScheme Create(string name)
        {
            UserScheme? tryUser = collection.AsQueryable().Where(d => d.Name == name).FirstOrDefault();
            if (tryUser != null) { return tryUser; }

            UserScheme user = new()
            {
                Name = name,
                Uid = (uint)AutoIncrement.GetNextNumber("UID", 1000),
                Nick = "",
                Exp = 0,
                Hcoin = 0,
                Stamina = 80,
                SelfDesc = "",
                IsFirstLogin = true,
                Token = Guid.NewGuid().ToString(),
                WarshipId = 0,
                WarshipAvatar = new WarshipAvatarData()
                {
                    WarshipFirstAvatarId = 101,
                    WarshipSecondAvatarId = 0
                },
                CustomHeadId = 161001,
                FrameId = 200001,
                AssistantAvatarId = 101,
                BirthDate = 0,
                AbyssDynamicHard = 100,
                AbyssGroupLevel = 8,
                AvatarTeamList = new List<AvatarTeam> { new AvatarTeam { AvatarIdLists = new uint[] { 101 }, StageType = ((uint)StageType.StageStory) } },
                CustomAvatarTeamList = new List<CustomAvatarTeam> { }
            };

            collection.InsertOne(user);

            return user;
        }

        public static UserScheme FromName(string name)
        {
            UserScheme? user = collection.AsQueryable().Where(d => d.Name == name).FirstOrDefault();
            return user ?? Create(name);
        }

        public static UserScheme? FromToken(string token)
        {
            UserScheme? user = collection.AsQueryable().Where(d => d.Token == token).FirstOrDefault();
            return user;
        }

        public static UserScheme? FromUid(uint uid)
        {
            UserScheme? user = collection.AsQueryable().Where(d => d.Uid == uid).FirstOrDefault();
            return user;
        }

    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class UserScheme
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public uint Uid { get; set; }
        public string Nick { get; set; }
        public int Exp { get; set; }
        public int Hcoin { get; set; }
        public int Stamina { get; set; }
        public string SelfDesc { get; set; }
        public bool IsFirstLogin { get; set; }
        public string Token { get; set; }
        public int CustomHeadId { get; set; }
        public int FrameId { get; set; }
        public int WarshipId { get; set; }
        public WarshipAvatarData WarshipAvatar { get; set; }
        public int AssistantAvatarId { get; set; }
        public int BirthDate { get; set; }
        public uint AbyssDynamicHard { get; set; }
        public uint AbyssGroupLevel { get; set; }
        public List<AvatarTeam> AvatarTeamList { get; set; }
        public List<CustomAvatarTeam> CustomAvatarTeamList { get; set; }
        public List<OpenWorldStoryScheme> OpenWorldStory { get; set; } = new();

        public void Save()
        {
            User.collection.ReplaceOne(Builders<UserScheme>.Filter.Eq(user => user.Id, Id), this);
        }
        
        public uint GetCreationTime()
        {
            return (uint)((DateTimeOffset)Id.CreationTime).ToUnixTimeSeconds();
        }
        
        public void AddExp(int exp)
        {
            Exp += exp;
            if (Exp > PlayerLevelData.GetInstance().GetMaxPossibleExp())
            {
                Exp = PlayerLevelData.GetInstance().GetMaxPossibleExp();
            }
        }

        public void AddOWStory(uint storyId)
        {
            OpenWorldStory.Add(new()
            {
                StoryId = storyId,
                StoryProgress = 0,
                AcceptTime = (uint)Global.GetUnixInSeconds(),
                IsDone = false
            });
        }

        public class OpenWorldStoryScheme : OpenworldStory
        {
            public bool IsDone;

            public OpenworldStory ToProto()
            {
                return new()
                {
                    StoryId = StoryId,
                    StoryProgress = StoryProgress,
                    AcceptTime = AcceptTime
                };
            }
        }
    }        
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
}
