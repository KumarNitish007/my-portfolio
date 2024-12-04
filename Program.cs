using portfolioApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<EmailService>(provider => new EmailService(builder.Configuration["SmtpSettings:SmtpServer"], int.Parse(builder.Configuration["SmtpSettings:SmtpPort"]),
    builder.Configuration["SmtpSettings:SmtpUser"], builder.Configuration["SmtpSettings:SmtpPass"]));
// Add services to the container.
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
        policy.WithOrigins("http://127.0.0.1:5502/")
              .AllowAnyMethod()
              .AllowAnyHeader());
});
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
    app.UseCors("AllowSpecificOrigin");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
