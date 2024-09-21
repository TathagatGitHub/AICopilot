using AICopilot.Components;
using Azure.Identity;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//Add serice for kernal
builder.Services.AddKernel();

var aiConfig = builder.Configuration.GetSection("AIConfig");

builder.Services.AddAzureOpenAIChatCompletion(
    deploymentName: aiConfig["DeploymentName"]!,
    endpoint: aiConfig["Endpoint"]!,
    apiKey: aiConfig["ApiKey"]!);
// "7c15ae1a4a8640bdbaad741285c822bb");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
