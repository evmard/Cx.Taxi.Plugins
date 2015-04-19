# Cx.Taxi.Plugins - Плагины для такси

### ClientsBountyPlugin
####Описание плагина:
* Отслеживает завершение заказа со статусом «оплачено» и указанной в параметрах услугой.
* Проверяет, есть ли у клиента счет, если нет, то создает его.
* Пополняет баланс клиента на сумму равную [стоимость заказ] * [Procent].
* Добавляет в лог счета информацию о пополнении с описанием [BountyDescription].
* И осуществляет оповещение клиента с помощью автоинформатора и/или СМС с текстом [MessageTemplate]
* Все ошибки или предупреждения пишутся в лог бизнес-сервера с тегом «ClientsBountyManager»

####Описание файла настроек:
* \<IDService>5252428308\</IDService> <i>Идентификатор услуги</i>
* \<Procent>0.025\</Procent> <i>Процент от стоимости заказа</i>
* \<BountyDescription>Вознаграждение за заказ.\</BountyDescription> <i>Строка описания для записи в лог операций со счетом</i>
* \<MessageTemplate>На Ваш счет начислено {0} рублей.\</MessageTemplate> <i>Строка для оповещения клиента</i>
* \<NeedMakeCall>true\</NeedMakeCall> <i>Указывает необходимость оповещать клиента через автоинформатор (true, false)</i>
* \<NeedSendSMS>true\</NeedSendSMS> <i>Указывает необходимость оповещать клиента с помощью СМС (true, false)</i>
