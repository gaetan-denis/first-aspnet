



using API.Repositories;
using API.Services;


var builder = WebApplication.CreateBuilder(args);

// Ajout de la politique CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//lower case for all controllers.
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Ajout du service PasswordManager
builder.Services.AddSingleton<IPasswordManager, PasswordManager>();


// Ajout du service de UserController chaque fois qu'une requête http sera effectuée

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Ajout du service de PostController chaque fois qu'une requête http sera effectuée

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService,PostService>();

// Ajout du service de DomainController chaque fois qu'une requête http sera effectuée

builder.Services.AddScoped<IDomainRepository, DomainRepository>();
builder.Services.AddScoped<IDomainService,DomainService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



//Appliquer la politique CORS
app.UseCors("AllowReactApp");

app.UseRouting();



//Map
app.MapControllers();

app.Run(); 
