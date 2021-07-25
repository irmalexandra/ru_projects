# API Gateway
This is the entry point to the microservice structure for Flamingo Records. It exposes five endpoints:
  * /api/customers [POST] - _Creates a customer which is structured in the following manner:_
    ``` json
    {
        "name": "Arnar Leifsson",
        "email": "arnarl@ru.is",
        "password": "some.secret.text"
    }
    ```
  * /api/customers [GET] - _Gets all customers_
      ``` json
      [
          {
              "id": 1,
              "name": "Arnar Leifsson",
              "email": "arnarl@ru.is"
          }
      ]
      ```
  * /api/customers/:customerId [GET] - _Get a customer by id_
      ``` json
      {
          "id": 1,
          "name": "Arnar Leifsson",
          "email": "arnarl@ru.is"
      }
      ```
  * /api/customers/:customerId/orders [GET] - _Get all orders associated with customer_
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
  * /api/customers/:customerId/orders [POST] - _Creates an order which is structured in the following manner:_
      ``` json
      {
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
