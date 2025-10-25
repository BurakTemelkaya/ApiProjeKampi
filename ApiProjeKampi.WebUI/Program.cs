using ApiProjeKampi.WebUI.Constants.Area;
using ApiProjeKampi.WebUI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddSignalR();
builder.Services.AddHttpClient("openai",c=>
{
    c.BaseAddress = new Uri("https://openrouter.ai/api");
    c.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenRouterKey"]}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapHub<ChatHub>("/chatHub");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: AreaNames.Admin,
    pattern: "/admin/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = AreaNames.Admin },
    constraints: new { area = AreaNames.Admin }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
