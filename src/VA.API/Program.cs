using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System;
using VA.API.Customers.CreateCustomer;
using VA.API.Customers.DeleteCustomer;
using VA.API.Customers.UpdateCustomer;
//using VA.API.Customers.UpdateCustomer;
using VA.Shared.Behaviors;
using VA.Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var assembly = typeof(Program).Assembly;
//builder.Services.AddMediatR(config =>
//{
//    config.RegisterServicesFromAssembly(assembly);
//    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
//    //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
//});

//builder.Services.AddScoped<ICommandHandler<CreateCustomerCommand, CreateCustomerResponse>, CreateCustomerCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<UpdateCustomerCommand, UpdateCustomerResponse>, UpdateCustomerCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<DeleteCustomerCommand, DeleteCustomerResponse>, DeleteCustomerCommandHandler>();



builder.Services.Scan(scan => scan.FromAssembliesOf(typeof(Program))
    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
    .AsImplementedInterfaces()
    .WithScopedLifetime()
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
    .AsImplementedInterfaces()
    .WithScopedLifetime()
    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
    .AsImplementedInterfaces()
    .WithScopedLifetime());
builder.Services.AddScoped<IDispatcher, Dispatcher>();

//builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
//builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandBaseHandler<>));

//builder.Services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
//builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
//builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandBaseHandler<>));

builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes:true);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


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

//builder.Services.AddDbContext<CustomerContext>(opts =>
//    {
//        opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
//    });
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();




var app = builder.Build();
//app.UseMigration();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CustomerContext>();
    db.Database.Migrate();
}



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.MapOpenApi();
app.MapScalarApiReference();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vertical Slice Architecture API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();


app.UseCors("AllowAll");
app.UseExceptionHandler(options => { });

//app.UseExceptionHandler("/error");

app.MapCarter();
//app.MapGet("/", () => "Hello World!");


app.Run();