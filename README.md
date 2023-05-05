# **Social_Network_API**
This is a Social Network API project built using .NET Core and Entity Framework. The API allows developers to create a social network application by providing endpoints to manage users, subscriptions,static files etc.
## Installation 🚀
1. Clone the repository `git clone https://github.com/radimbig/Social_Network_API.git`
2. Go to the folder `cd Social_Network_API`
3. Run `dotnet restore` to install the necessary dependencies
4. Rename `appsettingsEXAMPLE.json` to `appsettings.json`
5. It is recommended to replace the key in `appsettings.json` in `JWT:KEY`
6. Update the connection string in `Program.cs` file to your MySQL Server instance
7. Run `Update-Database` in package manager console to update the database
## Usage💡
`dotnet run` in repository folder to run the API
## API endpoints📝
All endpoints described in this postman [workspace](https://www.postman.com/radimbig/workspace/social-network-api) 
## Postman usage📡
For comfortable testing of the API, follow these steps in Postman:
1. Import the provided Postman collection and environment.
2. Register an account by using `Register` endpoint
3. Login and get tokin
4. Update `Token` variable with your new token and use all endpoint that require authorization
3. Send requests to the API endpoints using the imported collection.
## Features 🔍
Login/Register ✔️\
Subscriptions ✔️\
Static files ✔️\
Avatar uploading ✔️\
Entity framework ✔️

