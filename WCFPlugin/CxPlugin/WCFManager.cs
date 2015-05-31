using System;
using System.IO;
using System.ServiceModel;
using Cx.Client.Data.DataObject;
using Cx.Client.Managers;
using Cx.Client.Security.Users.Data;
using Cx.Client.Taxi.Billings.Data;
using Cx.Client.Utils;
using WCFPlugin.Contract;

namespace WCFPlugin.CxPlugin
{
    public class WCFManager : Manager
    {
        private PluginParams _param;
        private readonly Object _paramLock = new Object();
        private IBillings _billings;
        private IUsers _users;
        private FileSystemWatcher _watcher;

        public override void OnInitialize()
        {
            LoadParams();

            _billings = GlobalDataObjects.GetGlobalDataObject("Cx.Client.Taxi.Billings.Data.Billings") as IBillings;
            if (_billings == null)
            {
                GlobalLogManager.WriteString(
                    "Error: Ошибка в плагине WCFManager. Не удалось загрузить данные по счетам");
            }

            _users = GlobalDataObjects.GetGlobalDataObject("Cx.Client.Security.Users.Data.Users") as IUsers;
            if (_users == null)
            {
                GlobalLogManager.WriteString(
                    "Error: Ошибка в плагине WCFManager. Не удалось загрузить данные по пользователям");
            }

            var httpHostStr = string.Format("{0}:{1}/ClientBonusService", _param.HostName, _param.Port);
            var tcpHostStr = string.Format("{0}:{1}/ClientBonusService", _param.HostName, _param.Port + 1);
            GlobalLogManager.WriteString("WCFManager. Создание сервиса");
            var serviceHost = new ServiceHost(typeof (ClientBonusService));
            GlobalLogManager.WriteString("WCFManager. Установка параметров");
            ClientBonusService.DataProvider = new CxDataProvider(_param, _billings, _users);
            ClientBonusService.LogonManager = new LogonManager(GlobalUtils.AppDirectory);
            ClientBonusService.Params = _param;
            var portsharingBinding = new NetTcpBinding();
            var httpBinding = new BasicHttpBinding();
            serviceHost.AddServiceEndpoint(typeof(IClientBonusService), portsharingBinding, "net.tcp://" + tcpHostStr);
            serviceHost.AddServiceEndpoint(typeof(IClientBonusService), httpBinding, "http://" + httpHostStr);
            GlobalLogManager.WriteString("WCFManager. Запуск сервиса");
            serviceHost.Open();

            _watcher = new FileSystemWatcher(GlobalUtils.AppDirectory, PluginParams.FileName);
            _watcher.Changed += _watcher_Changed;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.EnableRaisingEvents = true;
            GlobalLogManager.WriteString("WCFManager. Сервис запущен");
        }

        void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            LoadParams();
            ClientBonusService.Params = _param;
        }

        private void LoadParams()
        {
            GlobalLogManager.WriteString("WCFManager. Загрузка параметров");
            lock (_paramLock)
            {
                _param = PluginParams.LoadParams(GlobalUtils.AppDirectory);
            }
            GlobalLogManager.WriteString("WCFManager. Загрузка параметров выполнена");
        }
    }
}