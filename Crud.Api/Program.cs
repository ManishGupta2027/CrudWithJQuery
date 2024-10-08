using Crud.Api.Profiler;
using Crud.Data.Dapper;
using Crud.Data.Repository;
using Crud.Service.BrandService;
using Crud.Service.CategoryService;
using Crud.Service.ProductService;
using Crud.Service.Service.List;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add CORS configuration
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp", policy =>
	{
		policy.WithOrigins("http://localhost:3000")  // Specify the React app's URL
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IDapperRepository, DapperRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IListService, ListService>();
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile)); // Replace MapperProfile with the name of your profile class


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(o =>
	{
		o.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
	});
}
// Enable CORS for development
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
