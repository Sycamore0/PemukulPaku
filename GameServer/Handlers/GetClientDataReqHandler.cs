using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetClientDataReq)]
    internal class GetClientDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetClientDataReq Data = packet.GetDecodedBody<GetClientDataReq>();
            ClientDataScheme? clientData = Common.Database.ClientData.GetClientData(session.Player.User.Uid, Data.Type, Data.Id);
            GetClientDataRsp Rsp = new() { retcode = GetClientDataRsp.Retcode.Succ, Id = Data.Id, Type = Data.Type };

            if (clientData is not null)
            {
                Rsp.ClientDataLists.Add(new()
                {
                    Id = clientData.ClientDataId,
                    Type = clientData.Type,
                    Data = clientData.Data
                });
            }
            else
            {
                Rsp.retcode = GetClientDataRsp.Retcode.NotFound;
            }

            session.Send(Packet.FromProto(Rsp, CmdId.GetClientDataRsp));
        }
    }
}
