using MxFace.Iris.SDK.Sample.Net.Data.Contexts;
using Microsoft.EntityFrameworkCore;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {

        var Configuration = builder.Configuration;

        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<MxFaceIrisSDKContext>((serviceProvider, options) =>
        {
            System.Diagnostics.Debug.WriteLine("MxFaceIrisSDKContext::conn ->" + connectionString);

            options.UseNpgsql(connectionString, npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });
        });

        builder.AddMxFaceIrisServices();
    }
}
