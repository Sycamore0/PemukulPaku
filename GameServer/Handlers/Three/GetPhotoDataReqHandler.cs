using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Three
{
    [PacketCmdId(CmdId.GetPhotoDataReq)]
    internal class GetPhotoDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetPhotoDataReq Data = packet.GetDecodedBody<GetPhotoDataReq>();

            session.Send(Packet.FromProto(new GetPhotoDataRsp()
            {
                retcode = GetPhotoDataRsp.Retcode.Succ,
                Type = Data.Type
            }, CmdId.GetPhotoDataRsp));
        }
    }
}
