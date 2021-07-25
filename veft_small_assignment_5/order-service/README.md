# Order service
This service is used to get and create new orders within the system. Can setup RabbitMQ (https://www.cloudamqp.com/). It exposes two endpoints:
  * /api/orders [POST] - _Create a new order_
      ``` json
      {
          "customerId": 1,
          "items": [
              {
                  "id": "5e48c290-02d1-4512-addd-32d4ca9c22fc",
                  "name": "Ping Pong racket",
                  "price": "10.99",
                  "quantity": 1
              },
              {
                  "id": "96c38ca8-dd68-447c-86b8-16fde9c59393",
                  "name": "Tennis racket",
                  "price": "25.99",
                  "quantity": 2
              }
          ],
          "paymentType": "CC"
      }
      ```
  * /api/customers/:customerId/orders - _Gets all orders associated with a specific customer_
      ``` json
      [
          {
              "id": "e33b63e2-9823-4260-89dc-653d1d9653cd",
              "items": [
                  {
                      "id": "5e48c290-02d1-4512-addd-32d4ca9c22fc",
                      "name": "Ping Pong racket",
                      "price": "10.99",
                      "quantity": 1
                  },
                  {
                      "id": "96c38ca8-dd68-447c-86b8-16fde9c59393",
                      "name": "Tennis racket",
                      "price": "25.99",
                      "quantity": 2
                  }
              ],
              "paymentType": "CC"
          }
      ]
      ```
