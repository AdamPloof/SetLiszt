using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

using SetLiszt.Web.Configuration;
using SetLiszt.Web.Exceptions;

namespace SetLiszt.Web.Services;

public class FileUploadHelper {
    private string _storageRoot;
    private int _maxFileSizeBytes;
    private string[] _allowedFileExtensions;
    private List<string> _errors;

    private static readonly Dictionary<string, byte[]> _fileSignatures =
        new Dictionary<string, byte[]> {
            {".jpeg", new byte[] { 0xFF, 0xD8 }},
            {".jpg", new byte[] { 0xFF, 0xD8 }},
            {".png", new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }},
            {".pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 }},
        };

    public FileUploadHelper(IOptions<FileUploadOptions> options) {
        _storageRoot = options.Value.RootDirectory;
        _maxFileSizeBytes = options.Value.MaxFileSizeBytes;
        _allowedFileExtensions = options.Value.AllowedFileExtensions;
        _errors = [];
    }

    public async Task<string> UploadToLocalStorage(IFormFile file, CancellationToken cancellation) {
        _errors.Clear();
        await ValidateFile(file, cancellation);
        if (!IsValid()) {
            throw new InvalidUploadException(string.Join(", ", _errors));
        }

        string serverName = GenerateServerName();
        string fullPath = Path.Combine(_storageRoot, serverName);
        await using (var fs = File.Create(fullPath)) {
            file.CopyTo(fs);
        }

        return serverName;
    }

    private static string GenerateServerName() {
        string rName = Path.GetRandomFileName();
        string now = DateTime.Now.ToString("yyyyMMddHmmssff");

        return $"{rName}_{now}";
    }

    private async Task ValidateFile(IFormFile file, CancellationToken cancellation) {
        if (file.Length == 0) {
            _errors.Add("Empty file");
        }

        if (file.Length > _maxFileSizeBytes) {
            _errors.Add($"File exceeds max file size: {_maxFileSizeBytes} bytes");
        }

        string ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!_allowedFileExtensions.Contains(ext)) {
            _errors.Add($"Unsupported file type: {ext}");
        }

        await using Stream uploadStream = file.OpenReadStream();
        if (!await SignatureIsValid(uploadStream, ext, cancellation)) {
            _errors.Add("Invalid file signature");
        }
    }

    private async Task<bool> SignatureIsValid(
        Stream uploadStream,
        string extension,
        CancellationToken cancellation
    ) {
        _fileSignatures.TryGetValue(extension, out byte[]? sig);
        if (sig == null) {
            return false;
        }

        var header = new byte[sig.Length];
        int read = await uploadStream.ReadAsync(header, cancellation);
        if (read != header.Length) {
            return false;
        }

        for (int i = 0; i < sig.Length; i++) {
            if (header[i] != sig[i]) {
                return false;
            }
        }
                
        return true;
    }

    private bool IsValid() {
        return _errors.Count == 0;
    }
}
