using System.Collections.Generic;
using Cx.Client.PluginManager;

namespace Cx.Client.Taxi.ClientsBounty
{
  [Plugin]
  public class ClientsBountyPluginInfo : PluginInfo
  {
      protected override void GetTools(IToolItems tools)
      {
      }

    protected override IEnumerable<IManagerInfo> GetManagers()
    {
        return new IManagerInfo[] { new SimpleManagerInfo<ClientsBountyManager>() };
    }
  }
}