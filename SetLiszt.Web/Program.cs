using Microsoft.EntityFrameworkCore;

using SetLiszt.Web.Data;
using SetLiszt.Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// TODO: make sure song file directory is relative to content root
builder.Services.Configure<FileUploadOptions>(
    builder.Configuration.GetSection("FileUploadOptions")
);

// Add database context
builder.Services.AddDbContext<SetLisztDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("SetLisztContext"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
