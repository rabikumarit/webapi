using WebAPI.Domain.Database;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repository;
using Azure.Identity;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);

string keyVaultUrl = "https://azkeyvaultwebapi1.vault.azure.net/";

// ✅ Smart credential switch: prefer ClientSecretCredential in container
TokenCredential credential;

if (builder.Environment.IsDevelopment())
{
    credential = new DefaultAzureCredential(); // local dev
}
else
{
    credential = new ClientSecretCredential(
        builder.Configuration["TenantId"],
        builder.Configuration["ClientId"],
        builder.Configuration["ClientSecret"]
    );
}

builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);

// ✅ CORS – only allow Angular dev client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:50342")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ✅ Configure EF Core with secret from Key Vault
builder.Services.AddDbContext<APIdbcontext>(options =>
    options.UseSqlServer(
        builder.Configuration["dbcn"],
        sql => sql.MigrationsAssembly("WebAPI"))
);

// ✅ Register repository
builder.Services.AddTransient<IUserRep, UserRep>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularDevClient");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
app.Run();
