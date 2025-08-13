using SkibidiBnb.Api.Apis;
using SkibidiBnb.Api.Bootstrapping;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.MapAuthenticationApi();
app.MapProperty();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();