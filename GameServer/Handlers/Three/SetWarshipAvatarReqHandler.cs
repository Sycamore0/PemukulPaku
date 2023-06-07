using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SetWarshipAvatarReq)]
    internal class SetWarshipAvatarReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetWarshipAvatarReq Data = packet.GetDecodedBody<SetWarshipAvatarReq>();

            // extra redundancy
            if (Data.FirstAvatarId == 0)
                Data.FirstAvatarId = 101;

            session.Player.User.WarshipAvatar = new()
            {
                WarshipFirstAvatarId = Data.FirstAvatarId,
                WarshipSecondAvatarId = Data.SecondAvatarId
            };

            GetMainDataRsp MainDataRsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                WarshipAvatar = session.Player.User.WarshipAvatar,
                TypeLists = new uint[] { 35 }
            };

            session.Send(Packet.FromProto(MainDataRsp, CmdId.GetMainDataRsp), Packet.FromProto(new SetWarshipAvatarRsp() { retcode = SetWarshipAvatarRsp.Retcode.Succ }, CmdId.SetWarshipAvatarRsp));
        }
    }
}
