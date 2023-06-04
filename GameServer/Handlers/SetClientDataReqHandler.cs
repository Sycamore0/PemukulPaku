using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SetClientDataReq)]
    internal class SetClientDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetClientDataReq Data = packet.GetDecodedBody<SetClientDataReq>();
            Common.Database.ClientData.SetClientData(session.Player.User.Uid, Data.ClientData.Type, Data.ClientData.Id, Data.ClientData.Data);

            session.Send(Packet.FromProto(new SetClientDataRsp() { retcode = SetClientDataRsp.Retcode.Succ, Id = Data.ClientData.Id, Type = Data.ClientData.Type }, CmdId.SetClientDataRsp));
        }
    }
}
