EF scaffold

Command link: https://docs.microsoft.com/en-us/ef/core/cli/dotnet
1: Package manager console: cd dal
2: 
dotnet ef dbcontext scaffold "Data Source=sqldev;Initial Catalog=SA_ISYS_USER_PERMISSIONS_MANAGER;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context MyExampleContext --context-dir Context --no-onconfiguring -f

dotnet ef dbcontext scaffold "Data Source=./;Initial Catalog=SA_ISYS_USER_PERMISSIONS_MANAGER;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context MyExampleContext --context-dir Context --no-onconfiguring -f

# Local sql scaffold command
dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrHazohar;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context OrHazoharContext --context-dir Context --no-onconfiguring -f

dotnet ef dbcontext scaffold "Server=mssql-92287-0.cloudclusters.net,19553;Initial Catalog=Sportive;User Id=sportive;Password=tT64006400" Microsoft.EntityFrameworkCore.SqlServer -o Entities --context OrHazoharContext --context-dir Context --no-onconfiguring -f


dotnet ef dbcontext scaffold "Data Source=213.151.46.56;Initial Catalog=Sportive;User Id=sa;Password=tT64006400;Initial Catalog=Sportive_Dev;"Microsoft.EntityFrameworkCore.SqlServer -o Entities --context SportiveContext --context-dir Context --no-onconfiguring -f

dotnet ef dbcontext scaffold "Data Source=213.151.46.56,30476;Initial Catalog=Sportiv;User Id=sportiv;Password=Hu6%}M[5p(b8O;"Microsoft.EntityFrameworkCore.SqlServer -o Entities --context SportiveContext --context-dir Context --no-onconfiguring -f

