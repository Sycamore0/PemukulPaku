using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.GetWarshipDataReq)]
    internal class GetWarshipDataReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetWarshipDataRsp Rsp = new()
            {
                retcode = GetWarshipDataRsp.Retcode.Succ,
                IsAll = true
            };

            List<WarshipThemeData> Warships = new()
            {
                new()
                {
                    WarshipId = 0,
                    BgmPlayMode = 0,
                    IsWeatherFixed = false,
                    WeatherIdx = 0
                },
                new()
                {
                    WarshipId = 400002,
                    BgmPlayMode = 0,
                    IsWeatherFixed = false,
                    WeatherIdx = 0
                },
                new()
                {
                    WarshipId = 400003,
                    BgmPlayMode = 0,
                    IsWeatherFixed = false,
                    WeatherIdx = 0
                },
                new()
                {
                    WarshipId = 400004,
                    BgmPlayMode = 0,
                    IsWeatherFixed = false,
                    WeatherIdx = 0
                },
                new()
                {
                    WarshipId = 400006,
                    BgmPlayMode = 0,
                    IsWeatherFixed = false,
                    WeatherIdx = 0
                }
            };
            Rsp.WarshipLists.AddRange(Warships);

            session.Send(Packet.FromProto(Rsp, CmdId.GetWarshipDataRsp));
        }
    }
}
