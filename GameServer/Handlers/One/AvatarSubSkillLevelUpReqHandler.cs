using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.AvatarSubSkillLevelUpReq)]
    internal class AvatarSubSkillLevelUpReqHandler : IPacketHandler
    {
        // TODO: Find out how to calculate cost with itemType 0
        public void Handle(Session session, Packet packet)
        {
            AvatarSubSkillLevelUpReq Data = packet.GetDecodedBody<AvatarSubSkillLevelUpReq>();
            AvatarScheme? avatar = session.Player.AvatarList.FirstOrDefault(avatar => avatar.AvatarId == Data.AvatarId);
            AvatarSubSkillLevelUpRsp Rsp = new() { retcode = AvatarSubSkillLevelUpRsp.Retcode.Succ };

            if (avatar is not null)
            {
                avatar.LevelUpSkill(Data.SubSkillId, Data.IsLevelUpAll);
                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { avatar.AvatarId } }, CmdId.GetAvatarDataReq));
            }
            else
            {
                Rsp.retcode = AvatarSubSkillLevelUpRsp.Retcode.AvatarNotExist;
            }
            session.Send(Packet.FromProto(Rsp, CmdId.AvatarSubSkillLevelUpRsp));
        }
    }
}
