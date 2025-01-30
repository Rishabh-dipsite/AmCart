# Getting Started with AmCart Multi Service Project

This project has 3 services ProductService, Redis Cache Service and Mongo DB Service.


## How to start

In the project directory (if you want to build with code) or copy docker-compose.yml file and .env file to any folder of your wish and then you can run:

### `docker compose up -d`

Runs the app in the development mode.
Open [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html) to view with swagger in your browser.
This command creates and runs: 
- Network
- Mongo container from [Image](https://hub.docker.com/r/drexcdp/mongo/tags)
- Redis container from [Image](https://hub.docker.com/r/drexcdp/redis/tags)
- Dotnet Web API container from [Image](https://hub.docker.com/r/drexcdp/amcart-product-service/tags)


### Click on POST `Products/create` endpoint

Click on `Try it out` and then `Execute`
This will insert product information in SQL DB as well as Redis Cache.


### Click on GET `Products/all` endpoint

Click on `Try it out` and then `Execute`
This will get product information from SQL DB.


### Click on GET `Products/{id}` endpoint

Click on `Try it out`.
Add product tag (SMRT_123_1) from the last request in the `id` field and then click on `Execute`. This will get information for specific product from Redis Cache.


**Bonus Point**

To run for different environments use the folder TESTING and PRODUCTION. Change directory to this folder update the env file and run the above commands.

Points to remember, in real world we will have different images for different environment and each image will have there separate environment variables which needs to be setup per service. This being out of scope is explained how it can be done.
