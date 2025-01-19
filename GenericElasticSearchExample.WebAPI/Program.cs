using GenericElasticSearchExample.Business.Abstract;
using GenericElasticSearchExample.Business.Concrete;
using GenericElasticSearchExample.Business.ElasticSearch.Abstract;
using GenericElasticSearchExample.Business.ElasticSearch.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IElasticsearchService, ElasticsearchManager>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
