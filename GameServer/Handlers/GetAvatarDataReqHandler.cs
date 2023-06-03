using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetAvatarDataReq)]
    internal class GetAvatarDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetAvatarDataReq Packet = packet.GetDecodedBody<GetAvatarDataReq>();

            GetAvatarDataRsp Rsp = new()
            {
                retcode = GetAvatarDataRsp.Retcode.Succ
            };

            if (Packet.AvatarIdLists.Contains((uint)0))
            {
                IEnumerable<Avatar> Avatars = session.Player.AvatarList.Select(avatar =>
                {
                    Avatar a = new()
                    {
                        AvatarId = avatar.AvatarId,
                        AvatarArtifact = avatar.AvatarArtifact,
                        DressId = avatar.DressId,
                        DressLists = avatar.DressLists,
                        Level = avatar.Level,
                        Exp = avatar.Exp,
                        Fragment = avatar.Fragment,
                        Mode = avatar.Mode,
                        StageGoodfeel = avatar.StageGoodfeel,
                        Star = avatar.Star,
                        StigmataUniqueId1 = avatar.StigmataUniqueId1,
                        StigmataUniqueId2 = avatar.StigmataUniqueId2,
                        StigmataUniqueId3 = avatar.StigmataUniqueId3,
                        SubStar = avatar.SubStar,
                        TodayHasAddGoodfeel = avatar.TodayHasAddGoodfeel,
                        TouchGoodfeel = avatar.TouchGoodfeel,
                        WeaponUniqueId = avatar.WeaponUniqueId
                    };
                    a.SkillLists.AddRange(avatar.SkillLists.Select(skill =>
                    {
                        AvatarSkill avatarSkill = new() { SkillId = skill.SkillId };
                        avatarSkill.SubSkillLists.AddRange(skill.SubSkillLists);
                        return avatarSkill;
                    }));

                    return a;
                });

                Rsp.IsAll = true;
                Rsp.AvatarLists.AddRange(Avatars);
            }
            else
            {
                IEnumerable<Avatar> Avatars = session.Player.AvatarList.Where(avatar => Packet.AvatarIdLists.Contains(avatar.AvatarId)).Select(avatar =>
                {
                    Avatar a = new()
                    {
                        AvatarId = avatar.AvatarId,
                        AvatarArtifact = avatar.AvatarArtifact,
                        DressId = avatar.DressId,
                        DressLists = avatar.DressLists,
                        Level = avatar.Level,
                        Exp = avatar.Exp,
                        Fragment = avatar.Fragment,
                        Mode = avatar.Mode,
                        StageGoodfeel = avatar.StageGoodfeel,
                        Star = avatar.Star,
                        StigmataUniqueId1 = avatar.StigmataUniqueId1,
                        StigmataUniqueId2 = avatar.StigmataUniqueId2,
                        StigmataUniqueId3 = avatar.StigmataUniqueId3,
                        SubStar = avatar.SubStar,
                        TodayHasAddGoodfeel = avatar.TodayHasAddGoodfeel,
                        TouchGoodfeel = avatar.TouchGoodfeel,
                        WeaponUniqueId = avatar.WeaponUniqueId
                    };
                    a.SkillLists.AddRange(avatar.SkillLists.Select(skill =>
                    {
                        AvatarSkill avatarSkill = new() { SkillId = skill.SkillId };
                        avatarSkill.SubSkillLists.AddRange(skill.SubSkillLists);
                        return avatarSkill;
                    }));

                    return a;
                });

                Rsp.AvatarLists.AddRange(Avatars);

                Rsp.IsAll = false;
            }

            session.Send(GameServer.Packet.FromProto(Rsp, CmdId.GetAvatarDataRsp));
        }
    }
}
