using Microsoft.EntityFrameworkCore;
using VA.Shared.Behaviors;
using VA.Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Vertical Slice Architecture API", Version = "v1" });
});
builder.Services.AddLogging(loggingBuilder =>
{
    //loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddDbContext<CustomerContext>(opts =>
    {
        opts.UseSqlite(builder.Configuration.GetConnectionString("Database"));
    });


//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();




var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.MapOpenApi();
app.UseHttpsRedirection();

app.UseMigration();
app.UseCors("AllowAll");
app.UseExceptionHandler(options => { });

//app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vertical Slice Architecture API V1");
    c.RoutePrefix = string.Empty;
});
app.MapCarter();
//app.MapGet("/", () => "Hello World!");


app.Run();