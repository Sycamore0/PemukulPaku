using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetPlayerCardReq)]
    internal class GetPlayerCardReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetPlayerCardRsp Rsp = new()
            {
                retcode = GetPlayerCardRsp.Retcode.Succ,
                Type = PlayerCardType.CardAll,
                ElfIdLists = new uint[] { 0 },
                AvatarIdLists = new uint[] { 0, 0, 0 },
            };
            Rsp.MedalLists.AddRange(new Medal[] { new Medal() { Id = 0, EndTime = 0, ExtraParam = 0 }, new Medal() { Id = 0, EndTime = 0, ExtraParam = 0 } });

            session.Send(Packet.FromProto(Rsp, CmdId.GetPlayerCardRsp));
        }
    }
}
