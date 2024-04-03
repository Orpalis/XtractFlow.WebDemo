namespace XtractFlow.Web.Data;

public static class Globals
{
    public static int MaxFileSize = 524288000; //500 * 1024 * 1024;
    public static string GetUserFileStore(string root)
    {
        return Path.Combine(root, "user_store", Guid.Empty.ToString());
    }

    public static TimeSpan FileProcessTimeout = new(0, 1, 30);
    
    public static readonly string[] SupportedPDFImportFormatsExtensions =
    {
        "3FR", "AI", "ARW", "BAY", "BMP", "BMQ", "BW", "CAP", "CINE", "CR2", "CUT", "CRW", "DC2","DCM", "DCR", "DRF", "DDS", "DICOM",
        "DNG", "DOC", "DOCM", "DOCX","DOTX", "DSC", "DXF", "EMF", "EML", "EPS", "ERF", "EXIF", "EXR", "FFF", "FAX", "G3", "GIF", "HDR", "HEIC", "HEIF", "HTM", "HTML", "IA", "ICO", "IFF", "IIQ", "J2K", "J2C",
        "JB2", "JBIG2", "JIF", "JNG", "JP2", "JPE", "JPEG", "JPG","K25", "KC2", "KD2", "KOA", "KOALA", "LBM", "MDC", "MEF","MEMORYBMP", "MHT",  "MHTML",
        "MNG", "MOS", "MRW", "MSG", "NEF", "NRW", "ODT", "ORF","PBM", "PBMRAW", "PCD", "PCT", "PCX", "PDF","PEF", "PFM","PGM", "PGMRAW", "PIC", "PICT",
        "PNG", "PPM", "PPMRAW", "PPT", "PPSX", "PPTM", "PPTX", "PS", "PSD", "PTX", "PXN", "QTK", "RAF", "RAS", "RAW", "RDC", "RGB", "RGBA", "RTF", "RW2", "RWL", "RWZ", "SGI",
        "SR2", "SRF", "SRW", "STI","SVG", "TARGA","TGA", "TIF", "TIFF","TXT", "WAP", "WBM", "WBMP", "WEBP", "WMF", "X3F", "XBM", "XLS", "XLSX", "XPM"
    };

    public static readonly string[] SupportedImageLoadingFormatsExtensions =
    {
        "3FR", "ARW", "BAY", "BMP", "BMQ", "BW", "CAP", "CINE", "CR2", "CUT", "CRW", "DC2","DCM", "DCR", "DRF", "DDS", "DICOM",
        "DNG", "DSC", "ERF", "EXIF", "EXR", "FFF", "FAX", "G3", "GIF", "HDR","HEIC", "HEIF", "IA", "ICO", "IFF", "IIQ", "J2K", "J2C",
        "JB2", "JBIG2", "JIF", "JNG", "JP2", "JPE", "JPEG", "JPG","K25", "KC2", "KD2", "KOA", "KOALA", "LBM", "MDC", "MEF","MEMORYBMP",
        "MNG", "MOS", "MRW", "NEF", "NRW", "ORF","PBM", "PBMRAW", "PCD", "PCT", "PCX", "PEF", "PFM","PGM", "PGMRAW", "PIC", "PICT",
        "PNG", "PPM", "PPMRAW", "PSD", "PTX", "PXN", "QTK", "RAF", "RAS", "RAW", "RDC", "RGB", "RGBA", "RW2", "RWL", "RWZ", "SGI",
        "SR2", "SRF", "SRW", "STI", "TARGA","TGA", "TIF", "TIFF", "WAP", "WBM", "WBMP", "WEBP", "X3F", "XBM", "XPM"
    };
    public const string API_KEY_QUERY_STRING_KEY = "key";
}