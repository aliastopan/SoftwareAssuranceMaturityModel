# EF Core

## Installation & Update
``` csharp
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

## CLI

### Migration
``` csharp
dotnet ef --startup-project startup.csproj migrations add "NAME" --project migration.csproj --context  "CONTEXT" --output-dir "OUTPUT\DIRECTORY"
```

### Update
``` csharp
dotnet ef --startup-project startup.csproj database update "NAME" --project migration.csproj
```

---
## Example

Use case of using 2 seperate DbContext:

### UserDbContext
``` csharp
dotnet ef --startup-project .\src\Presentation\Server\ migrations add "InitIdentity" --project .\src\Infrastructure\ --context UserDbContext --output-dir "Persistence/Migrations"

dotnet ef --startup-project .\src\Presentation\Server\ database update "InitIdentity" --project .\src\Infrastructure\ --context UserDbContext
```

### ApplicationDbContext
``` csharp
dotnet ef --startup-project .\src\Presentation\Server\ migrations add "InitApplication" --project .\src\Infrastructure\ --context ApplicationDbContext --output-dir "Persistence/Migrations/Application"

dotnet ef --startup-project .\src\Presentation\Server\ database update "InitApplication" --project .\src\Infrastructure\ --context ApplicationDbContext
```


