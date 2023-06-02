using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.AddAvatarExpByMaterialReq)]
    internal class AddAvatarExpByMaterialReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            AddAvatarExpByMaterialReq Data = packet.GetDecodedBody<AddAvatarExpByMaterialReq>();
            MaterialDataExcel? materialData = MaterialData.GetInstance().FromId(Data.MaterialId);
            AvatarScheme? avatar = session.Player.AvatarList.FirstOrDefault(avatar => avatar.AvatarId == Data.AvatarId);

            AddAvatarExpByMaterialRsp Rsp = new() { retcode = AddAvatarExpByMaterialRsp.Retcode.Succ };

            if (avatar is not null && materialData is not null)
            {
                session.Player.Equipment.AddMaterial(materialData.Id, -(int)Data.MaterialNum);
                GetEquipmentDataRsp EquipmentRsp = new()
                {
                    retcode = GetEquipmentDataRsp.Retcode.Succ,
                    VitalityValue = 0,
                    IsAll = true
                };
                EquipmentRsp.MaterialLists.Add(session.Player.Equipment.MaterialList.First(mat => mat.Id == materialData.Id));

                avatar.AddExp((uint)materialData.CharacterExpProvide * Data.MaterialNum);

                session.Send(Packet.FromProto(EquipmentRsp, CmdId.GetEquipmentDataRsp));
                session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { avatar.AvatarId } }, CmdId.GetAvatarDataReq));
            }
            else
            {
                Rsp.retcode = AddAvatarExpByMaterialRsp.Retcode.AvatarNotExist;
            }
            session.Send(Packet.FromProto(Rsp, CmdId.AddAvatarExpByMaterialRsp));
        }
    }
}
