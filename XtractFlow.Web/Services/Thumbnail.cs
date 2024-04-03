using GdPicture14;
using GdPicture14.Imaging;

namespace XtractFlow.Web.Helpers;

public class Thumbnail
{
    private static GdPictureImaging _gdImaging = new();

    public byte[] GenerateThumbnail(Stream s)
    {
        GdPicture14.DocumentFormat d = GdPicture14.DocumentFormat.DocumentFormatUNKNOWN;
        int thumbnailId = 0, pageCount = 0;

        GdPictureDocumentUtilities.GetDocumentPreview(s, "", 468, 540, GdPictureColor.Transparent.ToArgb(), false, ref d, ref thumbnailId, ref pageCount);

        using MemoryStream ms = new();
        _gdImaging.SaveAsStream(thumbnailId, ms, GdPicture14.DocumentFormat.DocumentFormatPNG, 5);
        _gdImaging.ReleaseGdPictureImage(thumbnailId);
        return ms.ToArray();
    }
}