using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelSample.Pages;

public class IndexModel(ChatHistory chatHistory, Kernel kernel) : PageModel
{
    private readonly ChatHistory _chatHistory = chatHistory;
    private readonly Kernel _kernel = kernel;

    public List<string> ChatHistory => _chatHistory.Select(ch => ch.ToString()).ToList();

    public void OnGet()
    {
        // _chatHistory.AddAssistantMessage("Hello! How can I help you today?");
    }

    public async Task OnPostAsync()
    {
        string message = Request.Form["message"]!;
        _chatHistory.AddUserMessage(message);

        FunctionResult response = await _kernel.InvokePromptAsync(message);
        _chatHistory.AddAssistantMessage(response.ToString());
    }
}
