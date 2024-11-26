using MxFace.Iris.SDK.Sample.Net.Components;

var builder = WebApplication.CreateBuilder(args);


builder.AddApplicationServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

