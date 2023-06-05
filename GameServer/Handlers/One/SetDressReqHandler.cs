using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SetDressReq)]
    internal class SetDressReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SetDressReq Data = packet.GetDecodedBody<SetDressReq>();
            SetDressRsp Rsp = new() { retcode = SetDressRsp.Retcode.Succ };
            AvatarScheme? avatar = session.Player.AvatarList.Where(x => x.AvatarId == Data.AvatarId).FirstOrDefault();
            if (avatar is not null)
            {
                if (avatar.DressLists.Contains(Data.DressId))
                {
                    avatar.SetDress(Data.DressId);
                    session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { avatar.AvatarId } }, CmdId.GetAvatarDataReq));
                }
                else
                {
                    Rsp.retcode = SetDressRsp.Retcode.DressNotExist;
                }
            }
            else
            {
                Rsp.retcode = SetDressRsp.Retcode.AvatarNotExist;
            }

            session.Send(Packet.FromProto(Rsp, CmdId.SetDressRsp));
        }
    }
}
