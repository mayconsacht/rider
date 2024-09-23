var builder = DistributedApplication.CreateBuilder(args);

// Infrastructure
var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres");

// Databases
var accountDb = postgres.AddDatabase("accountdb");
var rideDb = postgres.AddDatabase("ridedb");

// Services
var account = builder.AddProject<Projects.Account_Infrastructure>("account-infrastructure")
    .WithReference(rabbitMq)
    .WithReference(accountDb);

var ride = builder.AddProject<Projects.Ride_Infrastructure>("ride-infrastructure")
    .WithReference(rabbitMq)
    .WithReference(rideDb);

builder.Build().Run();