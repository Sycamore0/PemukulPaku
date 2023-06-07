using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetMainDataReq)]
    internal class GetMainDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetMainDataReq Data = packet.GetDecodedBody<GetMainDataReq>();
            UserScheme User = session.Player.User;
            PlayerLevelData.LevelData levelData = PlayerLevelData.GetInstance().CalculateLevel(User.Exp);

            GetMainDataRsp Rsp = new()
            {
                retcode = GetMainDataRsp.Retcode.Succ,
                AssistantAvatarId = (uint)User.AssistantAvatarId,
                Birthday = (uint)User.BirthDate,
                Nickname = User.Nick,
                Level = (uint)levelData.Level,
                Exp = (uint)levelData.Exp,
                FreeHcoin = (uint)User.Hcoin,
                Hcoin = (uint)User.Hcoin,
                CustomHeadId = (uint)User.CustomHeadId,
                Scoin = session.Player.Equipment.MaterialList.Where(mat => mat.Id == 100).FirstOrDefault()?.Num ?? 0,
                IsAll = true,
                RegisterTime = User.GetCreationTime(),
                PayHcoin = 0,
                WarshipAvatar = User.WarshipAvatar,
                SelfDesc = User.SelfDesc,
                UseFrameId = User.FrameId < 200001 ? 200001 : (uint)User.FrameId,
                OnPhonePendantId = 350005,
                Stamina = (uint)User.Stamina,
                StaminaRecoverConfigTime = 360,
                StaminaRecoverLeftTime = 360,
                EquipmentSizeLimit = 1000,
                OpenPanelActivityLists = new uint[] { 2 },
                ChatworldActivityInfo = new()
                {
                    IsHasNpcRedEnvelope = false,
                    TreasureScheduleId = 0
                },
                IsAllowCostSeniorEquipOnCurDevice = true,
                TypeLists = new uint[] { 2, 3, 4, 5, 6, 7, 8, 9, 14, 16, 17, 18, 19, 20, 21, 22, 23, 24, 26, 27, 28, 29, 30, 31, 32, 33, 35, 36, 37, 38, 39 },
                LevelLockId = 1,
                Mcoin = 0,
                MonthRechargePrice = 0,
                WarshipTheme = new ()
                {
                    WarshipId = (uint)User.WarshipId
                },
                TotalLoginDays = 1,
                NextEvaluateTime = 0,
                OnMedalId = 0,
                TodayRechargePrice = 0,
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetMainDataRsp));
        }
    }
}
