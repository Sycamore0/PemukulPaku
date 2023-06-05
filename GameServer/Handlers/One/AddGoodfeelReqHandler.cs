using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.AddGoodfeelReq)]
    internal class AddGoodfeelReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            AddGoodfeelReq Data = packet.GetDecodedBody<AddGoodfeelReq>();
            AvatarScheme? avatar = session.Player.AvatarList.Where(avatar => avatar.AvatarId == Data.AvatarId).FirstOrDefault();

            if(avatar != null)
            {
                avatar.TodayHasAddGoodfeel += (uint)Data.AddGoodfeel;
                avatar.TouchGoodfeel += (uint)Data.AddGoodfeel;

                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { avatar.AvatarId } }, CmdId.GetAvatarDataReq));
            }
            session.Send(Packet.FromProto(new AddGoodfeelRsp() { retcode = AddGoodfeelRsp.Retcode.Succ }, CmdId.AddGoodfeelRsp));
        }
    }
}
