using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetMainDataReq)]
    internal class GetMainDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UserScheme User = session.Player.User;

            GetMainDataRsp Rsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                AssistantAvatarId = (uint)User.AssistantAvatarId,
                Birthday = (uint)User.BirthDate,
                Nickname = User.Nick,
                Level = 4,
                Exp = (uint)User.Exp,
                FreeHcoin = (uint)User.Hcoin,
                Hcoin = (uint)User.Hcoin,
                CustomHeadId = 161001
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetMainDataRsp));
        }
    }
}
