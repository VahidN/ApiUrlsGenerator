using ApiUrlsGenerator;
using ApiUrlsGenerator.HostedBlazorWasm.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#if DEBUG
builder.Services.AddApiUrlsGenerator(options =>
                                     {
                                         options.OutputFolder = @"..\Shared";
                                         options.OutputFileName = "ApiUrls.cs";
                                         options.DefaultNamespace = "StronglyTypedApiUrls";
                                     });
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");


var summaries = new[]
                {
                    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
                };

app.MapGet("/GetMinimalWeatherForecast",
           () =>
           {
               var forecast = Enumerable
                              .Range(1, 5)
                              .Select(index =>
                                          new WeatherForecast
                                          {
                                              Date = DateTime.Now.AddDays(index),
                                              TemperatureC = Random.Shared.Next(-20, 55),
                                              Summary = summaries[Random.Shared.Next(summaries.Length)],
                                          }
                                     )
                              .ToArray();
               return forecast;
           })
   .WithName("GetMinimalWeatherForecast");

app.Run();