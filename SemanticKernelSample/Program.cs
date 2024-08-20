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
var chatHistory = new ChatHistory();

// Example 1. Invoke the kernel with a prompt and display the result
// Console.WriteLine(await kernel.InvokePromptAsync("What color is the sky?"));
// Console.WriteLine();
// Console.WriteLine("============================================================");

// Example 2. Invoke the kernel with a templated prompt and display the result
// KernelArguments arguments = new() { { "topic", "sea" } };
// Console.WriteLine(await kernel.InvokePromptAsync("What color is the {{$topic}}?", arguments));
// Console.WriteLine();
// Console.WriteLine("============================================================");

// Example 3. Invoke the kernel with a templated prompt and stream the results to the display
// await foreach (var update in kernel.InvokePromptStreamingAsync("What color is the {{$topic}}? Provide a detailed explanation.", arguments))
// {
//     Console.Write(update);
// }

// Console.WriteLine(string.Empty);
// Console.WriteLine("============================================================");

// Example 4. Invoke the kernel with a templated prompt and execution settings
// arguments = new(new OpenAIPromptExecutionSettings { MaxTokens = 500, Temperature = 0.5 }) { { "topic", "dogs" } };
// Console.WriteLine(await kernel.InvokePromptAsync("Tell me a story about {{$topic}}", arguments));
// Console.WriteLine("============================================================");

// Example 5. Invoke the kernel with a templated prompt and execution settings configured to return JSON
// #pragma warning disable SKEXP0010
// arguments = new(new OpenAIPromptExecutionSettings { ResponseFormat = "json_object" }) { { "topic", "chocolate" } };
// Console.WriteLine(await kernel.InvokePromptAsync("Create a recipe for a {{$topic}} cake in JSON format", arguments));
// Console.WriteLine("============================================================");
//







// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped(_ => kernel);
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
