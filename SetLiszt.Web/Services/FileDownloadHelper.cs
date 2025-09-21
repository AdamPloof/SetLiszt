using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using SetLiszt.Web.Models;
using SetLiszt.Web.Configuration;

namespace SetLiszt.Web.Services;

public class FileDownloadHelper {
    private string _storageRoot;
    private readonly IContentTypeProvider _typeConverter;

    public FileDownloadHelper(
        IOptions<FileUploadOptions> options,
        IContentTypeProvider typeConverter
    ) {
        _typeConverter = typeConverter;
        _storageRoot = options.Value.RootDirectory;
    }

    public FileResult? GetSongFile(SongFile songFile) {
        string songPath = Path.Combine(_storageRoot, songFile.Filepath ?? "");
        if (songPath is null || !File.Exists(songPath)) {
            return null;
        }

        if (!_typeConverter.TryGetContentType(songPath, out string? contentType)) {
            contentType = "application/octet-stream";
        }

        var fileResult = new PhysicalFileResult(songPath, contentType);
        fileResult.EnableRangeProcessing = true;

        // TODO: add last modified time to SongFile entity
        // file.LastModified = songFile.LastModified;

        return fileResult;
    }
}
