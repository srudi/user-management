
# Getting Started
## Local MongoDb 
Before you start the application please follow the steps below to setup a local MongoDb for the application to connect to.   
Prerequisite: "Docker Desktop" has to be installed on your machine.
+ Right click on the Solution in the Solution Explorer. 
+ Click on "Open Folder in File Exporer" in the context menu, type "cmd" into the Address bar (it will open a command prompt).
+ Copy-paste and execute the following command: 
```bash 
docker-compose up -d
```
+ if your "local-mongodb" container is running, then you can run the application (F5).

# Testing
## Manual
The application can be tested manaully through the Swagger UI after the local MongoDb container and the application are started.
http://localhost:5000/swagger/index.html

Sample data for testing: https://jsonplaceholder.typicode.com/users

## Unit tests
Can be executed anytime, does not require MongoDb connection.


