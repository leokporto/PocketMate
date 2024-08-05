using PocketMate.Api.Common.Api;
using PocketMate.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
	app.ConfigureDevEnvironment();


app.MapEndpoints();

app.Run();

