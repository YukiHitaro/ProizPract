var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// ВАЖНО: подключаем статику из wwwroot
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

app.Run();
