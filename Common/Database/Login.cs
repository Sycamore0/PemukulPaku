using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Database
{
    public class Login
    {
        public static readonly IMongoCollection<LoginScheme> collection = Global.db.GetCollection<LoginScheme>("Logins");

        public static bool UserLogin(uint Uid)
        {
            LoginScheme? LastLogin = GetUserLastLogin(Uid);
            if (LastLogin is not null && LastLogin.Id.CreationTime.Date < DateTime.Now.Date)
            {
                collection.InsertOne(new() { OwnerUid = Uid });
                return true;
            }
            collection.InsertOne(new() { OwnerUid = Uid });
            return false;
        }

        public static List<LoginScheme> GetUserLogins(uint Uid)
        {
            return collection.AsQueryable().Where(login => login.OwnerUid == Uid).ToList();
        }

        public static LoginScheme? GetUserLastLogin(uint Uid)
        {
            return GetUserLogins(Uid).LastOrDefault();
        }

        public static uint GetUserLoginDays(uint Uid)
        {
            return (uint)GetUserLogins(Uid).DistinctBy(login => login.Id.CreationTime.Date).Count();
        }
    }

    public class LoginScheme
    {
        public ObjectId Id { get; set; }
        public uint OwnerUid { get; set; }

        public uint GetCreationTime()
        {
            return (uint)((DateTimeOffset)Id.CreationTime).ToUnixTimeSeconds();
        }
    }
}
