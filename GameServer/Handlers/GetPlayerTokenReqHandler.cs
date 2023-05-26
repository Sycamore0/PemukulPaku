using Common.Resources.Proto;
using Common.Database;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetPlayerTokenReq)]
    internal class GetPlayerTokenReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet _packet)
        {
            GetPlayerTokenReq Packet = _packet.GetDecodedBody<GetPlayerTokenReq>();
            GetPlayerTokenRsp Rsp = new () { };
            User.UserScheme? CurrentUser = User.FromToken(Packet.AccountToken);

            if (CurrentUser == null || CurrentUser.Uid != uint.Parse(Packet.AccountUid))
            {
                Rsp.retcode = GetPlayerTokenRsp.Retcode.AccountVerifyError;
                Rsp.Msg = "Account verification failed, please re-login!";
            }
            else
            {
                session.Player = new Game.Player(CurrentUser);

                Rsp = new()
                {
                    retcode = GetPlayerTokenRsp.Retcode.Succ,
                    Uid = CurrentUser.Uid,
                    Token = CurrentUser.Token,
                    AccountType = Packet.AccountType,
                    AccountUid = Packet.AccountUid,
                    UserType = 4,
                    HoyolabAccountUid = Packet.AccountUid,
                    FightserverIp = 186782306,
                    FightserverPort = 2096693423
                };
            }

            session.Send(GameServer.Packet.FromProto(Rsp, CmdId.GetPlayerTokenRsp));
        }
    }
}
