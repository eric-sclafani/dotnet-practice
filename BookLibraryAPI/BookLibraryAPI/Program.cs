using BookLibraryAPI.Data;
using BookLibraryAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryContext>();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(cfg =>
	{
		cfg.WithOrigins(builder.Configuration["AllowedOrigins"]!);
		cfg.AllowAnyHeader();
		cfg.AllowAnyMethod();
	});

	options.AddPolicy(name: "AnyOrigin", cfg =>
	{
		cfg.AllowAnyOrigin();
		cfg.AllowAnyHeader();
		cfg.AllowAnyMethod();
	});
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/error", () => "Oops! Something went wrong :(");
app.MapControllers();

var parser = new CsvParser("Data/books.csv");
parser.CreateRecords();
//app.Run();