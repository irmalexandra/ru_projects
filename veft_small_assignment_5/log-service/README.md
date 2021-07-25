# Log service
This service is used to send out emails and can only be communicated through a message broker. It listens to two types of events:
  * customer_create_success - _The event handler should read from the data sent from the event and send an email using that data_
  * order_create_success - _The event handler should read from the data sent from the event and send an email using that data_
