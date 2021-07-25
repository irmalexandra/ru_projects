# Customer Service
This service is used to create and get customer associated with the system. It exposes three endpoints:
  * /api/customers [GET] - _Get all customers_
  * /api/customers/:customerId [GET] - _Get a customer by id_
  * /api/customers [POST] - _Create a customer_

If a user is created successfully the customer service should dispatch an event called 'customer_create_success' which notifies other services that the customer was created successfully. 
