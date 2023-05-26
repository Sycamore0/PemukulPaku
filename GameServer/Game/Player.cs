using Common.Database;

namespace PemukulPaku.GameServer.Game
{
    public class Player
    {
        public User.UserScheme User;

        public Player(User.UserScheme user)
        {
            User = user;
        }
    }
}
