using Microsoft.AspNetCore.Components.Forms;
using XTractFlow.API.Component;
using XtractFlow.Web.Data;
using XtractFlow.Web.Helpers;

namespace XtractFlow.Web.Services.DataStore;

public class CircuitDocumentdataStore : IDocumentDataStore
{
    private readonly IWebHostEnvironment _env;
    private readonly Thumbnail _thumbnail;

    public CircuitDocumentdataStore(IWebHostEnvironment env, Thumbnail thumbnail)
    {
        _env = env;
        _thumbnail = thumbnail;
        
        StorePath = Path.Combine(env.WebRootPath, "circuit_store",CircuitId.ToString());
        Directory.CreateDirectory(StorePath);
    }

    private Guid CircuitId { get; } = Guid.NewGuid();
    public List<Action> OnChangeEventHandler { get; } = new();
    public List<UserDocument> Documents { get; } = new();
    public ProcessorComponent Component { get; set; } = new();
    public string StorePath { get; }

    public void Initialization()
    {
        
    }

    public void Save()
    {
        
    }

    public void Notify()
    {
        foreach (var handler in OnChangeEventHandler)
        {
            handler?.Invoke();
        }    
    }

    public async Task AddFile(IBrowserFile file, CancellationToken ct = new())
    {
        var id = Guid.NewGuid();
        var fPath = Path.Combine(StorePath, id.ToString());
        await using var fs = new FileStream(fPath, FileMode.Create, FileAccess.ReadWrite);
        await using var bs = file.OpenReadStream(Globals.MaxFileSize);
        await bs.CopyToAsync(fs, ct);

        Documents.Add(new UserDocument
        {
            Id = id,
            ContentType = file.ContentType,
            Extention = file.Name.Split(".")[^1],
            Name = file.Name,
            FilePath = fPath,
            Thumbnail = _thumbnail.GenerateThumbnail(fs)
        });
        Notify();
    }

    public void Dispose()
    {
        if (Directory.Exists(StorePath))
        {
            Directory.Delete(StorePath, true);
        }
    }
}