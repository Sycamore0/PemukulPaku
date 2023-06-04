using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GachaReq)]
    internal class GachaReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GachaReq Data = packet.GetDecodedBody<GachaReq>();
            GachaRsp Rsp = new()
            {
                retcode = GachaRsp.Retcode.Succ,
                GachaRandom = Data.GachaRandom,
                IsUseFreeGacha = Data.IsUseFreeGacha
            };
            Rsp.ItemLists.Add(new()
            {
                ItemId = 10106,
                Level = 0,
                Num = 5,
                GiftItemId = 80026,
                GiftLevel = 0,
                GiftNum = 50
            });

            session.Send(Packet.FromProto(Rsp, CmdId.GachaRsp));
        }
    }
}
