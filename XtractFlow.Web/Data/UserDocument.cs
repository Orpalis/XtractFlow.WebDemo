using System.Text.Json.Serialization;

namespace XtractFlow.Web.Data;

public class UserDocument
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NameWithoutExtention => Path.GetFileNameWithoutExtension(Name);
    public string Extention { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }
    
    public byte[] Thumbnail { get; set; }

    public string Text { get; set; }
}
    
