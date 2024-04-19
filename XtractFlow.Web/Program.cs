using MudBlazor.Services;
using XTractFlow.API;
using XtractFlow.Web.Helpers;
using XtractFlow.Web.Services;
using XtractFlow.Web.Services.DataStore;
using XTractFlow.API.LLM.Providers;
using XtractFlow.Web.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMvcCore();
builder.Services.AddControllers();

builder.Services.AddSingleton<IDocumentDataStore, CircuitDocumentdataStore>();
builder.Services.AddTransient<Thumbnail>();
builder.Services.AddScoped<UserCacheService>();
builder.Services.AddScoped<BlazorTransitionableRoute.IRouteTransitionInvoker, BlazorTransitionableRoute.DefaultRouteTransitionInvoker>();
builder.Services.AddMudServices();
builder.WebHost.UseStaticWebAssets();
builder.Services.AddDocuVieware(dv =>
{
    dv.LicenseKey = builder.Configuration.GetSection("XtractFlow:LicenseKey").Value;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.UseDocuVieware();


using var sc = app.Services.CreateScope();
var uds = sc.ServiceProvider.GetService<IDocumentDataStore>();
uds.Initialization();

#region Configure XtractFlow

var licenseKey = builder.Configuration.GetValue("XtractFlow:LicenseKey", string.Empty);

if (string.IsNullOrEmpty(licenseKey))
{
    //We should add a link where customer can get a license key
    throw new NotSupportedException("XtractFlow:LicenseKey is required. Please provide a valid license key.");
}

Configuration.RegisterGdPictureKey(licenseKey);

var openAIConfig = builder.Configuration.GetSection("XtractFlow:OpenAI");
var azureOpenAIConfig = builder.Configuration.GetSection("XtractFlow:AzureOpenAI");

if (openAIConfig.Exists())
{
    var openAIApiKey = openAIConfig.GetSection("ApiKey");
    if (openAIApiKey.Exists() && string.IsNullOrEmpty(openAIApiKey.Value) is false)
    {
        var model = OpenAIProvider.OpenAILargeLanguageModel.Gpt35Turbo;
        var modelConfig = openAIConfig.GetSection("Model");

        if (modelConfig.Exists() && string.IsNullOrEmpty(modelConfig.Value) is false)
        {
            Enum.TryParse(modelConfig.Value, out model);
        }
        Configuration.RegisterLLMProvider(new OpenAIProvider(openAIApiKey.Value, model));
    }
}

if (azureOpenAIConfig.Exists())
{
    var azureOpenAIConfigKey = azureOpenAIConfig.GetSection("ApiKey");
    var azureOpenAIDeploymentId = azureOpenAIConfig.GetSection("DeploymentId");
    var azureOpenAIResourceName = azureOpenAIConfig.GetSection("ResourceName");
    
    if (azureOpenAIConfigKey.Exists() && string.IsNullOrEmpty(azureOpenAIConfigKey.Value) is false &&
        azureOpenAIDeploymentId.Exists() && string.IsNullOrEmpty(azureOpenAIDeploymentId.Value) is false &&
        azureOpenAIResourceName.Exists() && string.IsNullOrEmpty(azureOpenAIResourceName.Value) is false)
    {
        Configuration.RegisterLLMProvider(new AzureOpenAIProvider(azureOpenAIConfigKey.Value, azureOpenAIResourceName.Value, azureOpenAIDeploymentId.Value));
    }
}

var environment = sc.ServiceProvider.GetService<IWebHostEnvironment>();

var resources = Path.Combine(environment.ContentRootPath, "Resources", "xtractflow_dicts.zip");
var outputRootResources = Environment.ProcessPath;
if (Path.GetFileNameWithoutExtension(outputRootResources).EndsWith("dotnet"))
{
    outputRootResources = Environment.CommandLine.Split(' ').First();
}
var outputResources = Path.Combine(Path.GetDirectoryName(outputRootResources), "xtractFlowResources");

if (Directory.Exists(outputResources) is false || Directory.GetDirectories(outputResources).Length == 0)
{
    Directory.CreateDirectory(outputResources);
    System.IO.Compression.ZipFile.ExtractToDirectory(resources, outputResources);
}


Configuration.ResourcesFolder = Path.Combine(outputResources, "xtractflow_dicts");

#endregion
app.Run();
