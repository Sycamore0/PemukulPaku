using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers
{
    [PacketCmdId(CmdId.SelectNewStigmataRuneReq)]
    internal class SelectNewStigmataRuneReqHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SelectNewStigmataRuneReq Data = packet.GetDecodedBody<SelectNewStigmataRuneReq>();
            SelectNewStigmataRuneRsp Rsp = new() { retcode = SelectNewStigmataRuneRsp.Retcode.Succ, SelectUniqueId = Data.SelectUniqueId, IsSelect = Data.IsSelect };
            uint uid = Data.UniqueId;
            Stigmata? stigmata = session.Player.Equipment.StigmataList.FirstOrDefault(stig => stig.UniqueId == uid);
            //Packet.c.Log($"SelectNewStigmataRuneReqHandler: {Data.UniqueId} {Data.SelectUniqueId} {Data.IsSelect}");

            if (stigmata is not null)
            {
                if(Data.IsSelect)
                {                    
                    if(stigmata.WaitSelectRuneGroupLists is not null)
                    {
                        stigmata.RuneLists.Clear();
                        foreach(var groups in stigmata.WaitSelectRuneGroupLists)
                        {
                            if(groups.UniqueId == Data.SelectUniqueId)
                            {
                                for (var i = 0; i < groups.RuneLists.Count; i++)
                                {
                                    var group = groups.RuneLists[i];
                                    if(group is null) continue;
                                    stigmata.RuneLists.Add(group);
                                }
                            }
                        }
                    }
                }
                stigmata.WaitSelectRuneGroupLists?.Clear();
            }
            session.Player.Equipment.Save();
            session.Send(Packet.FromProto(Rsp, CmdId.SelectNewStigmataRuneRsp));
        }
    }
}
