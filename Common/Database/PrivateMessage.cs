using Common.Resources.Proto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Common.Database
{
    public class PrivateMessage
    {
        public static readonly IMongoCollection<PrivateMessageScheme> collection = Global.db.GetCollection<PrivateMessageScheme>("PrivateMessages");

        public static PrivateMessageScheme Create(uint uid, uint targetUid, ChatMsg chatMsg)
        {
            PrivateMessageScheme doc = new() { Msg = chatMsg, SenderUid = uid, TargetUid = targetUid, TimeSent = Global.GetUnixInSeconds() };
            collection.InsertOne(doc);
            return doc;
        }

        public static List<HistoryPrivateChatMsg> GetMessages(uint uid, uint targetUid = 0)
        {
            List<HistoryPrivateChatMsg> historyPrivateChats = new();

            if (uid == 0)
            {
                var gropedMessages = collection.AsQueryable().Where(x => x.SenderUid == uid || x.TargetUid == uid).ToList().GroupBy(x => x, new PrivateMessageComparer());

                foreach (var group in gropedMessages)
                {
                    List<PrivateMessageScheme> targetedMessages = group.ToList();
                    targetUid = targetedMessages.First().TargetUid == uid ? targetedMessages.First().SenderUid : targetedMessages.First().TargetUid;
                    historyPrivateChats.Add(new() { Uid = targetUid });
                    historyPrivateChats.First(x => x.Uid == targetUid).ChatMsgLists.AddRange(targetedMessages.Select(x => x.Msg));
                }
            }
            else
            {
                List<PrivateMessageScheme> targetedMessages = collection.AsQueryable().Where(x => (x.SenderUid == uid && x.TargetUid == targetUid) || (x.SenderUid == targetUid && x.TargetUid == uid)).ToList();
                historyPrivateChats.Add(new() { Uid = targetUid });
                historyPrivateChats.First(x => x.Uid == targetUid).ChatMsgLists.AddRange(targetedMessages.Select(x => x.Msg));
            }

            return historyPrivateChats;
        }

        public class PrivateMessageComparer : IEqualityComparer<PrivateMessageScheme>
        {
#pragma warning disable CS8767
            public bool Equals(PrivateMessageScheme x, PrivateMessageScheme y)
            {
                return x.SenderUid == y.TargetUid || y.SenderUid == x.TargetUid;
            }
#pragma warning restore CS8767

            public int GetHashCode(PrivateMessageScheme obj)
            {
                return (obj.SenderUid + obj.TargetUid).GetHashCode();
            }
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [BsonIgnoreExtraElements]
    public class PrivateMessageScheme
    {
        public ObjectId Id { get; set; }
        public uint TargetUid { get; set; }
        public uint SenderUid { get; set; }

        [BsonIgnore]
        public ChatMsg Msg
        {
            get
            {
                return JsonConvert.DeserializeObject<ChatMsg>(SerializedMsg) ?? new ChatMsg();
            }
            set
            {
                SerializedMsg = JsonConvert.SerializeObject(value);
            }
        }

        public string SerializedMsg { get; set; }
        public long TimeSent { get; set; }
    }
}
