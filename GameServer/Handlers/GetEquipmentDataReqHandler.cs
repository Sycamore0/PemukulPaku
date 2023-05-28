using Common.Database;
using Common.Resources.Proto;
using MongoDB.Driver;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetEquipmentDataReq)]
    internal class GetEquipmentDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            EquipmentScheme Equipment = session.Player.Equipment;

            GetEquipmentDataRsp Rsp = new()
            {
                retcode = GetEquipmentDataRsp.Retcode.Succ,
                VitalityValue = 0,
                IsAll = true
            };

            Rsp.MaterialLists.AddRange(Equipment.MaterialList.ToList());
            Rsp.MechaLists.AddRange(Equipment.MechaList.ToList());
            Rsp.StigmataLists.AddRange(Equipment.StigmataList.ToList());
            Rsp.WeaponLists.AddRange(Equipment.WeaponList.ToList());

            session.Send(Packet.FromProto(Rsp, CmdId.GetEquipmentDataRsp));
        }
    }
}
