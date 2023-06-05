using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.RefineStigmataRuneReq)]
    internal class RefineStigmataRuneReqHandler : IPacketHandler
    {
        static readonly uint[] AffixIds = { 30011, 30012, 30013, 30021, 30022, 30023, 30031, 30032, 30033, 30041, 30042, 30043, 30051, 30052, 30053, 30061, 30062, 30063, 30071, 30072, 30073, 30081, 30082, 30083, 30091, 30092, 30093, 30101, 30102, 30103, 30111, 30112, 30113, 30121, 30122, 30123, 30131, 30132, 30133, 30141, 30142, 30143, 30151, 30152, 30153, 30161, 30162, 30163, 30171, 30172, 30173, 30181, 30182, 30183, 30191, 30192, 30193, 30201, 30202, 30203, 30211, 30212, 30213, 40011, 40012, 40013, 40021, 40022, 40023, 40031, 40032, 40033, 40041, 40042, 40043, 50011, 50012, 50013, 50021, 50022, 50023, 50031, 50032, 50033, 50041, 50042, 50043 };

        public void Handle(Session session, Packet packet)
        {
            RefineStigmataRuneReq Data = packet.GetDecodedBody<RefineStigmataRuneReq>();
            RefineStigmataRuneRsp Rsp = new() { retcode = RefineStigmataRuneRsp.Retcode.Succ, TimesType = Data.TimesType };
            uint uid = Data.UniqueId;
            Stigmata? stigmata = session.Player.Equipment.StigmataList.FirstOrDefault(stig => stig.UniqueId == uid);

            /*
            foreach (EquipmentItem? consumeItem in Data.ConsumeItemList.ItemLists)
            {
                Material? material = session.Player.Equipment.MaterialList.FirstOrDefault(mat => mat.Id == consumeItem.IdOrUniqueId);

                if (material is not null)
                {
                    material.Num -= consumeItem.Num;
                }
            }
            */

            if (stigmata is not null)
            {
                StigmataRune rune = GenerateRune();
                StigmataRune rune2 = GenerateRune();
                StigmataRuneGroup group = new() { UniqueId = uid };

                switch (Data.Type)
                {
                    case StigmataRefineType.StigmataRefineNormal:
                        stigmata.RuneLists.Add(rune);
                        stigmata.RuneLists.Add(rune2);
                        group.RuneLists.Add(rune);
                        group.RuneLists.Add(rune2);
                        Rsp.RuneGroupLists.Add(group);
                        break;
                    case StigmataRefineType.StigmataRefineAddSlot:
                        stigmata.RuneLists.Clear();
                        stigmata.RuneLists.Add(rune);
                        stigmata.RuneLists.Add(rune2);
                        group.RuneLists.Add(rune);
                        group.RuneLists.Add(rune2);
                        Rsp.RuneGroupLists.Add(group);
                        break;
                    case StigmataRefineType.StigmataRefineSpecial:
                        throw new NotImplementedException("RefineStigmataRuneReqHandler StigmataRefineType.StigmataRefineSpecial");
                    case StigmataRefineType.StigmataRefineLock:
                        throw new NotImplementedException("RefineStigmataRuneReqHandler StigmataRefineType.StigmataRefineLock");
                }

                session.Player.Equipment.Save();
                session.Send(Packet.FromProto(Rsp, CmdId.RefineStigmataRuneRsp));

                session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
            }
        }

        StigmataRune GenerateRune()
            => new()
            {
                RuneId = GenerateAffixType(),
                StrengthPercent = CalculateAffixStrength()
            };

        uint GenerateAffixType()
            => AffixIds[new Random().Next(0, AffixIds.Length - 1)];

        uint CalculateAffixStrength(float min = 37.5f, float step = 0.625f)
            => (uint)(min + ((float)new Random().NextDouble() * step * 100f));
    }
}
