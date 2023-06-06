using Common.Resources.Proto;
using MongoDB.Bson;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.RefineStigmataRuneReq)]
    internal class RefineStigmataRuneReqHandler : IPacketHandler
    {
         
        static readonly uint[] AffixIds = { 30010, 30020, 30030, 30040, 30050, 30060, 30070, 30080, 30090, 30100, 30110, 30120, 30130, 30140, 30150, 30160, 30170, 30180, 30190, 30200, 30210, 30220, 40010, 40020, 40030, 40040, 50010, 50020, 50030, 50040 };

        public void Handle(Session session, Packet packet)
        {
            
            RefineStigmataRuneReq Data = packet.GetDecodedBody<RefineStigmataRuneReq>();
            RefineStigmataRuneRsp Rsp = new() { retcode = RefineStigmataRuneRsp.Retcode.Succ, TimesType = StigmataRefineTimesType.StigmataRefineTimesOne };
            uint uid = Data.UniqueId;
            //Packet.c.Log($"RefineStigmataRuneReqHandler: {Data.UniqueId} {Data.Type} {Data.SpecialId}");
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

                Packet.c.Log(message: $"{rune.ToJson()}{rune2.ToJson()}");

                switch (Data.Type)
                {
                    case StigmataRefineType.StigmataRefineNormal:
                        if (Data.IsRetry) stigmata.WaitSelectRuneGroupLists.Clear();
                        if (stigmata.SlotNum < 2 && new Random().Next(0, 10) == 9) stigmata.SlotNum++;
                        group.RuneLists.Add(rune);
                        if (stigmata.SlotNum == 2) group.RuneLists.Add(rune2);
                        stigmata.WaitSelectRuneGroupLists.Add(group);
                        Rsp.RuneGroupLists.Add(group);
                        break;
                    case StigmataRefineType.StigmataRefineAddSlot: //broken when doing the 2nd slot for some reason
                        //stigmata.RuneLists.Add(rune);
                        stigmata.SlotNum++;
                        group.RuneLists.Add(rune);
                        stigmata.WaitSelectRuneGroupLists.Add(group);
                        Rsp.RuneGroupLists.Add(group);
                        break;
                    case StigmataRefineType.StigmataRefineSpecial:
                        rune = GenerateRuneSpecial(1,Data.SpecialId);
                        rune2 = GenerateRuneSpecial(2,Data.SpecialId);
                        group.RuneLists.Add(rune);
                        group.RuneLists.Add(rune2);
                        stigmata.WaitSelectRuneGroupLists.Add(group);
                        Rsp.RuneGroupLists.Add(group);
                        break;
                    case StigmataRefineType.StigmataRefineLock:
                        if (Data.IsRetry) 
                        {
                            //need to add retry logic
                        }
                        //break;
                        throw new NotImplementedException("RefineStigmataRuneReqHandler StigmataRefineType.StigmataRefineLock");
                }
                session.Player.Equipment.Save();
                session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
                session.Send(Packet.FromProto(Rsp, CmdId.RefineStigmataRuneRsp));
            }
        }

        private StigmataRune GenerateRune()
            => new()
            {
                RuneId = GenerateAffixId(),
                StrengthPercent = GenerateAffixStrength()
            };
        private StigmataRune GenerateRuneSpecial(uint slot, uint id = 1) => new()
        {
            RuneId = GenerateAffixIdSpecial(id, slot),
            StrengthPercent = GenerateAffixStrengthSpecial(id, slot)
        };

        private uint GenerateAffixId(int min = 1, int max = 4)
            => ((uint)AffixIds[new Random().Next(0, AffixIds.Length - 1)] + (uint)new Random().Next(
                min < max && min > 0 ? min : 1, max > min && max < 5 ? max : 4
            ));
        //currently ignores Stigmata Rarity

        private uint GenerateAffixStrength(int min=1, int max = 101)
            => (uint)(new Random().Next(
                min > 0 && min < max ? min : 1, max > min && max < 102 ? max : 101
            ));
        private uint GenerateAffixIdSpecial(uint id, uint slot)
        {
            switch (id)
            {
                //case 3:
                //case 14:
                case 14:
                    return 30013; //Max HP for now. :P
                //Attribute ATK
                case 4: case 8:
                    return 30063;
                case 5: case 9:
                    return 30073;
                case 6: case 10:
                    return 30083;
                case 7: case 11:
                    return 30193;
                case 12: case 13:
                    return 30213;
                //SP
                case 15:
                    return (uint)(slot == 2 ? 50043 : 50033);
                default:
                    return 50043; // -SP%
            }
            
        }
        private uint GenerateAffixStrengthSpecial(uint id, uint slot)
        {
            switch (id)
            {
                case 3: case 14:
                    return GenerateAffixStrength();
                case 8: case 9: case 10: case 11: case 13:
                    return GenerateAffixStrength(7, 66);
                case 15:
                    return (uint)(slot == 2 ? 59 : 45);//want to double check these with live version
                default:
                    return 100;
            }
        }
        //Specials (ID, Type, Roll) | (Affix ID, Strength)
        //3 Offensive Random    |   
        //14 Support Random     |   
        //15 SP -2.7% .4/sp     |   (50043, 59) (50033, 45)
        //4  BIO  23            |   30063, 100
        //5  MECH 23            |   30073, 100
        //6  PSY  23            |   30083, 100
        //7  QUA  23            |   30193, 100
        //8  BIO  15-20         |   30063, 7,66
        //9  MECH 15-20         |   30073, 7,66
        //10 PSY  15-20         |   30083, 7,66
        //11 QUA  15-20         |   30193, 7,66
        //12 IMG  23            |   30213, 100
        //13 IMG  15-20         |   30213, 7,66
    }
}
