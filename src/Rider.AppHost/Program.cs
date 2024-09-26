var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres");

// Databases
var accountDb = postgres.AddDatabase("accountdb");
var rideDb = postgres.AddDatabase("ridedb");

builder.AddProject<Projects.Account_Infrastructure>("account").WithReference(rabbitMq).WithReference(accountDb);
builder.AddProject<Projects.Ride_Infrastructure>("ride").WithReference(rabbitMq).WithReference(rideDb);

builder.Build().Run();
