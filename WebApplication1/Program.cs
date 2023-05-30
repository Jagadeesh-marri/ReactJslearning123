using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WebapiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("webapi")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var key = "kygmtest12345678";

builder.Services.AddAuthentication(x => //The AddAuthentication method adds the authentication services to the application's 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;// specifies that the middleware should use JWT bearer authentication for both authentication and challenge responses.
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;   // specifies that the middleware should use JWT bearer authentication for both authentication and challenge responses.A challenge is a mechanism used to prompt an unauthenticated user to provide authentication credentials.
}).AddJwtBearer(x =>//AddJwtBearer is a method in ASP.NET Core authentication middleware that configures the middleware to use JWT bearer authentication for incoming requests, and allows developers to configure the JWT bearer authentication options.
{
    x.RequireHttpsMetadata = true;//When RequireHttpsMetadata is set to true, the authentication middleware will only accept tokens from HTTPS endpoints. If the endpoint that provides the JWT token is not HTTPS, the middleware will reject the token and return a 401 Unauthorized response.
    x.SaveToken = true;//When SaveToken is set to true, the middleware will store the validated security token in the AuthenticationProperties of the HttpContext after successful authentication. 
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,//When ValidateIssuerSigningKey is set to true, the middleware will validate the JWT token's signature against the key provided by the token issuer
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,//It ensures that the token was issued by the expected party and helps prevent security vulnerabilities.
        ValidateAudience = false
    };
});
builder.Services.AddSingleton<JwtAuthenticationManager>(new JwtAuthenticationManager(key));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
