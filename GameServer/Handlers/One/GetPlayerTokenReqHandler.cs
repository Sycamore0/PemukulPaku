using Common.Resources.Proto;
using Common.Database;
using Common;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetPlayerTokenReq)]
    internal class GetPlayerTokenReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet _packet)
        {
            GetPlayerTokenReq Packet = _packet.GetDecodedBody<GetPlayerTokenReq>();
            GetPlayerTokenRsp Rsp = new () { };
            UserScheme? CurrentUser = User.FromToken(Packet.AccountToken);

            if (CurrentUser is null || CurrentUser.Uid != uint.Parse(Packet.AccountUid))
            {
                Rsp.retcode = GetPlayerTokenRsp.Retcode.AccountVerifyError;
                Rsp.Msg = "Account verification failed, please re-login!";
            }
            else
            {
                session.Player = new Game.Player(CurrentUser);

                if(session.Player.User.IsFirstLogin)
                {
                    AvatarScheme avatar = Common.Database.Avatar.Create(101, session.Player.User.Uid, session.Player.Equipment);
                    session.Player.AvatarList = session.Player.AvatarList.Append(avatar).ToArray();
                    if ((int)Global.config.VerboseLevel > 0)
                        session.c.Log($"Automatically created avatar with id: {avatar.AvatarId}");
                }

                Rsp = new()
                {
                    retcode = GetPlayerTokenRsp.Retcode.Succ,
                    Uid = CurrentUser.Uid,
                    Token = CurrentUser.Token,
                    AccountType = Packet.AccountType,
                    AccountUid = Packet.AccountUid,
                    UserType = 4,
                    HoyolabAccountUid = Packet.AccountUid,
                    FightserverIp = 0,
                    FightserverPort = 0
                };
            }

            session.Send(GameServer.Packet.FromProto(Rsp, CmdId.GetPlayerTokenRsp));
        }
    }
}
