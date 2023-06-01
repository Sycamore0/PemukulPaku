using Common.Database;

namespace PemukulPaku.GameServer.Game
{
    public class Player
    {
        public UserScheme User;
        public AvatarScheme[] AvatarList;
        public EquipmentScheme Equipment;

        public Player(UserScheme user)
        {
            User = user;
            Equipment = Common.Database.Equipment.FromUid(user.Uid);
            AvatarList = Avatar.AvatarsFromUid(user.Uid);
        }

        public void SaveAll()
        {
            User.Save();
            Equipment.Save();

            foreach (AvatarScheme Avatar in AvatarList)
            {
                Avatar.Save();
            }
        }

        public void ResetAvatarsTodayGoodfeel() 
        {
            foreach (AvatarScheme avatar in AvatarList)
            {
                avatar.TodayHasAddGoodfeel = 0;
            }
        }
    }
}
