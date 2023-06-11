using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddPhp();

builder.WebHost.UseUrls("https://*:9001");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
    }
var contentPath = ResolveContentPath();



app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(contentPath) });
app.UseRouting();
app.UsePhp("/../", rootPath: contentPath);
//app.UseAuthorization();

//app.MapRazorPages();
static string ResolveContentPath()
{
    var devcontent = Path.GetFullPath("/../flarum");
    if (Directory.Exists(devcontent))
    {
        return devcontent;
    }

    return Directory.GetCurrentDirectory();
}

app.Run();
