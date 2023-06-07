using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetCustomHeadDataReq)]
    internal class GetCustomHeadDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetCustomHeadDataRsp Rsp = new() { retcode = GetCustomHeadDataRsp.Retcode.Succ, IsAll = true };
            Rsp.CustomHeadLists.AddRange(CustomHeadData.GetInstance().All.Where(x => x.HeadParaInt == 0 || session.Player.AvatarList.Select(x => x.AvatarId).ToList().Contains((uint)x.HeadParaInt) || session.Player.AvatarList.SelectMany(x => x.DressLists).ToList().Contains((uint)x.HeadParaInt)).Select(x => new CustomHead() { Id = (uint)x.HeadId }));

            session.Send(Packet.FromProto(Rsp, CmdId.GetCustomHeadDataRsp));
        }
    }
}