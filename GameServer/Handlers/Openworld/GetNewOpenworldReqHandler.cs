using System;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld
{
    [PacketCmdId(CmdId.GetNewOpenworldReq)]
    internal class GetNewOpenworldReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetNewOpenworldRsp Rsp = new()
            {
                retcode = GetNewOpenworldRsp.Retcode.Succ,
                DataType = OpenworldDataType.OpenworldDataAll,
                QuestLevel = 1,
                QuestStar = 0,
                MaxQuestLevel = 1,
                NextRefreshTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 3,
                CloseTime = (uint)Global.GetUnixInSeconds() + 3600 * 24 * 3,
                GlobalRandomSeed = Global.GetRandomSeed()
            };
            Rsp.Techs.AddRange(OpenWorld.ShowMapList.Select(x => new OpenworldTechData() { MapId = x }));
            Rsp.MapLists.AddRange(session.Player.OpenWorlds.Select(ow => new OpenworldMapBriefData() { 
                MapId = ow.MapId,
                Status = 3,
                Cycle = ow.Cycle,
                QuestLevel = ow.QuestLevel,
                HasTakeFinishRewardCycle = ow.HasTakeFinishRewardCycle 
            }));

            session.Send(Packet.FromProto(Rsp, CmdId.GetNewOpenworldRsp));
        }
    }
}
