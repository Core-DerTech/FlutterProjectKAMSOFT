using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options => {
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

services.AddTransient<IValidator<CipherRequestCaesar>, CaesarRequestValidator>();
services.AddTransient<IValidator<CipherRequestVigenere>, VigenereRequestValidator>();
services.AddTransient<IValidator<ChipherTextRequest>, EncryptionRequestValidator>();


var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();