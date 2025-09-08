namespace SetLiszt.Web.Configuration;

public class FileUploadOptions { 
    public required string RootDirectory { get; set; }
    public int MaxFileSizeBytes { get; init; }
    public string[] AllowedFileExtensions { get; init; } = Array.Empty<string>();
}
