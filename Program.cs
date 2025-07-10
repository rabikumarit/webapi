using WebAPI.Domain.Database;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repository;
using Azure.Identity;
using Azure.Core;

// git changes for local cicd 2

var builder = WebApplication.CreateBuilder(args);

// ✅ Key Vault URL
string keyVaultUrl = "https://azkeyvaultwebapi1.vault.azure.net/";

// ✅ Use DefaultAzureCredential always for AKS or container environments
TokenCredential credential = new DefaultAzureCredential();

builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);

// ✅ CORS – only allow Angular dev client (adjust if needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:50342") // or your deployed Angular frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ✅ Configure EF Core with secret from Key Vault
builder.Services.AddDbContext<APIdbcontext>(options =>
    options.UseSqlServer(
        builder.Configuration["dbcn"], // secret key should be `dbcn`
        sql => sql.MigrationsAssembly("WebAPI"))
);

// ✅ Register repository
builder.Services.AddTransient<IUserRep, UserRep>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Enable CORS
app.UseCors("AllowAngularDevClient");

// ✅ Enable Swagger only in development (optional)
// if (app.Environment.IsDevelopment())
// {
    
	app.UseSwagger();
    app.UseSwaggerUI();
	
// }

app.UseAuthorization();
app.MapControllers();
app.Run();
