using System.Collections.Generic;
using Cx.Client.PluginManager;

namespace WCFPlugin.CxPlugin
{
  [Plugin]
  public class ClientsBountyPluginInfo : PluginInfo
  {
      protected override void GetTools(IToolItems tools)
      {
      }

    protected override IEnumerable<IManagerInfo> GetManagers()
    {
        return new IManagerInfo[] { new SimpleManagerInfo<WCFManager>() };
    }
  }
}