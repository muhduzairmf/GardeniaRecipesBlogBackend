# GardeniaRecipesBlogBackend

This is a backend for [Gardenia Recipes Blog Frontend](). This is group project for Web Development and .NET Programming courses in my university.    

This group project aims for enhancing our skills to develop a Web-Based system using basic Web Technology (HTML, CSS and JavaScript) and also Server Side Technology (In this case, we are use ASP.NET Core Web API).       

This Gardenia Recipes Blog is a recipe blog for Gardenia's customer that interested to share or view any recipes that using any Gardenia related product.     

### Testing this Backend     

To test this Backend, please make sure to     
- Download [Microsoft SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads/) and [Microsoft SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- Download Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore.SqlServer and Microsoft.AspNetCore.Authentication.JwtBearer in NuGet Package Manager. To get it, right click on the project folder and click Manage NuGet Packages. Go to Browse tab and find these four packages.
- Write these two command on Package Manager Console to do migrations and updates on database. To get it, click on View (top left) then click on Other Windows and click Package Manager Console. These two console are
```
dotnet-ef migrations add CreateInitial
```   
   
```
dotnet-ef database update
```
