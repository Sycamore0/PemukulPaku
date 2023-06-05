using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.UseMaterialReq)]
    internal class UseMaterialReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            UseMaterialReq Data = packet.GetDecodedBody<UseMaterialReq>();
            MaterialDataExcel? materialData = MaterialData.GetInstance().FromId(Data.MaterialId);
            MaterialUseDataExcel? useData = MaterialUseData.GetInstance().FromId(materialData?.UseId ?? 0);
            UseMaterialRsp Rsp = new() { retcode = UseMaterialRsp.Retcode.ConsumeItemNotExist };

            Common.Resources.Proto.Material costMaterial = session.Player.Equipment.AddMaterial((int)Data.MaterialId, -(int)Data.Num);
            GetEquipmentDataRsp equipmentRsp = new() { retcode = GetEquipmentDataRsp.Retcode.Succ, VitalityValue = 5900 };
            equipmentRsp.MaterialLists.Add(costMaterial);

            if (useData is not null)
            {
                ParameterData parameterData = GetParameterData(useData, (int)Data.Parameter);
                RewardDataExcel? rewardData = Common.Utils.ExcelReader.RewardData.GetInstance().FromId(parameterData.RewardId);
                Rsp.retcode = UseMaterialRsp.Retcode.Succ;

                Common.Resources.Proto.RewardData reward = new() { };
                
                switch(useData.UseType)
                {
                    case (int)MaterialUseType.MaterialUseAvatarFragmentTransform:
                        AvatarDataExcel? avatarData = AvatarData.GetInstance().All.FirstOrDefault(x => x.AvatarFragmentId == parameterData.RewardId);
                        if (avatarData is not null)
                        {
                            reward.ItemLists.Add(new()
                            {
                                Id = (uint)parameterData.RewardId,
                                Num = (uint)parameterData.Num * Data.Num
                            });
                            AvatarScheme? avatar = session.Player.AvatarList.FirstOrDefault(avatar => avatar.AvatarId == avatarData.AvatarId);
                            avatar?.AddFragment((uint)parameterData.Num * Data.Num);
                            session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq() { AvatarIdLists = new uint[] { (uint)avatarData.AvatarId } }, CmdId.GetAvatarDataReq));
                        }
                        break;
                    default:
                        Rsp.retcode = UseMaterialRsp.Retcode.FeatureClosed;
                        break;
                }

                Rsp.GiftRewardLists.Add(reward);
            }

            session.Send(Packet.FromProto(equipmentRsp, CmdId.GetEquipmentDataRsp), Packet.FromProto(Rsp, CmdId.UseMaterialRsp));
        }

        private static ParameterData GetParameterData(MaterialUseDataExcel useData, int clientPara)
        {
            int Num = 1;
            if (clientPara == 0)
            {
                if(useData.ParaStr[0].Contains(":"))
                {
                    Num = int.Parse(useData.ParaStr[0].Split(":")[1]);
                    clientPara = int.Parse(useData.ParaStr[0].Split(":")[0]);
                }
                else
                {
                    clientPara = int.Parse(useData.ParaStr[0]);
                }
            }

            return new ParameterData(clientPara, Num);
        }

        private struct ParameterData
        {
            public ParameterData(int rewardId, int num)
            {
                Num = num;
                RewardId = rewardId;
            }

            public int RewardId { get; set; }
            public int Num { get; set; }
        }
    }
}
