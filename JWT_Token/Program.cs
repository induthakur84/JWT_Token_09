using JWT_Token.Entities;
using JWT_Token.Services;
using JWT_Token.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<UserInterface, UserService>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//swagger service
builder.Services.AddEndpointsApiExplorer();



//Role Base Authentication Configuration


// we can ensure we use the jwt tokens to identify the user and authorize them to access the resources or web api

var jwtSettings = builder.Configuration.GetSection("jwt");

builder.Services.AddAuthentication(options=>

{

    // when ever we authenticate or challenge the user we use the jwt bearer scheme
    // or use jwt bearer authentication scheme to authenticate the user and challenge the user when they try to access the resources without providing the token or with invalid token
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    //getting error 401 unauthorized when we try to access the resources without providing the token or with invalid token so we need to set the default challenge scheme to jwt bearer authentication scheme
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //Bearer eyroirohewtheroijtwjoeoewiowioewer



    // it enable jwt token processing like it will read the token and 

    // validate the token


    //Extract the claims from the token and set the user identity and principal

    //Create the authenticated user context for the request

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        // check the token is issued by a trusted authority or not
        ValidateIssuer = true,

        //check the token is intended for the correct audience or not
        ValidateAudience = true,

        // check the token is not expired and is still valid
        ValidateLifetime = true,

        // isssuer signing key is valid and matches the one used to sign the token
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings["Key"])),
      
    };
});





// here we add that code
builder.Services.AddSwaggerGen(options =>
{
    // 1) Define the Bearer Security Scheme
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the text box below (Do not include 'Bearer ' prefix)."
    });

    //Bearer   

    // 2) Make it a requirement for API testing
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" // This ID must exactly match the name defined above
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
