using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using ProtoBuf;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.StageEndReq)]
    internal class StageEndReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            StageEndReq Data = packet.GetDecodedBody<StageEndReq>();
            StageEndReqBody DecodedBody = Serializer.Deserialize<StageEndReqBody>(Data.Body.AsSpan());
            StageDataExcel? StageData = Common.Utils.ExcelReader.StageData.GetInstance().FromId((int)DecodedBody.StageId);

            if(StageData == null)
            {
                session.c.Error("StageData Excel Is Bad Please Fix");
                session.Send(Packet.FromProto(new StageEndRsp() { retcode = StageEndRsp.Retcode.StageError }, CmdId.StageEndRsp));
                return;
            }
            else if(StageData.LevelId == 10101)
            {
                session.Player.User.IsFirstLogin = false;
            }

            StageEndRsp Rsp = new()
            {
                retcode = StageEndRsp.Retcode.Succ,
                StageId = DecodedBody.StageId,
                EndStatus = DecodedBody.EndStatus
            };

            if(DecodedBody.EndStatus == StageEndStatus.StageWin)
            {
                EquipmentScheme Equipment = session.Player.Equipment;

                foreach (DropItem DropItem in DecodedBody.DropItemLists)
                {
                    Equipment.AddMaterial((int)DropItem.ItemId, (int)DropItem.Num);
                }

                session.Player.User.Hcoin += DecodedBody.ChallengeIndexLists.Length * 5;

                session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq() { }, CmdId.GetEquipmentDataReq));
                session.ProcessPacket(Packet.FromProto(new GetWorldMapDataReq() { }, CmdId.GetWorldMapDataReq));
                session.ProcessPacket(Packet.FromProto(new ChapterGroupGetDataReq() { }, CmdId.ChapterGroupGetDataReq));

                Rsp.PlayerExpReward = 100;
                Rsp.AvatarExpReward = DecodedBody.AvatarExpReward;
                Rsp.ScoinReward = DecodedBody.ScoinReward;
                Rsp.ChallengeLists.AddRange(DecodedBody.ChallengeIndexLists.Select(challengeIndex => new StageChallengeData() { ChallengeIndex = challengeIndex, Reward = new() { Hcoin = 5 } }));
            }

            session.Send(Packet.FromProto(Rsp, CmdId.StageEndRsp));
        }
    }
}
