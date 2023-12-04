var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/tax/individual/calculate", (IndividualTaxCalculationRequest request) =>
{
    return new IndividualTaxCalculationResponse(1, 1, 1);
})
.WithName("CalculateIndividualTax");
app.Run();

public record IndividualTaxCalculationRequest(
    decimal AnnualIncome, 
    string PostalCode);
public record IndividualTaxCalculationResponse(
    decimal TotalTaxPayable,
    decimal TaxableIncome,
    decimal TaxRate);