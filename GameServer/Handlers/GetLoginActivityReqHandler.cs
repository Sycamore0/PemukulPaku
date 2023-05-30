using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetLoginActivityReq)]
    internal class GetLoginActivityReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetLoginActivityRsp Rsp = new () { retcode = GetLoginActivityRsp.Retcode.Succ };

            Rsp.LoginLists.Add(new LoginActivityData
            {
                Id = 581,
                LoginDays = 1,
                AcceptTime = session.Player.User.GetCreationTime(),
                DurationEndTime = session.Player.User.GetCreationTime() + 604800 * 2
            });

            session.Send(Packet.FromProto(Rsp, CmdId.GetLoginActivityRsp));
        }
    }
}
