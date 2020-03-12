(1) Install-Package Microsoft.EntityFrameworkCore.SqlServer

(2) To enable reverse engineering from an existing database we need to install a couple of other packages too.

(3) Run Install-Package Microsoft.EntityFrameworkCore.Tools –Pre
(4) Run Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design
(5) Open project.json
    Locate the tools section and add the highlighted lines as shown below


    Install NuGet packages
The following NuGet packages should be added to work with the SQL Server database and scaffolding. Run these commands in Package Manager Console:

Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design This package helps generate controllers and views.
Install-Package Microsoft.EntityFrameworkCore.Tools This package helps create database context and a model class from the database.
Install-Package Microsoft.EntityFrameworkCore.SqlServer  The database provider allows Entity Framework Core to work with SQL Server.



Scaffolding
ASP.NET Core has a feature called scaffolding, which uses T4 templates to generate code of common functionalities to help keep developers from writing repeat code. We use scaffolding to perform the following operations:

Generate entity POCO classes and a context class for the database.
Generate code for create, read, update, and delete (CRUD) operations of the database model using Entity Framework Core, which includes controllers and views.
Connect application with database
Run the following scaffold command in Package Manager Console to reverse engineer the database to create database context and entity POCO classes from tables. The scaffold command will create POCO class only for the tables that have a primary key.

Scaffold-DbContext “Server=LAPTOP-IFRMU6GH\MSSQLSERVER19;Database=SchoolRecognition;User Id=sa;Password=sa@123;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Connection—Sets connection string of the database.
Provider—Sets which provider to use to connect database.
OutputDir—Sets the directory where the POCO classes are to be generated.
In our case, the Products class and Inventory context class will be created.


Menu items
1. Use Index.html
2. Adminitrative Operations
    Generate PINs Daniel
    Manage PINs
    Offices Abdul
    Office States Abdul
    School Categories Henry
3. School Management
    Payments Pius
    Manage Schools

4. Account Management
    Manage Users
    Manage Roles

5. Reporting
    View Error Logs
    View System Audit
    
