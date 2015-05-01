using System;
using System.IO;
using System.Xml.Serialization;
using Cx.Client.CallManagement;
using Cx.Client.Data;
using Cx.Client.Data.DataObject;
using Cx.Client.Managers;
using Cx.Client.Taxi.Billings.Data;
using Cx.Client.Taxi.Enums;
using Cx.Client.Taxi.Orders.Data;
using Cx.Client.Utils;
using Cx.Client.Utils.Server;

namespace Cx.Client.Taxi.ClientsBounty
{
    public class ClientsBountyManager : Manager
  {
      private IOrders _orders;
      private IBillings _billings;
      private OrderRoutine.OrderRoutine _orderRoutine;

      private PluginParams _param;
      private readonly Object _paramLock = new Object();
      private FileSystemWatcher _watcher;

      public override void OnInitialize()
      {
          base.OnInitialize();
          try
          {
              _orderRoutine = OrderRoutine.OrderRoutine.GetOrderRoutine();
              _orders = _orderRoutine.GetOrders();

              _billings = GlobalDataObjects.GetGlobalDataObject("Cx.Client.Taxi.Billings.Data.Billings") as IBillings;
              if (_billings == null)
              {
                  GlobalLogManager.WriteString("Error: Ошибка в плагине ClientsBountyManager. Не удалось загрузить данные по счетам");
              }

              _orders.StatePropertyChanged += _orders_StatePropertyChanged;
          }
          catch (Exception e)
          {
              GlobalLogManager.WriteString("Error: Ошибка в плагине ClientsBountyManager. {0}", e);
          }

          LoadParams();

          _watcher = new FileSystemWatcher(GlobalUtils.AppDirectory, "ClientsBountyParams.xml");
          _watcher.NotifyFilter = NotifyFilters.LastWrite;
          _watcher.Changed += watcher_Changed;
          _watcher.EnableRaisingEvents = true;         
      }

      private void watcher_Changed(object sender, FileSystemEventArgs e)
      {
          LoadParams();
      }

      private void LoadParams()
      {
          try 
          {
              string fileName = GlobalUtils.AppDirectory + "\\ClientsBountyParams.xml";
              GlobalLogManager.WriteString("Info: Загрузка параметров плагина ClientsBountyManager. fileName = {0}", fileName);
              using (Stream stream = new FileStream(fileName, FileMode.Open))
              {
                  XmlSerializer serializer = new XmlSerializer(typeof(PluginParams));
                  var temp = (PluginParams)serializer.Deserialize(stream);
                  lock(_paramLock)
                  {
                    _param = new PluginParams(temp);
                  }
              }
          }
          catch (Exception e)
          {
              GlobalLogManager.WriteString("Warning: Не удалось загрузить параметры плагина ClientsBountyManager. {0}", e);
              lock (_paramLock)
              {
                  _param = new PluginParams();
              }
          }
      }

      void _orders_StatePropertyChanged(object sender, CxPropertyChangedEventArgs<IOrder> e)
      {
          PluginParams param;
          lock (_paramLock)
          {
              param = new PluginParams(_param);
          }

          var order = e.Object;
          if (order.State == Enums.OrderStates.Paid &&                //Заказ оплачен
              order.IDService == param.IDService &&            //с указанной услугой
              order.Cost.HasValue && order.Cost.Value > 0.0 &&  //и не нулевой стоимостью
              order.Client != null) 
          {
              var client = order.Client;
              if (client.IDBilling == null) //Если у клиента еще нет счета, то создаем его
              {
                  client.Billing = _billings.AddNew(item =>
                  {
                      item.ID = DbUtils.GenID();
                      item.Account = item.ID.ToString();
                      item.Balance = 0;
                      item.Limit = 0;
                      item.OwnerType = BillingLogType.Client;
                  });
              }

              var bountySumm = order.Cost.Value * param.Procent;

              client.Billing.Balance += bountySumm; //Пополняем счет клиента
              PaymentRoutine.PaymentRoutine.AddBillingLogsInThreadPool( //И записываем в лог
                  bountySumm,
                  BillingTypes.Order,
                  client.Billing.ID,
                  order.ID,
                  string.Empty,
                  param.BountyDescription,
                  client.Billing.Balance ?? 0,
                  BillingLogType.Client,
                  order.Number);

              string number = ClientManager.ClientManager.GetManager().GetDefaultPhone(client.ID);
              string message = string.Format(param.MessageTemplate, bountySumm);

              if (!string.IsNullOrWhiteSpace(number))
              {
                  if (param.NeedMakeCall)
                      CallManagementInterface.StartAutoinformator(number, message, order.ID);

                  if (param.NeedSendSMS)
                      CallManagementInterface.SendSMS(number, message, order.ID);
              }
              else
              {
                  GlobalLogManager.WriteString("Warning: ClientsBountyManager. У клиента не указан номер телефона");
              }
          }

      }
  }
}