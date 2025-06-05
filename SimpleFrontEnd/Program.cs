using SimpleFrontEnd.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("SimpleCrudApiClient", client =>
{
    client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("SIMPLECRUDAPI_BASE_URL"));
});

builder.Services.AddHealthChecks().AddCheck<SimpleCrudApiHealthCheck>("SimpleCrudApiHealthCheck>");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Guitars/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guitars}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

app.Run();