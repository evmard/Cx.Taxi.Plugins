using System;
using System.IO;
using System.Xml.Serialization;

namespace Cx.Client.Taxi.ClientsBounty
{
    using Data;
    using Data.DataObject;
    using CallManagement;
    using Managers;
    using Utils;

    using Clients.Data;
    using Billings.Data;
    using Orders.Data;
    using ClientManager;

    using OrderRoutine;

    using Enums;
    
  public class ClientsBountyManager : Manager
  {
      private IOrders _orders;
      private IBillings _billings;
      private OrderRoutine _orderRoutine;

      private PluginParams _param;
      public override void OnInitialize()
      {
          base.OnInitialize();
          try
          {
              _orderRoutine = OrderRoutine.GetOrderRoutine();
              _orders = _orderRoutine.GetOrders();

              _billings = GlobalDataObjects.GetGlobalDataObject("Cx.Client.Taxi.Billings.Data.Billings") as IBillings;
              if (_billings == null)
              {
                  Utils.GlobalLogManager.WriteString("Error: Ошибка в плагине ClientsBountyManager. Не удалось загрузить данные по счетам");
              }

              _orders.StatePropertyChanged += _orders_StatePropertyChanged;
          }
          catch (Exception e)
          {
              Utils.GlobalLogManager.WriteString("Error: Ошибка в плагине ClientsBountyManager. {0}", e);
          }

          LoadParams();
          
      }

      private void LoadParams()
      {
          try 
          {
              string fileName = Utils.GlobalUtils.AppDirectory + "\\ClientsBountyParams.xml";
              Utils.GlobalLogManager.WriteString("Info: Загрузка параметров плагина ClientsBountyManager. fileName = {0}", fileName);
              using (Stream stream = new FileStream(fileName, FileMode.Open))
              {
                  XmlSerializer serializer = new XmlSerializer(typeof(PluginParams));
                  _param = (PluginParams)serializer.Deserialize(stream);
              }
          }
          catch (Exception e)
          {
              Utils.GlobalLogManager.WriteString("Warning: Не удалось загрузить параметры плагина ClientsBountyManager. {0}", e);
              _param = new PluginParams();
          }

      }

      void _orders_StatePropertyChanged(object sender, CxPropertyChangedEventArgs<IOrder> e)
      {
          var order = e.Object;
          if (order.State == OrderStates.Paid &&                //Заказ оплачен
              order.IDService == _param.IDService &&            //с указанной услугой
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

              var bountySumm = order.Cost.Value * _param.Procent;

              client.Billing.Balance += bountySumm; //Пополняем счет клиента
              PaymentRoutine.PaymentRoutine.AddBillingLogsInThreadPool( //И записываем в лог
                  bountySumm,
                  BillingTypes.Order,
                  client.Billing.ID,
                  order.ID,
                  string.Empty,
                  _param.BountyDescription,
                  client.Billing.Balance ?? 0,
                  BillingLogType.Client,
                  order.Number);

              string number = ClientManager.GetManager().GetDefaultPhone(client.ID);
              string message = string.Format(_param.MessageTemplate, bountySumm);

              if (!string.IsNullOrWhiteSpace(number))
              {
                  if (_param.NeedMakeCall)
                      CallManagementInterface.StartAutoinformator(number, message, order.ID);

                  if (_param.NeedSendSMS)
                      CallManagementInterface.SendSMS(number, message, order.ID);
              }
              else
              {
                  Utils.GlobalLogManager.WriteString("Warning: ClientsBountyManager. У клиента не указан номер телефона");
              }
          }

      }
  }
}