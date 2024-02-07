using PixieFit.Core.Services;
using PixieFit.Core.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IPhotoService, PhotoService>();
builder.Services.AddTransient<IResizer, Resizer>();

var app = builder.Build();


app.MapControllers();

app.UseRouting();

app.Run();
