using portfolioApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<EmailService>(provider => new EmailService(builder.Configuration["SmtpSettings:SmtpServer"], int.Parse(builder.Configuration["SmtpSettings:SmtpPort"]),
    builder.Configuration["SmtpSettings:SmtpUser"], builder.Configuration["SmtpSettings:SmtpPass"]));
// Add services to the container.
// Add CORS policy
// Add CORS services
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder => builder.WithOrigins("http://127.0.0.1:5503") // Adjust based on your front-end's URL
						  .AllowAnyHeader()
						  .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	// Use CORS before any other middleware
	app.UseCors("AllowSpecificOrigin");
	app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseAuthorization();

app.MapControllers();

app.Run();
