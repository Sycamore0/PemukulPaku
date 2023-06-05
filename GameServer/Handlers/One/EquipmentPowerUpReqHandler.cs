using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.EquipmentPowerUpReq)]
    internal class EquipmentPowerUpReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            EquipmentPowerUpReq Data = packet.GetDecodedBody<EquipmentPowerUpReq>();
            EquipmentPowerUpRsp Rsp = new() { retcode = EquipmentPowerUpRsp.Retcode.Succ, MainItem = Data.MainItem, BoostRate = 100 };

            GetEquipmentDataRsp EquipmentDataRsp = new() { retcode = GetEquipmentDataRsp.Retcode.Succ, VitalityValue = 5900 };
            switch (Data.MainItem.Type)
            {
                case EquipmentType.EquipmentWeapon:
                    Material[] WeaponUpConsumeItems = session.Player.Equipment.AddWeaponExpByConsumeItem(Data.MainItem.IdOrUniqueId, Data.ConsumeItemList.ItemLists);

                    EquipmentDataRsp.MaterialLists.AddRange(WeaponUpConsumeItems);
                    EquipmentDataRsp.WeaponLists.Add(session.Player.Equipment.WeaponList.FirstOrDefault(weapon => weapon.UniqueId == Data.MainItem.IdOrUniqueId));
                    break;
                case EquipmentType.EquipmentStigmata:
                    Material[] StigmataUpConsumeItems = session.Player.Equipment.AddStigmataExpByConsumeItem(Data.MainItem.IdOrUniqueId, Data.ConsumeItemList.ItemLists);

                    EquipmentDataRsp.MaterialLists.AddRange(StigmataUpConsumeItems);
                    EquipmentDataRsp.StigmataLists.Add(session.Player.Equipment.StigmataList.FirstOrDefault(stigmata => stigmata.UniqueId == Data.MainItem.IdOrUniqueId));
                    break;
                default:
                    Rsp.retcode = EquipmentPowerUpRsp.Retcode.MainItemNotExist;
                    break;
            }
            session.Send(Packet.FromProto(EquipmentDataRsp, CmdId.GetEquipmentDataRsp), Packet.FromProto(Rsp, CmdId.EquipmentPowerUpRsp));
        }
    }
}
