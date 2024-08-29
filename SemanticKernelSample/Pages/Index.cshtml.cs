using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelSample.Pages;

public class IndexModel : PageModel
{
    private readonly ChatHistory _chatHistory;
    private readonly Kernel _kernel;
    private readonly ILogger<IndexModel> _logger;

    public List<string> ChatHistory => _chatHistory.Select(ch => ch.ToString()).ToList();

    public IndexModel(ChatHistory chatHistory, Kernel kernel, ILogger<IndexModel> logger)
    {
        _chatHistory = chatHistory;
        _logger = logger;
        _kernel = kernel;
    }

    public void OnGet()
    {
        // _chatHistory.AddAssistantMessage("Hello! How can I help you today?");
    }

    public async Task OnPostAsync()
    {
        string message = Request.Form["message"]!;
        _chatHistory.AddUserMessage(message);

        var response = await _kernel.InvokePromptAsync(message);
        _chatHistory.AddAssistantMessage(response.ToString());
    }
}
