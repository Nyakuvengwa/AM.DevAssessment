using AM.DevAssessment.Api.Data;
using AM.DevAssessment.Api.Feature.IndividualTax;
using AM.DevAssessment.Api.Middleware;
using AM.DevAssessment.Api.Models;
using AM.DevAssessment.Api.Services.TaxCalculatorService;
using AM.DevAssessment.Api.Validations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddTaxCalculators();

builder.Services.AddDbContext<TaxDbContext>(options =>
        options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString(nameof(TaxDbContext))));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetService<TaxDbContext>();
        if (context is null)
        {
            return;
        }

        var pendingMigraitons = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigraitons.Any())
        {
            await context.Database.MigrateAsync();
        }
    }
    catch (Exception)
    {
        throw;
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapPost("/tax/individual/calculate", async (IndividualTaxCalculationRequest request, IMediator mediator) =>
{
    var validator = new IndividualTaxCalculationRequestValidator();

    var validationResult = validator.Validate(request) ?? throw new Exception("Unable to validate request. Try again.");
    if(!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var result = await mediator.Send(new IndividualTaxCommand(request.AnnualIncome, request.PostalCode));
    return Results.Ok(result);    
})
.Produces<IndividualTaxCalculationResponse>()
.ProducesValidationProblem()
.WithName("CalculateIndividualTax");

app.MapGet("/tax/individual", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetIndividualTaxCalculationsQuery());
    return Results.Ok(result);
}).Produces<List<CalculatedTaxAuditModel>>()
.WithName("GetLatestCalculatedTax");

app.Run();
