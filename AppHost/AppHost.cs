var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sqlServer = builder
        .AddSqlServer("todo-sqlserver", password: sqlPassword, port: 9000)
        .WithLifetime(ContainerLifetime.Persistent)
        .AddDatabase("tododb");

var migrationService = builder.AddProject<Projects.MigrationService>("migrationservice")
    .WithReference(sqlServer)
    .WaitFor(sqlServer);

var apiService = builder.AddProject<Projects.ApiService>("apiservice")
    .WithReference(sqlServer)
    .WaitFor(sqlServer)
    .WaitFor(migrationService)
    .WithHttpHealthCheck("/health");

builder.AddNpmApp("web", "../web", "dev")
    .WithReference(apiService)
    .WithHttpEndpoint(3000, env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
