using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ReportBirthdayReq)]
    internal class ReportBirthdayReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReportBirthdayReq Data = packet.GetDecodedBody<ReportBirthdayReq>();
            session.Player.User.BirthDate = (int)Data.Birthday;
            GetMainDataRsp mainDataRsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                Birthday = Data.Birthday,
                TypeLists = new uint[] { 21 } 
            };
            ReportBirthdayRsp Rsp = new() { retcode = ReportBirthdayRsp.Retcode.Succ };

            session.Send(Packet.FromProto(mainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(Rsp, CmdId.ReportBirthdayRsp));
        }
    }
}
