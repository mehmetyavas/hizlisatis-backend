dotnet-ef database update --project DataAccess --context HsDbContext
dotnet-ef migrations add Shortcuts --project DataAccess --context HsDbContext
dotnet publish "C:\Path\To\Solution\SolutionName.sln" --configuration Release