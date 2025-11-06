using System.Text;
using api.Database;
using api.Middlewares;
using api.Services.Auth;
using api.Services.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string corsOrigin = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsOrigin,
    policy => policy.WithOrigins(builder.Configuration.GetValue<string>("UIUrl"))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
    );
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Inventory API",
        Description = "An ASP.NET Core Web API for managing Inventory Items",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

                Array.Empty<string>()
        }
    });
});

builder.Services.AddHttpContextAccessor();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration.GetRequiredSection("Jwt").GetValue<string>("Issuer"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetRequiredSection("Jwt").GetValue<string>("Key"))),

    };
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseMiddleware<GlobalErrorHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    }
    );
}
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "OK",
        message = "Server is running!",
        timestamp = DateTime.UtcNow
    });
});


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors(corsOrigin);
app.UseAuthorization();

app.MapControllers();

app.Run();
