using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;

using SetLiszt.Web.Data;
using SetLiszt.Web.Configuration;
using SetLiszt.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FileUploadOptions>(
    builder.Configuration.GetSection("FileUploadOptions")
);

// Add database context
builder.Services.AddDbContext<SetLisztDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("SetLisztContext"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<PathConfigurationDelegate>();
builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
builder.Services.AddScoped<FileUploadHelper>();
builder.Services.AddScoped<FileDownloadHelper>();

// Configure options
// TODO: configure validation via annotations
builder.Services
    .AddOptions<FileUploadOptions>().Configure<PathConfigurationDelegate>(
        (opts, pathConfig) => {
            opts.RootDirectory = pathConfig.ConvertConfig(opts.RootDirectory);
        }
    )
    .Validate(opts => !string.IsNullOrWhiteSpace(opts.RootDirectory), "Root directory must be set")
    .ValidateOnStart();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    using (AsyncServiceScope scope = app.Services.CreateAsyncScope()) {
        IServiceProvider services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<SetLisztDbContext>();
        await DataSeeder.SeedSongsAsync(dbContext);
    }
} else {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();

app.Run();
