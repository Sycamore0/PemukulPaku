using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.DressEquipmentReq)]
    internal class DressEquipmentReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            DressEquipmentReq Data = packet.GetDecodedBody<DressEquipmentReq>();
            DressEquipmentRsp Rsp = new() { retcode = DressEquipmentRsp.Retcode.Succ };

            AvatarScheme? avatar = session.Player.AvatarList.FirstOrDefault(avatar => avatar.AvatarId == Data.AvatarId);
            if (avatar == null) 
            {
                Rsp.retcode = DressEquipmentRsp.Retcode.AvatarNotExist;
            }
            else
            {
                switch (Data.Slot)
                {
                    case EquipmentSlot.EquipmentSlotWeapon1:
                        avatar.WeaponUniqueId = Data.UniqueId;
                        break;
                    case EquipmentSlot.EquipmentSlotStigmata1:
                        avatar.StigmataUniqueId1 = Data.UniqueId;
                        break;
                    case EquipmentSlot.EquipmentSlotStigmata2:
                        avatar.StigmataUniqueId2 = Data.UniqueId;
                        break;
                    case EquipmentSlot.EquipmentSlotStigmata3:
                        avatar.StigmataUniqueId3 = Data.UniqueId;
                        break;
                    default:
                        Rsp.retcode = DressEquipmentRsp.Retcode.EquipmentSlotError;
                        break;
                }
                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { avatar.AvatarId } }, CmdId.GetAvatarDataReq));
            }

            session.Send(Packet.FromProto(Rsp, CmdId.DressEquipmentRsp));
        }
    }
}
