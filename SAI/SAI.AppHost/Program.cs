var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SAI>("sai");

builder.Build().Run();
