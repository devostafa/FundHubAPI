using System.Text;
using System.Text.Json.Serialization;
using FundHub.Services;
using FundHub.Services.Services.StartupService;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddServices();
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    var secretKey = builder.Configuration["SecretKey"];

    if (secretKey == null) return;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration["URL"],
        ValidAudience = builder.Configuration["clientURL"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.WithOrigins(builder.Configuration["Client"], builder.Configuration["URL"]).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();
var serviceScope = app.Services.CreateScope();
var services = serviceScope.ServiceProvider;
Startup.ExecuteStartupServices(services, app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Storage")),
    RequestPath = "/storage"
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run(builder.Configuration["URL"]);

