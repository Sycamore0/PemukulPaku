using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.NicknameModifyReq)]
    internal class NicknameModifyReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            NicknameModifyReq Data = packet.GetDecodedBody<NicknameModifyReq>();

            session.Player.User.Nick = Data.Nickname;

            GetMainDataRsp MainDataRsp = new() { retcode = GetMainDataRsp.Retcode.Succ, Nickname = session.Player.User.Nick, TypeLists = new uint[] { 2 } };
            session.Send(Packet.FromProto(MainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(new NicknameModifyRsp() { retcode = NicknameModifyRsp.Retcode.Succ }, CmdId.NicknameModifyRsp));
        }
    }
}
