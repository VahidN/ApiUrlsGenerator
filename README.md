# API URL's Generator

<p align="left">
  <a href="https://github.com/VahidN/ApiUrlsGenerator">
     <img alt="GitHub Actions status" src="https://github.com/VahidN/ApiUrlsGenerator/workflows/.NET%20Core%20Build/badge.svg">
  </a>
</p>

This library will help you to convert your `stringly typed` API URL's to strongly typed ones.

To use it, first add its package to your ASP.NET Core Web-API application:

[![Nuget](https://img.shields.io/nuget/v/ApiUrlsGenerator)](http://www.nuget.org/packages/ApiUrlsGenerator/)

```Console
dotnet add package ApiUrlsGenerator
```

Then add its services to your services registration pipeline:

```C#
#if DEBUG
builder.Services.AddApiUrlsGenerator(options =>
                                     {
                                         options.OutputFolder = @"..\Shared";
                                         options.OutputFileName = "ApiUrls.cs";
                                         options.DefaultNamespace = "StronglyTypedApiUrls";
                                     });
#endif
```

With the above settings, a new `OutputFileName` file will be created in the `OutputFolder` and this new class has the specified `DefaultNamespace` namespace.
Note: if the `OutputFolder` doesn't exist, nothing will happen.

After adding the above settings, run the application to create the `ApiUrls.cs` file.
Note: After each changes to the route definitions of your action methods, you should run the application to update `ApiUrls.cs` file automatically.
Now instead of writing a URL like:

```C#
var secretUrl = "api/WeatherForecast/_secretUrl";
```

You can replace it with a compiler/refactoring friendly version:

```C#
var secretUrl = ApiUrls.WeatherForecast.HttpGet.SecretUrl;
```

Here you can find a [sample controller](tests/ApiUrlsGenerator.HostedBlazorWasm/Server/Controllers/WeatherForecastController.cs) and [another one](tests/ApiUrlsGenerator.HostedBlazorWasm/Server/Areas/Test/Controllers/WeatherForecastController.cs) in a new area, with the generated [ApiUrls.cs](tests/ApiUrlsGenerator.HostedBlazorWasm/Shared/ApiUrls.cs) file.
