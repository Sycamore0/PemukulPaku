using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetWarshipItemDataReq)]
    internal class GetWarshipItemDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetWarshipItemDataRsp Rsp = new()
            {
                retcode = GetWarshipItemDataRsp.Retcode.Succ,
                IsAll = true,
                WarshipItemIdLists = EntryThemeItemData.GetInstance().All.Select(theme => (uint)theme.ThemeItemId).ToArray()
            };

            session.Send(Packet.FromProto(Rsp, CmdId.GetWarshipItemDataRsp));
        }
    }
}
