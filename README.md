# StockItToMe
Warehouse Monitoring System using MassTransit
1. General Description
The system aims to monitor the status of products in a warehouse. Users can add, delete, or update products via the Rest API. Each operation generates a message in the queue system, which is then processed by consumers.

2. Main Components of the System
Rest API: Allows for system interaction.
RabbitMQ / Kafka / SQS Amazon: Handles message passing.
Database: Storing product information.
Consumers: Processes the messages from the queues.
3. Features
3.1 Add Product
Users add a product via the Rest API.
A "ProductAdded" message is generated in the queue system.
The consumer processes the message and adds the product to the database.
3.2 Update Product
Users update a product via the Rest API.
A "ProductUpdated" message is generated.
The consumer processes the message and updates the product in the database.
3.3 Delete Product
Users delete a product via the Rest API.
A "ProductDeleted" message is generated.
The consumer processes the message and deletes the product from the database.
3.4 Notifications
For certain operations (e.g., low product stock), the system generates notifications sent to other services or users.

4. Workflow
Users perform an action (add/update/delete product) via the Rest API.
The API server generates the appropriate message in the queue system.
The consumer retrieves the message from the queue and processes it (e.g., adds the information to the database).
Optionally: the system generates a notification for certain events.
5. Technical Aspects
Technology: Recommended use of ASP.NET Core for the Rest API.
Database: Consider using a SQL database (e.g., PostgreSQL, MSSQL) or a NoSQL one (e.g., MongoDB), depending on your preference.
Queues: Utilizing MassTransit facilitates easy switching between different queue systems (RabbitMQ, Kafka, SQS).
