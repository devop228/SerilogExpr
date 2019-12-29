# Purpose

This porject experiences features of Serilog framework with .NET Core console.

## Features

1. Use Serilog directly in a .NET Core console app.
2. Use Serilog extension with Microsoft.Extensions.Logging package.
3. Use Serilog with ApplicationInsights sinks
4. Use Serilog with ApplicationInsights sinks integrating with Microsoft.Extensions.Logging package.
5. ApplicationInsights exception logging, exception with inner exception, custom properties in custom dimension.

## Details

### Configuration

ApplicationInsights configuration with appsettings.json. Reference to [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration).
   
```csharp
    var logSect = configRoot.GetSection("Logging");
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(logSect)
        .CreateLogger();
```
To configurate Microsoft.Extensions.Logging with Serilog, use code block below:
```csharp
    services.AddLogging(builder => {
        builder.AddSerilog(logger, dispose: true);
    });
```
After these setting, Microsoft.Extensions.Logging will use Serilog provider then redirect all logging to Serilog's sinks.

### Instrumentation key

ApplicationInsights(Ai)'s instrumentation key is configured with Ai sink's `WriteTo` confgiruation section. Reference to [Configuring with ReadFrom.Configuration](https://github.com/serilog/serilog-sinks-applicationinsights#configuring-with-readfromconfiguration). The instrumentation key value key is `Logging:erilog:WriteTo:1:Args:InstrumentationKey`, in which `1` is the indext of Ai sink in the `WriteTo` array.
