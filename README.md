1- You can open the project with VS Code and run tests.
--------------------------------
dotnet restore 
dotnet test

2-You can run project and test with swagger or postman
------------------------------
Create a new database and setup the connection string in appsetting.json.

.\api\appsetting.json

"DefaultConnection": "Host={yourhost};Database={databse-name};Username={username};Password={password}"

![image](https://github.com/user-attachments/assets/04f11e4d-ee23-4b1d-a8c0-0d88994fa5cb)

Then, you can create the database using the following command.

cd Api

dotnet restore

dotnet tool install --global dotnet-ef

dotnet ef database update

dotnet run

After running the program, you can send requests to the application using Swagger or a Postman file and see the results.

https://localhost:{port}/swagger/index.html
