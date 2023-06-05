using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.ClientCheckNetworkEnvReq)]
    internal class ClientCheckNetworkEnvReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ClientCheckNetworkEnvReq Data = packet.GetDecodedBody<ClientCheckNetworkEnvReq>();

            session.Send(Packet.FromProto(new ClientCheckNetworkEnvRsp() { retcode = ClientCheckNetworkEnvRsp.Retcode.Succ, TokenStr = Data.TokenStr }, CmdId.ClientCheckNetworkEnvRsp));
        }
    }
}
