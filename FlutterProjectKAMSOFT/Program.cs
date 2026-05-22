using FluentValidation;
using FlutterProjectKAMSOFT.Encryption.CipherFactory;
using FlutterProjectKAMSOFT.Encryption.CipherValidation;
using FlutterProjectKAMSOFT.Encryption.Ciphers;
using FlutterProjectKAMSOFT.Encryption.Models;
using FlutterProjectKAMSOFT.Patterns;
using FlutterProjectKAMSOFT.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddScoped<CipherFactory>();
services.AddScoped<CaesarCipher>();
services.AddScoped<VigenereCipher>();
services.AddScoped<RSAEncryption>();
services.AddScoped<SHAEncription>();
services.AddScoped<AppointmentService>();
services.AddScoped<PatientDataEncryptionService>();
services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateAppointmentCommand>());

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
