using System.ComponentModel;
using XTractFlow.API.Component;
using XTractFlow.API.Result;
using Microsoft.Extensions.Logging;
using XTractFlow.API;

namespace XtractFlow.Processor;

public class Processor
{
    private ILogger _logger;
    public Processor(ILogger<Processor> logger)
    {
        _logger = logger; 
    }
    
    public async IAsyncEnumerable<(string filepath, ProcessorResult result)> ProcessFilesAsync(IEnumerable<string> filePaths, ProcessorComponent component, CancellationToken cts = default)
    {
        foreach (var filepath in filePaths)
        {
            DocumentProcessor _documentprocessor = new();
            var result = await  _documentprocessor.ProcessAsync(filepath, component, cts);
            yield return (filepath, result);
        }
    }
}