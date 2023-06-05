using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.AvatarStarUpReq)]
    internal class AvatarStarUpReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            AvatarStarUpReq Data = packet.GetDecodedBody<AvatarStarUpReq>();
            AvatarStarUpRsp Rsp = new() { retcode = AvatarStarUpRsp.Retcode.Succ };
            AvatarScheme? avatar = session.Player.AvatarList.FirstOrDefault(x => x.AvatarId == Data.AvatarId);

            if (avatar is not null)
            {
                avatar.StarUp();
                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { avatar.AvatarId } }, CmdId.GetAvatarDataReq));
            }
            else
            {
                Rsp.retcode = AvatarStarUpRsp.Retcode.AvatarNotExist;
            }

            session.Send(Packet.FromProto(Rsp, CmdId.AvatarStarUpRsp));
        }
    }
}
