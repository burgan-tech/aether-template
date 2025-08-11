using System.Globalization;
using BBT.Aether.Domain.Services;
using BBT.Aether.Threading;
using Microsoft.AspNetCore.Localization;

namespace Microsoft.AspNetCore.Builder;

public static class MyProjectNameApiApplicationBuilderExtensions
{
    public static WebApplication UseApiHostModule(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseAppResponseCompression();
        app.UseCloudEvents();
        app.MapSubscribeHandler();
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseSecurityHeaders();
        app.UseCurrentUser();
        app.UseStaticFiles();

        var supportedCultures = new[] { "en-US", "tr-TR" };
        var localizationOptions = new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
            SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
        };
        app.UseRequestLocalization(localizationOptions);
        app.UseAetherApiVersioning();
        app.UseRouting();
        app.MapControllers();
        app.UseExceptionHandler();
        app.MapAppHealthChecks();

        SeedTestData(app.Services);

        return app;
    }

    private static void SeedTestData(IServiceProvider serviceProvider)
    {
        AsyncHelper.RunSync(async () =>
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider
                .GetRequiredService<IDataSeedService>()
                .SeedAsync(new SeedContext());
        });
    }
}