using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game
{
    public class Player
    {
        public UserScheme User;
        public AvatarScheme[] AvatarList;
        public EquipmentScheme Equipment;
        public List<OpenWorldScheme> OpenWorlds;

        public Player(UserScheme user)
        {
            User = user;
            Equipment = Common.Database.Equipment.FromUid(user.Uid);
            AvatarList = Common.Database.Avatar.AvatarsFromUid(user.Uid);
            OpenWorlds = OpenWorld.FromUid(user.Uid);
        }

        public void SaveAll()
        {
            User.Save();
            Equipment.Save();

            foreach (AvatarScheme Avatar in AvatarList)
            {
                Avatar.Save();
            }
            foreach (OpenWorldScheme OpenWorld in OpenWorlds)
            {
                OpenWorld.Save();
            }
        }

        public void ResetAvatarsTodayGoodfeel() 
        {
            foreach (AvatarScheme avatar in AvatarList)
            {
                avatar.TodayHasAddGoodfeel = 0;
            }
        }

        public PlayerCardData GetCardData()
        {
            return new()
            {
                Uid = User.Uid,
                MsgData = new()
                {
                    MsgIndex = 0,
                    MsgConfig = 1
                },
                OnPhonePendantId = 350005
            };
        }

        public PlayerDetailData GetDetailData()
        {
            return new()
            {
                Uid = User.Uid,
                Nickname = User.Nick,
                Level = (uint)PlayerLevelData.GetInstance().CalculateLevel(User.Exp).Level,
                SelfDesc = User.SelfDesc,
                CustomHeadId = (uint)User.CustomHeadId,
                FrameId = User.FrameId < 200001 ? 200001 : (uint)User.FrameId,
                LeaderAvatar = AvatarList.FirstOrDefault(x => x.AvatarId == User.AvatarTeamList.FirstOrDefault()?.AvatarIdLists[0])?.ToDetailData(Equipment) ?? new() { AvatarId = 101 }
            };
        }
    }
}
