using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Forms;
using XTractFlow.API.Component;
using XTractFlow.API.Document;
using XtractFlow.Web.Data;

namespace XtractFlow.Web.Services.DataStore;

public interface IDocumentDataStore : IDisposable
{
    [JsonIgnore]
    protected List<Action> OnChangeEventHandler { get; }
    
    List<UserDocument> Documents { get; }
    ProcessorComponent Component { get; set; }
    
    public string StorePath { get; }

    public void Initialization();

    public void Save();

    protected void Notify();

    public UserDocument? GetDocumentById(Guid documentId)
    {
        return Documents?.FirstOrDefault(d => d.Id == documentId);
    }
    
    internal DocumentTemplate GetTemplateById(string templateIdentifier)
    {
        return Component.Templates.FirstOrDefault(r => r.Identifier == templateIdentifier) ?? new();
    }

    public Task AddFile(IBrowserFile file, CancellationToken ct = new());
    
    public void RemoveFile(UserDocument document)
    {
        File.Delete(document.FilePath);
        Documents.Remove(document);
        Save();
        Notify();
    }
    
    public void RemoveAllFiles()
    {
        foreach (var document in Documents)
        {
            File.Delete(document.FilePath);
        }
        Documents.Clear();
        Save();
        Notify();
    }
    
    public void Subscribe(Action action)
    {
        OnChangeEventHandler.Add(action);
    }

    public void Unsubscribe(Action action)
    {
        OnChangeEventHandler.Remove(action);
    }
}