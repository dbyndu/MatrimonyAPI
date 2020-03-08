# MatrimonyAPI
Web API Project For Matrimony Site

Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design -Version 3.0.0-preview8-19413-06 This package helps generate controllers and views.
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.0.0-preview8.19405.11 This package helps create database context and a model class from the database.
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 3.0.0-preview8.19405.11 The database provider allows Entity Framework Core to work with SQL Server.


==>Run the following scaffold command in Package Manager Console to reverse engineer the database to create database context and entity POCO classes from tables.

Scaffold-DbContext "Server=LAPTOP-DVGRKESI\SQL2017;Database=Matrimony;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -force
