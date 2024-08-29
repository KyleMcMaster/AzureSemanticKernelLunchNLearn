using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Create a kernel with OpenAI chat completion capabilities
string? apiKey = builder.Configuration.GetRequiredSection("OpenAI:ApiKey").Value;
string? model = builder.Configuration.GetRequiredSection("OpenAI:Model").Value;
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: model!,
        apiKey: apiKey!)
    .Build();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped(_ => kernel);
var chatHistory = new ChatHistory();
builder.Services.AddScoped(_ => chatHistory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
