using Support;
using Apexa.REST.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new HttpMethodFilter(new[] { "GET", "POST", "DELETE", "PUT" }));
});


var app = builder.Build();

//prepare APEXA
Apexa.BLL.DependencyPreparation.PrepareDependencies(SystemAccessOps.Services);
SystemAccessOps.BuildServices();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
