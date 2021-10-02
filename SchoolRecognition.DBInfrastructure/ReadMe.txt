Introduction
The project uses active directory login on azure and all developers on the projects should be added as members of the project on the Azure cloud platform

Install NuGet packages
The following NuGet packages should be added to work with the SQL Server database and scaffolding. Run these commands in Package Manager Console:

Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design This package helps generate controllers and views.
Install-Package Microsoft.EntityFrameworkCore.Tools This package helps create database context and a model class from the database.
Install-Package Microsoft.EntityFrameworkCore.SqlServer  The database provider allows Entity Framework Core to work with SQL Server.

Scaffolding
ASP.NET Core has a feature called scaffolding, which uses T4 templates to generate code of common functionalities to help keep developers from writing repeat code. We use scaffolding to perform the following operations:

Scaffold-DbContext “Server=LAPTOP-IFRMU6GH\MSSQLSERVER08;Database=WSchoolRecognition;User Id=sa;Password=sa;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities

Scaffold-DbContext “Server=DESKTOP-KFTMVT1\SQLEXPRESS19;Database=WSchoolRecognition;Integrated Security=SSPI;User Id=sa;Password=sa@123;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -ContextDir "DbContexts" -f

Scaffold-DbContext “Server=DESKTOP-KFTMVT1\SQLEXPRESS12;Database=WSchoolRecognition;Integrated Security=SSPI;User Id=sa;Password=sa@123;” Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -ContextDir "DbContexts" -f


Connection—Sets connection string of the database.
Provider—Sets which provider to use to connect database.
OutputDir—Sets the directory where the POCO classes are to be generated.
In our case, the Products class and Inventory context class will be created.

The Project was developed by the Programming Team of ICTD, WAEC, Lagos Office
All credt to The West African Examinations Council

For help and explanations:
Contact luigwe@waec.org.ng or 
	patlichengine@gmail.com
	+2348066744424



	
        private readonly ConnectionString _connectionString;

        public WSchoolRecognitionContext(ConnectionString connectionString)
        {

        }

        public WSchoolRecognitionContext(DbContextOptions<WSchoolRecognitionContext> options, ConnectionString connectionString)
            : base(options)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("data source=.;initial catalog=WSchoolRecognition;persist security info=True;user id=managemyfunds;password=managemyfunds;MultipleActiveResultSets=True;");
                //optionsBuilder.UseSqlServer("Server=DESKTOP-KFTMVT1\\SQLEXPRESS19;Database=WSchoolRecognition;User Id=sa;Password=sa@123");
                optionsBuilder.UseSqlServer(_connectionString.Value);
            }
        }

