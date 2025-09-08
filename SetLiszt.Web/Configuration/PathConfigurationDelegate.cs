using Microsoft.AspNetCore.Hosting;

namespace SetLiszt.Web.Configuration;

/// <summary>
/// Replaces special environment strings such as %web_root% and %content_root%
/// in file paths used in configuration.
/// </summary>
public class PathConfigurationDelegate {
    private readonly IWebHostEnvironment _env;
    private readonly Dictionary<string, string> _replacements;

    public PathConfigurationDelegate(IWebHostEnvironment env) {
        _env = env;
        _replacements = new Dictionary<string, string>() {
            {"%web_root%", env.WebRootPath},
            {"%content_root%", env.ContentRootPath},
        };
    }

    public string ConvertConfig(string opt) {
        string converted = opt;
        foreach (KeyValuePair<string, string> pair in _replacements) {
            if (converted.Contains(pair.Key)) {
                converted = converted.Replace(pair.Key, pair.Value);
            }
        }

        return converted;
    }
}
